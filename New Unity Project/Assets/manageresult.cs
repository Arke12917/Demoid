using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class manageresult : MonoBehaviour {

	public string Chartnm;
	public Text MAXCOMBO;
	public Text MAXCHARMING;
	public Text charmcount;
	public Text highcombo;
	public Text FinalSc0re;
	public Image BG0;
	public GameObject ALL;
	public GameObject FULL;
	public Object[] prefabs;
	public string imagetype=".jpg";
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
		imagetype=".jpg";
		#endif
		ZPlayerPrefs.Initialize("what'sYourName", "salt12issalt");
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
		prefabs = GameObject.FindGameObjectsWithTag("YEABOI");
		foreach (GameObject goo in prefabs) {
			Chartnm = goo.name;
			GameObject.FindGameObjectWithTag ("NSN").GetComponent<Text> ().text = Chartnm;
		}
		if (GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming == true) {
			ALL.SetActive(true);
		} else if (GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo == true) {
			FULL.SetActive(true);
		}
		GameObject.FindGameObjectWithTag ("RDTEXT").GetComponent<Text> ().text = GM.DIFNAM;
		MAXCOMBO.text = GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo.ToString();
		MAXCHARMING.text = GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo.ToString();
		charmcount.text= GM.highestcharmingcount.ToString();
		highcombo.text = GM.highestcombo.ToString();
		FinalSc0re.text = GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().sc0re.ToString("F2")+"%";
		if (ZPlayerPrefs.GetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name) < GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().sc0re) {
			ZPlayerPrefs.SetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name, GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().sc0re);
			Debug.Log("Get Value" + GameObject.FindGameObjectWithTag("YEABOI").name + ZPlayerPrefs.GetFloat(GameObject.FindGameObjectWithTag("YEABOI").name) + ", Encrypt: " + ZPlayerPrefs.GetRowString(GameObject.FindGameObjectWithTag("YEABOI").name));
		}
		if (GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().AllCharming == true) {
			ZPlayerPrefs.SetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name + "AC", 1f);
		} else if (GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().FullCombo == true) {
			ZPlayerPrefs.SetFloat (GameObject.FindGameObjectWithTag ("YEABOI").name + "FC", 1f);
		}
		ZPlayerPrefs.Save ();

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
			Texture2D tex;
			tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
			www.LoadImageIntoTexture(tex);
			//5
			BG0.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f,1);
				//Sprite.Create(www.texture, new Rect(0,0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
		}
	}

	void Start(){
	}

	// Update is called once per frame
	void Update () {

	}
}
