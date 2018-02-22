using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadoffset : MonoBehaviour {
	public Text off;
	public bool doing=false;
	// Use this for initialization
	void Start () {
		ZPlayerPrefs.Initialize ("what'sYourName", "salt12issalt");
		off = this.gameObject.GetComponent<Text> ();
		CalibrationLoadingScript.offset=ZPlayerPrefs.GetFloat ("Offset");
	}
	
	// Update is called once per frame
	void Update () {
		off.text = CalibrationLoadingScript.offset.ToString("F2");
	}
	public void increment(){
		if (!doing) {
			doing = true;
			if (CalibrationLoadingScript.offset < 1.00f) {
				ZPlayerPrefs.Initialize ("what'sYourName", "salt12issalt");
				CalibrationLoadingScript.offset = CalibrationLoadingScript.offset += 0.01f;
				ExampleLoadingScript.offset = CalibrationLoadingScript.offset;
				ZPlayerPrefs.SetFloat ("Offset", CalibrationLoadingScript.offset);
				ZPlayerPrefs.Save ();
			}
			doing = false;
		}
	}
	public void decrement(){
		if (!doing) {
			doing = true;
			if (CalibrationLoadingScript.offset > -1.00f) {
				ZPlayerPrefs.Initialize ("what'sYourName", "salt12issalt");
				CalibrationLoadingScript.offset = CalibrationLoadingScript.offset -= 0.01f;
				ExampleLoadingScript.offset = CalibrationLoadingScript.offset;
				ZPlayerPrefs.SetFloat ("Offset", CalibrationLoadingScript.offset);
				ZPlayerPrefs.Save ();
			}
			doing = false;
		}
	}

}
