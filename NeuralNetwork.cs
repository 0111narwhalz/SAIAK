/*
   Transforms inputs to outputs
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;
using UnityEngine;

namespace Saiak
{
	public class NeuralNetwork
	{
		public float[][] nodeValues;		//Rank, File
		public float[][][] nodeWeight;	//Rank, File, SourceFile
		public int inputCounts = 15;
		public int outputCounts = 2;
		public int[] hiddenDimensions = new int[2]{3,18};
		public bool ready = false;
	
		// Use this for initialization
		public NeuralNetwork () {
		
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
		public void Update () {
			if(!ready)
				return;
		
			//Propagate the network
			for(int i = 1; i < hiddenDimensions[0] + 2; i++)
			{
				Propagate(i);
			}
		}
	
		void Propagate (int rank)
		{
			float[] val = new float[nodeValues[rank - 1].Length + 1];
			for(int i = 0; i < nodeValues[rank - 1].Length; i++)
			{
				val[i] = nodeValues[rank - 1][i];
			}
			for(int i = 0; i < nodeValues[rank].Length; i++)
			{
				val[val.Length - 1] = nodeValues[rank][i];
				try
				{
					nodeValues[rank][i] = Utility.Sigma(Utility.WeightedSum(val, nodeWeight[rank][i]));
				}
				catch(NullReferenceException nre)
				{
					Debug.Log(val + ", " + rank + ", " + i);
				}
			}
		}
		
		public void Zero ()
		{
			for(int i = 0; i < nodeValues.Length; i++)
			{
				for(int j = 0; j < nodeValues[i].Length; j++)
				{
					nodeValues[i][j] = 0f;
				}
			}
		}
	}
}
