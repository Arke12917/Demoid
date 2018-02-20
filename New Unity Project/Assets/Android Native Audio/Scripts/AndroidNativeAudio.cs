/*
 *  Android Native Audio
 *
 *  Copyright 2015-2016 Christopher Stanley
 *
 *  Documentation: "Android Native Audio.pdf"
 *
 *  Support: support@ChristopherCreates.com
 */


using ChristopherCreates.AndroidNativeAudio;
using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Provides access to the Android native SoundPool class for playing low-latency sound effects.
/// </summary>
public static class AndroidNativeAudio
{
	const string _logPrefix = "AndroidNativeAudio: ";

#if UNITY_ANDROID && !UNITY_EDITOR

	// Set DEBUG to "true" to enable activity logging
	static bool DEBUG = false;

	const int _loadPriority = 1;
	const int _sourceQuality = 0;

	static AndroidJavaObject _assetFileDescriptor;
	static AndroidJavaObject _assets;
	static AndroidJavaObject _soundPool = null;
	static bool _hasOBB;
	static int _streamMusic = new AndroidJavaClass("android.media.AudioManager").GetStatic<int>("STREAM_MUSIC");
	

	static AndroidNativeAudio()
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


	public static int load(string audioFile, bool usePersistentDataPath = false, Action<int> callback = null)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "load(\"" + audioFile + "\", " + usePersistentDataPath + "\", " + callback + ")");

		if (_soundPool == null)
			throw new InvalidOperationException(_logPrefix + "Use makePool() before load()!");

		if (callback != null)
			_soundPool.Call("setOnLoadCompleteListener", new OnLoadCompleteListener(callback));

		if (usePersistentDataPath)
			return _soundPool.Call<int>("load", Path.Combine(Application.persistentDataPath, audioFile), _loadPriority);

		if (_hasOBB)
			_assetFileDescriptor = _assets.Call<AndroidJavaObject>("getAssetFileDescriptor", Path.Combine("assets", audioFile));
		else
			_assetFileDescriptor = _assets.Call<AndroidJavaObject>("openFd", audioFile);

		return _soundPool.Call<int>("load", _assetFileDescriptor, _loadPriority);
	}


	public static void makePool(int maxStreams = 16)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "makePool(" + maxStreams + ")");
		
		if (_soundPool != null)
			_soundPool.Call("release");

		_soundPool = new AndroidJavaObject("android.media.SoundPool", maxStreams, _streamMusic, _sourceQuality);
	}


	public static void pause(int streamID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "pause(" + streamID + ")");
		
		_soundPool.Call("pause", streamID);
	}


	public static void pauseAll()
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "pauseAll()");
		
		_soundPool.Call("autoPause");
	}


	public static int play(int fileID, float leftVolume = 1, float rightVolume = -1, int priority = 1, int loop = 0, float rate = 1)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "play(" + fileID + ", " + leftVolume + ", " + rightVolume + ", " + priority + ", " + loop + ", " + rate + ")");

		if (rightVolume == -1)
			rightVolume = leftVolume;

		return _soundPool.Call<int>("play", fileID, leftVolume, rightVolume, priority, loop, rate);
	}


	public static void releasePool()
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "releasePool()");

		_soundPool.Call("release");
		_soundPool.Dispose();
		_soundPool = null;
	}


	public static void resume(int streamID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "resume(" + streamID + ")");
		
		_soundPool.Call("resume", streamID);
	}


	public static void resumeAll()
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "resumeAll()");
		
		_soundPool.Call("autoResume");
	}


	public static void setLoop(int streamID, int loop)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setLoop(" + streamID + ", " + loop + ")");
		
		_soundPool.Call("setLoop", streamID, loop);
	}


	public static void setPriority(int streamID, int priority)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setPriority(" + streamID + ", " + priority + ")");
		
		_soundPool.Call("setPriority", streamID, priority);
	}


	public static void setRate(int streamID, float rate)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setRate(" + streamID + ", " + rate + ")");
		
		_soundPool.Call("setRate", streamID, rate);
	}


	public static void setVolume(int streamID, float leftVolume, float rightVolume = -1)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "setVolume(" + streamID + ", " + leftVolume + ", " + rightVolume + ")");

		if (rightVolume == -1)
			rightVolume = leftVolume;

		_soundPool.Call("setVolume", streamID, leftVolume, rightVolume);
	}


	public static void stop(int streamID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "stop(" + streamID + ")");
		
		_soundPool.Call("stop", streamID);
	}


	public static bool unload(int fileID)
	{
		if (DEBUG)
			Debug.Log(_logPrefix + "unload(" + fileID + ")");

		return _soundPool.Call<bool>("unload", fileID);
	}


