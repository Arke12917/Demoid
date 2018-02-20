using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;


public class noteearlylate : MonoBehaviour {
	public Vector2 ScreenPosition;
	public Transform earlylateBurst;
	public float range = 0.0001f;
	[Tooltip("The prefab that gets spawned")]
	public Transform Prefab;

	[Tooltip("The distance from the finger the prefab will be spawned in world space")]
	public float Distance = 10.0f;
	// This will return the world position of this finger based on the distance from the camera
	public Vector3 GetWorldPosition(float distance, Camera camera = null)
	{
		if (LeanTouch.GetCamera(ref camera) == true)
		{
			ScreenPosition.y = 696.0f;
			var point = new Vector3(ScreenPosition.x, ScreenPosition.y, distance);

			return camera.ScreenToWorldPoint(point);
		}
		return default(Vector3);
	}
		


	void Update () {

	
			if (Prefab != null)
			{
				Instantiate(Prefab, GetWorldPosition(Distance), transform.rotation);
			}
	
		RaycastHit hit;
		Debug.DrawRay(transform.position, Vector3.up * range);
		if (Physics.Raycast (transform.position, Vector3.up, out hit, range)) {

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

