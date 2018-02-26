using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notrender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{

		foreach (Transform child in other.gameObject.transform) {
			child.gameObject.SetActive (false);
		}
		StartCoroutine(deactiv());


	}
	IEnumerator deactiv(){
		yield return new WaitForSeconds (0.5f);
		this.gameObject.SetActive (false);
	}
}
