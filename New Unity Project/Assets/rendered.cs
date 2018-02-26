using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rendered : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{

		foreach (Transform child in other.gameObject.transform) {
			child.gameObject.SetActive (true);
		}
		//StartCoroutine(deactiv());


	}
}
