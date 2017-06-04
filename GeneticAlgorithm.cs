/*
   Evaluates and modifies the genomes and their performance
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour {
	
	public static float[] fitness;
	static float[][][][] population;
	public static int popSize = 50;
	static public int id;
	static System.Random rand = new System.Random();
	public static bool init = true;
	static int genNum = 0;
	public static float[] extrema;		//min, max
	public static Queue<float[]> genExtrema;
	static float[] gEx;
	
	// Use this for initialization
	void Start () {
		fitness = new float[popSize];
		population = new float[popSize][][][];
		id = 0;
		extrema = new float[2]{1,1};
		gEx = new float[2]{1,1};
		genExtrema = new Queue<float[]>();
		for(int i = 0; i < Grapher.resolution; i++)
		{
			genExtrema.Enqueue(new float[2]{1,1});
		}
	}
	
	// FixedUpdate is called once per physics frame
	void Update() {
		if(init)
		{
			population[id] = Initialize(NeuralNetwork.inputCounts, NeuralNetwork.hiddenDimensions, NeuralNetwork.outputCounts);
			if(++id == popSize)
			{
				id = 0;
				NeuralNetwork.ready = true;
				init = false;
			}
			return;
		}
		if(id == popSize)
		{
			Breed();
			id = 0;
			print("Gen " + genNum);
			print(Utility.Average(fitness));
			genExtrema.Dequeue();
			genExtrema.Enqueue(gEx);
			gEx = new float[2]{1,1};
			genNum++;
			return;
		}
		NeuralNetwork.nodeWeight = population[id];
		fitness[id] = GetComponent<ExhaustManifold>().Evaluate();
		if(extrema[0] == 1)
		{
			extrema[0] = fitness[id];
			extrema[1] = fitness[id];
		}
		if(gEx[0] == 1)
		{
			gEx[0] = fitness[id];
			gEx[1] = fitness[id];
		}
		
		if(fitness[id] > extrema[1])
		{
			extrema[1] = fitness[id];
		}
		if(fitness[id] < extrema[0])
		{
			extrema[0] = fitness[id];
		}
		if(fitness[id] > gEx[0])
		{
			gEx[0] = fitness[id];
		}
		if(fitness[id] < gEx[1])
		{
			gEx[1] = fitness[id];
		}
		//print(fitness[id]);
		id++;
	}
	
	static float[][][] Initialize(int iCt, int[] dim, int oCt)
	{
		float[][][] weights = new float[dim[0] + 2][][];
		weights[0] = new float[iCt][];
		for(int i = 1; i < dim[0] + 2; i++)
		{
			weights[i] = new float[i == dim[0] + 1 ? oCt : dim[1]][];
			for(int j = 0; j < weights[i].Length; j++)
			{
				weights[i][j] = new float[weights[i - 1].Length];
				for(int k = 0; k < weights[i][j].Length; k++)
				{
					weights[i][j][k] = (float)(rand.NextDouble() * 4 - 2);
				}
			}
		}
		return weights;
	}
	
	static void Breed()
	{
		int mutFactor = 1000; //Expressed as one mutation in mutFactor (e.g. one in 1000)
		float mutType = .5f; //Balance between overwrite mutations and translation mutations (1 is guaranteed translation)
		Array.Sort<float, float[][][]>(fitness, population);
		Array.Sort(fitness);
		float[][][] test = population[0];
		for(int i = 0; i < popSize / 3; i++)
		{
			if(test != population[0])
				print("Outermost " + i);
			for(int j = 1; j < population[popSize - i - 1].Length; j++)
			{
				if(test != population[0])
					print("Outer " + i + ", " + j);
				for(int k = 0; k < population[popSize - i - 1][j].Length; k++)
				{
					if(test != population[0])
						print("Inner " + i + ", " + j+ ", " + k);
					for(int l = 0; l < population[popSize - i - 1][j][k].Length; l++)
					{
						if(test != population[0])
							print("Innermost " + i + ", " + j + ", " + k + ", " + l);
						population[popSize - i - 1][j][k][l] = rand.Next() % 2 == 0 ? population[popSize -i - 1][j][k][l] : population[i][j][k][l];
						if(rand.Next() % mutFactor == 0)
						{
							population[popSize - i - 1][j][k][l] = rand.NextDouble() > mutType ? 
								(float)(rand.NextDouble() - .5f) + population[popSize - i - 1][j][k][l] :
								(float)(rand.NextDouble() * 4 - 2);
						}
					}
				}
			}
		}
	}
}
