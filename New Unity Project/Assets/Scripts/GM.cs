using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour {

	List<float> whichNote = new List<float>() {1,10,5,15,22,30,36};



	public Transform noteObj;

	public string timerReset = "y";//done
	public float xPos;//done
	public int exChartcombo;//done
	public static int totalCombo = 0;//done
	public static int highestcombo = 0;//done
	public static int highestcharmingcount = 0;//done
	public float sc0re;
	public string SNGNAM;//done
	public static string DIFNAM;
	public bool AllCharming=false;//done
	public bool FullCombo=false;//done
	public GameObject Speeed;
	public static int c00unt;

	[Header("Speeds")]
	public static float currentSpeed = 4f;
	public static float speed4 = 4f;
	public static float speed5 = 5f;
	public static float speed6 = 6f;
	public static float speed7 = 7f;
	public static float CFT;


	void Awake() {
		Application.targetFrameRate = 120;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			Time.fixedDeltaTime = 0.01f;
		} else if (Application.platform == RuntimePlatform.Android) {
			Time.fixedDeltaTime = 0.01f;
		}
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		if (ZPlayerPrefs.HasKey ("Speed")) {
			currentSpeed = ZPlayerPrefs.GetFloat ("Speed", currentSpeed);
			ZPlayerPrefs.Save ();
		} else {
			ZPlayerPrefs.SetFloat("Speed", 4);
			currentSpeed = 4f;
			ZPlayerPrefs.Save ();
		}
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		if (ZPlayerPrefs.HasKey ("FixTime")) {
			if (ZPlayerPrefs.GetFloat ("FixTime") < 0.02f) {
				Time.fixedDeltaTime = ZPlayerPrefs.GetFloat ("FixTime");
				CFT = Time.fixedDeltaTime;
				ZPlayerPrefs.Save ();
			} else {
				ZPlayerPrefs.SetFloat("FixTime", Time.fixedDeltaTime);
				CFT = Time.fixedDeltaTime;
				ZPlayerPrefs.Save ();
			}
		} else {
			ZPlayerPrefs.SetFloat("FixTime", Time.fixedDeltaTime);
			CFT = Time.fixedDeltaTime;
			ZPlayerPrefs.Save ();
		}
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		if (ZPlayerPrefs.HasKey ("Offset")) {
			CalibrationLoadingScript.offset = ZPlayerPrefs.GetFloat ("Offset");
			ExampleLoadingScript.offset=ZPlayerPrefs.GetFloat ("Offset");
			ZPlayerPrefs.Save ();
		} else {
			ZPlayerPrefs.SetFloat("Offset", 0.00f);
			CalibrationLoadingScript.offset = 0.00f;
			ExampleLoadingScript.offset = 0.00f;
			ZPlayerPrefs.Save ();
		}

		//SNGNAM = GameObject.FindGameObjectWithTag ("LVLTXT").GetComponent<Text>().text;
		//StartCoroutine (SNG ());
	}

	void OnLevelWasLoaded(int level){
		if (level == 1) {
			//SNGNAM = GameObject.FindGameObjectWithTag ("LVLTXT").GetComponent<Text>().text;
			ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
			if (ZPlayerPrefs.HasKey ("Speed")) {
				currentSpeed = ZPlayerPrefs.GetFloat ("Speed", currentSpeed);
				ZPlayerPrefs.Save ();
			} else {
				ZPlayerPrefs.SetFloat("Speed", 4);
				currentSpeed = 4f;
				ZPlayerPrefs.Save ();
			}


		}
	}
	public void closecanvas(){
		Speeed.SetActive (false);
	}
	public void opencanvas(){
		Speeed.SetActive (true);
	}
	public void speedup(){
		if (currentSpeed < 7) {
			currentSpeed = currentSpeed += 1;
		}
	}
	public void speeddown(){
		if (currentSpeed > 4) {
			currentSpeed = currentSpeed -= 1;
		}
	}
	public void NEXTLEVEL(){
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
		ZPlayerPrefs.SetFloat("Speed", currentSpeed);
		ZPlayerPrefs.Save ();
		Application.LoadLevel (2);
	}
		
	// Use this for initialization
	void Start () {
		//maxchartcombo = GameObject.FindGameObjectsWithTag ("Note").Length + GameObject.FindGameObjectsWithTag ("slideynote").Length;
	}
	
	// Update is called once per frame
	void Update () {
		
		}

	public IEnumerator SNG(){
		if (c00unt == 1) {
			SNGNAM = DIFNAM;
			c00unt = 0;
		} else {
			yield return new WaitForSeconds (0.0f);
			SNGNAM = GameObject.FindGameObjectWithTag ("LVLTXT").GetComponent<Text>().text;
			c00unt = 0;
		}

	}
}

