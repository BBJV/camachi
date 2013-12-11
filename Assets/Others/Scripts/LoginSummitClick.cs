using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class LoginSummitClick : MonoBehaviour {

	public LoginController loginController;
	public AudioClip sound;
	
	void Awake() {
		loginController = GameObject.FindObjectOfType(typeof(LoginController)) as LoginController;
	}
	
	void OnClick() {
		AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position);
		PlayerPrefs.SetString("GameType", "Network");
		loginController.Login();
	}
}
