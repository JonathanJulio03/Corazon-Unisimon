using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCerebro : MonoBehaviour {
	
	public GameObject[] CerebroColor;
	public Material[] Mat;
	private Material[] Mate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PointerEnter()
	{		
		foreach(GameObject objet in CerebroColor)
		{
			objet.GetComponent<Renderer> ().materials = Mat;
		}
	}
}
