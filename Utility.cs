/*
		Various mathematical methods and other utilities
*/

using System;
using Saiak;
using UnityEngine;

namespace Saiak
{
	class Utility
	{
		public static System.Random rand = new System.Random();
		
		public static float Average(float[] input)
		{
			float output = 0;
			foreach(float f in input)
			{
				output += f;
			}
			return output / input.Length;
		}
	
		public static float Median(float[] input)
		{
			float output;
			output = input[input.Length / 2];
			if(input.Length % 2 == 0)
			{
				output += input[input.Length / 2 + 1];
				output /= 2;
			}
			return output;
		}
	
		public static void ArrayPrint<T>(ref T[] input, string delimiter)
		{
			string print = "";
			foreach(T val in input)
			{
				print += val.ToString();
				print += delimiter;
			}
			Console.Write(print + "\n");
		}
		
		public static float[] ArraySub(float[] sub1, float[] sub2)
		{
			float[] output;
			if(sub1.Length <= sub2.Length)
			{
				output = new float[sub1.Length];
				for(int i = 0; i < sub1.Length; i++)
				{
					output[i] = sub1[i] - sub2[i];
				}
			}else
			{
				output = new float[sub2.Length];
				for(int i = 0; i < sub2.Length; i++)
				{
					output[i] = sub1[i] - sub2[i];
				}
			}
			return output;
		}
		
		public static T[] ArrayChunk<T>(ref T[] arrIn, int elements)
		{
			if(elements >= arrIn.Length)
			{
				return arrIn;
			}
			T[] arrOut = new T[elements];
			for(int i = 0; i < elements; i++)
			{
				arrOut[i] = arrIn[i];
			}
			return arrOut;
		}
		
		public static float SurfaceDistance(double[] startPos, double[] finalPos)
		{
			double debug;
			try
			{
				debug = startPos[0];
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("start lat");
			}
			try
			{
				debug = startPos[1];
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("start long");
			}
			try
			{
				debug = finalPos[0];
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("end lat");
			}
			try
			{
				debug = finalPos[1];
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("end long");
			}
			return 600000 * (float) (Math.Acos(Math.Sin(startPos[0]) * Math.Sin(finalPos[0]) + 
			                          Math.Cos(startPos[0]) * Math.Cos(finalPos[0]) * Math.Cos(Math.Abs(startPos[1] - finalPos[1]))));
		}
	
		public static float Sigma(float x)
		{
			return (1f / (float)(1 + System.Math.Pow(System.Math.E,x))) * 2 - 1;
		}
	
		public static float WeightedSum(float[] values, float[] weights)
		{
			float sum = 0;
			float debug;
			try
			{
				debug = values.Length;
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("Values");
			}
			try
			{
				debug = weights.Length;
			}
			catch(NullReferenceException nre)
			{
				Debug.Log("Weights");
			}
			for(int i = 0; i < (values.Length > weights.Length ? weights.Length : values.Length); i++)
			{
				try
				{
					debug = values[i];
				}
				catch(NullReferenceException nre)
				{
					Debug.Log("Values " + i);
				}
				try
				{
					debug = weights[i];
				}
				catch(NullReferenceException nre)
				{
					Debug.Log("Weights " + i);
				}
				sum += values[i] * weights[i];
			}
			return sum;
		}
	}
}
