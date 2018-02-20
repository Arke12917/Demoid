using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCheck : MonoBehaviour {
	public GameObject chea;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Instantiate (chea, transform.position, Quaternion.identity);
	}
}
