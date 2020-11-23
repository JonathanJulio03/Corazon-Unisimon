using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Coloreality.LeapWrapper;

namespace Coloreality
{
	[RequireComponent(typeof(LineRenderer))]
	public class LeapHandConnect : MonoBehaviour {
		
		public GameObject Cam;
		public GameObject Pointer;
		public Camera cameras;
		public bool LeapMotion = true;
		public float speed;

		public Text Titulo; 
		public Text Descripcion;
		public float RayDista = 120;

		private bool move;
		ColorealityManager cManager;
		LineRenderer line;

		void Start () {
			if (LeapMotion) {
				cManager = ColorealityManager.Instance;
				if (cManager == null) {
					Debug.LogError ("Cannot find ColorealityManager Instance.");
					enabled = false;
				}

				line = GetComponent<LineRenderer> ();
				line.positionCount = 2;
			}
		}

		void Update(){
		}
		
		void FixedUpdate () {

			GameObject objeto;
			Ray ray = new Ray (cameras.transform.position, cameras.transform.forward);
			RaycastHit hit; 	         
			//Debug.DrawRay (cameras.transform.position, cameras.transform.forward*2, Color.red);
			//if (Physics.Raycast(cameras.transform.position, cameras.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
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
				if (Titulo != null) {
					Titulo.text = "";
					Descripcion.text = "";
				}
			} 



			if (LeapMotion) {
				if (cManager.Leap.Data != null) {
					List<LeapHand> hands = cManager.Leap.Data.frame.Hands;

					if (hands.Count > 0) {
						//Debug.Log ("Pitch: " + hands[0].PalmNormal.x);

						if (hands[0].PalmNormal.x >= -0.98f && hands[0].PalmNormal.x < -0.62f) {
							if (move) {
								Cam.transform.Translate (new Vector3 (ray.direction.x * Time.deltaTime * speed, ray.direction.y * Time.deltaTime * speed, ray.direction.z * Time.deltaTime * speed));
							}
						} 


					}
					if (hands.Count >= 2) {
						line.SetPositions (new Vector3[2] {
							hands [0].PalmPosition.ToHMDVector3 (), 
							hands [1].PalmPosition.ToHMDVector3 ()
						});
						line.enabled = true;
					} else {
						line.enabled = false;
					}
				} else {
					line.enabled = false;
				}

			} /*else {
				gvrclic.Invoke ();
				//Cam.transform.Translate (new Vector3 (ray.direction.x * Time.deltaTime, ray.direction.y * Time.deltaTime, ray.direction.z * Time.deltaTime));
			}*/
		}


		private void Colision(string parte,RaycastHit hit, GameObject objeto){
			if (Titulo != null) {
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

	}
}