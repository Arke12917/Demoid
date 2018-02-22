using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score2con : MonoBehaviour {
	public Text texT;
	public float notecon;
	// Use this for initialization
	void Start () {
		texT = this.gameObject.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

		texT.text=notecontrol.ingamescore.ToString("F2") + "%";
		
	}
}
