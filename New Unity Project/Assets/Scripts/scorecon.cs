﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorecon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text> ().text = " " + GM.totalCombo;
	}
	
	// Update is called once per frame
	void Update () {

		GetComponent<Text> ().text = " " + GM.totalCombo;
	
	}
}
