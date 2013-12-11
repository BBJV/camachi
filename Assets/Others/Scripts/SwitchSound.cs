using UnityEngine;
using System.Collections;

public class SwitchSound : MonoBehaviour {
	
	public bool isSoundOff;
	
	void OnClick () {
		PlayerPrefs.SetInt("SoundMute", System.Convert.ToInt32(!isSoundOff));
		AudioListener.volume = System.Convert.ToInt32(isSoundOff);
	}
	
	void OnEnable () {
		if(System.Convert.ToBoolean(PlayerPrefs.GetInt("SoundMute", 0)) != isSoundOff)
		{
			SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}
}
