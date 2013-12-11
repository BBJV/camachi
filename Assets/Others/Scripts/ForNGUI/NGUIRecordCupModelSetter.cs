using UnityEngine;
using System.Collections;

public class NGUIRecordCupModelSetter : MonoBehaviour {

	public int rankNumber;
	
	public MeshRenderer cupModel;
	
	void Awake() {
		PlayGameController.OnCarArrived += OnCarArrived;
		
		cupModel.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnCarArrived -= OnCarArrived;
	}
	
	void OnCarArrived(CarInsanityPlayer player, string record, int rank, bool isOwnedPlayer) {
		if(rankNumber == rank) {

			cupModel.enabled = true;

		}
	}
}
