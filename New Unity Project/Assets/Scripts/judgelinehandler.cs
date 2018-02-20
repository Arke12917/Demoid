using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class judgelinehandler : MonoBehaviour {

	public GameObject NoteActivator;
	// Use this for initialization
	void Start () {
		
	}

	void OnTouchDown(){
		Debug.Log ("pressed left click");
		Instantiate (NoteActivator, transform.position, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) 
		{
			Debug.Log ("pressed left click");
			Instantiate (NoteActivator, transform.position, transform.rotation);

		}
		
	}
}
