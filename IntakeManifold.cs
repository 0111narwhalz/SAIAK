/*
   Controls inputs and integrates the program into the environment
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntakeManifold : MonoBehaviour {
	
	System.Random rand = new System.Random();
	float[] inputs;

	// Use this for initialization
	void Start () {
		inputs = new float[NeuralNetwork.inputCounts];
		for(int i = 0; i < NeuralNetwork.inputCounts; i++)
		{
			inputs[i] = (float)(rand.NextDouble() * 2) - 1;
		}
	}
	
	void FixedUpdate()
	{
		//Make garbage inputs
		for(int i = 0; i < NeuralNetwork.inputCounts; i++)
		{
			inputs[i] = (float)(rand.NextDouble() * 2) - 1;
		}
		
		//Apply the inputs
		for(int i = 0; i < NeuralNetwork.inputCounts; i++)
		{
			NeuralNetwork.nodeValues[0][i] = inputs[i];
		}
	}
}
