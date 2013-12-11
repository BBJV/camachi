using UnityEngine;
using System.Collections;

public class NGUITalentInfoIcon : MonoBehaviour {

	public UISprite icon;
	
	enum IconName{
		
		PassiveSkill_1_police = 1,
		PassiveSkill_1_trunk = 2,
		PassiveSkill_1_ambulance = 3,
		PassiveSkill_1_fireMan = 4,
		PassiveSkill_1_garbage = 5,
		PassiveSkill_1_godofwealth = 6,
		PassiveSkill_1_hummer = 7,
		PassiveSkill_1_mascot = 8,
		PassiveSkill_1_classicCar = 9,
		
		
	}
	
	void Awake() {
		NGUIProductSceneViewer.OnTalentInfoOpened += OnTalentInfoOpened;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnTalentInfoOpened -= OnTalentInfoOpened;
	}
	
	void OnTalentInfoOpened(CarInsanityProductInfo car) {
//		Debug.Log ("NGUITalentInfoIcon.OnProductViewChanged:"+((IconName)car.ID).ToString());
		icon.spriteName = ((IconName)car.ID).ToString();
		icon.MakePixelPerfect();
	}
}
