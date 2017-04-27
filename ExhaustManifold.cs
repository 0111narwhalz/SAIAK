/*
   Controls the desired outputs and integrates the program into the environment
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustManifold : MonoBehaviour {
	
	public static float[] desiredOutput;
	System.Random rand = new System.Random();
	
	// Use this for initialization
	void Start () {
		desiredOutput = new float[NeuralNetwork.outputCounts];
		for(int i = 0; i < NeuralNetwork.outputCounts; i++)
		{
			desiredOutput[i] = (float)(rand.NextDouble() * 2 - 1);
			print(desiredOutput[i]);
		}
	}
	
	public static float Evaluate()
	{
		float fitness = 0f;
		for(int i = 0; i < NeuralNetwork.outputCounts; i++)
		{
			fitness -= Math.Abs(NeuralNetwork.nodeValues[NeuralNetwork.nodeValues.Length - 1][i] - desiredOutput[i]);
		}
		return fitness;
	}
}