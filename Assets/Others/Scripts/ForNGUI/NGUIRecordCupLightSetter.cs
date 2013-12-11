using UnityEngine;
using System.Collections;

public class NGUIRecordCupLightSetter : MonoBehaviour {
	
	public int rankNumber;
	
	public UISprite cupLight;
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		cupLight.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			
			if(isOwnedPlayer) {
				cupLight.enabled = true;
			}
		}
	}
}
