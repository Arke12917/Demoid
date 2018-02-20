using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {
    [HideInInspector]
	public bool start = false;
    [HideInInspector]
    public float fadeDamp = 0.0f;
    [HideInInspector]
    public string fadeScene;
    [HideInInspector]
    public float alpha = 0.0f;
    [HideInInspector]
    public Color fadeColor;
    [HideInInspector]
    public bool isFadeIn = false;
	public bool alreadyF=false;
	public static int CurrentScene=0; 
	public static int NextScene=0;

    //Set callback
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    //Remove callback
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    //Create a texture , Color it, Paint It , then Fade Away
    void OnGUI () {
        //Fallback check
        if (!start)
			return;
        //Assign the color with variable alpha
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        //Temp Texture
		Texture2D myTex;
		myTex = new Texture2D (1, 1);
		myTex.SetPixel (0, 0, fadeColor);
		myTex.Apply ();
        //Print Texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), myTex);
        //Fade in and out control
		if (isFadeIn) {
			if(fadeScene=="Select Song"){
				Time.timeScale = 1;
				alpha = Mathf.Lerp (alpha, -0.1f, fadeDamp * Time.unscaledDeltaTime);
			}
			alpha = Mathf.Lerp (alpha, -0.1f, fadeDamp * Time.unscaledDeltaTime);
		} else {
			alpha = Mathf.Lerp (alpha, 1.1f, fadeDamp * Time.unscaledDeltaTime);
		}
        //Load scene
		if (alpha >= 1 && !isFadeIn) {
			if (CurrentScene==1) {
				if (alreadyF) {
					SceneManager.LoadScene (fadeScene);
					DontDestroyOnLoad (gameObject);
					alreadyF = false;
				}
			} else {
				SceneManager.LoadScene (fadeScene);
				DontDestroyOnLoad (gameObject);

			}
		} else if (alpha <= 0 && isFadeIn) {
			Destroy(gameObject);		
		}

	}

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //We can now fade in
		StartCoroutine(Fadein());
	
    }
	public IEnumerator Fadein(){
		if (NextScene==1) {
			print("waiting...");
			yield return new WaitUntil (GameObject.FindGameObjectWithTag ("mainchrt").GetComponent<ExampleLoadingScript> ().fadein);
			isFadeIn = true;
			alreadyF = false;
		} else{
			isFadeIn = true;
			alreadyF = false;
		}
	}

}
