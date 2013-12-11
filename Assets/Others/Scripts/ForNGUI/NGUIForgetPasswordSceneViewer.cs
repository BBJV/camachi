using UnityEngine;
using System.Collections;

public class NGUIForgetPasswordSceneViewer : MonoBehaviour {
	
	public GameObject confirmMessageBoard;
	public GameObject messageBoard;
	public GameObject OKmessageBoard;
	
//	public GameObject buttonOnConfirmMessageBoard;
//	public GameObject buttonOnMessageBoard;
	public GameObject buttonOnOKMessageBoard;
	public GameObject buttonOfCancelOfForgetPassowrd;
	
	
	public UISprite confirmMessageSprite;
	public UISprite messageSprite;
	public UISprite OKMessageSprite;
	
	public void FillInAccountSceneSetting() {
		
//		Debug.Log("pleasefillintheaccount");
		
		OKMessageSprite.spriteName = "pleasefillintheaccount";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}
	
	public void ConnectingToServerSceneSetting() {
		
//		Debug.Log("connectingtoserver");
		
		messageSprite.spriteName = "connectingtoserver";
		messageSprite.MakePixelPerfect();
		messageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
	}
	
	public void SendingPasswordMailSceneSetting() {
		
//		Debug.Log("itissending");
		
		messageSprite.spriteName = "itissending";
		messageSprite.MakePixelPerfect();
		
		messageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
	}
	
	public void SendPasswordMailErrorSceneSetting() {
		
//		Debug.Log("therequestiserror");
		
		OKMessageSprite.spriteName = "therequestiserror";
		OKMessageSprite.MakePixelPerfect();
//		OKMessageLabel.text = "The request is error. Please restart the game and try again later.";
		OKmessageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}
	
	public void SendPasswordMailSuccessSceneSetting() {
		
//		Debug.Log("thepassword");
		
		OKMessageSprite.spriteName = "thepassword";
		OKMessageSprite.MakePixelPerfect();
//		OKMessageLabel.text = "The password is sent to your email. Please check your email.";
		OKmessageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
		
		ButtonClick button = buttonOnOKMessageBoard.AddComponent<ButtonClick>();
		button.target = buttonOfCancelOfForgetPassowrd;
		button.method = "OnClick";
		button.includeChild = false;
		
		NGUIDestroyOnClick destroyButton = buttonOnOKMessageBoard.AddComponent<NGUIDestroyOnClick>();
		destroyButton.component = button;
	}
	
	public void AccountIsNotExistSceneSetting() {
		
//		Debug.Log("theaccount");
		
		OKMessageSprite.spriteName = "theaccount";
		OKMessageSprite.MakePixelPerfect();
//		OKMessageLabel.text = "The account is unavailable. Please enter the correct account.";
		OKmessageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}
	
	public void ServiceUnavailableSceneSetting() {
		
//		Debug.Log("theservice");
		
		OKMessageSprite.spriteName = "theservice";
		OKMessageSprite.MakePixelPerfect();
//		OKMessageLabel.text = "The service is unavailable now. Please try again later.";
		OKmessageBoard.SetActiveRecursively(true);
		
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}
}
