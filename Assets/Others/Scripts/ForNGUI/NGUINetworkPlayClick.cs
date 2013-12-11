using UnityEngine;
using System.Collections;

public class NGUINetworkPlayClick : MonoBehaviour {

	void OnClick() {
		PlayerPrefs.SetString("GameType", "Network");
		Application.LoadLevel(Definition.eSceneID.LobbyScene.ToString());
	}
}
