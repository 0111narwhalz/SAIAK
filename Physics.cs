/*
	Provides simple mechanical simulation
*/

using System;
using Saiak;

namespace Saiak
{
	class Physics
	{
		public static float[] pos = new float[3]{0,0,0};
		public static float[] vel = new float[3]{0,0,0};
		public static float[] frc = new float[3]{0,0,0};
		static float g = 9.80665f;
		static float tick = .02f;
		
		public static void Update()
		{
			for(int i = 0; i < 3; i++)
			{
				pos[i] += vel[i] * tick;
				vel[i] += frc[i] * tick;
			}
			vel[2] -= g * tick;
		}
		
		public static void Zero()
		{
			for(int i = 0; i < 3; i++)
			{
				pos[i] = 0;
				vel[i] = 0;
				frc[i] = 0;
			}
		}
	}
}
