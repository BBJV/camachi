using UnityEngine;
using System.Collections;

public class SingleGameClick : MonoBehaviour {
	
	public AudioClip sound;
	
	void OnClick() {
		AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
		PlayerPrefs.SetInt("BeforeMapScene", Application.loadedLevel);
		PlayerPrefs.SetString("GameType", "Single");
	}
	
	void PlaySingle () {
		PlayerPrefs.SetInt("NextScene", (int)Definition.eSceneID.MapScene);
		Application.LoadLevel("EmptyScene");
	}
}
