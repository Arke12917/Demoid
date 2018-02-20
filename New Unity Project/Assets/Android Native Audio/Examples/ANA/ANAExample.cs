using System.Collections.Generic;
using UnityEngine;

public class ANAExample : MonoBehaviour
{
	// Each piece of audio needs two variables, file ID and sound ID
	int FileID;
	int SoundID;

	public GUISkin GUISkin;
	AudioSource UnityAudio;
	Dictionary<GUIRects, Rect> GUIRect = new Dictionary<GUIRects, Rect>();
	

	void Start()
	{
		// Set up Android Native Audio
		AndroidNativeAudio.makePool();
		FileID = AndroidNativeAudio.load("Android Native Audio/Tone Native.wav");

		// Set up Unity audio for comparison
		UnityAudio = GetComponent<AudioSource>();

		// Set up GUI
		SetupGUI();
	}


	void OnGUI()
	{
		GUI.skin = GUISkin;

		GUI.Label(GUIRect[GUIRects.NativeLabel], "Native Audio");
		if (GUI.Button(GUIRect[GUIRects.NativePlayButton], "Play"))
		{
			// Play native audio
			SoundID = AndroidNativeAudio.play(FileID);
		}

		GUI.Label(GUIRect[GUIRects.UnityLabel], "Unity Audio");
		if (GUI.Button(GUIRect[GUIRects.UnityPlayButton], "Play"))
		{
			// Play Unity audio for comparison
			UnityAudio.Play();
		}
	}


	void OnApplicationQuit()
	{
		// Clean up when done
		AndroidNativeAudio.unload(FileID);
		AndroidNativeAudio.releasePool();
	}


	void ModifySound()
	{
		// These aren't necessary, but show how you could work with a loaded sound.

		AndroidNativeAudio.pause(SoundID);
		AndroidNativeAudio.resume(SoundID);
		AndroidNativeAudio.stop(SoundID);

		AndroidNativeAudio.pauseAll();
		AndroidNativeAudio.resumeAll();

		AndroidNativeAudio.setVolume(SoundID, 0.5f);
		AndroidNativeAudio.setLoop(SoundID, 3);
		AndroidNativeAudio.setPriority(SoundID, 5);
		AndroidNativeAudio.setRate(SoundID, 0.75f);
	}


	enum GUIRects
	{
		NativeLabel,
		NativePlayButton,
		UnityLabel,
		UnityPlayButton
	}


	void SetupGUI()
	{
		GUIRect.Add(GUIRects.NativeLabel, new Rect(
			Screen.width * 0.3f,
			Screen.height * 0.2f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.NativePlayButton, new Rect(
			Screen.width * 0.4f,
			Screen.height * 0.3f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.UnityLabel, new Rect(
			Screen.width * 0.3f,
			Screen.height * 0.5f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.UnityPlayButton, new Rect(
			Screen.width * 0.4f,
			Screen.height * 0.6f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));
	}
}
