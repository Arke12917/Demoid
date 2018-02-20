using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class pagelist : MonoBehaviour {



	public int thispage;
	public int currentpage;

	public pagelist(int newthispage, int newcurrentpage)
	{
		thispage = newthispage;
		currentpage = newcurrentpage;
	}
	// Use this for initialization
	void Start () {
		List<pagelist> PageList = new List<pagelist> ();

		PageList.Add (new pagelist (0, 0));
		PageList.Add (new pagelist (1, 1));


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
