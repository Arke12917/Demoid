using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour {

	public int index;
	public string levelName;
	public Image black;
	public Animator anim;
	public string scene;
	public Color loadToColor = Color.black;
	public static bool readytoload = true;

	public void clicked()
	{
		
		OnStartGame ();
		
	}
	IEnumerator Fading()
	{
		anim.SetBool ("Fade", true);
		yield return new WaitUntil (() => black.color.a == 1);
		SceneManager.LoadScene (index);
	}
	

	// Use this for initialization
	public void OnStartGame()
	{
		Initiate.Fade(scene,loadToColor,0.5f);
		//Application.LoadLevel (1);	
	}
		
}

