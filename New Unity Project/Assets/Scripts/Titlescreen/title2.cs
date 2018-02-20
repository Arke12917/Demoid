using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title2 : MonoBehaviour {

	public int index;
	public string levelName;
	public Image black;
	public Animator anim;
			

	void Start ()
	{
		/*if (Input.GetMouseButton(0))
		StartCoroutine(Fading());*/
		
	}
	IEnumerator Fading()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (index);
	}
	

	// Use this for initialization
	public void OnStartGame(string scenetochangeto)
	{
		Application.LoadLevel (scenetochangeto);	
	}
		
}

