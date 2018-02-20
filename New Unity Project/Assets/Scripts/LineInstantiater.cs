using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineInstantiater : MonoBehaviour {

	public GameObject lineprefab;
	private GameObject linehandler;
	private Vector3 mousepos;

	void Update()
	{

		if (Input.GetMouseButtonDown(0)){
		mousepos = Input.mousePosition;
		mousepos.z = 4.9f;
		mousepos.y = 55.0f;

		mousepos = Camera.main.ScreenToWorldPoint(mousepos);
		linehandler = Instantiate(lineprefab, mousepos, Quaternion.identity) as GameObject;
		GameObject[] gos = GameObject.FindGameObjectsWithTag("noteclicker");
		foreach(GameObject go in gos)
			Destroy(go, 0.15f);
		
}
}
}