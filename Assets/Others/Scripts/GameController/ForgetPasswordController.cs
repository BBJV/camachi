// ChunHua Edit
using System;
using System.Net;
using UnityEngine;
using System.Net.Mail;
using System.Collections;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class ForgetPasswordController : MonoBehaviour {
	
	private enum ProcessState {
		FREE,
		PREPARE,
		START,
		CONNECTING,
		CONNECTED,
		SENDING,
		SENDED,
		FAIL,
		FAIL_USERUNEXIST,
		FAIL_NOTINPUTALL,
		UNAVAILABLE,
		FINISH,
		MASTERSERVERFAIL,
		CONNECTIONFAIL
		
	}
	
	private ForgetPasswordController.ProcessState processState = ForgetPasswordController.ProcessState.FREE;
	
	public string gameName = "";
	private String userMemberID = "";
//	private String getPasswordErrorMessage = "";
	private Rect getPasswordWindow;	
	public String getPasswordServiceIP = "127.0.0.1";
	public int getPasswordServicePort = 50000;
	
	
	public int connectToGameLobbyCount = 5;
	public int connectToGameServerCount = 5;
	private LoginSceneViewer loginScene;
	
	private void GetPasswordStart(string text) {
		if(text == "")
		{
			loginScene.FillInAccountSetting();
			return;
		}
		userMemberID = text;
		this.processState = ForgetPasswordController.ProcessState.START;
		
		if(Network.peerType == NetworkPeerType.Disconnected) {
			Network.Connect(getPasswordServiceIP, getPasswordServicePort);
			this.processState = ForgetPasswordController.ProcessState.CONNECTING;
			loginScene.SendingPasswordMailSceneSetting();
//			PasswordGettingSceneSetting();
		}else{
//			Debug.Log("FAIL");
			loginScene.SendPasswordMailErrorSetting();
			this.processState = ForgetPasswordController.ProcessState.FAIL;
		}
	}

	void OnConnectedToServer() {
		
		if(this.processState == ForgetPasswordController.ProcessState.CONNECTING) {
			this.processState = ForgetPasswordController.ProcessState.CONNECTED;
	        networkView.RPC("SendToGameLobby_GetPassword", RPCMode.Server, this.gameName, this.userMemberID);
		
			this.processState = ForgetPasswordController.ProcessState.SENDING;
		}
	}
	
	[RPC] //Define for client's RPC
	private void SendToGameLobby_GetPassword(String gameName, String userMemberID) {
		
	}
	 
	[RPC]
	public void ReceiveByClientPortal_GetPassword(int p) {
//		Debug.Log(p);
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
//		Network.Disconnect();
		
		if(this.processState == ForgetPasswordController.ProcessState.SENDING) {
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				loginScene.SendPasswordMailSuccessSetting();
				this.processState = ForgetPasswordController.ProcessState.SENDED;
//				Debug.Log("SendPasswordSuccess");
			} else if(resultState == Definition.RPCProcessState.USERNOTEXIST) {
				loginScene.SendPasswordMailErrorSetting();
				this.processState = ForgetPasswordController.ProcessState.FAIL_USERUNEXIST;
//				Debug.Log("SendPasswordFail: UserNotExist");
				
			} else if(resultState == Definition.RPCProcessState.FAIL) {
				loginScene.SendPasswordMailErrorSetting();
				this.processState = ForgetPasswordController.ProcessState.FAIL;
//				Debug.Log("SendPasswordFail: Fail");
			} else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
				loginScene.SendPasswordMailErrorSetting();
				this.processState = ForgetPasswordController.ProcessState.UNAVAILABLE;
//				Debug.Log("SendPasswordFail: Unavailable");
			}
		}
		
	}

	void OnFailedToConnect() {
//		Debug.Log("RegisterState OnFailedToConnect, Count:" + connectToGameLobbyCount);
		if(this.processState == ForgetPasswordController.ProcessState.CONNECTING) {
			if(connectToGameLobbyCount-- > 0) {
				
				GetPasswordStart(userMemberID);
				
			}else{
				this.processState = ForgetPasswordController.ProcessState.CONNECTIONFAIL;
				
				connectToGameLobbyCount = 5;
			}
		}
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			loginScene = FindObjectOfType(typeof(LoginSceneViewer)) as LoginSceneViewer;
		}
	}
}
