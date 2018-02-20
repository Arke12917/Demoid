/*
 *  Android Native Audio Music
 *
 *  Copyright 2016 Christopher Stanley
 *
 *  Documentation: "Android Native Audio Music.pdf"
 *
 *  Support: support@ChristopherCreates.com
 */


using ChristopherCreates.AndroidNativeAudio;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to the Android native MediaPlayer class for playing low-latency music tracks.
/// </summary>
public static class ANAMusic
{
	const string _logPrefix = "ANA Music: ";

#if UNITY_ANDROID && !UNITY_EDITOR

	// Set DEBUG to "true" to enable activity logging
	static bool DEBUG = false;

	static bool _hasOBB;
	static AndroidJavaObject _assets;
	static int _streamMusic = new AndroidJavaClass("android.media.AudioManager").GetStatic<int>("STREAM_MUSIC");
	static Dictionary<int, AndroidMediaPlayer> _mediaPlayers = new Dictionary<int, AndroidMediaPlayer>();
	static GameObject _applicationPauseMonitor;


	static ANAMusic()
	{
		var context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		
		if (Application.streamingAssetsPath.Substring(Application.streamingAssetsPath.Length - 12) == ".obb!/assets")
		{
			_hasOBB = true;
			int versionCode = context.Call<AndroidJavaObject>("getPackageManager").Call<AndroidJavaObject>("getPackageInfo", context.Call<string>("getPackageName"), 0).Get<int>("versionCode");
			_assets = new AndroidJavaClass("com.android.vending.expansion.zipfile.APKExpansionSupport").CallStatic<AndroidJavaObject>("getAPKExpansionZipFile", context, versionCode, 0);
		}
		else
		{
			_hasOBB = false;
			_assets = context.Call<AndroidJavaObject>("getAssets");
		}
	}


	public static int getCurrentPosition(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "getCurrentPosition(" + musicID + ")");

