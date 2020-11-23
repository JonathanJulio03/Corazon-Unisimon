using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Coloreality.LeapWrapper;
using Coloreality;

public class ControllMove : MonoBehaviour {

	ColorealityManager cManager;

	Controller controller;
	double HandPalmPitch;
	double HandPalmYam;
	double HandPalmRoll;
	double HandWristRot;
	// Use this for initialization
	void Start () {
		cManager = ColorealityManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		controller = new Controller ();
		Frame frame = controller.Frame ();
		if (cManager.Leap.Data != null) {
			List<LeapHand> hands = cManager.Leap.Data.frame.Hands; 

			if (hands.Count > 0) {
				/*HandPalmPitch = hands [0].PalmNormal	
			HandPalmRoll = hands [0].PalmNormal.Roll;
			HandPalmYam = hands [0].PalmNormal.Yaw;

			HandWristRot = hands [0].WristPosition.Pitch;*/

				/*Debug.Log ("Pitch: " + HandPalmPitch);
			Debug.Log ("Roll: " + HandPalmRoll);
			Debug.Log ("Yam: " + HandPalmYam);*/

				//transform.Translate (new Vector3 (0, 0, 0.3f * Time.deltaTime));

				/*if (HandPalmYam >= -3.1f && HandPalmYam < -2.8f) {
				transform.Translate (new Vector3 (0, 0, 0.5f * Time.deltaTime));
			} else if (HandPalmYam >= -0.3f && HandPalmYam <= 0.7f) {
				transform.Translate (new Vector3 (0, 0, -0.5f * Time.deltaTime));
			}*/
			}
		}
			

	}
}
