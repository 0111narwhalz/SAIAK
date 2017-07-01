/*
		Various mathematical methods and other utilities
*/

using System;
using Saiak;

namespace Saiak
{
	class Utility
	{
		public static Random rand = new Random();
		
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
	
		public static float Sigma(float x)
		{
			return (1f / (float)(1 + System.Math.Pow(System.Math.E,x))) * 2 - 1;
		}
	
		public static float WeightedSum(float[] values, float[] weights)
		{
			//Console.WriteLine("{0} {1}", values.Length, weights.Length);
			float sum = 0;
			for(int i = 0; i < (values.Length > weights.Length ? weights.Length : values.Length); i++)
			{
				sum += values[i] * weights[i];
			}
			return sum;
		}
	}
}
