using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class changedifimage : MonoBehaviour {
	public Image difficon;
	public Sprite Easy;
	public Sprite Normal;
	public Sprite Hard;
	public Sprite Extra;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeimagehere(){
		if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Easy")) {
			difficon.sprite = Easy;
		} else if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Normal")) {
			difficon.sprite = Normal;
		} else if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Hard")) {
			difficon.sprite = Hard;
		} else if(GameObject.FindGameObjectWithTag("LVLTXT").name.Contains ("Extra")) {
			difficon.sprite = Extra;
		}
	}
}
