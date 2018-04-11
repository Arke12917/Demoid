namespace UnityEngine.UI.Extensions{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class DONTCHARTER : MonoBehaviour {
		public int difpage;
		private string iscorrect;
		public List <GameObject> objecto= new List<GameObject>();
		// Use this for initialization
		IEnumerator look () {
			yield return new WaitForSeconds (0.0f);
			DirectoryInfo directoryInfoo = new DirectoryInfo (Application.persistentDataPath);
			//print ("Streaming Assets Path: " + Application.persistentDataPath);
			DirectoryInfo[] allFiless = directoryInfoo.GetDirectories ("*");
			foreach (DirectoryInfo directory in allFiless) {
				var toPath = directory + "/" + Path.GetFileName ("DEMOID.txt");
				print (toPath);
				if (File.Exists (toPath)) {
					//print ("YEAAAAAHHHDHSDHASHDSAHDAHDSAHD");
				}
				DirectoryInfo directoryInfo = new DirectoryInfo (directory + "/");
				//print ("Streaming Assets Path: " + directoryInfo);
				FileInfo[] allFiles = directoryInfo.GetFiles ("*.txt");
				foreach (FileInfo file in allFiles) {
					if (file.Name.Contains ("song")) {
						//print("HELLO THERE");
						StartCoroutine ("loadIllustrator", file);

					}
				}

			}

			//difpage = GameManager.ILcount;
		}
		void Awake(){
			
				StartCoroutine (waitagain ());
		}

		IEnumerator waitagain(){
			yield return new WaitForSeconds (3.0f);
			if (gameObject.name=="Charter") {
				StopCoroutine("waitagain");
			} else {

			}
			yield return new WaitUntil(GameManager.desfinished);
			StartCoroutine (look ());
		}
		IEnumerator loadIllustrator(FileInfo ChartFile)
		{
			yield return new WaitForSeconds (0.0f);
			//1
			if (ChartFile.Name.Contains ("meta")) {
				yield break;
			}
			//2
			else {
				string nameFilePath = ChartFile.FullName.ToString ();
				string cht = File.ReadAllText (nameFilePath);
				if (cht == "") {
				} else {
					string diffFilePath = ChartFile.FullName.ToString ();
					using (StreamReader reader = new StreamReader (diffFilePath)) {  
						for (int q = 0; q < 2; ++q) {
							iscorrect = reader.ReadLine ();
						}

					}
					if (iscorrect == gameObject.name) {
						string INAME = "song " + GameObject.FindGameObjectWithTag ("State0").name + ".txt";
						//print (INAME);
						string ChartFileWithoutExtension = Path.GetFileNameWithoutExtension (ChartFile.ToString ());
						string I1 = ChartFileWithoutExtension;
						print (I1);
						string INEXT = I1.Replace ("song", string.Empty);
						char[] arr = new char[] { ' ' };
						string nExt = INEXT.TrimStart (arr);
						print (nExt);

						foreach (GameObject namp in GameObject.FindGameObjectsWithTag("State0")) {
							objecto.Add (namp);
							if (namp.name == nExt) {
								using (StreamReader reader = new StreamReader(diffFilePath))
								{  

									this.gameObject.GetComponent<Text>().text= reader.ReadLine();

								}
								difpage = namp.GetComponent<songnamechange> ().thispagelol;
								print (namp.GetComponent<songnamechange> ().thispagelol.ToString ());
								objecto.Clear ();
							} else {
								objecto.Clear ();
							}
						}
						//string lol = GameObject.Find (nExt).GetComponent<songnamechange> ().thispagelol.ToString();
						/*if (I1 == "Illustrator "+nExt) {
					
					//difpage=GameObject.Find (nExt).GetComponent<songnamechange>().thispagelol;
				}*/
					} else {
					}
			}
				objecto.Clear ();
		}
	}

		// Update is called once per frame
		void Update () {

		}
	}
}
