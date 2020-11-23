using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Coloreality;

public class GRButton : MonoBehaviour {

	public Image circulo;
	public UnityEvent gvrclic;
	public float timet = 2;
	public bool LeapMotion = true;
	bool gvrstatus;
	public float gvrTimer;

	// Optional: Input Object to receive text 
	public ColorealityManager cManager;
	public InputField inputObjectIP;
	public InputField inputObjectPort;
	public Text ConDes;

	public GameObject Conn;
	public GameObject KeyBoard;
	public GameObject LobulosSubcortical;
	public GameObject[] Cerebro;

	private bool write;

	// Use this for initialization
	void Start () {
		if (Cerebro != null) {
			/*List<Renderer> rc = GetRenderersInChildren(Cerebro[0].transform);
  			 Renderer[] rend = rc.ToArray();*/
			foreach(GameObject objet in Cerebro)
			{
				foreach(Renderer ren in objet.GetComponentsInChildren<Renderer> ()){
					ren.enabled = false;
				}
				//objet.GetComponentsInChildren<Renderer> ().enabled = false;
			}
		}
		/*if (inputObjectIP != null) {
			inputObjectIP.text = "192.168.0.19";
			inputObjectPort.text = "2333";
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		if (gvrstatus) {
			gvrTimer += Time.deltaTime;
			circulo.fillAmount = gvrTimer / timet;
		}

		if (gvrTimer > timet && gvrTimer < (timet+0.1)) {
			write = true;
			StartCoroutine ("OneWrite");
		}
	}

	IEnumerator OneWrite(){
		yield return new WaitForSeconds (0.1F);
		gvrclic.Invoke ();
		StopCoroutine ("OneWrite");
	}

	public void GvrON(){
		gvrstatus = true;
	}

	public void GvrOFF(){
		gvrstatus = false;
		gvrTimer = 0;
		circulo.fillAmount = 0;
	}

	public void WriteText(){
		if (write && LeapMotion) {
			write = false;
			Text buttontex = this.gameObject.GetComponentInChildren<Text> ();

			if (inputObjectIP.enabled) {
				inputObjectIP.text = inputObjectIP.text + buttontex.text;
			} else if (inputObjectPort.enabled) {
				inputObjectPort.text = inputObjectPort.text + buttontex.text;
			}
		}
	}

	public void DeleteText(){
		if (write && LeapMotion) {
			write = false;

			if (inputObjectIP.enabled) {
				if (inputObjectIP.text.Length > 0)
					inputObjectIP.text = inputObjectIP.text.Substring (0, inputObjectIP.text.Length - 1);
			} else if (inputObjectPort.enabled) {
				if (inputObjectPort.text.Length > 0)
					inputObjectPort.text = inputObjectPort.text.Substring (0, inputObjectPort.text.Length - 1);
			}
		}
	}

	public void Connected(){

		if (LeapMotion) cManager.TryConnect(inputObjectIP.text, int.Parse(inputObjectPort.text));

		if (cManager.network.IsConnected || LeapMotion == false) {
			Conn.SetActive(false);
			LobulosSubcortical.SetActive (true);
			KeyBoard.SetActive(false);
			foreach(GameObject objet in Cerebro)
			{
				foreach(Renderer ren in objet.GetComponentsInChildren<Renderer> ()){
					ren.enabled = true;
				}
				//objet.GetComponentsInChildren<Renderer> ().enabled = false;
			}
			circulo.fillAmount = 0;
		} else {
			Conn.SetActive(true);
			KeyBoard.SetActive(true);
			LobulosSubcortical.SetActive (false);
			foreach(GameObject objet in Cerebro)
			{
				foreach(Renderer ren in objet.GetComponentsInChildren<Renderer> ()){
					ren.enabled = false;
				}
				//objet.GetComponentsInChildren<Renderer> ().enabled = false;
			}
		}
		/*
		cManager.network.OnConnected += (object sender, System.EventArgs e) => {
			ConDes.text = "Conectado!";
		};
		cManager.network.OnDisconnected += (object sender, System.EventArgs e) => {
			ConDes.text = "Desconectado";
		};
		cManager.network.OnError += (object sender, ErrorEventArgs e) => ConDes.text = "Error: " + e.Message;*/
	}
}
