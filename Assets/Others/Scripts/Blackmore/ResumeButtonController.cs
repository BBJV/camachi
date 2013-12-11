using UnityEngine;
using System.Collections;

public class ResumeButtonController : MonoBehaviour {
	public Transform PauseMenu;
	
	void OnClick(){
		//if(ispressed){
			Time.timeScale = 1.0f;
			//close the pause menu
			PauseMenu.gameObject.SetActiveRecursively(false);
		//}
	}
}
