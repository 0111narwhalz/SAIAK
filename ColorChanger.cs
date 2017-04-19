using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

	float[] rgb;
	Renderer rend;
	int[] id;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Specular");
		string[] name = this.transform.name.Split(' ');
		if(name.Length == 3)
		{
			id = new int[2]{int.Parse(name[1]), int.Parse(name[2])};
		}else
		{
			id = new int[2]{-1, int.Parse(name[1])};
		}
		rgb = new float[3]{.5f,.5f,.5f};
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(id[0] != -1)
		{
			for(int i = 0; i < 3; i++)
			{
				rgb[i] = NeuralNetwork.nodeValues[id[0]][id[1] * (i + 1)] + 1;
				rgb[i] /= 2;
			}
		}else
		{
			for(int i = 0; i < 3; i++)
			{
				rgb[i] = NeuralNetwork.desired[id[1]];
			}
		}
		rend.material.SetColor("_Color", new Color(rgb[0],rgb[1],rgb[2]));
		rend.material.SetColor("_SpecColor", new Color(rgb[0],rgb[1],rgb[2]));
	}
}
