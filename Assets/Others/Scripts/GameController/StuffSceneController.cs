using UnityEngine;
using System.Collections;

public class StuffSceneController : MonoBehaviour {
	
	public GameObject GUI_Stuff;
	public LoginSceneViewer loginSceneViewer;
	
	private void Free() {
		GUI_Stuff.SetActiveRecursively(false);
		this.enabled = false;
	}
	
	public void InitialSceneSetting() {
		GUI_Stuff.SetActiveRecursively(true);
	}
	
	public void Switch() {
		this.Free();
		
		if(loginSceneViewer) {
			loginSceneViewer = GameObject.Find("LoginSceneViewer").GetComponent<LoginSceneViewer>();
		}
		
		loginSceneViewer.InitialSceneSetting();
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == 1) {
			GUI_Stuff = GameObject.Find("GUI_Stuff");
			GUI_Stuff.SetActiveRecursively(false);
			
			
		}
	}
}
