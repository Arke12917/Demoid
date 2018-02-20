using System.Collections.Generic;
using UnityEngine;

public class ANAMusicExample : MonoBehaviour
{
	// Each piece of music needs a music ID
	int MusicID;

	public GUISkin GUISkin;
	GUIStyle StatusStyle;
	bool IsLoaded;
	string PlayPauseButton = "Play";
	string IsPlayingString = "Is Playing: False";
	string LoopUnloopButton = "Loop";
	string IsLoopingString = "Is Looping: False";
	bool IsMute = false;
	string VolumeMuteButton = "Mute";
	string VolumeString = "Volume: 1.0";
	bool IsPlayInBackground = false;
	string PlayInBackgroundString = "Play In Background: False";
	string DurationString;
	AudioSource UnityAudio;
	Dictionary<GUIRects, Rect> GUIRect = new Dictionary<GUIRects, Rect>();
	

	void Start()
	{
		// Load the native music
		Load();

		// Set up Unity audio for comparison
		UnityAudio = GetComponent<AudioSource>();

		// Set up GUI
		SetupGUI();
	}


	void Load()
	{
		// Load the music with callback
		MusicID = ANAMusic.load("Android Native Audio/Music Native.ogg", false, true, Loaded);
	}


	void Loaded(int musicID)
	{
		// Get music duration
		DurationString = "Duration: " + ANAMusic.getDuration(musicID);

		IsLoaded = true;
		PlayPauseButton = "Play";
		IsPlayingString = "Is Playing: False";
		LoopUnloopButton = "Loop";
		IsLoopingString = "Is Looping: False";
		IsMute = false;
		VolumeMuteButton = "Mute";
		VolumeString = "Volume: 1.0";
		IsPlayInBackground = false;
		PlayInBackgroundString = "Play In Background: False";
	}


	void PlayPause()
	{
		// Check playing state
		if (ANAMusic.isPlaying(MusicID))
		{
			// Pause
			ANAMusic.pause(MusicID);
			StoppedStrings(MusicID);
		}
		else
		{
			// Play with completion callback
			ANAMusic.play(MusicID, StoppedStrings);
			PlayPauseButton = "Pause";
			IsPlayingString = "Is Playing: True";
		}
	}


	void Stop()
	{
		// To "stop", pause and seek to beginning
		ANAMusic.pause(MusicID);
		ANAMusic.seekTo(MusicID, 0);
		StoppedStrings(MusicID);
	}


	string GetPositionLabel()
	{
		// Get current position
		return "Position: " + ANAMusic.getCurrentPosition(MusicID);
	}


	void Seek()
	{
		// Seek to near end of music
		ANAMusic.seekTo(MusicID, 73070);
	}


	void LoopUnloop()
	{
		// Check looping state
		if (ANAMusic.isLooping(MusicID))
		{
			// Unloop
			ANAMusic.setLooping(MusicID, false);
			LoopUnloopButton = "Loop";
			IsLoopingString = "Is Looping: False";
		}
		else
		{
			// Loop
			ANAMusic.setLooping(MusicID, true);
			LoopUnloopButton = "Unloop";
			IsLoopingString = "Is Looping: True";
		}
	}


	void VolumeMute()
	{
		if (IsMute)
		{
			// Restore volume
			ANAMusic.setVolume(MusicID, 1.0f);
			VolumeMuteButton = "Mute";
			VolumeString = "Volume: 1.0";
			IsMute = false;
		}
		else
		{
			// Mute volume
			ANAMusic.setVolume(MusicID, 0.0f);
			VolumeMuteButton = "Volume";
			VolumeString = "Volume: 0.0";
			IsMute = true;
		}
	}


	void PlayInBackground()
	{
		if (IsPlayInBackground)
		{
			// Disable play in background
			ANAMusic.setPlayInBackground(MusicID, false);
			PlayInBackgroundString = "Play In Background: False";
			IsPlayInBackground = false;
		}
		else
		{
			// Enable play in background
			ANAMusic.setPlayInBackground(MusicID, true);
			PlayInBackgroundString = "Play In Background: True";
			IsPlayInBackground = true;
		}
	}


