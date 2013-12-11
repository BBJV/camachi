using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class KeepAccountAndPasswordClick : MonoBehaviour {
	
	public bool isClicked = false;
	
	void Awake() {
		string checkbox = PlayerPrefs.GetString("checkBoxOnOrNot");
		if(checkbox == "On") {
			isClicked = true;
		}else{
			isClicked = false;
		}
		renderer.enabled = isClicked;
	}
	
	void OnClick() {
		
		isClicked = !isClicked;
		renderer.enabled = isClicked;
			
	}
}
