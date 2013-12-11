using UnityEngine;
using System.Collections;

public class NGUIRegisterSceneViewer : MonoBehaviour {

	public GameObject confirmMessageBoard;
	public GameObject messageBoard;
	public GameObject OKmessageBoard;
	
//	public GameObject buttonOnConfirmMessageBoard;
//	public GameObject buttonOnMessageBoard;
	public GameObject buttonOnOKMessageBoard;
	public GameObject buttonOfCancelOfRegister;
	
	
	public NGUIText3DClick regAccText3DClick;
	public NGUIText3DClick regPwText3DClick;
	public NGUIText3DClick regPwConfirmText3DClick;
	public NGUIText3DClick regEMailText3DClick;
	public NGUIText3DClick regNickNameText3DClick;
	
	public UISprite confirmMessageSprite;
	public UISprite messageSprite;
	public UISprite OKMessageSprite;
	
	public UISlicedSprite photo;
	public string strRegisterPhoto;
	
	public void SetPhoto(string photoName) {

		strRegisterPhoto = photoName;
//		Register_Photo.GetComponentInChildren<MsgBoardChange>().ChangeMatetial(csSearch.GetCustomStyleByName(photoSkin, photoName).normal.background);
		
		photo.spriteName = photoName;
	}
	
	public void ConnectingToServerSceneSetting() {
		
//		Debug.Log("connectingtoserver");
		
		messageSprite.spriteName = "connectingtoserver";
		messageSprite.MakePixelPerfect();
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		
	}
	
	public void ConnectedToServerSceneSetting() {
		
//		Debug.Log("connected");
		
		messageSprite.spriteName = "connected";
		messageSprite.MakePixelPerfect();
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		
	}
	
	public void RegisteringSceneSetting() {
		
//		Debug.Log("Registering");
		
		messageSprite.spriteName = "Registering";
		messageSprite.MakePixelPerfect();
		messageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		OKmessageBoard.SetActiveRecursively(false);
		
	}
	
	public void RegisterErrorSceneSetting() {
		
//		Debug.Log("registrationError");
		
		OKMessageSprite.spriteName = "registrationError";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void RegisterSuccessSceneSetting() {
		
//		Debug.Log("registrationsuccess");
		
		OKMessageSprite.spriteName = "registrationsuccess";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
		ButtonClick button = buttonOnOKMessageBoard.AddComponent<ButtonClick>();
		button.target = buttonOfCancelOfRegister;
		button.method = "OnClick";
		button.includeChild = false;
		
		NGUIDestroyOnClick destroyButton = buttonOnOKMessageBoard.AddComponent<NGUIDestroyOnClick>();
		destroyButton.component = button;
		
	}
	
	public void FillInAlltheTextFieldsSceneSetting() {
		
//		Debug.Log("pleasefill");
		
		OKMessageSprite.spriteName = "pleasefill";
		OKMessageSprite.MakePixelPerfect();
//		Debug.Log("OKMessageSprite.spriteName:"+OKMessageSprite.spriteName);
		
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void DoubleCheckPasswordSceneSetting() {
//		OKMessageSprite.spriteName = "Please double check the password is match or not.";
		
//		Debug.Log("please");
		
		OKMessageSprite.spriteName = "please";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void NotAllLettersOrDigitsErrorSceneSetting() {
		
//		Debug.Log("Account");
		
		OKMessageSprite.spriteName = "Account";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void EmailIsNotValidErrorSceneSetting() {
		
//		Debug.Log("Email");
//		Debug.Log ("EmailError");
		
		OKMessageSprite.spriteName = "Email";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}
	
	public void PasswordIsNotValidErrorSceneSetting() {
		
//		Debug.Log("password");
		
		OKMessageSprite.spriteName = "password";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
	}

	public void RegisterNickNameErrorSceneSetting() {
		
//		Debug.Log("nicknameisalreadyused");
		
		OKMessageSprite.spriteName = "nicknameisalreadyused";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void RegisterAccountErrorSceneSetting() {
		OKMessageSprite.spriteName = "accountisalready";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void FailedToConnectErrorSceneSetting () {
		OKMessageSprite.spriteName = "Failed to connect to server";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	
	public void RegistrationUnavailableSceneSetting() {
		OKMessageSprite.spriteName = "registrationserviceisunavailable";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
	public void RegisterFailSceneSetting() {
		OKMessageSprite.spriteName = "registrationfail";
		OKMessageSprite.MakePixelPerfect();
		OKmessageBoard.SetActiveRecursively(true);
		confirmMessageBoard.SetActiveRecursively(false);
		messageBoard.SetActiveRecursively(false);
		
	}
		
}
