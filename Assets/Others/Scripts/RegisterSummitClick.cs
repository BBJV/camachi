using UnityEngine;
using System.Collections;

public class RegisterSummitClick : MonoBehaviour {

	public RegisterController registerController;
	
	void Awake() {
		registerController = GameObject.FindObjectOfType(typeof(RegisterController)) as RegisterController;
	}
	
	void OnClick() {		
		registerController.Register();
	}
}
