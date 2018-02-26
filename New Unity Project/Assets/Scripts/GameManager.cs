/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
namespace UnityEngine.UI.Extensions{
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class GameManager : MonoBehaviour
{

    [Header("Arena Objects")]
	public Image BG;
	public Text SongName;
	public Text ActualName;
	public GameObject m_Prefab;
	public GameObject z_Prefab;
	public GameObject parentGameObject;
	public GameObject newelement;
	public Transform anothertransform;
	public Transform aAnothertransform;

    [Header("Game UI")]
    public GameObject loadingScreen;
    public GameObject pauseMenuCamera;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public string arenaName = "Starter Level";

    public GameObject PlayerUI;
	public AudioSource musicplayer;
	public static songnamechange s0ng;
	public static int ccount = -1;
	public static int rcount = -1;


    [Space]
    public int currentLevel = 1;
    private bool isPaused = false;

    public Text timerText;
    private float timer;
    public string formattedTime;
	private string log;
		private int zres=0;
		private int[] progress = new int[1];

		//individual file progress (in bytes)
		private int[] progress2 = new int[1];
		void plog(string t)
		{
			log += t + "\n"; ;
		}
	private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}

    void Awake()
    {
			if (Directory.Exists (Application.streamingAssetsPath + "/Summer Night")) {
				print ("SN is here!");
				if (Directory.Exists (Application.persistentDataPath + "/Summer Night")) {
					#if UNITY_EDITOR
					//Directory.Delete(Application.persistentDataPath + "/Summer Night");
					#endif
					if (Application.platform == RuntimePlatform.IPhonePlayer) {
						//System.IO.Directory.Delete("/private" + Application.persistentDataPath+"/"+"Summer Night");
						//File.Delete ("/private"+file.ToString());
					}

				}
				#if UNITY_EDITOR
				DirectoryInfo directoryII = new DirectoryInfo (Application.streamingAssetsPath);
				print ("Streaming Assets Path: " + Application.streamingAssetsPath);
				DirectoryInfo[] Filee = directoryII.GetDirectories ("*");
				foreach (DirectoryInfo directory in Filee) {
					var toPath = directory + "/" + Path.GetFileName ("DEMOID.txt");
					print (toPath);
					if (File.Exists (toPath)) {
						print ("SN 1 here!");
						print (directory.FullName);
						if (Directory.Exists (Application.persistentDataPath + "/" + "Summer Night")) {
						} else {
							DirectoryCopy (directory.FullName, Application.persistentDataPath + "/" + "Summer Night", true);
						}

					}
					//System.IO.Directory.Move("/private"+Application.streamingAssetsPath+"/"+"Summer Night/","/private"+Application.persistentDataPath+"/Summer Night/");
					//File.Delete ("/private"+file.ToString());
				}
			
				#endif
			}
				if (Application.platform == RuntimePlatform.IPhonePlayer) {
					print ("/private" + Application.streamingAssetsPath + "/" + "Summer Night/");
					print ("/private" + Application.persistentDataPath + "/Summer Night/");
					DirectoryInfo directoryI = new DirectoryInfo (Application.streamingAssetsPath);
					print ("Streaming Assets Path: " + Application.streamingAssetsPath);
					DirectoryInfo[] Filess = directoryI.GetDirectories ("*");
					foreach (DirectoryInfo directory in Filess) {
						var toPath = directory + "/" + Path.GetFileName ("DEMOID.txt");
						print (toPath);
						if (File.Exists (toPath)) {
							print ("SN 1 here!");
							print (directory.FullName);
							if (Directory.Exists (Application.persistentDataPath + "/" + "Summer Night")) {
							} else {
								DirectoryCopy (directory.FullName, Application.persistentDataPath + "/" + "Summer Night", true);
							}

						}
						//System.IO.Directory.Move("/private"+Application.streamingAssetsPath+"/"+"Summer Night/","/private"+Application.persistentDataPath+"/Summer Night/");
						//File.Delete ("/private"+file.ToString());
					}
			
				} else if (Application.platform == RuntimePlatform.Android) {
					string ppath = Application.persistentDataPath;
					FileManager.RereadFile ("Summer Night.zip");
				DirectoryInfo directoryInfo = new DirectoryInfo (Application.persistentDataPath);
				print ("Streaming Assets Path: " + Application.persistentDataPath);
					FileInfo[] allFiles = directoryInfo.GetFiles ("*.zip");
					foreach (FileInfo file in allFiles) {
				var fileBuffer = File.ReadAllBytes (Application.persistentDataPath + "/" + file.Name);

						plog ("Validate: " + lzip.validateFile (null, fileBuffer).ToString ());


						//decompress the downloaded file
						zres = lzip.decompress_File (null, ppath + "/", progress, fileBuffer, progress2);
						plog ("decompress: " + zres.ToString ());

						//yield return new WaitForSecondsRealtime (1.0f);
						if (Application.platform == RuntimePlatform.Android) {
							//System.IO.File.Delete(Application.persistentDataPath + "/" + file.Name);
						}

					}
				}

			
			DirectoryInfo directoryInfoo = new DirectoryInfo(Application.persistentDataPath);
			print("Streaming Assets Path: " + Application.persistentDataPath);
			DirectoryInfo[] allFiless = directoryInfoo.GetDirectories("*");
			foreach (DirectoryInfo directory in allFiless) {
				var toPath =  directory + "/" + Path.GetFileName ("DEMOID.txt");
				print (toPath);
				if (File.Exists (toPath)) {
					print ("YEAAAAAHHHDHSDHASHDSAHDAHDSAHD");
				}
					DirectoryInfo directoryInfo = new DirectoryInfo(directory+"/");
					print("Streaming Assets Path: " + directoryInfo);
					FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
					foreach (FileInfo file in allFiles) {
						if (file.Name.Contains ("song")) {
							StartCoroutine ("loadnames", file);
						} else if (file.Name.Contains ("bg")) {
							StartCoroutine ("LoadPlayerUI", file);
						} else if (file.Name.Contains ("soundtrack")) {
							StartCoroutine ("LoadBackgroundMusic", file);
						} else if (file.Name.Contains ("diff")) {
							StartCoroutine ("loaddifficulty", file);
						}
					}
				
			}
	
        loadingScreen.SetActive(true);
        Time.timeScale = 0.0f;
        StartCoroutine("RemoveLoadingScreen");
		newelement=GameObject.Find ("Element 0");
		print("I found it"+gameObject.name);
		title.readytoload = true;
    }

