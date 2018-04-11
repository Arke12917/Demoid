namespace UnityEngine.UI.Extensions{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
public class DONTILLUSTRATOR : MonoBehaviour {
	public int difpage;
		private string iscorrect;
		public List <GameObject> objects= new List<GameObject>();
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
					if (file.Name.Contains ("Illustrator")) {
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
			//yield return new WaitForSeconds (3.0f);
			if (gameObject.name=="Illustrator") {
				print("I SSTIO U BrO");
				StopCoroutine("waitagain");
			} else {
				
			}
				yield return new WaitUntil(GameManager.desfinished);
			StartCoroutine (look ());
		}
		IEnumerator loadIllustrator(FileInfo IllustratorFile)
		{
			yield return new WaitForSeconds (0.0f);
			//1
			//print("this works Too");
			if (IllustratorFile.Name.Contains("meta"))
			{
				yield break;
			}
			//2
			else
			{
				
				string diffFilePath = IllustratorFile.FullName.ToString ();
				using (StreamReader reader = new StreamReader (diffFilePath)) {  
					for (int q = 0; q < 2; ++q) {
						iscorrect = reader.ReadLine ();
					}

				}
				if (iscorrect == gameObject.name) {
					string INAME = "Illustrator " + GameObject.FindGameObjectWithTag ("State0").name + ".txt";
					//print (INAME);
					string IllustratorFileWithoutExtension = Path.GetFileNameWithoutExtension (IllustratorFile.ToString ());
					string I1 = IllustratorFileWithoutExtension;
					//print (I1);
					string INEXT = I1.Replace ("Illustrator", string.Empty);
					char[] arr = new char[] { ' ' };
					string nExt = INEXT.TrimStart (arr);
					//print (nExt);
				 
					foreach (GameObject name in GameObject.FindGameObjectsWithTag("State0")) {
						objects.Add (name);
						if (name.name == nExt) {
							using (StreamReader reader = new StreamReader(diffFilePath))
							{  
								
								this.gameObject.GetComponent<Text>().text= reader.ReadLine();
						
							}
							difpage = name.GetComponent<songnamechange> ().thispagelol;
							objects.Clear ();
							//print (name.GetComponent<songnamechange> ().thispagelol.ToString ());
						} else {
							objects.Clear ();
						}
					}
					//string lol = GameObject.Find (nExt).GetComponent<songnamechange> ().thispagelol.ToString();
					/*if (I1 == "Illustrator "+nExt) {
					
					//difpage=GameObject.Find (nExt).GetComponent<songnamechange>().thispagelol;
				}*/
	
				} else {
				}
				objects.Clear ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
}