		CheckMusicID(musicID);
		return _mediaPlayers[musicID].AndroidJavaObject.Call<int>("getCurrentPosition");
	}


	public static int getDuration(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "getDuration(" + musicID + ")");

		CheckMusicID(musicID);
		return _mediaPlayers[musicID].AndroidJavaObject.Call<int>("getDuration");
	}


	public static bool isLooping(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "isLooping(" + musicID + ")");

		CheckMusicID(musicID);
		return _mediaPlayers[musicID].AndroidJavaObject.Call<bool>("isLooping");
	}


	public static bool isPlaying(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "isPlaying(" + musicID + ")");

		CheckMusicID(musicID);
		return _mediaPlayers[musicID].AndroidJavaObject.Call<bool>("isPlaying");
	}


	public static int load(string audioFile, bool usePersistentDataPath = false, bool loadAsync = false, Action<int> loadedCallback = null, bool playInBackground = false)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "load(\"" + audioFile + "\", " + usePersistentDataPath + ", " + loadAsync + ", " + loadedCallback + ", " + playInBackground + ")");

		var mediaPlayer = new AndroidJavaObject("android.media.MediaPlayer");

		mediaPlayer.Call("setAudioStreamType", _streamMusic);

		if (usePersistentDataPath)
			mediaPlayer.Call("setDataSource", Path.Combine(Application.persistentDataPath, audioFile));
		else
		{
			AndroidJavaObject assetFileDescriptor;
			if (_hasOBB)
				assetFileDescriptor = _assets.Call<AndroidJavaObject>("getAssetFileDescriptor", Path.Combine("assets", audioFile));
			else
				assetFileDescriptor = _assets.Call<AndroidJavaObject>("openFd", audioFile);
			var fileDescriptor = assetFileDescriptor.Call<AndroidJavaObject>("getFileDescriptor");
			var startOffset = assetFileDescriptor.Call<long>("getStartOffset");
			var length = assetFileDescriptor.Call<long>("getLength");
			mediaPlayer.Call("setDataSource", fileDescriptor, startOffset, length);
		}

		if (loadedCallback != null)
			mediaPlayer.Call("setOnPreparedListener", new OnPreparedListener(loadedCallback));

		if (loadAsync)
			mediaPlayer.Call("prepareAsync");
		else
			mediaPlayer.Call("prepare");

		var id = mediaPlayer.Call<int>("getAudioSessionId");
		_mediaPlayers.Add(id, new AndroidMediaPlayer(mediaPlayer, id, playInBackground));
		CheckPauseMonitor(playInBackground);
		return id;
	}


	public static void OnApplicationPause(bool isPaused)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "OnApplicationPause(" + isPaused + ")");

		if (isPaused)
			BackgroundPause();
		else
			BackgroundResume();
	}


	public static void pause(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "pause(" + musicID + ")");

		CheckMusicID(musicID);
		_mediaPlayers[musicID].AndroidJavaObject.Call("pause");
	}


	public static void pauseAll()
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "pauseAll()");

		foreach (var mediaPlayer in _mediaPlayers)
			if (mediaPlayer.Value != null && isPlaying(mediaPlayer.Value.ID))
			{
				pause(mediaPlayer.Value.ID);
				mediaPlayer.Value.IsAllPaused = true;
			}
	}


	public static void play(int musicID, Action<int> completionCallback = null)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "play(" + musicID + ", " + completionCallback + ")");

		CheckMusicID(musicID);
		var mediaPlayer = _mediaPlayers[musicID].AndroidJavaObject;

		if (completionCallback != null)
			mediaPlayer.Call("setOnCompletionListener", new OnCompletionListener(completionCallback));

		mediaPlayer.Call("start");
	}


	public static void release(int musicID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "release(" + musicID + ")");

		CheckMusicID(musicID);
		_mediaPlayers[musicID].AndroidJavaObject.Call("release");
		_mediaPlayers[musicID].AndroidJavaObject.Dispose();
		_mediaPlayers.Remove(musicID);
	}


	public static void resumeAll()
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "resumeAll()");

		foreach (var mediaPlayer in _mediaPlayers)
			if (mediaPlayer.Value != null && mediaPlayer.Value.IsAllPaused)
			{
				play(mediaPlayer.Value.ID);
				mediaPlayer.Value.IsAllPaused = false;
			}
	}


	public static void seekTo(int musicID, int msec)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "seekTo(" + musicID + ", " + msec + ")");

		CheckMusicID(musicID);
		_mediaPlayers[musicID].AndroidJavaObject.Call("seekTo", msec);
	}


	public static void setLooping(int musicID, bool looping)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setLooping(" + musicID + ", " + looping + ")");

		CheckMusicID(musicID);
		_mediaPlayers[musicID].AndroidJavaObject.Call("setLooping", looping);
	}


	public static void setPlayInBackground(int musicID, bool playInBackground)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setPlayInBackground(" + musicID + ", " + playInBackground + ")");

		_mediaPlayers[musicID].PlayInBackground = playInBackground;
		CheckPauseMonitor(playInBackground);
	}


	public static void setVolume(int musicID, float leftVolume, float rightVolume = -1)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setVolume(" + musicID + ", " + leftVolume + ", " + rightVolume + ")");

		if (rightVolume == -1)
			rightVolume = leftVolume;

		CheckMusicID(musicID);
		_mediaPlayers[musicID].AndroidJavaObject.Call("setVolume", leftVolume, rightVolume);
	}


	static void BackgroundPause()
	{
		foreach (var mediaPlayer in _mediaPlayers)
		{
			if (mediaPlayer.Value.PlayInBackground)
				continue;
			if (isPlaying(mediaPlayer.Value.ID))
			{
				mediaPlayer.Value.WasPlaying = true;
				pause(mediaPlayer.Value.ID);
			}
			else
				mediaPlayer.Value.WasPlaying = false;
		}
	}


	static void BackgroundResume()
	{
		foreach (var mediaPlayer in _mediaPlayers)
			if (!mediaPlayer.Value.PlayInBackground && mediaPlayer.Value.WasPlaying)
				play(mediaPlayer.Value.ID);
	}


	static void CheckMusicID(int musicID)
	{
		if (!_mediaPlayers.ContainsKey(musicID))
			throw new InvalidOperationException(_logPrefix + "Music ID not found.");
	}


	static void CheckPauseMonitor(bool playInBackground)
	{
		if (!playInBackground && _applicationPauseMonitor == null)
		{
			_applicationPauseMonitor = new GameObject("ANA Music Application Pause Monitor");
			_applicationPauseMonitor.AddComponent<ANAMusicBackgroundPause>();
		}
	}


