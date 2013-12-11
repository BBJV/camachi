using UnityEngine;
using System.Collections;

public class NGUIAccumulativeShowGainGold : MonoBehaviour {
	
	public int rankNumber;
	
	public Animation addingStarAnimation;
	
	public UILabel totalGoldLabel;
	public UISprite animationStar;
	
	
	public GameUserPlayer player;
	
	private int gainGold = 0;
	
	private bool isThisRank = false;
	
	void Awake() {
		PlayGameController.OnShowRecord += OnShowRecord;
		PlayGameController.OnShowGainGold += OnShowGainGold;
		
		GameObject pgo = GameObject.Find("PlayerInfo");
		player = pgo.GetComponent<GameUserPlayer>();
		
		animationStar.enabled = false;
	}
	
	void OnDestroy() {
		PlayGameController.OnShowRecord -= OnShowRecord;
		PlayGameController.OnShowGainGold -= OnShowGainGold;
	}
	
	void OnShowRecord() {
		
		if(gainGold > 0 && isThisRank) {
			
			animationStar.enabled = true;
			
			addingStarAnimation.Play();
			
			StartCoroutine(AccumulativeCount());
		}
		
	}
	
	void OnShowGainGold(int gg, int rank) {
		if(rankNumber == rank) {
			gainGold = gg;
			isThisRank = true;
		}
		
	}
	
	IEnumerator AccumulativeCount(){
		totalGoldLabel.text = player.money.ToString();
		yield return new WaitForSeconds(2.0f);
		
		while(gainGold > 0) {
			gainGold--;
			
			player.money++;
			totalGoldLabel.text = player.money.ToString();
			yield return new WaitForSeconds(0.2f);
		}
		
		addingStarAnimation.Stop();
		
	}
	
	
}
