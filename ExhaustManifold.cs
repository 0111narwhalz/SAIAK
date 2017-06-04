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
	public float satWeight = .5f;
	public float valWeight = .5f;
	
	// Use this for initialization
	void Start () {
		desiredOutput = new float[NeuralNetwork.outputCounts];
		for(int i = 0; i < NeuralNetwork.outputCounts; i++)
		{
			desiredOutput[i] = (float)(rand.NextDouble() * 2 - 1);
			print(desiredOutput[i]);
		}
	}
	
	public float Evaluate()
	{
		float fitness = 0f;
		for(int i = 0; i < NeuralNetwork.outputCounts / 3; i++)
		{
			Color rgb = new Color(
				(NeuralNetwork.nodeValues[NeuralNetwork.nodeValues.Length - 1][i * 3] + 1) / 2,
				(NeuralNetwork.nodeValues[NeuralNetwork.nodeValues.Length - 1][i * 3 + 1] + 1) / 2,
				(NeuralNetwork.nodeValues[NeuralNetwork.nodeValues.Length - 1][i * 3 + 2] + 1) / 2);
			float[] hsv = new float[3];
			Color.RGBToHSV(rgb, out hsv[0], out hsv[1], out hsv[2]);
			Color rgbDesired = new Color(
				(desiredOutput[i * 3] + 1) / 2,
				(desiredOutput[i * 3 + 1] + 1) / 2,
				(desiredOutput[i * 3 + 2] + 1) / 2);
			float[] hsvDesired = new float[3];
			Color.RGBToHSV(rgbDesired, out hsvDesired[0], out hsvDesired[1], out hsvDesired[2]);
			fitness -= (float)Math.Abs(hsv[0] - hsvDesired[0]);
			fitness -= (float)Math.Abs(hsv[1] - hsvDesired[1]) * satWeight;
			fitness -= (float)Math.Abs(hsv[2] - hsvDesired[2]) * valWeight;
		}
		return fitness;
	}
}