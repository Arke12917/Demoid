using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{

		try{
			Debug.Log ("Fail!!");
			//Instantiate (failBurst, transform.position, failBurst.rotation);
			GM.totalCombo = (GM.totalCombo-=GM.totalCombo);
			notecontrol.AllCharming = " ";
			notecontrol.FullCombo = " ";
			other.gameObject.SetActive (false);
		} catch{
		}

	}
}
