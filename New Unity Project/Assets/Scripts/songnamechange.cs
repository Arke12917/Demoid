namespace UnityEngine.UI.Extensions{

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class songnamechange : MonoBehaviour {
	public Text Nname;
	public GameObject nol;
	public AudioSource musicplayerr;
	public static UpdateScrollSnap USP;
		public GameManager GM0;
	public int thispagelol;
		public int difpage;
		public Image BG;
		public string imagetype = ".jpg";
		public string soundext=".mp3";
	// Use this for initialization
	void Awake () {
			#if !UNITY_EDITOR
			// On IOS...
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
			imagetype=".jpg";
			soundext=".mp3";
			}
			#endif
			#if UNITY_EDITOR
			imagetype=".jpg";
			soundext=".mp3";
			#endif
		StartCoroutine (loadsongname ());
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
			} else if (file.Name.Contains("bg"))
				{
					StartCoroutine("LoadPlayerUI", file);
				}
					thispagelol = GameManager.ccount;
					difpage = GameManager.rcount;
		}
			



	}

		
}
	void start(){
			StartCoroutine(changethesong());
	}

	// Update is called once per frame
	void Update () {
			
	}
		public IEnumerator tempBGcolor()
		{ yield return new WaitForSeconds (0.0f);
			BG = GetComponent<Image>();
			var tempColor = BG.color;
			tempColor.a = 1f;
			BG.color = tempColor;
			Debug.Log ("hihihihihih");
		}
		public IEnumerator tempBGcolor1()
		{yield return new WaitForSeconds (0.0f);
			BG = GetComponent<Image>();
			var tempColor = BG.color;
			tempColor.a = 0f;
			BG.color = tempColor;
		}

		void songplay()
		{
		}

	/*IEnumerator loadnames(){
		yield return new WaitForSeconds(0.0f);
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
		print("Streaming Assets Path: " + Application.streamingAssetsPath);
		FileInfo[] allFiles = directoryInfo.GetFiles("*.*");


		}*/
		
	IEnumerator loadsongname(){
		yield return new WaitForSeconds (0.0f);
		gameObject.name = gameObject.name.Replace ("(clone)", "");
		string s = "";
			s = gameObject.name.Remove(name.Length - 1);
			gameObject.name = s;
		Nname.text = gameObject.name;

	}

	IEnumerator LoadBackgroundMusic (FileInfo musicFile) 
	{
		//mfile = musicFile.Name.Replace(".ogg.meta","");

		yield return new WaitForSeconds (0.0f);
		print("lolololol");
		if (musicFile.Name.Contains("meta")) 
		{
			yield break;
		}
			else if(musicFile.Name=="soundtrack " + gameObject.name +soundext)
		{
			string musicFilePath = musicFile.FullName.ToString();
				print (musicFilePath);
			string url = string.Format("file://{0}", musicFilePath);
			WWW www = new WWW(url);
			yield return www;
			musicplayerr.clip = www.GetAudioClip(false, false);
			print("2ndcheck");
				print (musicFile.Name);
			yield return new WaitForSeconds (0.0f);
			//musicplayerr.Play();
			if (gameObject.name.Contains("-")){
				musicplayerr.Stop ();
			}
		
		}
	}
		IEnumerator changethesong(){
			yield return new WaitForSeconds (0.6f);

		}
		IEnumerator LoadPlayerUI(FileInfo playerFile)
		{
			yield return new WaitForSeconds (0.0f);
			//1
			if (playerFile.Name.Contains("meta"))
			{
				yield break;
			}
			//2
			else if(playerFile.Name=="bg " + gameObject.name +imagetype)
			{
				string playerFileWithoutExtension = Path.GetFileNameWithoutExtension(playerFile.ToString());
				string[] playerNameData = playerFileWithoutExtension.Split(" "[0]);
				//3
				string tempSongName = "";
				int i = 0;
				foreach (string stringFromFileName in playerNameData)
				{
					if (i != 0)
					{
						tempSongName = tempSongName + stringFromFileName + " ";
					}
					i++;
				}
				//4

				string wwwPlayerFilePath = "file://" + playerFile.FullName.ToString();
				WWW www = new WWW(wwwPlayerFilePath);
				yield return www;
				Texture2D texx;
				texx = new Texture2D(512, 512, TextureFormat.RGB24, false);
				www.LoadImageIntoTexture(texx);
				//5
				//5
				BG.sprite = Sprite.Create(texx, new Rect(0.0f, 0.0f, texx.width, texx.height), new Vector2(0.5f, 0.5f), 100.0f,1);
					//Sprite.Create(www.texture, new Rect(0,0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
			}
		}

		IEnumerator chooseBG()
		{
			yield return new WaitForSeconds (0.0f);
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
					if (file.Name.Contains ("bg")) {
						StartCoroutine ("LoadPlayerUI", file);

					}
				}

			}
		}
}
}
