
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class deactivatebg : MonoBehaviour {
	public Image BG;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene ().name == "Select Song") {
			StartCoroutine (tempBGcolor ());
		} else {
			StartCoroutine (tempBGcolor1 ());
		}
	}

	public IEnumerator tempBGcolor()
	{ yield return new WaitForSeconds (0.0f);
		BG = GetComponent<Image>();
		var tempColor = BG.color;
		tempColor.a = 1f;
		BG.color = tempColor;
	}
	public IEnumerator tempBGcolor1()
	{yield return new WaitForSeconds (0.0f);
		BG = GetComponent<Image>();
		var tempColor = BG.color;
		tempColor.a = 0f;
		BG.color = tempColor;
	}

}
