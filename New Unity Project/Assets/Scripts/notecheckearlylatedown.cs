using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notecheckearlylatedown : MonoBehaviour {

	public Transform earlylateBurst;
	public float range = 0.0001f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Debug.DrawRay(transform.position, Vector3.down * range);
		if (Physics.Raycast (transform.position, Vector3.down, out hit, range)) {

			Destroy(hit.collider.gameObject);
			Debug.Log ("Hit!!");
			Instantiate (earlylateBurst, transform.position, earlylateBurst.rotation);
			GM.totalCombo += 1;
			if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore < 100) {
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore += 0.02228164f;


			}


		}
	}

}