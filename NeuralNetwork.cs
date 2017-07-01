/*
   Transforms inputs to outputs
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;

namespace Saiak
{
	public class NeuralNetwork
	{
		public static float[][] nodeValues;		//Rank, File
		public static float[][][] nodeWeight;	//Rank, File, SourceFile
		public static int inputCounts = 9;
		public static int outputCounts = 3;
		public static int[] hiddenDimensions = new int[2]{1,18};
		public static bool ready = false;
	
		// Use this for initialization
		public static void Start () {
		
			//Initialize the empty network
			nodeValues = new float[hiddenDimensions[0] + 2][];
			nodeWeight = new float[hiddenDimensions[0] + 2][][];
			for(int i = 0; i < nodeValues.Length; i++)
			{
				int quantity;
				if(i == 0)
				{
					quantity = inputCounts;
				}else if(i == hiddenDimensions[0] + 1)
				{
					quantity = outputCounts;
				}else
				{
					quantity = hiddenDimensions[1];
				}
				nodeValues[i] = new float[quantity];
			}
		}
	
		// FixedUpdate is called once per physics frame
		public static void Update () {
			if(!ready)
				return;
		
			//Propagate the network
			for(int i = 1; i < hiddenDimensions[0] + 2; i++)
			{
				Propagate(i);
			}
		}
	
		static void Propagate (int rank)
		{
			float[] val = new float[nodeValues[rank - 1].Length + 1];
			for(int i = 0; i < nodeValues[rank - 1].Length; i++)
			{
				val[i] = nodeValues[rank - 1][i];
			}
			for(int i = 0; i < nodeValues[rank].Length; i++)
			{
				val[val.Length - 1] = nodeValues[rank][i];
				nodeValues[rank][i] = Utility.Sigma(Utility.WeightedSum(val, nodeWeight[rank][i]));
			}
		}
	}
}
