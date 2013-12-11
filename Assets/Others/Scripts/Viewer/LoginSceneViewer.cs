using UnityEngine;
using System.Collections;

public class LoginSceneViewer : MonoBehaviour {
	
	public GUISkin simpleChineseLanguageSkin;
	public GUISkin tranditionalChineseLanguageSkin;
	public GUISkin englishLanguageSkin;
	public GUISkin photoSkin;
	
	public GUISkin currentSkin;
	
	public GameObject GUI_Login;
	public GameObject Panel_Login;
	//public GameObject Panel_ModifyPassword;
	public GameObject Panel_PasswordSentSuccess;
	public GameObject Panel_Register;
	public GameObject Panel_SelectPhotoBoard;
	public GameObject Register_Photo;
	
	public GameObject Panel_ForgetPW;
	
	public GameObject Panel_ResultMsgBoard;
	public GameObject Panel_ResultContent;
	public GameObject Panel_ResultTitle;
	
	public GameObject Panel_YesMsgBoard;
	public GameObject Panel_NoMsgBoard;
	
	public GameObject GUI_Sound;
	public GameObject GUI_Stuff;
	public GameObject loginGear;
	
	public CustomStylesSearch csSearch;
	
	public KeepAccountAndPasswordClick keepAccPassClick;	
	public Text3DClick lgnAccText3DClick;
	public Text3DClick lgnPwText3DClick;
	
	public Text3DClick regAccText3DClick;
	public Text3DClick regPwText3DClick;
	public Text3DClick regPwConfirmText3DClick;
	public Text3DClick regNickNameText3DClick;
	public Text3DClick regEMailText3DClick;
	
	public string strRegisterPhoto = "";
	
	void Awake() {
		string language = PlayerPrefs.GetString("Language", "english");
		switch(language)
		{
			case "english" :
				currentSkin = englishLanguageSkin;
				break;
			case "simpleChinese" :
				currentSkin = simpleChineseLanguageSkin;
				break;
			case "tranditionalChinese" :
				currentSkin = tranditionalChineseLanguageSkin;
				break;
		}
		
	}
	
	private void ReloadSkin() {
		
	}
	
	public void SetLanguageToSimpleChinese() {
		currentSkin = simpleChineseLanguageSkin;
		ReloadSkin();
	}
	
	public void SetLanguageToTranditionalChinese() {
		currentSkin = tranditionalChineseLanguageSkin;
		ReloadSkin();
	}
	
	public void SetLanguageToEnglish() {
		currentSkin = englishLanguageSkin;
		ReloadSkin();
	}
	
	public void SetPhoto(string photoName) {

		strRegisterPhoto = photoName;
		Register_Photo.GetComponentInChildren<MsgBoardChange>().ChangeMatetial(csSearch.GetCustomStyleByName(photoSkin, photoName).normal.background);
	}
	
