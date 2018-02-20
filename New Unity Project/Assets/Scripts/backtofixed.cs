using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backtofixed : MonoBehaviour {
	public Camera miann;
	public string width;
	public string height;
	public string scene;
	public string scene169;
	public string scenesong;
	public Color loadToColor = Color.black;
	// Use this for initialization
	void Awake () {
		height = Screen.currentResolution.height.ToString();
		width = Screen.currentResolution.width.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void back(){
		Destroy(GameObject.FindGameObjectWithTag ("Scoreobject"));
		//Application.LoadLevel (1);
		GM.c00unt=1;
		Initiate.Fade(scenesong,loadToColor,0.5f);
	}
	public void reload(){
		GM.totalCombo = 0;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo = false;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming = false;
		GM.highestcombo = 0;
		GM.highestcharmingcount = 0;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo = 0;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().sc0re = 0;
		float conheight = float.Parse(height);
		float conwidth = float.Parse(width);
		if ((conwidth*1.0)/conheight >= 1.7f||(conwidth*1.0)/conheight>=2.05f) {
			//16:9
			//18:9
			print ("yah");
			//Application.LoadLevel (4);
			Initiate.Fade(scene169,loadToColor,0.5f);
		} else {
			Initiate.Fade(scene,loadToColor,0.5f);
			//Application.LoadLevel (2);
		}
	}

}
