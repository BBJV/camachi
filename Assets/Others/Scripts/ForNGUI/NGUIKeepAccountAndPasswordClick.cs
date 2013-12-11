using UnityEngine;
using System.Collections;

public class NGUIKeepAccountAndPasswordClick : MonoBehaviour {

//	public bool isClicked = false;
	
	public UICheckbox uiCheckBox;
	
	void Awake() {
		string checkbox = PlayerPrefs.GetString(Definition.ePlayerPrefabKey.CHECKBOXONORNOT.ToString());
		if(checkbox == "On") {
			uiCheckBox.isChecked = true;
		}else{
			uiCheckBox.isChecked = false;
		}
		
//		Debug.Log("uiCheckBox.isChecked:" +uiCheckBox.isChecked);
//		uiCheckBox.isChecked = isClicked;

	}
	
//	void OnClick() {
//		
//		
//		Debug.Log ("check box is check ? :" + uiCheckBox.isChecked.ToString());
//			
//	}
}
