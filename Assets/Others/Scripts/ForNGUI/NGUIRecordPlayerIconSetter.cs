using UnityEngine;
using System.Collections;

public class NGUIRecordPlayerIconSetter : MonoBehaviour {

	public int rankNumber;
	
	public UISprite iconSprite;
	
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		iconSprite.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			iconSprite.enabled = true;
			iconSprite.spriteName = player.selectedCar.name;
			iconSprite.MakePixelPerfect();
		}
	}
}
