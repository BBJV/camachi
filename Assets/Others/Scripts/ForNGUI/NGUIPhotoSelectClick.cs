using UnityEngine;
using System.Collections;

public class NGUIPhotoSelectClick : MonoBehaviour {

	public NGUIRegisterSceneViewer nguiRegisterSceneViewer;
	public string photoName;
	
	void Awake() {
		nguiRegisterSceneViewer = GameObject.FindObjectOfType(typeof(NGUIRegisterSceneViewer)) as NGUIRegisterSceneViewer;
	}
	
	void OnClick() {
		
		
		nguiRegisterSceneViewer.SetPhoto(photoName);
			
		//nguiRegisterSceneViewer.RegisterSceneSetting();
	
	}
}
