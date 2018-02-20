using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroyspecial : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);

		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
