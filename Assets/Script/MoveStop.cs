using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MoveStop : MonoBehaviour {


	public GameObject Cam;
	public GameObject Pointer;
	public Camera cameras;
	public float speed;

	private bool move;
	private bool mover;
	private Text Texto;

	public Text Titulo; 
	public Text Descripcion;
	public float RayDista = 120;
	// Use this for initialization
	void Start () {
		Texto = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		Ray ray = new Ray (cameras.transform.position, cameras.transform.forward);
		RaycastHit hit; 	         
		if (Physics.Raycast (ray, out hit, 2)) {
			if (hit.collider.tag == "Collider" || hit.collider.tag == "Frontal" || hit.collider.tag == "Temporal"
				|| hit.collider.tag == "Parietal" || hit.collider.tag == "Occipital" || hit.collider.tag == "Talamo"
				|| hit.collider.tag == "Tercerventrículo" || hit.collider.tag == "Tronco" || hit.collider.tag == "Nucleo"
				|| hit.collider.tag == "Amigdalino" || hit.collider.tag == "Caudado" || hit.collider.tag == "Cerebelo"
				|| hit.collider.tag == "Hipocampo" || hit.collider.tag == "Ventriculos" || hit.collider.tag == "Pallidus"
				|| hit.collider.tag == "Putamen") {
				move = false;
			} else {
				move = true;
			}
		} else {
			move = true;
		}

		//Ray Text
		Ray rayText = new Ray (cameras.transform.position, cameras.transform.forward);
		RaycastHit hitText; 
		//Debug.DrawRay (cameras.transform.position, cameras.transform.forward*RayDista, Color.green);
		if (Physics.Raycast (rayText, out hitText, RayDista)) {
			Colision ("Frontal", hitText, GameObject.FindGameObjectWithTag ("FrontalTag"));
			Colision ("Temporal", hitText, GameObject.FindGameObjectWithTag ("TemporalTag"));
			Colision ("Parietal", hitText, GameObject.FindGameObjectWithTag ("ParietalTag"));
			Colision ("Occipital", hitText, GameObject.FindGameObjectWithTag ("OccipitalTag"));

			Colision ("Talamo", hitText, GameObject.FindGameObjectWithTag ("TalamoTag"));
			Colision ("Tercerventrículo", hitText, GameObject.FindGameObjectWithTag ("TercerventrículoTag"));
			Colision ("Tronco", hitText, GameObject.FindGameObjectWithTag ("TroncoTag"));
			Colision ("Nucleo", hitText, GameObject.FindGameObjectWithTag ("NucleoTag"));
			Colision ("Amigdalino", hitText, GameObject.FindGameObjectWithTag ("AmigdalinoTag"));
			Colision ("Caudado", hitText, GameObject.FindGameObjectWithTag ("CaudadoTag"));
			Colision ("Cerebelo", hitText, GameObject.FindGameObjectWithTag ("CerebeloTag"));
			Colision ("Hipocampo", hitText, GameObject.FindGameObjectWithTag ("HipocampoTag"));
			Colision ("Ventriculos", hitText, GameObject.FindGameObjectWithTag ("VentriculosTag"));
			Colision ("Pallidus", hitText, GameObject.FindGameObjectWithTag ("PallidusTag"));
			Colision ("Putamen", hitText, GameObject.FindGameObjectWithTag ("PutamenTag"));
		} else {

			Titulo.text = "";
			Descripcion.text = "";
		} 


		if (move && mover) {
			Cam.transform.Translate (new Vector3 (ray.direction.x * Time.deltaTime * speed, ray.direction.y * Time.deltaTime * speed, ray.direction.z * Time.deltaTime * speed));
		} else {
			Cam.transform.Translate (new Vector3 (ray.direction.x * Time.deltaTime * 0, ray.direction.y * Time.deltaTime * 0, ray.direction.z * Time.deltaTime * 0));
		}
					
	}

	public void MoverCam(){
		if (Texto.text == "Mover") {
			mover = true;
			Texto.text = "Detener";
		} else {
			mover = false;
			Texto.text = "Mover";
		}
	}

	private void Colision(string parte,RaycastHit hit, GameObject objeto){
		if (hit.collider.tag == parte) {
			if (objeto != null) {
				Titulo.text = objeto.transform.GetComponentsInChildren<Text> () [0].text;
				Descripcion.text = objeto.transform.GetComponentsInChildren<Text> () [1].text;
			} else {
				Titulo.text = "";
				Descripcion.text = "";
			}
		}
	}

}