#else

	/// <summary>
	/// Gets the current playback position.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <returns>The current position in milliseconds.</returns>
	public static int getCurrentPosition(int musicID)
	{
		Debug.Log(_logPrefix + "getCurrentPosition(" + musicID + ")");
		return 1;
	}


	/// <summary>
	/// Gets the duration of the file.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <returns>The duration in milliseconds.</returns>
	public static int getDuration(float musicID)
	{
		Debug.Log(_logPrefix + "getDuration(" + musicID + ")");
		return 1;
	}


	/// <summary>
	/// Checks whether the MediaPlayer is looping or non-looping.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <returns>True if the MediaPlayer is currently looping, false otherwise.</returns>
	public static bool isLooping(int musicID)
	{
		Debug.Log(_logPrefix + "isLooping(" + musicID + ")");
		return false;
	}


	/// <summary>
	/// Checks whether the MediaPlayer is playing.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <returns>True if currently playing, false otherwise.</returns>
	public static bool isPlaying(int musicID)
	{
		Debug.Log(_logPrefix + "isPlaying(" + musicID + ")");
		return false;
	}


	/// <summary>
	/// Loads a music file into a native Android media player.
	/// </summary>
	/// <param name="audioFile">The path to the music file, relative to Assets\StreamingAssets.</param>
	/// <param name="usePersistentDataPath">If true, audioFile is relative to Application.persistentDataPath.  If false, it is relative to Assets\StreamingAssets.</param>
	/// <param name="loadAsync">If true, the file is loaded asynchronously and the method immediately returns.  If false, the method will not return until the load has completed.</param>
	/// <param name="loadedCallback">If given, the method to call when the load is complete.  Must take one int parameter which is the loaded music ID.</param>
	/// <param name="playInBackground">If true, the music will continue playing when the game is not active.  If false, the music will be paused when the game is not active and resumed when it becomes active again.</param>
	/// <returns>The ID of the loaded music.</returns>
	public static int load(string audioFile, bool usePersistentDataPath = false, bool loadAsync = false, Action<int> loadedCallback = null, bool playInBackground = false)
	{
		Debug.Log(_logPrefix + "load(\"" + audioFile + "\", " + usePersistentDataPath + ", " + loadAsync + ", " + loadedCallback + ", " + playInBackground + ")");
		return 1;
	}


	/// <summary>
	/// Used for automatically handling music when the game is paused or resumed.  You shouldn't need to use this manually.
	/// </summary>
	/// <param name="isPaused">Whether or not the application is currently paused.</param>
	public static void OnApplicationPause(bool isPaused)
	{
		Debug.Log(_logPrefix + "OnApplicationPause(" + isPaused + ")");
	}


	/// <summary>
	/// Pauses playback. Call play() to resume.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	public static void pause(int musicID)
	{
		Debug.Log(_logPrefix + "pause(" + musicID + ")");
	}


	/// <summary>
	/// Pauses all currently playing music.  Use resumeAll() to resume only music that was paused with pauseAll().
	/// </summary>
	public static void pauseAll()
	{
		Debug.Log(_logPrefix + "pauseAll()");
	}


	/// <summary>
	/// Starts or resumes playback.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <param name="completionCallback">If given, the method to call when playback is complete.</param>
	public static void play(int musicID, Action<int> completionCallback = null)
	{
		Debug.Log(_logPrefix + "play(" + musicID + ", " + completionCallback + ")");
	}


	/// <summary>
	/// Releases resources associated with this MediaPlayer object.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	public static void release(int musicID)
	{
		Debug.Log(_logPrefix + "release(" + musicID + ")");
	}


	/// <summary>
	/// Resumes play of all music paused with pauseAll().
	/// </summary>
	public static void resumeAll()
	{
		Debug.Log(_logPrefix + "resumeAll()");
	}


	/// <summary>
	/// Seeks to specified time position.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <param name="msec">the offset in milliseconds from the start to seek to</param>
	public static void seekTo(int musicID, int msec)
	{
		Debug.Log(_logPrefix + "seekTo(" + musicID + ", " + msec + ")");
	}


	/// <summary>
	/// Sets the player to be looping or non-looping.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <param name="looping">whether to loop or not</param>
	public static void setLooping(int musicID, bool looping)
	{
		Debug.Log(_logPrefix + "setLooping(" + musicID + ", " + looping + ")");
	}


	/// <summary>
	/// Sets whether or not the music will play when the game is inactive.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <param name="playInBackground">If true, the music will continue playing when the game is not active.  If false, the music will be paused when the game is not active and resumed when it becomes active again.</param>
	public static void setPlayInBackground(int musicID, bool playInBackground)
	{
		Debug.Log(_logPrefix + "setPlayInBackground(" + musicID + ", " + playInBackground + ")");
	}


	/// <summary>
	/// Sets the volume on this player.
	/// </summary>
	/// <param name="musicID">The ID of the music to use.</param>
	/// <param name="leftVolume">left volume scalar (0.0 to 1.0) If rightVolume is omitted, this value will be used for both.</param>
	/// <param name="rightVolume">right volume scalar (0.0 to 1.0) Defaults to leftVolume.</param>
	public static void setVolume(int musicID, float leftVolume, float rightVolume = -1)
	{
		Debug.Log(_logPrefix + "setVolume(" + musicID + ", " + leftVolume + ", " + rightVolume + ")");
	}

#endif

}
