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
		double[] startPos;
		
		public ExhaustManifold(Vessel ves)
		{
			if(ves == null)
			{
				return;
			}
			startPos = new double[2]{ves.latitude * Math.PI / 180, ves.longitude * Math.PI / 180};
		}
		
		public void Update(Vessel ves, NeuralNetwork nn)
		{
			ves.ctrlState.wheelSteer = nn.nodeValues[nn.nodeValues.Length - 1][0];
			ves.ctrlState.wheelThrottle = nn.nodeValues[nn.nodeValues.Length - 1][1];
		}
	
		public float Evaluate(Vessel ves)
		{
			return Utility.SurfaceDistance(startPos, new double[2]{ves.latitude * Math.PI / 180, ves.longitude * Math.PI / 180});
		}
	}
}
