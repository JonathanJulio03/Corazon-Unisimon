using UnityEngine;
using System.Collections;

public class LUI_PAK : MonoBehaviour {

	[Header("VARIABLES")]
	public GameObject mainCanvas;
	public GameObject scriptObject;
	public Animator animatorComponent;
	public string animName;
	private float tiempo;

	void Start ()
	{
		tiempo = 0F;
		animatorComponent.GetComponent<Animator>();
	}

	void Update ()
	{
		tiempo += Time.deltaTime;
		if (tiempo >= 5F) 
		{
			animatorComponent.Play (animName);
			mainCanvas.SetActive(true);
			Destroy (scriptObject);
		}
	}
}