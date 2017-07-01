/*
		Acts as a surrogate for the Unity engine.
*/

using System;
using Saiak;

namespace Saiak
{
	class Environment
	{
		public static bool exit = false;
		static int simLength = 500;
		static int time = 0;
		
		public static void Main()
		{
			Console.Write("Initializing");
			Start();
			Console.WriteLine("Complete");
			for(;;)
			{
				if(Loop())
				{
					break;
				}
			}
			Console.WriteLine("Terminated");
			Console.Read();
		}
		
		static void Start()
		{
			NeuralNetwork.Start();
			Console.Write('.');
			GeneticAlgorithm.Start();
			Console.Write('.');
			IntakeManifold.Start();
			Console.Write('.');
			ExhaustManifold.Start();
		}
		
		static bool Loop()
		{
			time++;
			if(time == simLength)
			{
				GeneticAlgorithm.id++;
				Physics.Zero();
				time = 0;
			}
			IntakeManifold.Update();
			NeuralNetwork.Update();
			ExhaustManifold.Update();
			GeneticAlgorithm.Update();
			Physics.Update();
			return exit;
		}
	}
}
