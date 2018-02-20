namespace UnityEngine.UI.Extensions{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongBK : MonoBehaviour {
		public int count=0;
		public float fadeDamp = 0.5f;
		[HideInInspector]
		public string fadeScene;
		[HideInInspector]
		public float alpha = 0.0f;
		[HideInInspector]
		public Color fadeColor=Color.black;
		public bool slowing;
	// Use this for initialization
	void Start () {
		
	}
		IEnumerator startsongs(){
			yield return new WaitForSecondsRealtime(2.0f);
			GameObject.FindGameObjectWithTag("USP").GetComponent<UpdateScrollSnap>().stopsongs();
		}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnLevelWasLoaded(int level) {
			if (level == 1) {
				if (count == 0) {
					GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().PageChange (0);
					GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().setBG ();
					GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().changediff ();
					GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().FINDDIF ();
					GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().getsc0re ();
					StartCoroutine (startsongs ());
					count = 1;

				} else if (count == 2) {
					Time.timeScale = 1;
					GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().StartCoroutine ("SNG");
					StartCoroutine (startsongs ());
				}
			} else if (level == 2 || level == 4) {
				
				
		}
	}
		IEnumerator decrease(){
			yield return new WaitForSecondsRealtime (0.0f);
			while (slowing) {
				if (alpha > 0) {
					alpha = Mathf.Lerp (alpha, -0.1f, fadeDamp * Time.deltaTime);
				} else {
					slowing = false;
				}
			}
		}
}
}
