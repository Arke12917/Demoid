using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SLC2 : MonoBehaviour {
	bool started = true;
	bool nostarted=false;
	bool nextstepp=true;
	public Transform sucessBurst;
	public Transform lateburst;
	public float range = 0.65f;
	public float range1 = 0.3f;
	public bool collidertoucched = false;
	public static float charmingintt;
	public static float normintt;
	public GameObject chea;
	public LayerMask myLayerMask;

	private Ray ray;
	private RaycastHit hit;
	private Ray ray1;
	private RaycastHit hit1;
	private Ray ray2;
	private RaycastHit hit2;
	public void DestroyNow()
	{
		Destroy(gameObject);
	}
	// Use this for initialization

	void Update()
	{
		raycastleft ();
		raycastdown ();
		ray = new Ray(transform.position, Vector3.right);
		Debug.DrawRay (transform.position, Vector3.right * range, Color.white, 10f);
		if (Physics.Raycast(ray, out hit, range,myLayerMask)) {
			if (hit.collider.gameObject.tag == "slideynote") {
				if (collidertoucched == false) {
					hit.collider.gameObject.SetActive (false);
					Debug.Log ("Hit!!");
					Instantiate (lateburst, transform.position, lateburst.rotation);
					GM.totalCombo += 1;
					notecontrol.AllCharming = " ";
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
		ray1 = new Ray(transform.position, Vector3.left);
		Debug.DrawRay (transform.position, Vector3.left * range, Color.white, 10f);
		if (Physics.Raycast(ray1, out hit1, range,myLayerMask)) { 
			if (hit1.collider.gameObject.tag == "slideynote") {
				if (collidertoucched == true) {
				}
				else if (collidertoucched == false) {
					hit1.collider.gameObject.SetActive (false);
					Debug.Log ("Hit!!");
					Instantiate (lateburst, transform.position, lateburst.rotation);
					GM.totalCombo += 1;
					notecontrol.AllCharming = " ";
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
		ray2 = new Ray(transform.position, Vector3.down);
		Debug.DrawRay (transform.position, Vector3.down * range1, Color.white, 10f);
		if (Physics.Raycast(ray2, out hit2, range1,myLayerMask))  {
			if (hit2.collider.gameObject.tag == "slideynote") {
				if (collidertoucched == true) {
				} else if (collidertoucched == false) {
					hit2.collider.gameObject.SetActive (false);
					Debug.Log ("Hit!!");
					Instantiate (lateburst, transform.position, lateburst.rotation);
					GM.totalCombo += 1;
					notecontrol.AllCharming = " ";
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
			charmingintt = 100.0f / GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo;
			normintt = charmingintt / 2f;
			Debug.Log (normintt);
		}
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
