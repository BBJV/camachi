using UnityEngine;
using System;

public class RegisterController : MonoBehaviour {
	
	private enum ProcessState {
		FREE,
		PREPARE,
		START,
		CONNECTING,
		CONNECTED,
		REGISTERING,
		REGISTERED,
		FAIL,
		FAIL_USEREXIST,
		FAIL_PLAYEREXIST,
		FAIL_NOTINPUTALL,
		FAIL_PASSWORDCONFIRMWRONG,
		UNAVAILABLE,
		FINISH,
		MASTERSERVERFAIL,
		CONNECTIONFAIL
		
	}
	
	private RegisterController.ProcessState processState = RegisterController.ProcessState.FREE;

	private String registerErrorMessage = "";
	
	public String registrationServiceIP = "127.0.0.1";
	public int registrationServicePort = 50000;	

	public int connectToGameLobbyCount = 5;
	public int connectToGameServerCount = 5;
	
	public LoginSceneViewer loginSceneViewer;
	
	#region RPC
	
	[RPC] //Define for client's RPC
	private void SendToGameLobby_Register(string registerMemberID, string registerPassword, string registerNickName, string Email, string photoName ) {
		
	}
	 
	[RPC]
	public void ReceiveByClientPortal_Register(int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		Network.Disconnect();
		
		if(this.processState == RegisterController.ProcessState.REGISTERING) {
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				this.processState = RegisterController.ProcessState.REGISTERED;
				
				loginSceneViewer.RegisterSuccessSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.USEREXIST) {
				this.processState = RegisterController.ProcessState.FAIL_USEREXIST;
				
				loginSceneViewer.RegisterAccountErrorSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.PLAYEREXIST) {
				this.processState = RegisterController.ProcessState.FAIL_PLAYEREXIST;
				
				loginSceneViewer.RegisterNickNameErrorSceneSetting();
				
			} else if(resultState == Definition.RPCProcessState.FAIL) {
				this.processState = RegisterController.ProcessState.FAIL;
				
				loginSceneViewer.RegisterErrorSceneSetting();
			} else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
				this.processState = RegisterController.ProcessState.UNAVAILABLE;
				
				loginSceneViewer.RegisterErrorSceneSetting();
			}
		}
	}
	
	#endregion
	
	public void Register() {
		if((loginSceneViewer.regAccText3DClick.text == "") || 
		   (loginSceneViewer.regPwText3DClick.text == "") || 
		   (loginSceneViewer.regPwConfirmText3DClick.text == "")|| 
		   (loginSceneViewer.regEMailText3DClick.text =="") || 
		   (loginSceneViewer.regNickNameText3DClick.text == "") || 
		   (loginSceneViewer.regAccText3DClick.text == loginSceneViewer.regAccText3DClick.strBaseText)|| 
		   (loginSceneViewer.regPwText3DClick.text == loginSceneViewer.regPwText3DClick.strBaseText)|| 
		   (loginSceneViewer.regPwConfirmText3DClick.text == loginSceneViewer.regPwConfirmText3DClick.strBaseText)|| 
		   (loginSceneViewer.regEMailText3DClick.text == loginSceneViewer.regEMailText3DClick.strBaseText)|| 
		   (loginSceneViewer.regNickNameText3DClick.text == loginSceneViewer.regNickNameText3DClick.strBaseText)
		   
		   ) {
			
				this.registerErrorMessage = "Please fill in all the text fields.";
				loginSceneViewer.FillInAlltheTextFieldsSceneSetting();
			
			}else if(loginSceneViewer.regPwText3DClick.text != loginSceneViewer.regPwConfirmText3DClick.text) {
				loginSceneViewer.DoubleCheckPasswordSceneSetting();
				this.registerErrorMessage = this.registerErrorMessage + "Please double check the password is match or not.";
			}else {
				RegisterStart();
			}
	}

	private void RegisterStart() {
		this.processState = RegisterController.ProcessState.START;
		
		if(Network.peerType == NetworkPeerType.Disconnected) {
			Network.Connect(registrationServiceIP, registrationServicePort);
			this.processState = RegisterController.ProcessState.CONNECTING;
			loginSceneViewer.RegisteringSceneSetting();
		}else{
			loginSceneViewer.RegisterErrorSceneSetting();
//			Debug.Log("FAIL");
			this.processState = RegisterController.ProcessState.FAIL;
		}
		
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			loginSceneViewer = GameObject.FindObjectOfType(typeof(LoginSceneViewer)) as LoginSceneViewer;
			
		}
//		Debug.Log("Register:OnLevelWasLoaded");
	}
	
	void OnConnectedToServer() {
		
		if(this.processState == RegisterController.ProcessState.CONNECTING) {
			this.processState = RegisterController.ProcessState.CONNECTED;
	        networkView.RPC("SendToGameLobby_Register", RPCMode.Server, loginSceneViewer.regAccText3DClick.text, loginSceneViewer.regPwText3DClick.text, loginSceneViewer.regNickNameText3DClick.text, loginSceneViewer.regEMailText3DClick.text, loginSceneViewer.strRegisterPhoto);
			this.processState = RegisterController.ProcessState.REGISTERING;
		}
	}
	
	void OnFailedToConnect() {
		
		if(this.processState == RegisterController.ProcessState.CONNECTING) {
			if(connectToGameLobbyCount-- > 0) {
				RegisterStart();
			}else{
				this.processState = RegisterController.ProcessState.CONNECTIONFAIL;
				loginSceneViewer.RegisterErrorSceneSetting();
				connectToGameLobbyCount = 5;
			}
		}
	}
}