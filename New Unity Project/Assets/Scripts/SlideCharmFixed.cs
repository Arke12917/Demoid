using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlideCharmFixed : MonoBehaviour {
	bool started = true;
	bool nostarted=false;
	bool nextstepp=true;
	public Transform sucessBurst;
	public Transform lateburst;
	public float range = 0.0001f;
	public float range1 = 0.001f;
	public bool collidertoucched = false;
	public static float charmingintt;
	public static float normintt;
	public GameObject chea;
	public void DestroyNow()
	{
		Destroy(gameObject);
	}
	// Use this for initialization

	void update(){
			raycastleft ();
			raycastdown ();
			RaycastHit hit;
			Debug.DrawRay (transform.position, Vector3.right * range, Color.white, 50f);
			if (Physics.Raycast (transform.position, Vector3.right, out hit, range)) {
				if (hit.collider.gameObject.tag == "slideynote") {
					if (collidertoucched == false) {
						hit.collider.gameObject.SetActive (false);
						Debug.Log ("Hit!!");
						Instantiate (lateburst, transform.position, lateburst.rotation);
						GM.totalCombo += 1;
						if (GM.highestcombo <= GM.totalCombo) {
							GM.highestcombo += 1;
						}
						if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore < 100) {
							GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore += normintt;
						}
					}
				}

			}

	}
	void Start () {
		StartCoroutine (Starto ());

	}

	void raycastleft(){
		RaycastHit hit1;
		Debug.DrawRay (transform.position, Vector3.left * range,Color.white,50f);
		if (Physics.Raycast (transform.position, Vector3.left, out hit1, range)) {
			if (hit1.collider.gameObject.tag == "slideynote") {
				if (collidertoucched == true) {
				}
			else if (collidertoucched == false) {
				hit1.collider.gameObject.SetActive (false);
				Debug.Log ("Hit!!");
				Instantiate (lateburst, transform.position, lateburst.rotation);
				GM.totalCombo += 1;
				if (GM.highestcombo <= GM.totalCombo) {
					GM.highestcombo += 1;
				}
					if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore < 100) {
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore += normintt;
				}
			}
		}
	}
}
	void raycastdown(){
		RaycastHit hit2;
		Debug.DrawRay (transform.position, Vector3.down * range1,Color.white,50f);
		if (Physics.Raycast (transform.position, Vector3.down, out hit2, range1)) {
			if (hit2.collider.gameObject.tag == "slideynote") {
				if (collidertoucched == true) {
				} else if (collidertoucched == false) {
				hit2.collider.gameObject.SetActive (false);
				Debug.Log ("Hit!!");
				Instantiate (lateburst, transform.position, lateburst.rotation);
				GM.totalCombo += 1;
				if (GM.highestcombo <= GM.totalCombo) {
					GM.highestcombo += 1;
				}
					if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore < 100) {
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore += normintt;
				}
			}
		}
	}
}
	IEnumerator Starto(){
		if (SceneManager.GetActiveScene ().name == "Calibration" || SceneManager.GetActiveScene ().name == "Calibration 169") {
		} else {
			yield return new WaitUntil (GameObject.FindGameObjectWithTag ("mainchrt").GetComponent<ExampleLoadingScript> ().readytoplaymusic);
		}
		charmingintt=100.0f/GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo;
		normintt = charmingintt / 2f;
		Debug.Log (normintt);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "slideynote") 
		{
			collidertoucched = true;
			Debug.Log ("Hit!!");
			Instantiate (sucessBurst, transform.position, sucessBurst.rotation);
			GM.totalCombo += 1;
			GM.highestcharmingcount += 1;
			if (GM.highestcombo <= GM.totalCombo) {
				GM.highestcombo += 1;
			}

			if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore < 100) {
				GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore += charmingintt;

			}
			other.gameObject.SetActive (false);
		}
	}
}

