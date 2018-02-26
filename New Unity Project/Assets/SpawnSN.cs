using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSN : MonoBehaviour {
	public GameObject notetospawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{

			Instantiate (notetospawn, transform.position, Quaternion.identity,this.transform.parent);

	}
}
