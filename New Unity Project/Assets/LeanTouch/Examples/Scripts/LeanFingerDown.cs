using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Lean.Touch
{
	// This script calls the OnFingerDown event when a finger touches the screen
	public class LeanFingerDown : MonoBehaviour
	{
		public Camera choose;
		public Transform sucessBurst;
		public Transform lateBurst;
		public Transform failBurst;
		public Vector3 PSS;
		public LayerMask mylayermask;
		public GameObject line;
		public GameObject Slidelinehit;
		public GameObject SLA;
		public float getFinger;
		// Event signature
		[System.Serializable] public class LeanFingerEvent : UnityEvent<LeanFinger> {}

		[Tooltip("If the finger is over the GUI, ignore it?")]
		public bool IgnoreIfOverGui;

		public LeanFingerEvent OnFingerDown;

		protected virtual void OnEnable()
		{
			// Hook events
			LeanTouch.OnFingerDown += FingerDown;
		}

		protected virtual void OnDisable()
		{
			// Unhook events
			LeanTouch.OnFingerDown -= FingerDown;
		}

	

		private void FingerDown(LeanFinger finger)
		{
			// Ignore?
			if (IgnoreIfOverGui == true && finger.IsOverGui == true)
			{
				return;
			}

			Vector3 touchposfar = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.farClipPlane);
			Vector3 touchposnear = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.nearClipPlane);
			Vector3 touchposF = choose.ScreenToWorldPoint (touchposfar);
			Vector3 touchposN = choose.ScreenToWorldPoint (touchposnear);
			Debug.DrawRay(touchposN, touchposF-touchposN, Color.green);
			Ray TR = new Ray(touchposN, touchposF-touchposN);
			RaycastHit hit;
			string nameOfLayer = "actlayer";
			LayerMask layer =  ~(1 << LayerMask.NameToLayer(nameOfLayer)); 
			if(Physics.Raycast(TR.origin,TR.direction,out hit,Mathf.Infinity,layer)){

					//getFinger = finger.GetWorldPosition (4.3f).x;
					Instantiate (SLA, new Vector3 (hit.point.x, line.gameObject.transform.position.y, line.gameObject.transform.position.z), Quaternion.identity);
				
				}

				/*else if (hit.collider.gameObject.tag == "slideynote") {
					if (hit.collider.gameObject.GetComponent<Slidenoteburst> ().canclick == true) {
						if (hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime <= 2.92f && hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime >= 2.824f) {
							hit.collider.gameObject.SetActive (false);
							Debug.Log ("Hit!!");
							PSS = new Vector3 (hit.collider.transform.position.x, -1.848f, -3.559f);
							Instantiate (sucessBurst, PSS, sucessBurst.rotation);
							GM.totalCombo += 1;
							GM.highestcharmingcount += 1;
							if (GM.highestcombo <= GM.totalCombo) {
								GM.highestcombo += 1;
							}
							if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
								GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += notecheck.charmingint;
							}
						} else if ((hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime <= 2.967537 && hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime > 2.92f) || (hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime >= 2.78f && hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime < 2.824f)) {
							hit.collider.gameObject.SetActive (false);
							Debug.Log ("Hit!!");
							Instantiate (lateBurst, hit.collider.transform.position, lateBurst.rotation);
							GM.totalCombo += 1;
							if (GM.highestcombo <= GM.totalCombo) {
								GM.highestcombo += 1;
							}
							notecontrol.AllCharming = " ";
							if (GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore < 100) {
								GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore += notecheck.normint;
							}
						} else if (hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime <= 3.0f && hit.collider.gameObject.GetComponent<Slidenoteburst> ().startTime > 2.967537f) {
							hit.collider.gameObject.SetActive (false);
							Debug.Log ("Fail!!");
							Instantiate (failBurst, hit.point, failBurst.rotation);
							GM.totalCombo = (GM.totalCombo -= GM.totalCombo);
							notecontrol.AllCharming = " ";
							notecontrol.FullCombo = " ";
						}
					}
				}*/
			

			/*RaycastHit hit;
			Ray ray = choose.ViewportPointToRay(finger.GetWorldPosition(0.0f));
			Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green,50f);*/

			//Debug.DrawLine(ray.origin, ray.direction * 5000, Color.white,50f);
	
			// Call event
			OnFingerDown.Invoke(finger);
		}
	}
}