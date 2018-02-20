using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class actiavtorspawner : MonoBehaviour {
	public Transform Prefab;
	public GameObject triggerObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

	}
	void OnTriggerEnter(Collider col)
	{
		/*if (col.gameObject.name == "Judgeline") {
			Instantiate(Prefab, transform.position, Quaternion.Euler(-90,0,0));
			Debug.Log ("triggered");*/
		

		}

	public void DestroyNow()
	{
		Destroy(gameObject);
	}
}
