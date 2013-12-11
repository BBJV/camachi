using UnityEngine;
using System.Collections;

public class NGUITalentPrice : MonoBehaviour {

	public UILabel talentPriceLabel;
	
	public CarInsanityProductInfo currentCar;
	
	void Awake() {
		NGUIProductSceneViewer.OnTalentInfoOpened += OnTalentInfoOpened;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnTalentInfoOpened -= OnTalentInfoOpened;
	}
	
	void OnTalentInfoOpened(CarInsanityProductInfo car) {
		currentCar = car;
//		Debug.Log ("NGUITalentPrice.OnProductViewChanged:"+car.talentPrice.ToString());
		talentPriceLabel.text = car.talentPrice;
	}
}
