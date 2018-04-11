namespace UnityEngine.UI.Extensions
{
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class changedifimage : MonoBehaviour {
	public Image difficon;
	public Sprite Easy;
	public Sprite Normal;
	public Sprite Hard;
	public Sprite Extra;
	public static int currentdiff;
	public static string currentsong;
	public string finaldifficulty;
	public bool foundnewdiff=false;
	public string checkdiff;
	public static string ENHE;
	public int defaultdiff;
	// Use this for initialization
		void Awake(){
			currentdiff = 0;
		}
		void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void newdif(){
			print ("started");
			checkdiff = GameObject.FindWithTag ("USP").GetComponent<UpdateScrollSnap> ().difficulty.text;
			foundnewdiff = false;
			int actualpage = GameObject.FindWithTag ("USP").GetComponent<UpdateScrollSnap> ().pagechanged;
		var Diff = GameObject.FindGameObjectsWithTag("listedSN");
		foreach (GameObject dif in Diff) {
				if (dif.GetComponent<songnamechange> ().difpage ==actualpage ) {
					currentsong = dif.gameObject.name;
					finaldifficulty = currentsong;
			}
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath+"/"+currentsong);
		//print("Streaming Assets Path: " + directoryInfo);
		FileInfo[] allFiles = directoryInfo.GetFiles("*.txt");
			foreach (FileInfo file in allFiles) {
				if (file.Name.Contains ("EASY")) {
					foundnewdiff = true;
				} if (file.Name.Contains ("NORMAL")) {
					foundnewdiff = true;
				}  if (file.Name.Contains ("HARD")) {
					foundnewdiff = true;
				}  if (file.Name.Contains ("EXTRA")) {
					foundnewdiff = true;
				}
			}
			if (foundnewdiff == false) {
				currentdiff = 0;
			} else {
				if (checkdiff.Contains ("Easy")) {
					defaultdiff = 1;
				}else if (checkdiff.Contains ("Normal")) {
					defaultdiff = 2;
				}else if (checkdiff.Contains ("Hard")) {
					defaultdiff = 3;
				}if (checkdiff.Contains ("Extra")) {
					defaultdiff = 4;
				}
				if (checkdiff.Contains ("Easy")&&defaultdiff == 1) {
					if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "NORMAL.txt")) {
						currentdiff = 2;
						ENHE = "NORMAL";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("NORMAL")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "HARD.txt")) {
						currentdiff = 3;
						ENHE = "HARD";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("HARD")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EXTRA.txt")) {
						currentdiff = 4;
						ENHE = "EXTRA";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EXTRA")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					}

				} else if (checkdiff.Contains ("Normal")&&defaultdiff == 2) {
					if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "HARD.txt")) {
						currentdiff = 3;
						ENHE = "HARD";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("HARD")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EXTRA.txt")) {
						currentdiff = 4;
						ENHE = "EXTRA";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EXTRA")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EASY.txt")) {
						currentdiff = 1;
						ENHE = "EASY";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EASY")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					}
				} else if (checkdiff.Contains ("Hard")&&defaultdiff == 3) {
					if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EXTRA.txt")) {
						currentdiff = 4;
						ENHE = "EXTRA";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EXTRA")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EASY.txt")) {
						currentdiff = 1;
						ENHE = "EASY";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EASY")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "NORMAL.txt")) {
						currentdiff = 2;
						ENHE = "NORMAL";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("NORMAL")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					}
				} else if (checkdiff.Contains ("Extra")&&defaultdiff == 4) {
					if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "EASY.txt")) {
						currentdiff = 1;
						ENHE = "EASY";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("EASY")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "NORMAL.txt")) {
						currentdiff = 2;
						ENHE = "NORMAL";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("NORMAL")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					} else if (File.Exists (Application.persistentDataPath + "/" + currentsong + "/" + "HARD.txt")) {
						currentdiff = 3;
						ENHE = "HARD";
						foreach (FileInfo file in allFiles){
							if (file.Name.Contains ("HARD")) {
								StartCoroutine ("Changedthedif",file);
							}
						}
					}
				}
			
			}
		/*if (currentdiff == 1) {
			currentdiff = 2;
		} else if (currentdiff == 2) {
			currentdiff = 3;
		} else if (currentdiff == 3) {
			currentdiff = 4;
		} else if(currentdiff == 4) {
			currentdiff = 1;
		} */
			
	}
		IEnumerator Changedthedif(FileInfo difFile){
			yield return new WaitForSeconds (0.0f);
			//1
			if (difFile.Name.Contains("meta"))
			{
				yield break;
			}
			//2
			else if(difFile.Name==ENHE+".txt")
			{
				string diffFilePath = difFile.FullName.ToString ();
				using (StreamReader reader = new StreamReader(diffFilePath))
				{  

					GameObject.FindWithTag ("USP").GetComponent<UpdateScrollSnap> ().difficulty.text = reader.ReadLine();
					GameObject.FindWithTag ("USP").GetComponent<UpdateScrollSnap> ().FINDDIF ();
					GameObject.FindWithTag ("USP").GetComponent<UpdateScrollSnap> ().Diffsc0re ();
				}
		}
	}

	public void changeimagehere(){
		if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Easy")) {
			difficon.sprite = Easy;
		} else if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Normal")) {
			difficon.sprite = Normal;
		} else if (GameObject.FindGameObjectWithTag ("LVLTXT").name.Contains ("Hard")) {
			difficon.sprite = Hard;
		} else if(GameObject.FindGameObjectWithTag("LVLTXT").name.Contains ("Extra")) {
			difficon.sprite = Extra;
		}
	}
 }
}