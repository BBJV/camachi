using UnityEngine;
using System.Collections;

public class SelectMap : MonoBehaviour {
	
	public int mapCount = 1;
	private int selectedMap = 0;
	
	void TurnLeft () {
		selectedMap -= 1;
		if(selectedMap < 0)
		{
			selectedMap = mapCount - 1;
		}
	}
	
	void TurnRight () {
		selectedMap += 1;
		if(selectedMap >= mapCount)
		{
			selectedMap = 0;
		}
	}
	
	void Select () {
		LobbyController controller;
		switch(selectedMap)
		{
			case 0:
				controller = FindObjectOfType(typeof(LobbyController)) as LobbyController;
				controller.SendMessage("SelectMapMatch", Definition.eSceneID.MinePit, SendMessageOptions.DontRequireReceiver);
				break;
			case 1:
				controller = FindObjectOfType(typeof(LobbyController)) as LobbyController;
				controller.SendMessage("SelectMapMatch", Definition.eSceneID.HighSpeedRoad, SendMessageOptions.DontRequireReceiver);
				break;
			case 2:
				controller = FindObjectOfType(typeof(LobbyController)) as LobbyController;
				controller.SendMessage("SelectMapMatch", Definition.eSceneID.JapanNoLight, SendMessageOptions.DontRequireReceiver);
				break;
		}
	}
	
	void Cancel () {
		
		NGUILoginController nguiLoginController = GameObject.FindObjectOfType(typeof(NGUILoginController)) as NGUILoginController;
		
		if(nguiLoginController.processState == NGUILoginController.ProcessState.PREPARE) {
			Application.LoadLevel(PlayerPrefs.GetInt("BeforeMapScene"));
		}else{
			if(PlayerPrefs.GetInt("BeforeMapScene") == (int)Definition.eSceneID.LobbyScene) {
				Application.LoadLevel(PlayerPrefs.GetInt("BeforeMapScene"));
			}else{
				
				nguiLoginController.Back();
			}
		}
		
		
	}
}
