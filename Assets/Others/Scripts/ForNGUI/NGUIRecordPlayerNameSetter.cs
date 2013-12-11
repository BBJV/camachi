using UnityEngine;
using System.Collections;

public class NGUIRecordPlayerNameSetter : MonoBehaviour {

	public int rankNumber;
	
	public UILabel playerNameLabel;
	
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		playerNameLabel.text = "";
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			
			
			playerNameLabel.text = player.GetComponent<GameUserPlayer>().playerName;
			
			if(isOwnedPlayer) {
				playerNameLabel.color = Color.red;
			}else{
				playerNameLabel.color = Color.white;
			}
		}
	}
}
