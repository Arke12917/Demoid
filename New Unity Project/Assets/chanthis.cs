using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chanthis : MonoBehaviour {
	public Text tect;
	// Use this for initialization
	void Awake () {
		this.gameObject.name = tect.text;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
