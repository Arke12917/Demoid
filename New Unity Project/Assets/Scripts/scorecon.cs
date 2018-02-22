using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorecon : MonoBehaviour {
	public Text tExt;
	// Use this for initialization
	void Start () {
		tExt = this.gameObject.GetComponent<Text>();
		GetComponent<Text> ().text = " " + GM.totalCombo;
	}
	
	// Update is called once per frame
	void Update () {

		tExt.text = " " + GM.totalCombo;
	
	}
}
