using UnityEngine;
using System.Collections;

public class NGUIMatchRoomTalentLock : MonoBehaviour {

	public UISlicedSprite lockIcon;
	
	
	void Awake() {
		NGUICarSelecting.OnCarSelectingChanged += OnCarSelectingChanged;

	}
	
	void OnDestroy() {
		NGUICarSelecting.OnCarSelectingChanged -= OnCarSelectingChanged;
		
	}
	
	void OnCarSelectingChanged(CarInsanityCarInfo car) {
//		Debug.Log ("NGUIMatchRoomTalentLock.OnCarSelectingChanged:"+car.isTalentOpened.ToString());
		lockIcon.enabled = !car.isTalentOpened;
	}

}
