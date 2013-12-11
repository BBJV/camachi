using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class MenuController : MonoBehaviour {
	
	public LoginSceneViewer loginSceneViewer;
	
	public int menuIndex = 0;
	
	void OnClick() {
		
		if(menuIndex == 0) {
			loginSceneViewer.SwitchToLoginScene();
		}else if(menuIndex == 1){
			loginSceneViewer.SwitchToSoundScene();
		}else if(menuIndex == 2) {
			loginSceneViewer.SwitchToStuffScene();
		}
	
		menuIndex++;
		
		if(menuIndex > 2) {
			menuIndex = 0;
		}
	}
}
