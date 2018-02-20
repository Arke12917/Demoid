using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backto : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void back(){
GM.totalCombo = 0;//done
GM.highestcombo = 0;//done
GM.highestcharmingcount = 0;//done
		GameObject.FindGameObjectWithTag("songcharming").GetComponent<Text>().text="0";
		GameObject.FindGameObjectWithTag ("songcombo").GetComponent<Text> ().text = "0";
		Destroy(GameObject.FindGameObjectWithTag ("Scoreobject"));
		Application.LoadLevel (1);


	}
	public void reload(){
		GM.totalCombo = 0;//done
		GM.highestcombo = 0;//done
		GM.highestcharmingcount = 0;//done
		GameObject.FindGameObjectWithTag("songcharming").GetComponent<Text>().text="0";
		GameObject.FindGameObjectWithTag ("songcombo").GetComponent<Text> ().text = "0";
		Application.LoadLevel (2);
	}
}
