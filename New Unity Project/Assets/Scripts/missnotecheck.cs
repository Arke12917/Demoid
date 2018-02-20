using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missnotecheck : MonoBehaviour {
	bool started = true;
	bool nostarted=false;
	public Transform failBurst;
	public float range = 0.0001f;
	public float range1 = 0.001f;
	public LayerMask myLayerMask;
	public void DestroyNow()
	{
		Destroy(gameObject);
	}
	// Use this for initialization
	void Start () {

			RaycastHit hit;
			Debug.DrawRay (transform.position, Vector3.up * range);
		if (Physics.Raycast (transform.position, Vector3.up, out hit, range,myLayerMask)) {
			if (hit.collider.gameObject.tag == "SlideBox") {

				hit.collider.transform.parent.gameObject.SetActive (false);
				Debug.Log ("Fail!!");
				Instantiate (failBurst, hit.point, failBurst.rotation);
				GM.totalCombo = (GM.totalCombo -= GM.totalCombo);
				notecontrol.AllCharming = " ";
				notecontrol.FullCombo = " ";

				started = false;
				nostarted = true;
			}
		}
	}
	
}