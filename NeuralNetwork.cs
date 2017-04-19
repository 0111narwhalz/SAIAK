using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NeuralNetwork : MonoBehaviour {

	public static float[][] nodeValues;	//Rank, File
	public static float[][][] nodeWeight;	//Rank, File, SourceFile
	public static float[] desired;
	public static int inputCounts = 6;
	int outputCounts = 6;
	int[] hiddenDimensions = new int[2]{1,18};
	System.Random rand = new System.Random();
	
	// Use this for initialization
	void Start () {
		
		//Initialize the empty network
		nodeValues = new float[hiddenDimensions[0] + 2][];
		nodeWeight = new float[hiddenDimensions[0] + 2][][];
		desired = new float[outputCounts];
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
			nodeWeight[i] = new float[quantity][];
			nodeValues[i] = new float[quantity];
			if(i != 0)
			{
				for(int j = 0; j < quantity; j++)
				{
					nodeWeight[i][j] = new float[nodeValues[i - 1].Length];
				}
			}
		}
		
		//Generate garbage weights
		for(int i = 1; i < nodeWeight.Length; i++)
		{
			for(int j = 0; j < nodeWeight[i].Length; j++)
			{
				for(int k = 0; k < nodeWeight[i][j].Length; k++)
				{
					nodeWeight[i][j][k] = (float)(rand.NextDouble() * 4) - 2;
				}
			}
		}
	}
	
	// FixedUpdate is called once per physics frame
	void FixedUpdate () {
		
		//Set some garbage inputs
		/* for(int i = 0; i < inputCounts; i++)
		{
				nodeValues[0][i] = (float)(rand.NextDouble() * 2) - 1;
		} */
		
		//Propagate the network
		for(int i = 1; i < hiddenDimensions[0] + 2; i++)
		{
			Propagate(i);
		}
	}
	
	void Propagate (int rank)
	{
		for(int i = 0; i < nodeValues[rank].Length; i++)
		{
			nodeValues[rank][i] = Sigma(WeightedSum(nodeValues[rank-1], nodeWeight[rank][i]));
		}
	}
	
	float Sigma(float x)
	{
		return 1f / (float)(1 + System.Math.Pow(System.Math.E,x));
	}
	
	//Requires the values of the previous rank and a weight for each
	float WeightedSum(float[] values, float[] weights)
	{
		float sum = 0;
		for(int i = 0; i < values.Length; i++)
		{
			sum += values[i] * weights[i];
		}
		return sum;
	}
}
 