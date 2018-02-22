using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACcontroller : MonoBehaviour {
	public Text TEXt;
	// Use this for initialization
	void Start () {
		TEXt = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		TEXt.text = notecontrol.AllCharming + "";
		
	}
}