	void Release()
	{
		// Release music resources
		ANAMusic.release(MusicID);

		IsLoaded = false;
	}


	void OnApplicationQuit()
	{
		// Clean up when done
		ANAMusic.release(MusicID);
	}


	// Everything below here is GUI stuff, everything ANAMusic is above


	void StoppedStrings(int musicID)
	{
		PlayPauseButton = "Play";
		IsPlayingString = "Is Playing: False";
	}


	void OnGUI()
	{
		GUI.skin = GUISkin;
		StatusStyle = GUI.skin.GetStyle("status");
		GUI.Label(GUIRect[GUIRects.NativeLabel], "Native Audio");
		if (IsLoaded)
		{
			if (GUI.Button(GUIRect[GUIRects.PlayPauseButton], PlayPauseButton))
				PlayPause();
			GUI.Label(GUIRect[GUIRects.IsPlayingLabel], IsPlayingString, StatusStyle);
			if (GUI.Button(GUIRect[GUIRects.NativeStopButton], "Stop"))
				Stop();
			GUI.Label(GUIRect[GUIRects.PositionLabel], GetPositionLabel(), StatusStyle);
			if (GUI.Button(GUIRect[GUIRects.SeekButton], "Seek"))
				Seek();
			if (GUI.Button(GUIRect[GUIRects.LoopUnloopButton], LoopUnloopButton))
				LoopUnloop();
			GUI.Label(GUIRect[GUIRects.IsLoopingLabel], IsLoopingString, StatusStyle);
			if (GUI.Button(GUIRect[GUIRects.VolumeMuteButton], VolumeMuteButton))
				VolumeMute();
			GUI.Label(GUIRect[GUIRects.VolumeLabel], VolumeString, StatusStyle);
			if (GUI.Button(GUIRect[GUIRects.PlayInBackgroundButton], "PIBG"))
				PlayInBackground();
			GUI.Label(GUIRect[GUIRects.PlayInBackgroundLabel], PlayInBackgroundString, StatusStyle);
			if (GUI.Button(GUIRect[GUIRects.ReleaseLoadButton], "Release"))
				Release();
			GUI.Label(GUIRect[GUIRects.DurationLabel], DurationString, StatusStyle);
		}
		else
			if (GUI.Button(GUIRect[GUIRects.ReleaseLoadButton], "Load"))
				Load();
		GUI.Label(GUIRect[GUIRects.UnityLabel], "Unity Audio");
		if (GUI.Button(GUIRect[GUIRects.UnityPlayButton], "Play"))
			UnityAudio.Play();
		if (GUI.Button(GUIRect[GUIRects.UnityStopButton], "Stop"))
			UnityAudio.Stop();
	}


	enum GUIRects
	{
		NativeLabel,
		PlayPauseButton,
		IsPlayingLabel,
		NativeStopButton,
		PositionLabel,
		SeekButton,
		LoopUnloopButton,
		IsLoopingLabel,
		VolumeMuteButton,
		VolumeLabel,
		PlayInBackgroundButton,
		PlayInBackgroundLabel,
		ReleaseLoadButton,
		DurationLabel,
		UnityLabel,
		UnityPlayButton,
		UnityStopButton
	}


	void SetupGUI()
	{
		GUIRect.Add(GUIRects.NativeLabel, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.0f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.PlayPauseButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.1f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.IsPlayingLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.15f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.NativeStopButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.2f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.PositionLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.25f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.SeekButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.3f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.LoopUnloopButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.4f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.IsLoopingLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.4f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.VolumeMuteButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.5f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.VolumeLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.5f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.PlayInBackgroundButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.6f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.PlayInBackgroundLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.6f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.ReleaseLoadButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.7f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.DurationLabel, new Rect(
			Screen.width * 0.5f,
			Screen.height * 0.7f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.UnityLabel, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.8f,
			Screen.width * 0.5f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.UnityPlayButton, new Rect(
			Screen.width * 0.2f,
			Screen.height * 0.9f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));

		GUIRect.Add(GUIRects.UnityStopButton, new Rect(
			Screen.width * 0.4f,
			Screen.height * 0.9f,
			Screen.width * 0.2f,
			Screen.height * 0.1f));
	}
}
