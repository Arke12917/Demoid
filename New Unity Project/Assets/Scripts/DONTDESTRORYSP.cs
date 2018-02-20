namespace UnityEngine.UI.Extensions{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DONTDESTRORYSP : MonoBehaviour {
	public int difpage;
	// Use this for initialization
	void Awake () {
		difpage = GameManager.rcount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
}
