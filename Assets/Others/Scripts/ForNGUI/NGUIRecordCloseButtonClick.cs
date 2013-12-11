using UnityEngine;
using System.Collections;

public class NGUIRecordCloseButtonClick : MonoBehaviour {
	
	public PlayGameController playerGameController;
	public RedirectController rediectController;
	
	public string methodName;
	
	private bool isClicked = false;
	
	void Awake() {
		playerGameController = GameObject.FindObjectOfType(typeof(PlayGameController)) as PlayGameController;
		rediectController = GameObject.FindObjectOfType(typeof(RedirectController)) as RedirectController;
	}
	
	void OnClick() {
		
		if(!isClicked) {
//			Debug.Log ("OnClick");
			playerGameController.BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
			rediectController.BroadcastMessage(methodName, SendMessageOptions.DontRequireReceiver);
			
			isClicked = true;
		}
		
		
	}
}
