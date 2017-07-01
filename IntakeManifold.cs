/*
   Controls inputs and integrates the program into the environment
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;

namespace Saiak
{
	public class IntakeManifold
	{
		static float[] inputs;

		public static void Start () {
			inputs = new float[9];
			for(int i = 0; i < inputs.Length; i++)
			{
				inputs[i] = 0;
			}
		}
	
		public static void Update()
		{
			//Get the inputs
			for(int i = 0; i < 3; i++)
			{
				inputs[i] = Physics.pos[i];
				inputs[i + 3] = Physics.vel[i];
				inputs[i + 6] = ExhaustManifold.tgt[i];
			}
			//Apply the inputs
			for(int i = 0; i < 9; i++)
			{
				NeuralNetwork.nodeValues[0][i] = inputs[i];
			}
		}
	}
}
