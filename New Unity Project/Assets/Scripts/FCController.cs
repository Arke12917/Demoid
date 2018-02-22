using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FCController : MonoBehaviour {
	public Text TEXT;
	// Use this for initialization
	void Start () {
		TEXT = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

		TEXT.text = notecontrol.FullCombo + "";
		
	}
}