#else

	/// <summary>
	/// Loads an audio file into the sound pool. Call makePool() before loading.
	/// </summary>
	/// <param name="audioFile">The path to the audio file, relative to Assets\StreamingAssets. (Unless usePersistentDataPath is true.)</param>
	/// <param name="usePersistentDataPath">Makes audioFile relative to Application.persistentDataPath.</param>
	/// <param name="callback">Method to call when load is complete.  Must take one int parameter which is the loaded file ID.</param>
	/// <returns>The file ID if successful, -1 if the load fails.</returns>
	public static int load(string audioFile, bool usePersistentDataPath = false, Action<int> callback = null)
	{
		Debug.Log(_logPrefix + "load(\"" + audioFile + "\", " + usePersistentDataPath + "\", " + callback + ")");
		return 1;
	}


	/// <summary>
	/// Makes a native Android sound pool.
	/// </summary>
	/// <param name="maxStreams">The maximum number of streams. (The maximum number of simultaneously playing files.)</param>
	public static void makePool(int maxStreams = 16)
	{
		Debug.Log(_logPrefix + "makePool(" + maxStreams + ")");
	}


	/// <summary>
	/// Pauses a stream.  Call resume() to resume.
	/// </summary>
	/// <param name="streamID">The ID of the stream to pause.</param>
	public static void pause(int streamID)
	{
		Debug.Log(_logPrefix + "pause(" + streamID + ")");
	}


	/// <summary>
	/// Pauses all playing streams.  Call resumeAll() to resume.
	/// </summary>
	public static void pauseAll()
	{
		Debug.Log(_logPrefix + "pauseAll()");
	}


	/// <summary>
	/// Plays a file. Call load() before playing.
	/// </summary>
	/// <param name="fileID">The ID of the file to play.</param>
	/// <param name="leftVolume">The left volume to play at (0.0 - 1.0). If rightVolume is omitted, this value will be used for both.</param>
	/// <param name="rightVolume">The right volume to play at (0.0 - 1.0). Defaults to leftVolume.</param>
	/// <param name="priority">The priority of this stream. If the number of simultaneously playing streams exceeds maxStreams in makePool, higher priority streams will play and lower priority streams will not.</param>
	/// <param name="loop">How many times to loop the audio. A value of 0 will play once, -1 will loop until stopped.</param>
	/// <param name="rate">The rate to play at. A value of 0.5 will play at half speed, 2 will play at double speed.</param>
	/// <returns>The stream ID if successful, -1 if the play fails.</returns>
	public static int play(int fileID, float leftVolume = 1, float rightVolume = -1, int priority = 1, int loop = 0, float rate = 1)
	{
		Debug.Log(_logPrefix + "play(" + fileID + ", " + leftVolume + ", " + rightVolume + ", " + priority + ", " + loop + ", " + rate + ")");
		return 1;
	}


	/// <summary>
	/// Releases the sound pool resources.
	/// </summary>
	public static void releasePool()
	{
		Debug.Log(_logPrefix + "releasePool()");
	}


	/// <summary>
	/// Resumes a paused stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to resume.</param>
	public static void resume(int streamID)
	{
		Debug.Log(_logPrefix + "resume(" + streamID + ")");
	}


	/// <summary>
	/// Resumes all streams paused with pauseAll().
	/// </summary>
	public static void resumeAll()
	{
		Debug.Log(_logPrefix + "resumeAll()");
	}


	/// <summary>
	/// Sets the loop of a stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to change.</param>
	/// <param name="loop">How many times to loop the audio. A value of 0 will play once, -1 will loop until stopped.</param>
	public static void setLoop(int streamID, int loop)
	{
		Debug.Log(_logPrefix + "setLoop(" + streamID + ", " + loop + ")");
	}


	/// <summary>
	/// Sets the priority of a stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to change.</param>
	/// <param name="priority">The priority of this stream. If the number of simultaneously playing streams exceeds maxStreams in makePool, higher priority streams will play and lower priority streams will not.</param>
	public static void setPriority(int streamID, int priority)
	{
		Debug.Log(_logPrefix + "setPriority(" + streamID + ", " + priority + ")");
	}


	/// <summary>
	/// Sets the rate of a stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to change.</param>
	/// <param name="rate">The rate to play at. A value of 0.5 will play at half speed, 2 will play at double speed.</param>
	public static void setRate(int streamID, float rate)
	{
		Debug.Log(_logPrefix + "setRate(" + streamID + ", " + rate + ")");
	}


	/// <summary>
	/// Sets the volume of a stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to change.</param>
	/// <param name="leftVolume">The left volume to play at (0.0 - 1.0). If rightVolume is omitted, this value will be used for both.</param>
	/// <param name="rightVolume">The right volume to play at (0.0 - 1.0). Defaults to leftVolume.</param>
	public static void setVolume(int streamID, float leftVolume, float rightVolume = -1)
	{
		Debug.Log(_logPrefix + "setVolume(" + streamID + ", " + leftVolume + ", " + rightVolume + ")");
	}


	/// <summary>
	/// Stops a stream.
	/// </summary>
	/// <param name="streamID">The ID of the stream to stop.</param>
	public static void stop(int streamID)
	{
		Debug.Log(_logPrefix + "stop(" + streamID + ")");
	}


	/// <summary>
	/// Unloads a file from the sound pool.
	/// </summary>
	/// <param name="fileID">The ID of the file to unload.</param>
	/// <returns>True if unloaded, false if previously unloaded.</returns>
	public static bool unload(int fileID)
	{
		Debug.Log(_logPrefix + "unload(" + fileID + ")");
		return true;
	}

#endif

}
