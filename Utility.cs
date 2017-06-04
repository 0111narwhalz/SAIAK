using System;
using UnityEngine;

class Utility
{
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
	
	public static void ArrayPrint<T>(ref T[] input)
	{
		foreach(T val in input)
		{
			MonoBehaviour.print(val.ToString());
		}
	}
}