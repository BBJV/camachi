using UnityEngine;
using System.Collections;

public class NGUIRecordTimeSetter : MonoBehaviour {

	public int rankNumber;
	
	public UILabel timeLabel;
	
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		timeLabel.text = "";
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			timeLabel.text = record;
			
			if(isOwnedPlayer) {
				timeLabel.color = Color.red;
			}else{
				timeLabel.color = Color.white;
			}
			
		}
	}
}
