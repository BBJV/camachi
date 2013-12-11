using UnityEngine;
using System.Collections;

public class NGUISendMailClick : MonoBehaviour {

	public NGUIText3DClick account;
	private NGUIForgetPasswordController nguiForgetPasswordController;
	
	void Awake () {
		nguiForgetPasswordController = FindObjectOfType(typeof(NGUIForgetPasswordController)) as NGUIForgetPasswordController;
	}
	
	void OnClick () {
		nguiForgetPasswordController.SendMessage("GetPasswordStart", account.text, SendMessageOptions.DontRequireReceiver);
	}
}
