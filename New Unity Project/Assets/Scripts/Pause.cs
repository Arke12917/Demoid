using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public AudioClip Musicclip;
	public AudioSource MusicSource;
	public Transform canvas;
	public Transform canvas1;
	public ExampleLoadingScript ELS;
	public bool ispaused=false;
	public float slength;
	public GameObject AC;
	public GameObject FC;
	public string soundext=".mp3";
	public Camera miann;
	public string width;
	public string height;
	public string scene;
	public string scene169;
	public string sceneres;
	public string scenenorm;
	public Color loadToColor = Color.black;
	public GameObject Loader;
	public bool loadingother=false;
	public int musicID;
	public string mdirect;
	public static int c0unt=0;
	public string currentname;

	void Awake(){
		#if !UNITY_EDITOR
		// On IOS...
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
		soundext=".mp3";
		}
		#endif
		#if UNITY_EDITOR
		soundext = ".mp3";
		#endif
		currentname=SceneManager.GetActiveScene().name;
		height = Screen.currentResolution.height.ToString();
		width = Screen.currentResolution.width.ToString();
		DirectoryInfo directoryInfoo = new DirectoryInfo (Application.persistentDataPath);
		print ("Streaming Assets Path: " + Application.persistentDataPath);
		DirectoryInfo[] allFiless = directoryInfoo.GetDirectories ("*");
		foreach (DirectoryInfo directory in allFiless) {
			var toPath = directory + "/" + Path.GetFileName ("DEMOID.txt");
			print (toPath);
			if (File.Exists (toPath)) {
				print ("YEAAAAAHHHDHSDHASHDSAHDAHDSAHD");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo (directory + "/");
			print ("Streaming Assets Path: " + directoryInfo);
			FileInfo[] allFiles = directoryInfo.GetFiles ("*.*");
			foreach (FileInfo file in allFiles) {
				if (file.Name.Contains("soundtrack"))
				{
					StartCoroutine("LoadBackgroundMusic", file);

				}
			}

		}
	}
		
		/*DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
		print("Streaming Assets Path: " + Application.persistentDataPath);
		FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
		foreach (FileInfo file in allFiles) {
			if (file.Name.Contains("soundtrack"))
			{
				StartCoroutine("LoadBackgroundMusic", file);

			}
		}
	}*/

	// Use this for initialization
	void Start () {
		if (c0unt == 0) {
			GM.DIFNAM=GameObject.FindGameObjectWithTag("Scoreobject").GetComponent<GM>().SNGNAM;
			c0unt = 1;
		}
		GM.totalCombo = 0;//done
		GM.highestcombo = 0;//done
		GM.highestcharmingcount = 0;//done
		//StartCoroutine(Faded());
	


	}

	IEnumerator LoadBackgroundMusic (FileInfo musicFile) 
	{

		yield return new WaitForSeconds (0.0f);
		if (musicFile.Name.Contains("meta")) 
		{
			yield break;
		}
		else if(musicFile.Name=="soundtrack " + GameObject.FindGameObjectWithTag("mainchrt").name +soundext)
		{
			/*DirectoryInfo directoryInfa = new DirectoryInfo (Application.persistentDataPath);
			print ("Streaming Assets Path: " + Application.persistentDataPath);
			DirectoryInfo[] allFil = directoryInfa.GetDirectories ("*");
			foreach (DirectoryInfo directory in allFil) {
				//print("soundtrack "+directory.Name);
				if ("soundtrack "+ directory.Name+soundext == musicFile.Name) {
					print (directory.Name);
					mdirect = directory.Name;
				}
			}*/
			string musicFilePath = musicFile.FullName.ToString();
			print (musicFilePath);
			string url = string.Format("file://{0}", musicFilePath);
			WWW www = new WWW(url);
			yield return www;
			//musicID = ANAMusic.load (mdirect+"/"+musicFile.Name,true,false);
			MusicSource.clip = www.GetAudioClip(false, false);
			print (musicFile.Name);
			Musicclip = MusicSource.clip;
			slength = Musicclip.length;
			//slength=ANAMusic.getDuration(musicID);
			yield return new WaitForSeconds (0.0f);
			//musicplayerr.Play();

		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Audiothing (){
		if (loadingother == false) {
			if (canvas.gameObject.activeInHierarchy == false) {
				canvas.gameObject.SetActive (true);
				ispaused = true;
				Time.timeScale = 0;
				MusicSource.Pause ();

			} else {
				
				StartCoroutine (Waiting ());
			}
		}
	}
	public void restart(){
		if (SceneManager.GetActiveScene ().name == "Calibration" || SceneManager.GetActiveScene ().name == "Calibration 169") {
			loadingother = true;
			ispaused = false;
			GM.totalCombo = 0;
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore = 0.00f;
			notecontrol.AllCharming = "AC";
			notecontrol.FullCombo = "FC";
			GM.highestcombo = 0;
			GM.highestcharmingcount = 0;
			Time.timeScale = 1;
			SceneManager.LoadScene (currentname);
		} else{
			loadingother = true;
		ispaused = false;
		//Time.timeScale = 1;
		GM.totalCombo = 0;
		notecontrol.AllCharming = "AC";
		notecontrol.FullCombo = "FC";
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore = 0.00f;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo = false;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming = false;
		GM.highestcombo = 0;
		GM.highestcharmingcount = 0;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo = 0;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().sc0re = 0;
		float conheight = float.Parse (height);
		float conwidth = float.Parse (width);
		if ((conwidth * 1.0) / conheight >= 1.7f || (conwidth * 1.0) / conheight >= 2.05f) {
			//16:9
			//18:9
			print ("yah");
			Initiate.Fade (scene169, loadToColor, 0.5f);

			//Application.LoadLevel (4);
		} else {
			Initiate.Fade (scenenorm, loadToColor, 0.5f);
		
			//Application.LoadLevel (2);
		}
	}
	}
	IEnumerator Faded()
	{yield return new WaitForSeconds (0.0f);
		//yield return new WaitUntil(GameObject.FindGameObjectWithTag("mainchrt").GetComponent<ExampleLoadingScript>().readytoplaymusic);
		//MusicSource.Play();
		//StartCoroutine (SongEndo ());
		//Debug.Log("played!");
	}

	IEnumerator SongEndo(){
		if (SceneManager.GetActiveScene ().name == "Calibration" || SceneManager.GetActiveScene ().name == "Calibration 169") {
		}else{
		yield return new WaitForSeconds (slength);
		if(loadingother==false){
			loadingother = true;
		yield return new WaitForSecondsRealtime (0.2f);
		if (GM.highestcharmingcount == GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo) {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().totalScore=100.00f;
			GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming = true;
			AC.SetActive (true);
		} else if (GM.highestcombo == GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo) {
			GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo = true;
			FC.SetActive (true);
		}
		GameObject.FindGameObjectWithTag("Scoreobject").GetComponent<GM>().sc0re =  GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<notecontrol> ().finalscore;
		yield return new WaitForSecondsRealtime (2.0f);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore = 0.00f;
		GameObject.FindGameObjectWithTag("score").GetComponent<Text>().text = 0.00f+"%";
		notecontrol.AllCharming = "AC";
		notecontrol.FullCombo = "FC";
		//GameObject.FindGameObjectWithTag("ALL").GetComponent<Text>().text ="AC"+"";
		//GameObject.FindGameObjectWithTag("FULL").GetComponent<Text>().text ="FC"+"";
		Debug.Log("Level loaded");
			c0unt = 0;
		//ANAMusic.release(musicID);
		Initiate.Fade(sceneres,loadToColor,0.5f);
		//Application.LoadLevel(3);
		
		}
	}
}
	IEnumerator Waiting()
	{
		canvas1.gameObject.SetActive (true);
		float Waittime = Time.realtimeSinceStartup + 3;
		canvas.gameObject.SetActive (false);
		yield return new WaitWhile (() => Time.realtimeSinceStartup < Waittime);
		canvas1.gameObject.SetActive (false);
		ispaused = false;
		Time.timeScale = 1;
		MusicSource.Play();
		Debug.Log("played!");
	}

	IEnumerator StartChart()
	{
		ispaused = true;
		Time.timeScale = 0;
		canvas1.gameObject.SetActive (true);
		float Waittime = Time.realtimeSinceStartup + 2.5f;
		//canvas.gameObject.SetActive (false);
		yield return new WaitWhile (() => Time.realtimeSinceStartup < Waittime);
		canvas1.gameObject.SetActive (false);
		ispaused = false;
		Time.timeScale = 1;
		MusicSource.Play();
		//ANAMusic.play(musicID);
		StartCoroutine (SongEndo ());
		Debug.Log("played!");
	}

	public void gotosongs(){
		if (SceneManager.GetActiveScene ().name == "Calibration" || SceneManager.GetActiveScene ().name == "Calibration 169") {
			loadingother = true;
			c0unt = 0;
			GM.c00unt = 1;
			GM.totalCombo = 0;
			notecontrol.AllCharming = "AC";
			notecontrol.FullCombo = "FC";
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore = 0.00f;
			Destroy(GameObject.FindGameObjectWithTag ("Scoreobject"));
			Time.timeScale = 1;
			GameObject.FindGameObjectWithTag ("BOIYEA").tag="YEABOI";
			SceneManager.LoadScene ("Select Song");
		}else{
		loadingother = true;
		c0unt = 0;
		GM.c00unt = 1;
		GM.totalCombo = 0;
		notecontrol.AllCharming = "AC";
		notecontrol.FullCombo = "FC";
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<notecontrol>().totalScore = 0.00f;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo = false;
		GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming = false;
		Destroy(GameObject.FindGameObjectWithTag ("Scoreobject"));
		Initiate.Fade(scene,loadToColor,0.5f);
		StartCoroutine (waiting());
		//Destroy(Loader);
		//SceneManager.LoadScene("Select Song");
	}
}

	IEnumerator waiting(){
		yield return new WaitForSecondsRealtime (0.0f);
		//Time.timeScale = 1;
	}
}