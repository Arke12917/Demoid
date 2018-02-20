using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class Chngloadernm : MonoBehaviour {

	public string Chartnm;
	public Object[] prefabs;
	// Use this for initialization
	void Awake () {
		prefabs = GameObject.FindGameObjectsWithTag("YEABOI");
		foreach (GameObject goo in prefabs) {
			Chartnm = goo.name;
		}
		/*var taggedObjects = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g=>g.tag=="YEABOI").ToList();
		foreach (GameObject goo in taggedObjects) {
			Chartnm = goo.gameObject.name;
			Debug.Log(goo.gameObject.name);
		}*/
		gameObject.name = Chartnm;
	}

	void Start(){
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
