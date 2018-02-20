using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changetxtsize : MonoBehaviour {

	[SerializeField]
	int size = 300;

	Text t;

	// Use this for initialization
	void Awake () {
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		t.fontSize = size;
	}
}
