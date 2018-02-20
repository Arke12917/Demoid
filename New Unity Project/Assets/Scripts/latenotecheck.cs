using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class latenotecheck : MonoBehaviour {
	public LayerMask myLayerMask;
	bool started = true;
	bool nostarted=false;
	bool nextstepp=true;
	public Transform sucessBurst;
	public GameObject nextstep;
	public float range = 0.0001f;
	public float range1 = 0.001f;
	public static float charmingint;
	public static float normint;
	public void DestroyNow()
	{
		Destroy(gameObject);
	}

	IEnumerator Starto(){
		yield return new WaitUntil(GameObject.FindGameObjectWithTag("mainchrt").GetComponent<ExampleLoadingScript>().readytoplaymusic);
		charmingint=100.0f/GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo;
		normint = charmingint / 2f;
	}
	void Awake(){
		
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (Starto ());

		if (started) {

			RaycastHit hit;
			Debug.DrawRay (transform.position, Vector3.up * range);
			if (Physics.Raycast (transform.position, Vector3.up, out hit, range,myLayerMask)) {
				if (hit.collider.gameObject.tag == "SlideBox") {


					hit.collider.transform.parent.gameObject.SetActive (false);
					Debug.Log ("Hit!!");
					Instantiate (sucessBurst, hit.point, sucessBurst.rotation);
					GM.totalCombo += 1;
					notecontrol.AllCharming = " ";
					if (GM.highestcombo <= GM.totalCombo) {
						GM.highestcombo += 1;
					}
					if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
						GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += normint;
					}
					started = false;
					nostarted = true;
				} else {
					nostart ();
					//nostarted = true;
				}
			} else {
				nostart ();
			}
		}
	}

	public void nostart(){

				if (nostarted == false) {
				RaycastHit hit1;
				Debug.DrawRay (transform.position, Vector3.down * range1);
			if (Physics.Raycast (transform.position, Vector3.down, out hit1, range1,myLayerMask)) {
				if (hit1.collider.gameObject.tag == "SlideBox") {
					hit1.collider.transform.parent.gameObject.SetActive (false);
					Debug.Log ("Hit!!");
					Instantiate (sucessBurst, hit1.point, sucessBurst.rotation);
					GM.totalCombo += 1;
					notecontrol.AllCharming = " ";
					if (GM.highestcombo <= GM.totalCombo) {
						GM.highestcombo += 1;
					}
					if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
						GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += normint;
					}
					nextstepp = false;
				} else {
					Instantiate(nextstep, transform.position, Quaternion.Euler(-90,0,0));
					//nextstepp = false;
				}

			} else if (nextstepp == true) {
				Instantiate(nextstep, transform.position, Quaternion.Euler(-90,0,0));

			}



		}
	}
}

