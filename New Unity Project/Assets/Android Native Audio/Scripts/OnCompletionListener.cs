using System;
using UnityEngine;

namespace ChristopherCreates.AndroidNativeAudio
{
	public class OnCompletionListener : AndroidJavaProxy
	{
		public Action<int> Callback;


		public OnCompletionListener(Action<int> callback) : base("android.media.MediaPlayer$OnCompletionListener")
		{
			Callback = callback;
		}


		void onCompletion(AndroidJavaObject mediaPlayer)
		{
			Callback(mediaPlayer.Call<int>("getAudioSessionId"));
		}
	}
}
