using System.IO;
using UnityEngine;
public static class FileManager
{
	public static string RereadFile(string fileName)
	{  //copies and unpacks file from apk to persistentDataPath where it can be accessed
		string destinationPath = Path.Combine(Application.persistentDataPath, fileName);
		#if UNITY_EDITOR
		string sourcePath = Path.Combine(Application.streamingAssetsPath, fileName);
		#else
		string sourcePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
		#endif

		//UnityEngine.Debug.Log(string.Format("{0}-{1}-{2}-{3}", sourcePath,  File.GetLastWriteTimeUtc(sourcePath), File.GetLastWriteTimeUtc(destinationPath)));

		//copy whatsoever

		//if DB does not exist in persistent data folder (folder "Documents" on iOS) or source DB is newer then copy it
		//if (!File.Exists(destinationPath) || (File.GetLastWriteTimeUtc(sourcePath) > File.GetLastWriteTimeUtc(destinationPath)))
		{
			if (sourcePath.Contains("://"))
			{
				Debug.Log(sourcePath);
				// Android  
				WWW www = new WWW(sourcePath);
				while (!www.isDone) {; }                // Wait for download to complete - not pretty at all but easy hack for now 
				if (string.IsNullOrEmpty(www.error))
				{
					File.WriteAllBytes(destinationPath, www.bytes);
				}
				else
				{
					Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
				}
			}
			else
			{
				// Mac, Windows, Iphone                
				//validate the existens of the DB in the original folder (folder "streamingAssets")
				if (File.Exists(sourcePath))
				{
					//copy file - alle systems except Android
					File.Copy(sourcePath, destinationPath, true);
				}
				else
				{
					Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
				}
			}
		}

		StreamReader reader = new StreamReader(destinationPath);
		var jsonString = reader.ReadToEnd();
		reader.Close();


		return jsonString;
	}
}