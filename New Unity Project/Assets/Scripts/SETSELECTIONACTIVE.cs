using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SETSELECTIONACTIVE : MonoBehaviour {

	public GameObject inactiveObject;
	public GameObject creds;
	public GameObject tutorial;
	public Transform holdempty;
	public GameObject butA;
	public GameObject butB;
	public GameObject butC;
	public bool tutorialiscomplete=false;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(creds);
		tutorial.SetActive (false);
		creds.SetActive (false);
		butA.SetActive (false);
		butB.SetActive (false);
		butC.SetActive (false);
		DontDestroyOnLoad(transform.gameObject);
		GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
		foreach(GameObject go in gos)
		{
			if(go.layer==10){
				DontDestroyOnLoad (go.gameObject);

				} 
	}

}

	void Start(){
		StartCoroutine (LevelWasLoaded());
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		if (ZPlayerPrefs.HasKey ("Tut")) {
			tutorialiscomplete = true;
		} else {
			ZPlayerPrefs.SetFloat("Tut", 1);
			tutorialiscomplete = false;
			ZPlayerPrefs.Save ();
		}
	}

	void OnLevelWasLoaded(int level) {
		if (level == 1) {
			if (tutorialiscomplete = false) {
				tutorial.SetActive (true);
				tutorialiscomplete = true;
			}
			creds.SetActive (false);
			butA.SetActive (true);
			butB.SetActive (true);
			butC.SetActive (true);
			inactiveObject.SetActive (true);
			inactiveObject.transform.DetachChildren();
			StopAllAudio ();
		} else if (level == 2) {
			GameObject[] godd = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
			foreach(GameObject go in godd)
			{
				if(go.layer==10){

					go.transform.SetParent(holdempty);
				} 
		}
			inactiveObject.SetActive (false);
		}else if (level == 4) {
		GameObject[] godd = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
		foreach(GameObject go in godd)
		{
			if(go.layer==10){

				go.transform.SetParent(holdempty);
			} 
		}
		inactiveObject.SetActive (false);
		}else if (level == 5||level==6) {
		Destroy(GameObject.FindGameObjectWithTag ("Scoreobject"));
		
		GameObject[] godd = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
		foreach(GameObject go in godd)
		{
			if(go.layer==10){

				go.transform.SetParent(holdempty);
			} 
		}
		inactiveObject.SetActive (false);
	}
}

	public AudioSource[] allAudioSources;

	public void StopAllAudio() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach( AudioSource audioS in allAudioSources) {
			audioS.Stop();
		}
	}


	IEnumerator LevelWasLoaded() {
		yield return new WaitForSeconds(0.9f);
		if (SceneManager.GetActiveScene().name == "Select Song") {
			GameObject[] god = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
			foreach(GameObject go in god)
			{
				if(go.layer==10){
					
					go.transform.SetParent(holdempty);
				} 
			}
			inactiveObject.SetActive (true);
		}
		else {
			GameObject[] goa = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]; //will return an array of all GameObjects in the scene
			foreach(GameObject go in goa)
			{
				if(go.layer==10){
					go.transform.SetParent(holdempty);
				} 
			}
			}
		}
	
	// Update is called once per frame
	void Update () {
		
	}
}
