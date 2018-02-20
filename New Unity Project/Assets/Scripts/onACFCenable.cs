using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onACFCenable : MonoBehaviour {
	public Animator anim;
	// Use this for initialization
	void Start () {
		//anim.Play("Get ready");
	}
	void OnEnable() {
		anim.Play("Get ready");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
