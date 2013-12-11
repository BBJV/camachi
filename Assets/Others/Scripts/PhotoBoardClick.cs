using UnityEngine;
using System.Collections;

public class PhotoBoardClick : MonoBehaviour {

	public LoginSceneViewer loginSceneViewer;
		
	void Awake() {
		loginSceneViewer = GameObject.FindObjectOfType(typeof(LoginSceneViewer)) as LoginSceneViewer;
	}
	
	void OnClick() {

		loginSceneViewer.RegisterPhotoBoardSceneSetting();
	
	}
}
