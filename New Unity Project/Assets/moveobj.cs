namespace Lean.Touch{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveobj : MonoBehaviour {
		public Vector3 pos;
		public Vector3 poss;
	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
			Vector3 pos = GameObject.FindGameObjectWithTag ("slidechecker").GetComponent<LeanFingerHeld> ().firsttouch;
			transform.position = pos;
	}
  }
}
