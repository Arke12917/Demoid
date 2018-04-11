using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCredits : MonoBehaviour {
	public GameObject Creds;

	// Use this for initialization
	void Awake () {
		//GameObject.FindGameObjectWithTag ("Credits").SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void cred(){
		Creds.SetActive (true);

	}

	public void close(){
		Creds.SetActive (false);
	}

	public void OK(){
		Creds.SetActive (false);
	}
}
