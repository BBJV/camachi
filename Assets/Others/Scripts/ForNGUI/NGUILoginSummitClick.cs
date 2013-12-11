using UnityEngine;
using System.Collections;

public class NGUILoginSummitClick : MonoBehaviour {

	public NGUILoginController nguiLoginController;
	public AudioClip sound;
	
	void Awake() {
		nguiLoginController = GameObject.FindObjectOfType(typeof(NGUILoginController)) as NGUILoginController;
	}
	
	void OnClick() {
		
		PlayerPrefs.SetString("GameType", "Network");
		nguiLoginController.Login();
		AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
	}
}
