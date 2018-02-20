using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class deactivatediff : MonoBehaviour {
	public Image BG;
	public Sprite black;
	public Sprite DEF;
	int m_IndexNumber;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Initialise the Sibling Index to 0
		m_IndexNumber = 0;
		//Set the Sibling Index
		transform.SetSiblingIndex(m_IndexNumber);
		if (SceneManager.GetActiveScene ().name == "Select Song") {
			BG.sprite = DEF;
		} else {
			BG.sprite = black;
		}
	}
}
