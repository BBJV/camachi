using UnityEngine;
using System.Collections;

public class NGUILoginSceneViewer : MonoBehaviour {
	
	public GameObject confirmMessageBoard;
	public GameObject messageBoard;
	public GameObject OKmessageBoard;
	public GameObject failMessageBoard;
	public GameObject dailyRewardBoard;
	
	public UILabel loginRewardLabel;
	
//	public GameObject buttonOnConfirmMessageBoard;
//	public GameObject buttonOnMessageBoard;
	public GameObject buttonOnOKMessageBoard;
	
	
	public UISprite confirmMessageSprite;
	public UISprite messageSprite;
	public UISprite OKMessageSprite;
	public UISprite failMessageSprite;
	public UISprite loginRewardMessageSprite;
	
	public NGUIText3DClick lgnAccText3DClick;
	public NGUIText3DClick lgnPwText3DClick;
	
	public NGUIKeepAccountAndPasswordClick nguiKeepAccountAndPasswordClick;
	
	public GameObject loginedMarketButton;
	public GameObject unloginedMarketButton;
	
	
	public GameObject singleGameButton;
	public GameObject loginButton;
	public GameObject startButton;
	public GameObject logoutButton;
	
	public GameObject loginedBoard;
	
	public GameObject registerButton;
	public GameObject forgetPasswordButton;
	
	public void ConnectingToServerSceneSetting() {
		messageSprite.spriteName = "connectingtoserver";
		messageSprite.MakePixelPerfect();
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void LoginingSceneSetting() {
		messageSprite.spriteName = "loginingtolobby";
		messageSprite.MakePixelPerfect();
		
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void LoadingPlayerSceneSetting() {
		messageSprite.spriteName = "loadingplayerdata";
		messageSprite.MakePixelPerfect();
		
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void LoginErrorSceneSetting() {
		OKMessageSprite.spriteName = "LoginingError";
		OKMessageSprite.MakePixelPerfect();
		
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void AccountIsBeingUsedSceneSetting() {
		OKMessageSprite.spriteName = "theaccountisbeingused";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void LoadPlayerErrorSceneSetting() {
		OKMessageSprite.spriteName = "loadplayerdataerror";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void KickByGameLobbySceneSetting() {
		OKMessageSprite.spriteName = "Anotheruser";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	
	public void WrongVersionNumberSceneSetting() {
		OKMessageSprite.spriteName = "pleaseUpdateLatestVersion";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void FailedToConnectToServerSceneSetting() {
		OKMessageSprite.spriteName = "failtoconnect";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void LoginedMenuSceneSetting() {
		
		dailyRewardBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
		
		registerButton.SetActiveRecursively(false);
		forgetPasswordButton.SetActiveRecursively(false);
		
//		loginedMarketButton.SetActiveRecursively(true);
		
		unloginedMarketButton.SetActiveRecursively(false);
//		singleGameButton.SetActiveRecursively(true);
		loginButton.SetActiveRecursively(false);
		startButton.SetActiveRecursively(true);
		logoutButton.SetActiveRecursively(true);
		
		loginedBoard.SetActiveRecursively(true);
	}
	
	public void GainLoginRewardSceneSetting(int gold) {
		
		loginRewardLabel.text = gold.ToString();
		loginRewardMessageSprite.spriteName = "dailyReward_title";
		loginRewardMessageSprite.MakePixelPerfect();
		dailyRewardBoard.SetActiveRecursively(true);
		
		
		OKmessageBoard.SetActiveRecursively(false);		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	public void UnloginMenuSceneSetting() {
		
		dailyRewardBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
		
		registerButton.SetActiveRecursively(true);
		forgetPasswordButton.SetActiveRecursively(true);
		
		loginedMarketButton.SetActiveRecursively(false);
		unloginedMarketButton.SetActiveRecursively(true);
		
//		singleGameButton.SetActiveRecursively(true);
		loginButton.SetActiveRecursively(true);
		startButton.SetActiveRecursively(false);
		logoutButton.SetActiveRecursively(false);
		
		loginedBoard.SetActiveRecursively(false);
	}
	
	public void LoginFailSceneSetting() {
		OKMessageSprite.spriteName = "loginfail";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		dailyRewardBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		failMessageBoard.SetActiveRecursively(false);
	}
	
	void Start () {
		ProductController.OnFailedPurchase += FailedPurchase;
	}
	
	void OnDestroy () {
		ProductController.OnFailedPurchase -= FailedPurchase;
	}
	
	void FailedPurchase () {
		failMessageSprite.spriteName = PlayerPrefs.GetString("Language", "English");
		failMessageBoard.SetActiveRecursively(true);
		messageBoard.SetActiveRecursively(false);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		dailyRewardBoard.SetActiveRecursively(false);
	}
}
