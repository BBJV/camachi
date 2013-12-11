using UnityEngine;
using System.Collections;

public class NGUIPlayerPrefsSetting : MonoBehaviour {

	public Definition.ePlayerPrefabKey prefsKey;
	//public string prefValue;
	
	public NGUIText3DClick nguiText3DClick;
	
	void Awake() {
		nguiText3DClick.text = PlayerPrefs.GetString(prefsKey.ToString());
		nguiText3DClick.SendMessage("Assign", SendMessageOptions.DontRequireReceiver);
	}
}
