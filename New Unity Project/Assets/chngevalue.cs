using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chngevalue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.GetComponent<Text> ().text = GM.currentSpeed.ToString()+".0";
		//this.gameObject.GetComponent<Text> ().text = GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().currentSpeed.ToString()+".0";
	}
}
