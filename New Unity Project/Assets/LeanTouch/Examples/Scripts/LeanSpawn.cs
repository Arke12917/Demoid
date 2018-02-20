using UnityEngine;

namespace Lean.Touch
{
	// This script can be used to spawn a GameObject via an event
	public class LeanSpawn : MonoBehaviour
	{
		public GameObject gameObjectt;
		public GameObject todestroy;
		[Tooltip("The prefab that gets spawned")]
		public Transform Prefab;

		[Tooltip("The distance from the finger the prefab will be spawned in world space")]
		public float Distance = 10.0f;

		public void Spawn()
		{
			if (GameObject.FindGameObjectWithTag ("title").GetComponent<Pause>().ispaused==false) {
				if (Prefab != null) {
					Instantiate (Prefab, transform.position, transform.rotation);
				}
			}
		}

		public void cusdestroy(){
			gameObjectt = GameObject.FindGameObjectWithTag("noteslide");
			if (gameObjectt == null) {
				GameObject[] gameObjects;
				gameObjects = GameObject.FindGameObjectsWithTag ("cheatcheck");

				for(var i = 0 ; i < gameObjects.Length ; i ++)
				{
					Destroy(gameObjects[i]);
				}
			}
	
			//Destroy(GameObject.FindGameObjectWithTag("cheatcheck"));
		}

		public static void slidedestroy(){
			GameObject[] gameObjects;
			gameObjects = GameObject.FindGameObjectsWithTag ("noteslide");

			for(var i = 0 ; i < gameObjects.Length ; i ++)
			{
				Destroy(gameObjects[i]);
			}
			//Destroy(GameObject.FindGameObjectWithTag("cheatcheck"));
		}

		public void Spawn(LeanFinger finger)
		{
			if (GameObject.FindGameObjectWithTag ("title").GetComponent<Pause> ().ispaused==false) {
				if (Prefab != null && finger != null) {
					Instantiate (Prefab, finger.GetWorldPosition (Distance), transform.rotation);
				}
			}
		}
	}
}