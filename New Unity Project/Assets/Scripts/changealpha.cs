using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changealpha : MonoBehaviour {

	public KeyCode increasealpha;
	public KeyCode decreasealpha;
	public float alphalevel = .5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (increasealpha))
			alphalevel += 0.5f;
		
		if (Input.GetKeyDown (decreasealpha))
			alphalevel -= 0.5f;

		GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, alphalevel);
		
	}
}
