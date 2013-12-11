using UnityEngine;
using System.Collections;

public class GameSoundController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		AudioListener.volume = (PlayerPrefs.GetInt("SoundMute", 0) == 0) ? 1 : 0;
	}
}