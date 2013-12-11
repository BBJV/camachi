using UnityEngine;
using System.Collections;

public class NGUILogoutButtonClick : MonoBehaviour {

	public NGUILoginController nguiLoginController;
	public AudioClip sound;
	
	void Awake() {
		nguiLoginController = GameObject.FindObjectOfType(typeof(NGUILoginController)) as NGUILoginController;
	}
	
	void OnClick() {
		
		PlayerPrefs.SetString("GameType", "Single");
		nguiLoginController.Logout();
		AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
	}
}
