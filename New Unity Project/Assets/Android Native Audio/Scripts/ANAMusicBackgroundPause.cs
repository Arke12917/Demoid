using UnityEngine;

namespace ChristopherCreates.AndroidNativeAudio
{
	public class ANAMusicBackgroundPause : MonoBehaviour
	{
		void OnApplicationPause(bool isPaused)
		{
			ANAMusic.OnApplicationPause(isPaused);
		}
	}
}
