using UnityEngine;
using System.Collections;

public class NGUIMatchRoomTalentIcon : MonoBehaviour {

	public UISlicedSprite icon;
	
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
		NGUICarSelecting.OnCarSelectingChanged += OnCarSelectingChanged;
	}
	
	void OnDestroy() {
		NGUICarSelecting.OnCarSelectingChanged -= OnCarSelectingChanged;
	}
	
	void OnCarSelectingChanged(CarInsanityCarInfo car) {
//		Debug.Log ("NGUIMatchRoomTalentIcon.OnCarSelectingChanged:"+((IconName)car.carID).ToString());
		icon.spriteName = ((IconName)car.carID).ToString();
		icon.MakePixelPerfect();
	}
}
