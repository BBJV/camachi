
using System;
using System.Net;
using UnityEngine;
using System.Net.Mail;
using System.Collections;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class NGUIForgetPasswordController : MonoBehaviour {

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
	
	private NGUIForgetPasswordController.ProcessState processState = NGUIForgetPasswordController.ProcessState.FREE;
	
	public string gameName = "";
	private String userMemberID = "";
//	private String getPasswordErrorMessage = "";
	private Rect getPasswordWindow;	
	public String getPasswordServiceIP = "127.0.0.1";
	public int getPasswordServicePort = 50000;
	
	
	public int connectToGameLobbyCount = 0;
	public int maxConnectToGameLobbyCount = 0;
	public NGUIForgetPasswordSceneViewer nguiForgetPasswordSceneViewer;
	
	private void GetPasswordStart(string text) {
		if(text == "")
		{
			nguiForgetPasswordSceneViewer.FillInAccountSceneSetting();
			
			return;
		}
		userMemberID = text;
		this.processState = NGUIForgetPasswordController.ProcessState.START;
		
		if(Network.peerType == NetworkPeerType.Disconnected) {
			Network.Connect(getPasswordServiceIP, getPasswordServicePort);
			this.processState = NGUIForgetPasswordController.ProcessState.CONNECTING;
			
			nguiForgetPasswordSceneViewer.ConnectingToServerSceneSetting();
			
//			PasswordGettingSceneSetting();
		}else{
//			Debug.Log("FAIL");
			
			nguiForgetPasswordSceneViewer.SendPasswordMailErrorSceneSetting();
			
			this.processState = NGUIForgetPasswordController.ProcessState.FAIL;
		}
	}

	void OnConnectedToServer() {
		
		if(this.processState == NGUIForgetPasswordController.ProcessState.CONNECTING) {
			this.processState = NGUIForgetPasswordController.ProcessState.CONNECTED;
	        networkView.RPC("SendToGameLobby_GetPassword", RPCMode.Server, this.gameName, this.userMemberID);
		
			nguiForgetPasswordSceneViewer.SendingPasswordMailSceneSetting();
			this.processState = NGUIForgetPasswordController.ProcessState.SENDING;
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
		
		if(this.processState == NGUIForgetPasswordController.ProcessState.SENDING) {
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				nguiForgetPasswordSceneViewer.SendPasswordMailSuccessSceneSetting();
				this.processState = NGUIForgetPasswordController.ProcessState.SENDED;
//				Debug.Log("SendPasswordSuccess");
			} else if(resultState == Definition.RPCProcessState.USERNOTEXIST) {
				nguiForgetPasswordSceneViewer.AccountIsNotExistSceneSetting();
				this.processState = NGUIForgetPasswordController.ProcessState.FAIL_USERUNEXIST;
//				Debug.Log("SendPasswordFail: UserNotExist");
				
			} else if(resultState == Definition.RPCProcessState.FAIL) {
				nguiForgetPasswordSceneViewer.SendPasswordMailErrorSceneSetting();
				this.processState = NGUIForgetPasswordController.ProcessState.FAIL;
//				Debug.Log("SendPasswordFail: Fail");
			} else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
				nguiForgetPasswordSceneViewer.ServiceUnavailableSceneSetting();
				this.processState = NGUIForgetPasswordController.ProcessState.UNAVAILABLE;
//				Debug.Log("SendPasswordFail: Unavailable");
			}
		}
		
	}

	void OnFailedToConnect() {
//		Debug.Log("RegisterState OnFailedToConnect, Count:" + connectToGameLobbyCount);
		if(this.processState == NGUIForgetPasswordController.ProcessState.CONNECTING) {
			if(connectToGameLobbyCount-- > 0) {
				
				GetPasswordStart(userMemberID);
				
			}else{
				this.processState = NGUIForgetPasswordController.ProcessState.CONNECTIONFAIL;
				
				connectToGameLobbyCount = maxConnectToGameLobbyCount;
			}
		}
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			nguiForgetPasswordSceneViewer = FindObjectOfType(typeof(NGUIForgetPasswordSceneViewer)) as NGUIForgetPasswordSceneViewer;
		}
	}
}
