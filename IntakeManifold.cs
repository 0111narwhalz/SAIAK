/*
   Controls inputs and integrates the program into the environment
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Saiak;
using UnityEngine;
using KSP;

namespace Saiak
{
	public class IntakeManifold
	{
		float[] inputs;

		public IntakeManifold (NeuralNetwork nn) {
			inputs = new float[nn.inputCounts];
			for(int i = 0; i < inputs.Length; i++)
			{
				inputs[i] = 0;
			}
		}
	
		public void Update(Vessel ves, NeuralNetwork nn)
		{
			//Get the inputs
			//Inputs 0-2 are position
			inputs[0]  = (float) ves.latitude / 180;
			inputs[1]  = (float) ves.longitude / 180;
			inputs[2]  = (float) ves.heightFromTerrain;
			//Inputs 3-6 are velocity
			Vector3d vec = ves.srf_velocity;
			inputs[3]  = (float) vec.x / 60;
			inputs[4]  = (float) vec.y / 60;
			inputs[5]  = (float) vec.z / 60;
			inputs[6]  = (float) vec.magnitude / 60;
			//Inputs 7-10 are rotation
			Quaternion rot = ves.srfRelRotation;
			inputs[7]  = rot.x;
			inputs[8]  = rot.y;
			inputs[9]  = rot.z;
			inputs[10] = rot.w;
			//Inputs 11-15 are angular velocity
			vec = ves.angularVelocity;
			inputs[11] = (float) vec.x;
			inputs[12] = (float) vec.y;
			inputs[13] = (float) vec.z;
			inputs[14] = (float) vec.magnitude;
			
			//Apply the inputs
			for(int i = 0; i < nn.inputCounts; i++)
			{
				nn.nodeValues[0][i] = inputs[i];
			}
		}
	}
}
