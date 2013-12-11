using UnityEngine;
using System.Collections;

public class NGUIRecordRowBackgroundSetter : MonoBehaviour {

	public int rankNumber;
	
	public UISprite recordRow;
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		recordRow.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			recordRow.enabled = true;
		}
	}
}