	IEnumerator loadnames(FileInfo nameFile)
	{
		yield return new WaitForSeconds (0.0f);
		//1
		if (nameFile.Name.Contains("meta"))
		{
			yield break;
		}
		//2
		else
		{
			string nameFileWithoutExtension = Path.GetFileNameWithoutExtension(nameFile.ToString());
			string[] nameNameData = nameFileWithoutExtension.Split(" "[0]);
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
			string wwwnameFilePath = "file://" + nameFile.FullName.ToString();
			WWW www = new WWW(wwwnameFilePath);
			yield return www;
			//5
			m_Prefab.name = tempoSongName;

			ccount += 1;
			GameObject go = (Instantiate (m_Prefab, transform.position, Quaternion.identity) as GameObject);
			//go.transform.parent = GameObject.Find("Content").transform;
			go.transform.SetParent(anothertransform, false);
			go.name = m_Prefab.name;
	}						
}
		IEnumerator loaddifficulty(FileInfo diffFile)
		{
			yield return new WaitForSeconds (0.0f);
			//1
			if (diffFile.Name.Contains("meta"))
			{
				yield break;
			}
			//2
			else
			{
				string diffFileWithoutExtension = Path.GetFileNameWithoutExtension(diffFile.ToString());
				string[] diffNameData = diffFileWithoutExtension.Split(" "[0]);
				//3
				string tempooSongName = "";
				int i = 0;
				foreach (string stringFromFileName in diffNameData)
				{
					if (i != 0)
					{
						tempooSongName = tempooSongName + stringFromFileName + " ";
					}
					i++;
				}
				//4
				string wwwdiffFilePath = "file://" + diffFile.FullName.ToString();
				WWW www = new WWW(wwwdiffFilePath);
				yield return www;
				//5
				rcount += 1;
				z_Prefab.name = tempooSongName;
				string diffFilePath = diffFile.FullName.ToString ();
				GameObject goo = (Instantiate (z_Prefab, transform.position, Quaternion.identity) as GameObject);
				//go.transform.parent = GameObject.Find("Content").transform;
				goo.transform.SetParent(aAnothertransform, false);
				goo.name = File.ReadAllText(diffFilePath);
				//MyFile =chartFile.Name;
				print (goo.name);
			}						
		}

    IEnumerator RemoveLoadingScreen()
    {
        yield return new WaitForSecondsRealtime(1);
        loadingScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                PlayerUI.SetActive(true);
                pauseScreen.SetActive(false);
                pauseMenuCamera.SetActive(false);
                isPaused = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                PlayerUI.SetActive(false);
                pauseScreen.SetActive(true);
                pauseMenuCamera.SetActive(true);
                isPaused = true;
                Time.timeScale = 0.0f;
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene"); 
        Time.timeScale = 1.0f;
    }


    public void StartNextLevel()
    {
        // Method to be completed via tutorial
    }

    public void ApplySkin(int indexOfSkin)
    {
        // Method to be completed via tutorial 
    }

	IEnumerator LoadPlayerUI(FileInfo playerFile)
	{
			yield return new WaitForSeconds (0.0f);
	
	}
	IEnumerator LoadBackgroundMusic (FileInfo musicFile) 
	{
		if (musicFile.Name.Contains("meta")) 
		{
			yield break;
		}
		else 
		{
			string musicFilePath = musicFile.FullName.ToString();
			string url = string.Format("file://{0}", musicFilePath);
			WWW www = new WWW(url);
			yield return www;
			musicplayer.clip = www.GetAudioClip(false, false);
			//musicplayer.Play();
		}
	}

}
}
