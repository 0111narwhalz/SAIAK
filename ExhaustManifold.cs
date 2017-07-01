/*
   Controls the desired outputs and integrates the program into the environment
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;

namespace Saiak
{
	public class ExhaustManifold
	{
	
		public static float[] tgt;
	
		// Use this for initialization
		public static void Start () {
			tgt = new float[3];
			for(int i = 0; i < tgt.Length; i++)
			{
				tgt[i] = (float)(Utility.rand.NextDouble() * 2 - 1);
			}
		}
		
		public static void Update()
		{
			for(int i = 0; i < 3; i++)
			{
				Physics.frc[i] = NeuralNetwork.nodeValues[NeuralNetwork.nodeValues.Length - 1][i] * 10;
			}
		}
	
		public static float Evaluate()
		{
			float fitness = 0f;
			for(int i = 0; i < 3; i++)
			{
				fitness -= (float)Math.Pow(tgt[i] - Physics.pos[i], 2);
			}
			return fitness;
		}
	}
}
