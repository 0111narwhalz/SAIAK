using System;
using System.Collections.Generic;
using UnityEngine;

public class Grapher : MonoBehaviour
{
	public int resolution = 500;
	private ParticleSystem.Particle[] points;
	Queue<float> fitTime;
	public bool abs = true;
	
	void Start()
	{
		fitTime = new Queue<float>();
		for(int i = 0; i < resolution; i++)
		{
			fitTime.Enqueue(0f);
		}
		SetParticle();
	}
	
	void SetParticle()
	{
		for(int i = 0; i < 2; i++)
		{
			points = new ParticleSystem.Particle[resolution];
			float increment = 6.75f / (resolution - 1);
			if(abs)
			{
				Absolute(increment);
			}else
			{
				Relative(increment);
			}
			GetComponent<ParticleSystem>().SetParticles(points, points.Length);
		}
	}
	
	void Relative(float increment)
	{
		float[] fT = fitTime.ToArray();
		for(int i = 1; i < resolution; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x - 5f, .1f, (fT[i] - fT[i - 1]) * 5);
			points[i].startColor = new Color(1f, 0f, 0f);
			points[i].startSize = .1f;
		}
	}
	
	void Absolute(float increment)
	{
		float[] fT = fitTime.ToArray();
		for(int i = 0; i < resolution; i++)
		{
			float x = i * increment;
			points[i].position = new Vector3(x - 5f, 0f, fT[i] + 2.5f);
			points[i].startColor = new Color(0f, 0f, 0f);
			points[i].startSize = .2f;
		}
	}
	
	void FixedUpdate()
	{
		if(GeneticAlgorithm.id % GeneticAlgorithm.popSize == 0)
		{
			fitTime.Enqueue(Utility.Average(GeneticAlgorithm.fitness));
			fitTime.Dequeue();
			SetParticle();
		}
	}
}