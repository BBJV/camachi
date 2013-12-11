using UnityEngine;
using System.Collections;

public class NGUIGoldBackGround : MonoBehaviour {

	public int rankNumber;
	
	public UISprite goldBackGround;
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		goldBackGround.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			
			if(isOwnedPlayer) {
				goldBackGround.enabled = true;
			}
		}
	}
}
