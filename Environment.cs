/*
		Acts as a surrogate for the Unity engine.
*/

using System;
using Saiak;
using UnityEngine;
using UnityEngine.UI;
using KSP;
using System.Collections.Generic;

namespace Saiak
{
	class Brain
	{
		public GeneticAlgorithm ga;
		public IntakeManifold im;
		public NeuralNetwork nn;
		public ExhaustManifold em;
		
		public Brain(GeneticAlgorithm gA, IntakeManifold iM, NeuralNetwork nN, ExhaustManifold eM)
		{
			ga = gA;
			im = iM;
			nn = nN;
			em = eM;
		}
	}
	public class Environment : VesselModule
	{
		static bool firstRun = true;
		static Dictionary<string, Brain> pop = new Dictionary<string, Brain>();
		string vName;
		double[] lastPos = new double[2]{0,0};
		
		public new void Start()
		{
			ExhaustManifold em = new ExhaustManifold(this.vessel);
			if(HighLogic.LoadedScene != GameScenes.FLIGHT || !firstRun)
			{
				return;
			}
			vName = this.vessel.vesselName;
			NeuralNetwork nn = new NeuralNetwork();
			//Console.Write('.');
			GeneticAlgorithm ga = new GeneticAlgorithm();
			//Console.Write('.');
			IntakeManifold im = new IntakeManifold(nn);
			//Console.Write('.');
			nn.Zero();
			pop[vName] = new Brain(ga,im,nn,em);
			firstRun = false;
		}
		
		public void FixedUpdate()
		{
			if(HighLogic.LoadedScene != GameScenes.FLIGHT)
			{
				return;
			}
			if(vName != this.vessel.vesselName)
			{
				vName = this.vessel.vesselName;
			}
			if(this.vessel.state == Vessel.State.INACTIVE)
			{
				return;
			}
			if(this.vessel.missionTime <= 10)
			{
				return;
			}
			try
			{
				if(Terminate())
				{
					Debug.Log(string.Format("Individual {0} is dead.", pop[vName].ga.id));
					pop[vName].ga.fitness[pop[vName].ga.id] = pop[vName].em.Evaluate(this.vessel);
					Debug.Log(string.Format("Fitness {0}", pop[vName].ga.fitness[pop[vName].ga.id]));
					pop[vName].ga.id++;
					pop[vName].nn.Zero();
					GamePersistence.LoadGame("SaiakStartPoint", HighLogic.fetch.GameSaveFolder, false, false).Start();
					ScreenMessages.PostScreenMessage(string.Format("Last: Generation {0} ID {1} Fitness {2}", 
					                                               pop[vName].ga.genNum, pop[vName].ga.id - 1,
					                                               pop[vName].ga.fitness[pop[vName].ga.id -1]),
					                                 60,
					                                 ScreenMessageStyle.UPPER_LEFT);
				}
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("Problems with termination!");
			}
			try
			{
				pop[vName].im.Update(this.vessel, pop[vName].nn);
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("IntakeManifold");
			}
			try
			{
				pop[vName].nn.Update();
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("NeuralNetwork");
			}
			try
			{
				pop[vName].em.Update(this.vessel, pop[vName].nn);
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("ExhaustManifold");
			}
			try
			{
				pop[vName].ga.Update(ref pop[vName].nn);
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("GA");
			}
		}
		
		bool Terminate()
		{
			if(this.vessel.state == Vessel.State.DEAD)
			{
				return true;
			}
			if(this.vessel.state == Vessel.State.INACTIVE)
			{
				return false;
			}
			if(this.vessel.CurrentControlLevel == Vessel.ControlLevel.NONE)
			{
				return true;
			}
			if(this.vessel.missionTime > 120 && pop[vName].em.Evaluate(this.vessel) < 3f)
			{
				return true;
			}
			if(this.vessel.latitude  < lastPos[0] + .01f && this.vessel.latitude  > lastPos[0] - .01f && 
			   this.vessel.longitude < lastPos[1] + .01f && this.vessel.longitude > lastPos[1] - .01f &&
			   this.vessel.missionTime % 10f == 0)
			{
				return true;
			}
			else
			{
				lastPos = new double[2]{this.vessel.latitude, this.vessel.longitude};
			}
			return false;
		}
	}
}
