using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeemoChart;
using System.IO;
using UnityEngine.Networking;

public class ExampleLoadingScript : MonoBehaviour {
	//This script will show you how to deserialize/use a chart
	//First we need to Deserialize a chart! In same folder with this script is a chart file (samsaraamberwishes.hard.json)
	//(I import it to project to use it easily. U could load chart files which not in the project. After all, we only need the data - text)
	//If In the project, json file will became TextAsset, which can easily read, but if it's not in the project, it will be read by File.ReadAllText(string path) (remember "using System.IO"), but i think it's still better if the chart is not in the project, so we can load many, dynamicly charts
	//I will set to public, which could be editted in the Inspector, or you'll have to load it my Path
	public TextAsset Chart;
	public Rigidbody rb;
	//prefabs for spawning
	public GameObject ClickNote_NoSound_prefab;
	public GameObject ClickNote_Sound_prefab;
	public GameObject SlideNote_prefab;
	public Pause paused;
    public string ndresult;
	public int counter;
	public int counterr;
	public float speed4=10f;
	public float speed5=12f;
	public float speed6=14f;
	public float speed7=16f;
	public GameObject JD4;
	public GameObject JD5;
	public GameObject JD6;
	public GameObject JD7;
	public static float speed;
	public GameObject LineHandler;
	public static float offset;


	public static float SPEED = 10f;
	IEnumerator waittostart(){
	yield return new WaitForSeconds(0.0f);
		counter = 1;
	}

	//now we'll read it and deSerialize it and get the D_Chart. Using 
	IEnumerator Chart_ing() {
		yield return new WaitUntil(desfinished);
		float Begin = 0.0f;
		float StartTime = Time.realtimeSinceStartup;
		yield return new WaitForSeconds(Begin);
		 // to get the time it took to load
		//declare the text of the chart (or you can skip this)
		string text = ndresult;
		//now we can get the D_Chart
		D_Chart _Chart = ChartLoader.DeserializeChart(text);
		//now we got all things we need
		//In case the chart is invalid or havesome problem, may be we could but this in a try-catch block (but this's a "totally valid" chart from the game
		//now let's build the notes
		//So we need to build click note (no sound and have sound) and the slide note.
		//First, Know a note is a slidenote, in the bottom of the chart, we have "links" and some "$ref" (guess it's reference). So "$ref": "13" mean the note has id: 13 is a slide note
		//Second, Remove piano note (black wave note in the background), they'll have position > 2 or < -2
		List<D_Note> ClickNotes = new List<D_Note>(); //Include NoSound note
		List<D_Note> SlideNotes = new List<D_Note> ();
		foreach (D_Note note in _Chart.notes) {
			
			bool isSlide = false;
			//Debug.Log ("ID" + note.id_);
			//determine which is slide or click
			foreach (D_Link links in _Chart.links) {
				if (isSlide) { //if it's already is slide note
					continue;
				} else {
					foreach (D_Note2 note2 in links.notes) {
						if (isSlide) {
							continue;
						} else {
							if (note.id_.ToString () == note2.ref_) {
								SlideNotes.Add (note); // add to slide note collection
								isSlide = true;
							}
						}
					}
				}
			}
			if (isSlide) { 
				continue;
			} else {
				//if it's not slide -> click note
				ClickNotes.Add (note);
			}
		}
		//after this, we'll have all notes we need, now for spawning object.
		foreach (D_Note note in ClickNotes) {
			if (note.sounds == null) { //if it's no Sound Note
				if (note.pos <= 2) {
					GameObject note_ = Object.Instantiate (ClickNote_NoSound_prefab, new Vector3 ((float)note.pos, (float)(note._time * SPEED), -3.556f), Quaternion.Euler (0, 0, 0));
					//for knowing
					note_.name = "Note ID: " + note.id_;
					//set size
					note_.transform.localScale = new Vector3 ((float)note.size/2, 0.6275f, 0.2f);
					note_.transform.SetParent (transform);
					GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo += 1;
				}
			} else {
				if (note.pos <= 2) {
					GameObject note_ = Object.Instantiate (ClickNote_Sound_prefab, new Vector3 ((float)note.pos, (float)(note._time * SPEED), -3.556f), Quaternion.Euler (0, 0, 0));
					//for knowing
					note_.name = "Note ID: " + note.id_;
					//set size
					note_.transform.localScale = new Vector3 ((float)note.size/2, 0.6275f, 0.2f);
					note_.transform.SetParent (transform);
					GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo += 1;
				}
			}
		}
		foreach (D_Note note in SlideNotes) {
			if (note.pos <= 2) {
				GameObject note_ = Object.Instantiate (SlideNote_prefab, new Vector3 ((float)note.pos, (float)(note._time * SPEED), -3.556f), Quaternion.Euler (0, 0, 0));
				note_.name = "Slide Note ID: " + note.id_;
				note_.transform.localScale = new Vector3 ((float)note.size/2, 0.6275f, 0.2f);
				note_.transform.SetParent (transform);
				GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo += 1;
			}
		}
		Debug.Log ("DONE");
		Debug.Log (string.Format ("Load took : {0}s", (Time.realtimeSinceStartup - StartTime)));
		print (100.0f / GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo);
		//notecontrol.charmingint += 100/GameObject.FindGameObjectWithTag ("Scoreobject").GetComponent<GM> ().exChartcombo;
		//notecontrol.normint += notecontrol.charmingint/2 ;
		//print (notecontrol.charmingint);
		//print (notecontrol.normint);
		//GameObject.FindGameObjectWithTag ("title").GetComponent<Pause> ().MusicSource.Play ();
		counterr = 1;
		GameObject.FindWithTag("title").GetComponent<Pause> ().StartCoroutine ("StartChart");
		//rb.velocity=new Vector2(0,-Note1.speed);


	}

