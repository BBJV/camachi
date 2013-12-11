using UnityEngine;
using System.Collections;

public class QuitButtonController : MonoBehaviour {
	public Transform PauseMenu;
	
	public MatchController matchController;
	
	void Start() {
		matchController = GameObject.FindObjectOfType(typeof(MatchController)) as MatchController;
	}
	
	void OnClick(){
		
		
		matchController.Back();
		
		//if(ispressed){
			Time.timeScale = 1.0f;
			//close the pause menu
			Application.LoadLevel("selectMap");
		//}
	}
}
