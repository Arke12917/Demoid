using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findcanvas : MonoBehaviour {
	public Transform activebg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.SetParent (activebg);
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			transform.SetParent (activebg);
			
		} else {
			transform.SetParent (activebg);
		}
	
	}
}


