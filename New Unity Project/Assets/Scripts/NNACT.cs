using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNACT : MonoBehaviour {
	public Transform sucessBurst;
	public Transform lateBurst;
	public Transform failBurst;
	public Vector3 PSS;
	public LayerMask mylayermask;
	public GameObject CharmHit;
	public GameObject ClickHit;
	public GameObject failburst;
	public GameObject line;
	public GameObject Slidelinehit;
	public GameObject NACT;
	public float getFinger;
	private bool CCK;
	private float time;
	// Use this for initialization
	void Start () {
		RaycastHit hit;
		string nameOfLayer = "actlayer";
		LayerMask layer =  ~(1 << LayerMask.NameToLayer(nameOfLayer)); 
		Debug.DrawRay (transform.position, Vector3.up * 12f, Color.white, 10f);
		if(Physics.Raycast(transform.position, Vector3.up,out hit,12f,mylayermask)){
			if (hit.collider.gameObject.GetComponent<CanclickOG> () == null) {
			} else {
				CCK = hit.collider.gameObject.GetComponent<CanclickOG> ().canclick;
				time = hit.collider.gameObject.GetComponent<CanclickOG> ().startTime;
			
				if (CCK == true) {
					if (time <= 2.92f && time >= 2.824f) {
						hit.collider.gameObject.SetActive (false);
						//Debug.Log ("Hit!!");
						PSS = new Vector3 (hit.collider.transform.position.x, -1.848f, -3.559f);
						CharmHit=ObjectPooler.SharedInstance.GetPooledObject("CharmHit");
						CharmHit.transform.position = PSS;
						CharmHit.transform.rotation = Quaternion.Euler(-90,0,0);
						CharmHit.SetActive(true);
						//Instantiate (sucessBurst, PSS, sucessBurst.rotation);
						GM.totalCombo += 1;
						GM.highestcharmingcount += 1;
						if (GM.highestcombo <= GM.totalCombo) {
							GM.highestcombo += 1;
						}
						if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
							GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += notecheck.charmingint;
						}
					} else if ((time <= 2.967537f && time > 2.92f) || (time >= 2.78f && time < 2.824)) {
						hit.collider.gameObject.SetActive (false);
						//Debug.Log ("Hit!!");
						ClickHit=ObjectPooler.SharedInstance.GetPooledObject("ClickHit");
						ClickHit.transform.position = hit.collider.transform.position;
						ClickHit.transform.rotation = Quaternion.Euler(-90,0,0);
						ClickHit.SetActive(true);
						//Instantiate (lateBurst, hit.collider.transform.position, lateBurst.rotation);
						GM.totalCombo += 1;
						if (GM.highestcombo <= GM.totalCombo) {
							GM.highestcombo += 1;
						}
						notecontrol.AllCharming = " ";
						if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
							GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += notecheck.normint;
						}
					} else if (time <= 3.0f && time > 2.967537f) {
						hit.collider.gameObject.SetActive (false);
						//Debug.Log ("Fail!!");
						failburst=ObjectPooler.SharedInstance.GetPooledObject("failBurst");
						failburst.transform.position = hit.collider.transform.position;
						failburst.transform.rotation = Quaternion.Euler(-90,0,0);
						failburst.SetActive(true);
						//Instantiate (failBurst, hit.point, failBurst.rotation);
						GM.totalCombo = (GM.totalCombo -= GM.totalCombo);
						notecontrol.AllCharming = " ";
						notecontrol.FullCombo = " ";
					}
				} else {
					//Instantiate (NACT, transform.position, Quaternion.identity);

				}
			}
	}
}
	
	// Update is called once per frame
	void Update () {
		
	}
}
