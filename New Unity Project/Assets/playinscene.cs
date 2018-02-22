using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playinscene : MonoBehaviour {
	public AudioClip otherClip;
	public AudioSource audioo;
	// Use this for initialization
	void Awake(){
		//AudioSource audioo = this.gameObject.GetComponent<AudioSource>();
	}
	void Start () {
		audioo.clip = otherClip;
		StartCoroutine (Play ());
	}
	IEnumerator Play(){
		yield return new WaitUntil(GameObject.FindGameObjectWithTag("mainchrt").GetComponent<CalibrationLoadingScript>().readytoplaymusic);
		audioo.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
