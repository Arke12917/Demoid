namespace UnityEngine.UI.Extensions
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class prefabat : MonoBehaviour {
	public static UpdateScrollSnap USP;
	void Start(){
			chngname ();
		CreatePrefab ();
			Debug.Log("wooooo");
	}

		void Awake(){
			DontDestroyOnLoad(transform.gameObject);
		}




	public void CreatePrefab()
	{
			GameObject[] objs = GameObject.FindGameObjectsWithTag("YEABOI");

		foreach (GameObject go in objs) {
				string localPath = "Assets/Resources/Prefabs/" + go.name + ".prefab";
				gameObject.name = GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().songtoload;
		}
	}
		

	public void CreateNew(GameObject obj, string localPath)
	{
		/*Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
		PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
			var taggedObjects = Resources.LoadAll("Prefabs", typeof(GameObject)).ToList();
			foreach (GameObject goo in taggedObjects) {
				if (goo.name != gameObject.name) {
					DestroyImmediate(goo,true);
				}
			}*/
	}
	public void chngname(){
			gameObject.name = GameObject.FindGameObjectWithTag ("USP").GetComponent<UpdateScrollSnap> ().songtoload;
	}

		public void NEXTLEVEL(){
			GameObject.FindGameObjectWithTag ("SpeedCan").GetComponent<COSCAN> ().opencanvas();
			//Application.LoadLevel (2);
		}
		public void FiX(){
			GameObject.FindGameObjectWithTag ("SpeedCan").GetComponent<COSCAN> ().fixactive();
		}
}
}
