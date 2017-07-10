/*
   Evaluates and modifies the genomes and their performance
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;
using UnityEngine;

namespace Saiak
{
	public class GeneticAlgorithm
	{
	
		public float[] fitness;
		static float[][][][] population;
		public int popSize = 25;
		public int id;
		static bool init = true;
		public int genNum = 0;
		//public static float[] extrema;		//min, max
		//public static Queue<float[]> genExtrema;
		//static float[] gEx;
	
		// Use this for initialization
		public GeneticAlgorithm () 
		{
			fitness = new float[popSize];
			population = new float[popSize][][][];
			id = 0;
			/*
			extrema = new float[2]{1,1};
			gEx = new float[2]{1,1};
			genExtrema = new Queue<float[]>();
			for(int i = 0; i < Grapher.resolution; i++)
			{
				genExtrema.Enqueue(new float[2]{1,1});
			}
			*/
		}
	
		// FixedUpdate is called once per physics frame
		public void Update(ref NeuralNetwork nn) 
		{
			while(init)
			{
				Debug.Log("Generating individual " + id);
				population[id] = Initialize(nn.inputCounts, nn.hiddenDimensions, nn.outputCounts);
				nn.nodeWeight = population[id];
				if(++id == popSize)
				{
					id = 0;
					nn.ready = true;
					init = false;
				}
			}
			
			if(id == popSize)
			{
				Breed();
				id = 0;
				//Console.Clear();
				//Console.WriteLine("Gen " + genNum);
				//Console.WriteLine(Utility.Median(Utility.ArrayChunk(ref fitness, 25)));
				Utility.ArrayPrint<float>(ref fitness, "\n");
				genNum++;
				for(int i = 0; i < fitness.Length; i++)
				{
					fitness[i] = 0;
				}
				/*
				genExtrema.Dequeue();
				genExtrema.Enqueue(gEx);
				gEx = new float[2]{1,1};
				*/
				return;
			}
			nn.nodeWeight = population[id];
			/*
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
			//Console.WriteLine(fitness[id]);
			*/
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
					weights[i][j] = new float[weights[i - 1].Length + 1];
					for(int k = 0; k < weights[i][j].Length; k++)
					{
						weights[i][j][k] = (float)(Utility.rand.NextDouble() * 4 - 2);
					}
				}
			}
			return weights;
		}
	
		void Breed()
		{
			int mutFactor = 1000; //Expressed as one mutation in mutFactor (e.g. one in 1000)
			float mutType = .5f;  //Balance between overwrite mutations and translation mutations (1 is guaranteed translation)
			Array.Sort<float, float[][][]>(fitness, population);
			Array.Sort(fitness);
			float[][][] test = population[0];
			for(int i = 0; i < popSize / 3; i++)
			{
				if(test != population[0])
				{
					//Console.WriteLine("Outermost " + i);
				}
				for(int j = 1; j < population[popSize - i - 1].Length; j++)
				{
					if(test != population[0])
					{
						//Console.WriteLine("Outer " + i + ", " + j);
					}
					for(int k = 0; k < population[popSize - i - 1][j].Length; k++)
					{
						if(test != population[0])
						{
							//Console.WriteLine("Inner " + i + ", " + j+ ", " + k);
						}
						for(int l = 0; l < population[popSize - i - 1][j][k].Length; l++)
						{
							if(test != population[0])
							{
								//Console.WriteLine("Innermost " + i + ", " + j + ", " + k + ", " + l);
							}
							population[popSize - i - 1][j][k][l] = Utility.rand.Next() % 2 == 0 ? population[popSize -i - 1][j][k][l] : population[i][j][k][l];
							if(Utility.rand.Next() % mutFactor == 0)
							{
								population[popSize - i - 1][j][k][l] = Utility.rand.NextDouble() > mutType ? 
									(float)(Utility.rand.NextDouble() - .5f) + population[popSize - i - 1][j][k][l] :
									(float)(Utility.rand.NextDouble() * 4 - 2);
							}
						}
					}
				}
			}
		}
	}
}
