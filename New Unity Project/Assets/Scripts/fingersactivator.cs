using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class fingersactivator : MonoBehaviour {
	public Transform Prefab;
	public float Distance = 10.0f;

	void OnEnable()
	{
		LeanTouch.OnFingerDown += OnFingerDown;
	}
	void OnDisable()
	{
		LeanTouch.OnFingerDown -= OnFingerDown;
	}
	void OnFingerDown (LeanFinger finger)
	{
		Instantiate(Prefab, finger.GetWorldPosition(Distance), Quaternion.Euler(-90,0,0));
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
