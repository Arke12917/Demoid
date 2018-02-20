using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class movebg : MonoBehaviour {
	
	public RectTransform rectTransform;
	public RectTransform rect2;
	// Use this for initialization
	void Start () {
		gameObject.name = transform.parent.name;
		//transform.parent = null;
		rect2 = GameObject.FindWithTag("State0").GetComponent<RectTransform>();
		//StartCoroutine(start());
	}

	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator start (){
		yield return new WaitForSeconds (0.6f);
		rectTransform.anchoredPosition = rect2.anchoredPosition;
		rectTransform.sizeDelta = rect2.sizeDelta;
		rectTransform.eulerAngles = rect2.eulerAngles;
	
	}
}
