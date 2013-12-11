using UnityEngine;
using System.Collections;

public class PhotoSelectClick : MonoBehaviour {
	
	public LoginSceneViewer loginSceneViewer;
	public string photoName;
	
	void Awake() {
		loginSceneViewer = GameObject.FindObjectOfType(typeof(LoginSceneViewer)) as LoginSceneViewer;
	}
	
	void OnClick() {
		
		
		loginSceneViewer.SetPhoto(photoName);
			
		loginSceneViewer.RegisterSceneSetting();
	
	}
}
