using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Lean.Touch
{
	// This script fires events if a finger has been held for a certain amount of time without moving
	public class LeanFingerHeld : MonoBehaviour
	{

		public bool ischeating=false;
		public bool CRunning=false;
		public static Vector2 checkpos;
		public Vector3 firsttouch;
		public Vector2 currenttouch;
		public GameObject SAA;
		public GameObject gameObjectt;
		public Camera choose;
		public GameObject SLA;


		public Vector3 Posee;
		public GameObject line;
		public GameObject Slidelinehit;

		// Event signature
		[System.Serializable] public class FingerEvent : UnityEvent<LeanFinger> {}

		// This class will store extra Finger data
		[System.Serializable]
		public class Link
		{
			// The finger associated with this link
			public LeanFinger Finger;

			// Was this finger held?
			public bool LastSet;



			// The total movement so we can ignore it if it gets too high
			public Vector2 TotalScaledDelta;
		}

		[Tooltip("If the finger started over the GUI, ignore it?")]
		public bool IgnoreIfStartedOverGui;

		[Tooltip("The finger must be held for this many seconds")]
		public float MinimumAge = 1.0f;

		[Tooltip("The finger cannot move more than this many pixels relative to the reference DPI")]
		public float MaximumMovement = 5.0f;

		// Called on the first frame the conditions are met
		public FingerEvent onFingerHeldDown;

		// Called on every frame the conditions are met
		public FingerEvent onFingerHeldSet;

		// Called on the last frame the conditions are met
		public FingerEvent onFingerHeldUp;

		// This contains all the active and enabled LeanFingerHeld instances
		public static List<LeanFingerHeld> Instances = new List<LeanFingerHeld>();

		// This gets fired when a finger begins being held on the screen (LeanFinger = The current finger)
		public static System.Action<LeanFinger> OnFingerHeldDown;

		// This gets fired when a finger is held on the screen (LeanFinger = The current finger)
		public static System.Action<LeanFinger> OnFingerHeldSet;

		// This gets fired when a finger stops being held on the screen (LeanFinger = The current finger)
		public static System.Action<LeanFinger> OnFingerHeldUp;

		// This stores all finger links
		private List<Link> links = new List<Link>();

		protected virtual void OnEnable()
		{
			Instances.Add(this);

			// Hook events
			LeanTouch.OnFingerDown += OnFingerDown;
			LeanTouch.OnFingerSet  += OnFingerSet;
			LeanTouch.OnFingerUp   += OnFingerUp;
		}

		protected virtual void OnDisable()
		{
			Instances.Remove(this);

			// Unhook events
			LeanTouch.OnFingerDown -= OnFingerDown;
			LeanTouch.OnFingerSet  -= OnFingerSet;
			LeanTouch.OnFingerUp   -= OnFingerUp;
		}

		private void OnFingerDown(LeanFinger finger)
		{

			// Ignore?
			if (IgnoreIfStartedOverGui == true && finger.StartedOverGui == true)
			{
				return;
			}
		
			//slidey = Object.Instantiate (SAA, (finger.GetWorldPosition(4.3f)), Quaternion.identity);
			Vector3 touchposfar = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.farClipPlane);
			Vector3 touchposnear = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.nearClipPlane);
			Vector3 touchposF = choose.ScreenToWorldPoint (touchposfar);
			Vector3 touchposN = choose.ScreenToWorldPoint (touchposnear);
			//Debug.DrawRay (touchposN, touchposF - touchposN, Color.green, 2f);
			Ray TR = new Ray (touchposN, touchposF - touchposN);
			RaycastHit hit;
			string nameOfLayer = "TouchInput";
			//LayerMask layer =  ~(1 << LayerMask.NameToLayer(nameOfLayer )); 
			if (Physics.Raycast (TR.origin, TR.direction, out hit, Mathf.Infinity)) {
				if (hit.collider.gameObject.tag == "Note") {

					//Instantiate (SLA, new Vector3 (hit.point.x, line.gameObject.transform.position.y, Slidelinehit.gameObject.transform.position.z), Quaternion.identity);
				} else {
				}
			}
			//Instantiate(SLA,
			// Try and find the link for this finger
			var link = FindLink(finger);

			if (link == null)
			{
				link = new Link();

				link.Finger = finger;

				links.Add(link);
			}

			// Reset its data
			link.LastSet          = false;
			link.TotalScaledDelta = Vector2.zero;
}

		private void OnFingerSet(LeanFinger finger)
		{
			checkpos = finger.ScreenPosition;
			firsttouch = finger.GetWorldPosition(4.3f);
			if (finger.slidey != null) {
				Vector3 touchposfar = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.farClipPlane);
				Vector3 touchposnear = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.nearClipPlane);
				Vector3 touchposF = choose.ScreenToWorldPoint (touchposfar);
				Vector3 touchposN = choose.ScreenToWorldPoint (touchposnear);
				Debug.DrawRay (touchposN, touchposF - touchposN, Color.green, 2f);
				Ray TR = new Ray (touchposN, touchposF - touchposN);
				RaycastHit hit;
				string nameOfLayer = "TouchInput";
				LayerMask layer =  ~(1 << LayerMask.NameToLayer(nameOfLayer )); 
				if (Physics.Raycast (TR.origin, TR.direction, out hit,Mathf.Infinity)) {
					//if (hit.collider.gameObject.tag == "placehere") {
						finger.slidey.transform.position = new Vector3 (hit.point.x, line.gameObject.transform.position.y, line.gameObject.transform.position.z);
					//}
					//finger.slidey.transform.position=finger.GetWorldPosition(4.3f);
				}
			}


			if (CRunning) {
				
			} else if (CRunning == false) {
				
				//StartCoroutine(cheatdet(finger));
			}

				// Try and find the link for this finger
				var link = FindLink (finger);

				if (link != null) {
				
					// Has this finger been held for more than MinimumAge without moving more than MaximumMovement?
					var set = finger.Age >= MinimumAge;
					link.TotalScaledDelta += finger.ScaledDelta;

					if (set == true && link.LastSet == false) {
						onFingerHeldDown.Invoke (finger);

						if (Instances [0] == this) {
							if (OnFingerHeldDown != null)
								OnFingerHeldDown (finger);
							//Posee = finger.GetStartWorldPosition(4.3f);
							//finger.spawnslide(SAA, finger.GetStartWorldPosition(4.3f));
						}
					}

					if (set == true) {
						onFingerHeldSet.Invoke (finger);
						if (ischeating == false) {
						/*Vector3 touchposfar = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.farClipPlane);
						Vector3 touchposnear = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.nearClipPlane);
						Vector3 touchposF = choose.ScreenToWorldPoint (touchposfar);
						Vector3 touchposN = choose.ScreenToWorldPoint (touchposnear);
						Debug.DrawRay(touchposN, touchposF-touchposN, Color.green);*/
						if (finger.slidey == null) {
							Vector3 touchposfarr = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.farClipPlane);
							Vector3 touchposnearr = new Vector3 (finger.ScreenPosition.x, finger.ScreenPosition.y, choose.nearClipPlane);
							Vector3 touchposFF = choose.ScreenToWorldPoint (touchposfarr);
							Vector3 touchposNN = choose.ScreenToWorldPoint (touchposnearr);
							Debug.DrawRay (touchposNN, touchposFF - touchposNN, Color.green, 2f);
							Ray ZR = new Ray (touchposNN, touchposFF - touchposNN);
							RaycastHit hit1;
							string nameOfLayer = "TouchInput";
							LayerMask layer =  ~(1 << LayerMask.NameToLayer(nameOfLayer )); 
							if (Physics.Raycast (ZR.origin, ZR.direction, out hit1,Mathf.Infinity)) {
								//if (hit1.collider.gameObject.tag == "placehere") {
									finger.slidey = GameObject.Instantiate (SAA, new Vector3 (hit1.point.x, line.gameObject.transform.position.y, line.gameObject.transform.position.z), Quaternion.identity);
								//}
							}
						}
					} else if (ischeating == true) {
						Destroy(finger.slidey);
							//ischeating = false;
						}
						if (Instances [0] == this) {
							if (OnFingerHeldSet != null)
								OnFingerHeldSet (finger);
						}
					}

					if (set == false && link.LastSet == true) {
						onFingerHeldUp.Invoke (finger);

						if (Instances [0] == this) {
							if (OnFingerHeldUp != null)
								OnFingerHeldUp (finger);
						}
					}

					// Store last value
					link.LastSet = set;
				}
		
	}


		private void OnFingerUp(LeanFinger finger)
		{
				ischeating = false;
	
			// Try and find the link for this finger
			var link = FindLink(finger);
			Destroy(finger.slidey);
			// Link exists?
			if (link != null)
			{
				if (link.LastSet == true)
				{
					onFingerHeldUp.Invoke(finger);

					if (Instances[0] == this)
					{
						if (OnFingerHeldUp != null) OnFingerHeldUp(finger);
					}
				}

				// Remove link from list
				links.Remove(link);
			}
		}

		public IEnumerator cheatdet(LeanFinger finger){
			CRunning = true;
			yield return new WaitForSecondsRealtime (1f);
			/*while (firsttouch == currenttouch) {
				ischeating = true;
			}*/
			ischeating = true;
			yield return new WaitForSecondsRealtime(1f);
			ischeating = false;
			CRunning = false;
		}


			

		private Link FindLink(LeanFinger finger)
		{
			for (var i = 0; i < links.Count; i++)
			{
				var link = links[i];

				if (link.Finger == finger)
				{
					return link;
				}
			}

			return null;
		}
	}
}