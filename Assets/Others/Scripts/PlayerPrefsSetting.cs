using UnityEngine;
using System.Collections;

public class PlayerPrefsSetting : MonoBehaviour {
	
	public string prefsKey;
	//public string prefValue;
	
	public Text3DClick text3DClick;
	
	void Awake() {
		text3DClick.text = PlayerPrefs.GetString(prefsKey);
		text3DClick.SendMessage("Assign", SendMessageOptions.DontRequireReceiver);
	}
}