	void Awake(){
		rb = GetComponent<Rigidbody> ();
		if (GM.currentSpeed == 4f) {
			SPEED = speed4;
			JD4.GetComponent<BoxCollider>().enabled = true;
			JD5.GetComponent<BoxCollider> ().enabled = false;
			JD6.GetComponent<BoxCollider> ().enabled = false;
			JD7.GetComponent<BoxCollider> ().enabled = false;
		} else if (GM.currentSpeed == 5f) {
			SPEED = speed5;
			JD4.GetComponent<BoxCollider>().enabled = false;
			JD5.GetComponent<BoxCollider> ().enabled = true;
			JD6.GetComponent<BoxCollider> ().enabled = false;
			JD7.GetComponent<BoxCollider> ().enabled = false;
		} else if (GM.currentSpeed == 6f) {
			SPEED = speed6;
			JD4.GetComponent<BoxCollider>().enabled = false;
			JD5.GetComponent<BoxCollider> ().enabled = false;
			JD6.GetComponent<BoxCollider> ().enabled = true;
			JD7.GetComponent<BoxCollider> ().enabled = false;
		} else if (GM.currentSpeed == 7f) {
			SPEED = speed7;
			JD4.GetComponent<BoxCollider>().enabled = false;
			JD5.GetComponent<BoxCollider> ().enabled = false;
			JD6.GetComponent<BoxCollider> ().enabled = false;
			JD7.GetComponent<BoxCollider> ().enabled = true;
		} else {
			SPEED = speed4;
			JD4.GetComponent<BoxCollider>().enabled = true;
			JD5.GetComponent<BoxCollider> ().enabled = false;
			JD6.GetComponent<BoxCollider> ().enabled = false;
			JD7.GetComponent<BoxCollider> ().enabled = false;
		}

		counter = 0;
		counterr = 0;
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
				if (file.Name.Contains ("chart")) {
					StartCoroutine ("LoadChart", file);

				}
			}

		}
	}
	// Use this for initialization
	void Start () {
		//Deserialize at the start
		StartCoroutine(waittostart());
		StartCoroutine (Chart_ing ());
		if (GM.currentSpeed == 4f) {
			speed = speed4;
		} else if (GM.currentSpeed == 5f) {
			speed = speed5;
		} else if (GM.currentSpeed == 6f) {
			speed = speed6;
		} else if (GM.currentSpeed == 7f) {
			speed = speed7;
		} else {
			speed = speed4;
		}
		rb.velocity=new Vector2(0,-speed);
		LineHandler.transform.position= new Vector3(-0.5032098f, -3.351392f+offset, 0.7f);
	}
		

	IEnumerator LoadChart (FileInfo chartFile) 
	{

		yield return new WaitForSeconds (0.0f);
		//1
		if (chartFile.Name.Contains("meta"))
		{
			yield break;
		}
		//2
		else if(chartFile.Name=="chart " + gameObject.name +".json")
		{
			string chartFileWithoutExtension = Path.GetFileNameWithoutExtension(chartFile.ToString());
			string[] nameNameData = chartFileWithoutExtension.Split(" "[0]);
			//3
			string tempoSongName = "";
			int i = 0;
			foreach (string stringFromFileName in nameNameData)
			{
				if (i != 0)
				{
					tempoSongName = tempoSongName + stringFromFileName + " ";
				}
				i++;
			}
			//4
			string wwwnameFilePath = "file://" + chartFile.FullName.ToString();
			string nameFilePath = chartFile.FullName.ToString ();
			WWW www = new WWW(wwwnameFilePath);
			yield return www;
			//5
			ndresult= File.ReadAllText(nameFilePath);
			//MyFile =chartFile.Name;
			print (ndresult);



		}
	}

	public bool desfinished(){
		if(counter==0){ return false; }
		else { return true; }
	}

	public bool readytoplaymusic(){
		if(counterr==0){ return false; }
		else { return true; }
	}
	public bool fadein(){
		if(counterr==0){ return false; }
		else { return true; }
	}



	// Update is called once per frame
	void Update () {
		
	}

}
