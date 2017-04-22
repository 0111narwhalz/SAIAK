/*
   Controls the desired outputs and integrates the program into the environment
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustManifold : MonoBehaviour {
	
	public static float[] desiredOutput;
	System.Random rand = new System.Random();
	
	// Use this for initialization
	void Start () {
		desiredOutput = new float[NeuralNetwork.outputCount];
		for(int i = 0; i < NeuralNetwork.outputCount; i++)
		{
			desiredOutput[i] = (float)(rand.NextDouble() * 2 - 1);
		}
	}
	
}
