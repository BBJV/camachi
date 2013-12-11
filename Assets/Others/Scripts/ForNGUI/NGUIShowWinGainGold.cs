using UnityEngine;
using System.Collections;

public class NGUIShowWinGainGold : MonoBehaviour {
	
	public GameObject gainGoldBoard;
	
	public UILabel gainGoldLebel;
	
	void Awake() {
		PlayGameController.OnShowGainGold += OnShowGainGold;
	}
	
	void OnDestroy() {
		PlayGameController.OnShowGainGold -= OnShowGainGold;
	}
	
	void OnShowGainGold(int gainGold, int rank) {
		
		
		gainGoldBoard.SetActiveRecursively(true);
		gainGoldLebel.text = gainGold.ToString();		
		
		
	}
}
