namespace Lean.Touch{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class deletthis : MonoBehaviour {
	public Vector3 pos1;
	public Vector3 pos2;
	public bool started=false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (started == false) {
			StartCoroutine (poscheck ());
		}
	}

	IEnumerator poscheck(){
		started = true;
		pos1 = gameObject.transform.position;
		yield return new WaitForSecondsRealtime (1.5f);
		pos2 = gameObject.transform.position;
		if (pos1 == pos2) {
			GameObject.FindGameObjectWithTag("slidechecker").GetComponent<LeanFingerHeld> ().ischeating=true;
			Destroy (gameObject);
		}
		yield return new WaitForSecondsRealtime (0.0f);
		started = false;
	}
}
}
