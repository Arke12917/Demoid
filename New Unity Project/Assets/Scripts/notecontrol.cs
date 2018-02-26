using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notecontrol : MonoBehaviour {

	public Transform sucessBurst;
	public Transform failBurst;
	public float totalScore = 0.00f;
	public static string AllCharming = "AC";
	public static string FullCombo = "FC";
	public float finalscore;
	public static float ingamescore;
	public static float charmingint = 0;
	public static float normint = 0;
	public bool canclick=false;
	public float startTime=3f;

	// Use this for initialization
	void Awake () {		
	}

	public IEnumerator checkclick(){
		yield return new WaitForSeconds (0.0f);
		canclick = true;
	}
	
	// Update is called once per frame
	void Update () {
		finalscore = totalScore;
		ingamescore = totalScore;
	}



}
