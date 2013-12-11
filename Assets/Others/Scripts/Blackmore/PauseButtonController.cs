using UnityEngine;
using System.Collections;

public class PauseButtonController : MonoBehaviour {
	public Transform PauseMenu;
	private bool IsPressed;
	// Use this for initialization
	IEnumerator Start () {
		PauseMenu.gameObject.SetActiveRecursively(false);
		collider.enabled = false;
		if(PlayerPrefs.GetString("GameType", "Single") == "Single")
		{
			transform.gameObject.SetActiveRecursively(true);
		}else{
			transform.gameObject.SetActiveRecursively(false);
		}
		yield return new WaitForSeconds(10);
		collider.enabled = true;
		//IsPressed = false;
	}
	
	void OnClick(){
		//if(ispressed){
			if(!PauseMenu.gameObject.active){
				PauseMenu.gameObject.SetActiveRecursively(true);
				PauseMenu.animation["makerBoard_1_3"].normalizedTime = 1.0f;
				PauseMenu.animation["makerBoard_1_3"].speed = 0.5f;
				PauseMenu.animation.Play();
				Time.timeScale = 0.0f;
			}else {
				Time.timeScale = 1.0f;
				//close the pause menu
				PauseMenu.gameObject.SetActiveRecursively(false);
			}
			
		//}
	}
}
