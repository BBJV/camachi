using UnityEngine;
using System.Collections;

public class NGUIRegisterSummitClick : MonoBehaviour {

	public NGUIRegisterController nguiRegisterController;
	
	void Awake() {
		nguiRegisterController = GameObject.FindObjectOfType(typeof(NGUIRegisterController)) as NGUIRegisterController;
	}
	
	void OnClick() {		
		nguiRegisterController.Register();
	}
}
