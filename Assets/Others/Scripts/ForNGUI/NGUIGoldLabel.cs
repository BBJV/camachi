using UnityEngine;
using System.Collections;

public class NGUIGoldLabel : MonoBehaviour {

	public int rankNumber;
	
	public UILabel goldLabel;
	
	private int gainGold = 0;
	private bool isMine = false;
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		
		goldLabel.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {
			
			if(isOwnedPlayer) {
				goldLabel.enabled = true;
			}
		}
	}
}
