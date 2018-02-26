using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidenoteburst : MonoBehaviour {
	public Transform failBurst;
	public Transform sucessBurst;
	public bool canclick;
	public float startTime=3f;

	// Use this for initialization
	void Start () {

	}
	public IEnumerator checkclick(){
		yield return new WaitForSeconds (0.0f);
		canclick = true;
	}

	void Update () {
		/*if (canclick) {
			startTime -= Time.deltaTime;
			print (startTime);
		}*/
	}

	// Update is called once per frame

}