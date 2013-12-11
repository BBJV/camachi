using UnityEngine;
using System.Collections;

public class SoundSceneController : MonoBehaviour {
	
	public GameObject GUI_Sound;
	
	public StuffSceneController stuffSceneController;
	
	private void Free() {
		GUI_Sound.SetActiveRecursively(false);
	}
	
	public void InitialSceneSetting() {
		GUI_Sound.SetActiveRecursively(true);
	}
	
	public void Switch() {
		this.Free();
		
		if(stuffSceneController) {
			stuffSceneController = GameObject.Find("StuffSceneController").GetComponent<StuffSceneController>();
		}
		
		stuffSceneController.InitialSceneSetting();
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == 1) {
			GUI_Sound = GameObject.Find("GUI_Sound");
			GUI_Sound.SetActiveRecursively(false);
			
			
		}
	}
}
