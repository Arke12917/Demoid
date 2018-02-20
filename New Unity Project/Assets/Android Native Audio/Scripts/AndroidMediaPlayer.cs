using UnityEngine;

namespace ChristopherCreates.AndroidNativeAudio
{
	public class AndroidMediaPlayer
	{
		public AndroidJavaObject AndroidJavaObject;
		public int ID;
		public bool PlayInBackground;
		public bool IsAllPaused;
		public bool WasPlaying;


		public AndroidMediaPlayer(AndroidJavaObject androidJavaObject, int id, bool playInBackground)
		{
			AndroidJavaObject = androidJavaObject;
			ID = id;
			PlayInBackground = playInBackground;
			IsAllPaused = false;
			WasPlaying = false;
		}
	}
}
