using UnityEngine;
using System.Collections;

public class NGUIUnpurchasedIcon : MonoBehaviour {
	
	public UISprite icon;
	
	void Awake() {
		NGUIProductSceneViewer.OnTalentInfoOpened += OnTalentInfoOpened;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnTalentInfoOpened -= OnTalentInfoOpened;
	}
	
	void OnTalentInfoOpened(CarInsanityProductInfo car) {

		icon.enabled = !car.purchased;
	}
}
