namespace UnityEngine.UI.Extensions{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COSCAN : MonoBehaviour {
	public GameObject Speeed;
	public GameObject Fixedd;
	public string width;
	public string height;
		private float fix1=0.015f;
		private float fix2=0.02f;
		private float fix3=0.03f;
		private float fix4=0.04f;
		private float fix5=0.05f;

		public string scene;
		public string scene169;
		public string Calibration="Calibration";
		public string Calibration169="Calibration 169";
		public Color loadToColor = Color.black;
	// Use this for initialization
	void Awake () {
		height = Screen.currentResolution.height.ToString();
		width = Screen.currentResolution.width.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void closecanvas(){
		Speeed.SetActive (false);
	}
	public void opencanvas(){
		Speeed.SetActive (true);
	}
	public void speedup(){
		if (GM.currentSpeed < 7) {
			GM.currentSpeed = GM.currentSpeed += 1;
		}
	}
		public void fixup(){
			if (Time.fixedDeltaTime == 0.01f) {
				Time.fixedDeltaTime = 0.015f;
				GM.CFT = Time.fixedDeltaTime;
			}else if (Time.fixedDeltaTime == 0.015f) {
				Time.fixedDeltaTime = 0.02f;
				GM.CFT = Time.fixedDeltaTime;
			} else {
			}
				
		}
	public void fixdown(){
			if (Time.fixedDeltaTime == 0.02f) {
				Time.fixedDeltaTime = 0.015f;
				GM.CFT = Time.fixedDeltaTime;
			} else if (Time.fixedDeltaTime == 0.015f) {
				Time.fixedDeltaTime = 0.01f;
				GM.CFT = Time.fixedDeltaTime;
			}  else if (Time.fixedDeltaTime == 0.01f) {
		} else {
		}
	}
		public void fixactive(){
			Fixedd.SetActive (true);
		}
		public void fixclose(){
			Fixedd.SetActive (false);
		}
	public void speeddown(){
		if (GM.currentSpeed > 4) {
			GM.currentSpeed = GM.currentSpeed -= 1;
		}
	}
		public void setfixedtime(){
			ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
			ZPlayerPrefs.SetFloat("FixTime", GM.CFT);
			ZPlayerPrefs.Save();
			Fixedd.SetActive (false);
		}
	public void NEXTLEVEL(){
		float conheight = float.Parse(height);
		float conwidth = float.Parse(width);
		print (conheight);
		print (conwidth);
		print ((conwidth*1.0)/conheight);
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		ZPlayerPrefs.SetFloat("Speed", GM.currentSpeed);
		ZPlayerPrefs.Save ();
		GameObject.FindGameObjectWithTag ("Credits").GetComponent<SongBK> ().count = 2;
		if ((conwidth*1.0)/conheight >= 1.7f||(conwidth*1.0)/conheight>=2.05f) {
		//16:9
		//18:9
			print ("yah");
				Time.timeScale = 1;
				Initiate.Fade(scene169,loadToColor,0.5f);
		//Application.LoadLevel (4);
		} else {
				Time.timeScale = 1;
				Initiate.Fade(scene,loadToColor,0.5f);
		//Application.LoadLevel (2);
		}

	}
		public void LoadCalibrate(){
			float conheight = float.Parse(height);
			float conwidth = float.Parse(width);
			print (conheight);
			print (conwidth);
			print ((conwidth*1.0)/conheight);
			ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
			ZPlayerPrefs.SetFloat("Speed", GM.currentSpeed);
			ZPlayerPrefs.Save ();
			GameObject.FindGameObjectWithTag ("Credits").GetComponent<SongBK> ().count = 2;
			GameObject.FindGameObjectWithTag ("YEABOI").tag="BOIYEA";
			if ((conwidth*1.0)/conheight >= 1.7f||(conwidth*1.0)/conheight>=2.05f) {
				//16:9
				//18:9
				print ("yah");
				Time.timeScale = 1;
				Initiate.Fade(Calibration169,loadToColor,0.5f);
				//Application.LoadLevel (4);
			} else {
				Time.timeScale = 1;
				Initiate.Fade(Calibration,loadToColor,0.5f);
				//Application.LoadLevel (2);
			}

		}
	}
}