	public void InitialSceneSetting() {
		loginGear.animation.Play();
		audio.Play();
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);	
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
	}
	
	public void LoginingSceneSetting() {
		if(!Panel_Login.active)
		{
			GUI_Login.SetActiveRecursively(true);
			Panel_Login.SetActiveRecursively(true);
		}
		
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);		
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_NoMsgBoard.SetActiveRecursively(true);
		(Panel_NoMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "ConnectServer").normal.background);
		
	}
	
	public void LoginErrorSceneSetting() {
		
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
		
		Panel_YesMsgBoard.SetActiveRecursively(true);
		(Panel_YesMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "LoginError").normal.background);
		
	}
	
	public void DisconnectServerSceneSetting() {
		
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
		
		Panel_YesMsgBoard.SetActiveRecursively(true);
		(Panel_YesMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "DisconnectServer").normal.background);
		
	}
	
	public void LoadPlayerSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_NoMsgBoard.SetActiveRecursively(true);
		(Panel_NoMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "LoadPlayer").normal.background);
	}
	
	public void LoadPlayerErrorSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "LoadPlayerResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "LoadPlayerError").normal.background);
		
	}
	
	public void RegisterSceneSetting() {
		if(!Panel_Register.active)
		{
			GUI_Login.SetActiveRecursively(true);
			Panel_Register.SetActiveRecursively(true);
			loginGear.animation.Play();
			audio.Play();
		}
		
		Panel_Login.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);	
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
	}
	
	public void RegisterPhotoBoardSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(true);
		Panel_SelectPhotoBoard.SetActiveRecursively(true);
		Panel_ForgetPW.SetActiveRecursively(false);	
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
	}
	
	public void RegisteringSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);		
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_NoMsgBoard.SetActiveRecursively(true);
		(Panel_NoMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "Registering").normal.background);
		
	}
	
	public void RegisterSuccessSceneSetting() {
//		Debug.Log ("RegisterSuccessSceneSetting");
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);	
		Panel_NoMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterSuccess").normal.background);
		
	}
	
	public void RegisterAccountErrorSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);		
		Panel_NoMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterErrorAccountExist").normal.background);
		
	}
	
	public void RegisterNickNameErrorSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(true);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);		
		Panel_NoMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterErrorNickNameExist").normal.background);
		
	}
	
	public void RegisterErrorSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(true);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);		
		Panel_NoMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "RegisterError").normal.background);
		
	}
	
	public void ForgetPasswordSceneSetting() {
		loginGear.animation.Play();
		audio.Play();
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(true);
		
		Panel_NoMsgBoard.SetActiveRecursively(false);
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		
		
	}
	
	public void SendingPasswordMailSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(false);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(true);
		
		Panel_YesMsgBoard.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		
		Panel_NoMsgBoard.SetActiveRecursively(true);
		(Panel_NoMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "SendingPasswordMail").normal.background);
	}
	
	public void FillInAccountSetting () {
		Panel_NoMsgBoard.SetActiveRecursively(true);
		(Panel_NoMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "FillInAlltheTextFields").normal.background);
	}
	
	public void SendPasswordMailSuccessSetting() {
//		GUI_Login.SetActiveRecursively(true);
//		Panel_Login.SetActiveRecursively(false);
//		Panel_Register.SetActiveRecursively(true);
//		Panel_SelectPhotoBoard.SetActiveRecursively(false);
//		Panel_ForgetPW.SetActiveRecursively(false);
//		Panel_NoMsgBoard.SetActiveRecursively(false);
//		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "SendPasswordMailResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "SendPasswordMailSuccess").normal.background);
	}
	
	public void SendPasswordMailErrorSetting() {
//		GUI_Login.SetActiveRecursively(true);
//		Panel_Login.SetActiveRecursively(false);
//		Panel_Register.SetActiveRecursively(false);
//		Panel_SelectPhotoBoard.SetActiveRecursively(false);
//		Panel_ForgetPW.SetActiveRecursively(false);		
//		Panel_NoMsgBoard.SetActiveRecursively(false);
//		Panel_YesMsgBoard.SetActiveRecursively(false);
		
		Panel_ResultMsgBoard.SetActiveRecursively(true);
		Panel_ResultTitle.SetActiveRecursively(true);
		(Panel_ResultTitle.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "SendPasswordMailResult").normal.background);
		Panel_ResultContent.SetActiveRecursively(true);
		(Panel_ResultContent.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "SendPasswordMailError").normal.background);
	}
	
	public void FillInAlltheTextFieldsSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
		
		Panel_YesMsgBoard.SetActiveRecursively(true);
		(Panel_YesMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "FillInAlltheTextFields").normal.background);
		
	}
	
	public void DoubleCheckPasswordSceneSetting() {
		GUI_Login.SetActiveRecursively(true);
		Panel_Login.SetActiveRecursively(true);
		Panel_Register.SetActiveRecursively(false);
		Panel_SelectPhotoBoard.SetActiveRecursively(false);
		Panel_ForgetPW.SetActiveRecursively(false);
		Panel_ResultMsgBoard.SetActiveRecursively(false);
		Panel_NoMsgBoard.SetActiveRecursively(false);
		
		Panel_YesMsgBoard.SetActiveRecursively(true);
		(Panel_YesMsgBoard.GetComponentInChildren<MsgBoardChange>()).ChangeMatetial(csSearch.GetCustomStyleByName(currentSkin, "DoubleCheckPassword").normal.background);
		
	}
	
	
	
	public void SwitchToLoginScene() {
		InitialSceneSetting();
		GUI_Sound.SetActiveRecursively(false);
		GUI_Stuff.SetActiveRecursively(false);
	}
	
	public void SwitchToSoundScene() {
		GUI_Login.SetActiveRecursively(false);
		GUI_Sound.SetActiveRecursively(true);
		GUI_Stuff.SetActiveRecursively(false);
	}
	
	public void SwitchToStuffScene() {
		GUI_Login.SetActiveRecursively(false);
		GUI_Sound.SetActiveRecursively(false);
		GUI_Stuff.SetActiveRecursively(true);
	}
}
