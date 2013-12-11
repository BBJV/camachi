using UnityEngine;
using System.Collections;

public class NGUICarGold : MonoBehaviour {
	
	public UILabel goldLabel;
	
	void Awake() {
		NGUIProductSceneViewer.OnProductViewChanged += OnProductViewChanged;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnProductViewChanged -= OnProductViewChanged;
	}
	
	void OnProductViewChanged(CarInsanityProductInfo car) {
//		Debug.Log ("NGUICarGold.OnProductViewChanged:"+car.goldPrice.ToString());
		goldLabel.text = car.goldPrice;
	}
}
