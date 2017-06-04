using System;
using System.Collections.Generic;
using UnityEngine;

public class Grapher : MonoBehaviour
{
	public const int resolution = 500;
	private ParticleSystem.Particle[] points;
	Queue<float> fitTime;
	public string abs = "a";
	
	void Start()
	{
		fitTime = new Queue<float>();
		for(int i = 0; i < resolution; i++)
		{
			fitTime.Enqueue(0f);
		}
		if(abs == "a" || abs == "r")
		{
			SetParticle();
		}
	}
	
	void SetParticle()
	{
		for(int i = 0; i < 2; i++)
		{
			points = new ParticleSystem.Particle[(abs == "a" || abs == "r") ? resolution : resolution / 2];
			float increment = 6.75f / ((abs == "a" || abs == "r") ? resolution - 1 : resolution / 2 - 1);
			switch(abs)
			{
				case "a":
					Absolute(increment);
					break;
					
				case "r":
					Relative(increment);
					break;
				
				case "g":
					GenExtreme(increment, false);
					break;
				
				case "G":
					GenExtreme(increment, true);
					break;
				
				case "e":
					AbsExtreme(increment, GeneticAlgorithm.extrema[0]);
					break;
				
				case "E":
					AbsExtreme(increment, GeneticAlgorithm.extrema[1]);
					break;
			}
			GetComponent<ParticleSystem>().SetParticles(points, points.Length);
		}
	}
	
	void GenExtreme(float increment, bool max)
	{
		float[][] eT = GeneticAlgorithm.genExtrema.ToArray();
		Array.Reverse(eT);
		for(int i = 0; i < resolution / 2; i++)
		{
			float x = (resolution / 2 - i) * increment;
			points[i].position = new Vector3(x - 5f, .1f, eT[i][max ? 1 : 0] + 2.5f);
			points[i].startColor = new Color(1f, 1f, 1f);
			points[i].startSize = .15f;
		}
	}
	
	void AbsExtreme(float increment, float val)
	{
		for(int i = 0; i < resolution / 2; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x - 5f, .1f, val + 2.5f);
			points[i].startColor = new Color(0f, 1f, 0f);
			points[i].startSize = .1f;
		}
	}
	
	void Relative(float increment)
	{
		float[] fT = fitTime.ToArray();
		for(int i = 1; i < resolution; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x - 5f, .2f, (fT[i] - fT[i - 1]) * 5);
			points[i].startColor = new Color(1f, 0f, 0f);
			points[i].startSize = .1f;
		}
	}
	
	void Absolute(float increment)
	{
		float[] fT = fitTime.ToArray();
		for(int i = 0; i < resolution; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x - 5f, 0f, fT[i] + 2.5f);
			points[i].startColor = new Color(0f, 0f, 0f);
			points[i].startSize = .2f;
		}
	}
	
	void Update()
	{
		if(GeneticAlgorithm.init)
		{
			return;
		}
		if(GeneticAlgorithm.id % GeneticAlgorithm.popSize == 0)
		{
			fitTime.Enqueue(Utility.Average(GeneticAlgorithm.fitness));
			fitTime.Dequeue();
			SetParticle();
		}
	}
}