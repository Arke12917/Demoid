namespace Lean.Touch{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheatcheck : MonoBehaviour {
		public GameObject cheatc;
		public LeanSpawn LNSPWN;
		public bool lul=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake () {
			//StartCoroutine (forcheat ());
	}

		IEnumerator forcheat(){
			yield return new WaitForSecondsRealtime (2.3f);
			var gor = GameObject.FindGameObjectsWithTag ("noteslide");
			foreach(GameObject gos in gor){
				if (gos.transform.position == this.gameObject.transform.position) {
					StartCoroutine (lolucheat ());
				} else {
					Destroy (this.gameObject);
			}
		}
				
  }


		IEnumerator lolucheat(){
			lul = true;
			yield return new WaitForSeconds (0.0f);
			GameObject.FindGameObjectWithTag ("slidechecker").GetComponent<LeanFingerHeld> ().ischeating=true;
			LeanSpawn.slidedestroy();
				//Destroy(GameObject.FindGameObjectWithTag("cheatcheck"));
			//GameObject.FindGameObjectWithTag ("slidechecker").GetComponent<LeanFingerHeld> ().ischeating=false;
			lul = false;
			}

		void OnDisable(){
			//GameObject.FindGameObjectWithTag ("slidechecker").GetComponent<LeanFingerHeld> ().ischeating=false;
		}

}
}
