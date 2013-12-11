using UnityEngine;
using System.Collections;
using System;

public class NGUIOwnedGold : MonoBehaviour {

	public UILabel goldsLabel;
	
	void Awake() {
		NGUILoginController.OnPlayerLoaded += OnPlayerLoaded;
		NGUILoginController.OnGoldReloaded += OnGoldReloaded;
	}
	
	void OnDestroy() {
		NGUILoginController.OnPlayerLoaded -= OnPlayerLoaded;
		NGUILoginController.OnGoldReloaded -= OnGoldReloaded;
	}
	
	void OnPlayerLoaded(GameUserPlayer player) {
//		Debug.Log ("OnPlayerLoaded:"+player.money.ToString());
		goldsLabel.text = player.money.ToString();
	}
	
	void OnGoldReloaded(int gold) {
//		Debug.Log ("OnGoldReloaded:"+gold.ToString());
		goldsLabel.text = gold.ToString();
	}
}
