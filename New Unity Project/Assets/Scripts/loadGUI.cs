namespace UnityEngine.UI.Extensions{

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.IO;

public class loadGUI : MonoBehaviour {
	public Text songheader;
		public Image BG0;
		public string imagetype = ".jpg";
	// Use this for initialization
	void Awake () {
			#if !UNITY_EDITOR
			// On IOS...
			if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
			imagetype=".jpg";
			}
			#endif
			#if UNITY_EDITOR
			imagetype = ".jpg";
			#endif
			songheader.text = GameObject.FindGameObjectWithTag ("YEABOI").name + " " + GameObject.FindGameObjectWithTag ("YEABOI").GetComponent<Text> ().text;
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


		IEnumerator LoadPlayerUI(FileInfo playerFile)
		{
			yield return new WaitForSeconds (0.0f);
			//1
			if (playerFile.Name.Contains("meta"))
			{
				yield break;
			}
			//2
			else if(playerFile.Name=="bg " + GameObject.FindGameObjectWithTag("YEABOI").name +imagetype)
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
				Texture2D texxx;
				texxx = new Texture2D(512, 512, TextureFormat.RGB24, false);
				www.LoadImageIntoTexture(texxx);
				//5
				BG0.sprite = Sprite.Create(texxx, new Rect(0.0f, 0.0f, texxx.width, texxx.height), new Vector2(0.5f, 0.5f), 100.0f,1);
				//5

				//BG0.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
			}
		}
	
	// Update is called once per frame
	void Update () {
		
	}
  }
}
