using UnityEngine;
using System.Collections;

public class NGUITalentContentInfo : MonoBehaviour {

	public UISprite icon;
	
	enum ContentName{
		
		language_policeman_talent = 1,
		language_trunk_talent = 2,
		language_ambulance_talent = 3,
		language_fireman_talent = 4,
		language_garbage_talent = 5,
		
		language_godofwealth_talent = 6,
		language_hummer_talent = 7,
		language_mascot_talent = 8,
		language_classicCar_talent = 9,
		
		
	}
	
	void Awake() {
		NGUIProductSceneViewer.OnTalentInfoOpened += OnTalentInfoOpened;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnTalentInfoOpened -= OnTalentInfoOpened;
	}
	
	void OnTalentInfoOpened(CarInsanityProductInfo car) {
//		Debug.Log ("NGUITalentContentInfo.OnProductViewChanged:"+((ContentName)car.ID).ToString());
		icon.spriteName = ((ContentName)car.ID).ToString();
		icon.MakePixelPerfect();
	}
}
