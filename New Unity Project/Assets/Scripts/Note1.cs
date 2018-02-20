using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note1 : MonoBehaviour {

	Rigidbody rb;
	public float speed4=10f;
	public float speed5=12f;
	public float speed6=14f;
	public float speed7=16f;

	public static float speed=10;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		if (GM.currentSpeed == 4f) {
			speed = speed4;
		} else if (GM.currentSpeed == 5f) {
			speed = speed5;
		} else if (GM.currentSpeed == 6f) {
			speed = speed6;
		} else if (GM.currentSpeed == 7f) {
			speed = speed7;
		} else {
			speed = speed4;
		}
		rb.velocity=new Vector2(0,-speed);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
