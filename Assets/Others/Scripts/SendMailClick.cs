using UnityEngine;
using System.Collections;

public class SendMailClick : MonoBehaviour {
	
	public Text3DClick account;
	private ForgetPasswordController forgetPasswordController;
	
	void Awake () {
		forgetPasswordController = FindObjectOfType(typeof(ForgetPasswordController)) as ForgetPasswordController;
	}
	
	void OnClick () {
		forgetPasswordController.SendMessage("GetPasswordStart", account.text, SendMessageOptions.DontRequireReceiver);
	}
}
