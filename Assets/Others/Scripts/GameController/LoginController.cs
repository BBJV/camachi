using UnityEngine;
using System;

public class LoginController : MonoBehaviour {

	private enum ProcessState {
		FREE,
		PREPARE,
		LOGINSTART,
		LOGINCONNECTING,
		LOGINCONNECTED,
		LOGINING,
		LOGINED,
		LOGINFAIL,
		LOGINFAIL_USEREXIST,
		UNAVAILABLE,
		KICKEDBYGAMELOBBY,
		//BACK,
		FINISH,
		
		PLAYERLOADING,
		PLAYERLOADED,
		PLAYERLOADERROR,
		
		STARTREDIRECT,
		REDIRECTING,
		REDIRECTED,
		REDIRECTERROR,
	}
	
	
	
	private LoginController.ProcessState processState = LoginController.ProcessState.PREPARE;
//	private String errorMessage = "";
	
	public GameUserPlayer carInsanityPlayer;
	public String gameLobbyIP = "127.0.0.1";
	public int gameLobbyPort = 50000;
	public int connectToGameLobbyCount = 5;
	
	public LoginSceneViewer loginSceneViewer;
	
	#region RPC
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_Login(String userName, String password) {
		
	}
	
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_LoadPlayerData(int gupid) {
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_Login(int gupid, String memberID, NetworkPlayer np, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
//		Debug.Log("ReceiveByClientPortal_Login called, p:"+p +" resultState: "+ resultState+ ". GUPID:"+gupid);
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
//			Debug.Log("resultState ==LoginController.RPCProcessState.SUCCESS");
			
			if(this.processState == LoginController.ProcessState.LOGINING) {
				//Debug.Log("this.processState == LoginController.ProcessState.LOGINING");
				
				if(Network.player == np) {
//					Debug.Log("Network.player == np :true");
					this.carInsanityPlayer.GUPID = gupid;
					this.carInsanityPlayer.memberID = memberID;
					this.carInsanityPlayer.networkPlayer = np;
					
					this.processState = LoginController.ProcessState.LOGINED;
					
					LoadPlayerStart();
				}else{ 
//					Debug.Log("Network.player == np");
				}
			}else{
//				Debug.Log("this.processState != LoginController.ProcessState.LOGINING");
				Network.Disconnect();
				this.processState = LoginController.ProcessState.LOGINFAIL;
				
				loginSceneViewer.LoginErrorSceneSetting();
			}			
		}else if(resultState == Definition.RPCProcessState.USEREXIST) {
//			Debug.Log("resultState == Definition.RPCProcessState.USEREXIST");
			
			this.processState = LoginController.ProcessState.LOGINFAIL_USEREXIST;
			loginSceneViewer.LoginErrorSceneSetting();
			Network.Disconnect();
		}else if(resultState == Definition.RPCProcessState.FAIL) {
//			Debug.Log("resultState == Definition.RPCProcessState.FAIL");
			Network.Disconnect();
			this.processState = LoginController.ProcessState.LOGINFAIL;
			loginSceneViewer.LoginErrorSceneSetting();
		}else{
//			Debug.Log("XXX");
			Network.Disconnect();
			loginSceneViewer.LoginErrorSceneSetting();
		}
	}
	
	
	[RPC] 
	public void ReceiveByClientPortal_LoadPlayerData(int gupid, string playerData, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			string[] playerDataArray = playerData.Split(',');
			
			carInsanityPlayer.playerID = Convert.ToInt32(playerDataArray[0]);
			carInsanityPlayer.playerName = playerDataArray[1];
			carInsanityPlayer.photo = playerDataArray[2];
			carInsanityPlayer.playerState = (Definition.ePlayerState)Enum.Parse(typeof(Definition.ePlayerState),playerDataArray[3]);
			
			SendMessage("LoadProductList", gupid, SendMessageOptions.DontRequireReceiver);
//			Application.LoadLevel(Definition.eSceneID.LobbyScene.ToString());
			
			
			
		}else{
			Network.Disconnect();
//			this.errorMessage = resultState.ToString();
//			Debug.Log(this.errorMessage);			
			loginSceneViewer.LoadPlayerErrorSceneSetting();
		}
	}
	
	
	
	
//	[RPC]
//	public void ReceiveByClientPortal_KickedByGameLobby(int gupid, int p) {
//		//if() {}
//		if(this.carInsanityPlayer.loginedGameUserPlayerID == gupid) {
//			Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
//			
//			if(resultState == Definition.RPCProcessState.SUCCESS) {
//				
//				KickByGameLobby();
//			}
//		}else{
//			Debug.Log("ReceiveByClientPortal_KickedByGameLobby : wrong gupid");
//		}
//	
//	}
	 
	#endregion
	
	private void LoginStart() {
		this.processState = LoginController.ProcessState.LOGINSTART;
		Network.Connect(gameLobbyIP, gameLobbyPort);
		loginSceneViewer.LoginingSceneSetting();

	}
	
	
	
	public void LoadPlayerStart() {
		
		if(this.processState == LoginController.ProcessState.LOGINED) {
			loginSceneViewer.LoadPlayerSceneSetting();
		
			networkView.RPC("SendToGameLobby_LoadPlayerData", RPCMode.Server, this.carInsanityPlayer.GUPID);

			this.processState = LoginController.ProcessState.PLAYERLOADING;
		}else{
			loginSceneViewer.LoadPlayerErrorSceneSetting();
		}
		
	}
	
	public void Login() {
		
		if((loginSceneViewer.lgnAccText3DClick.text == "") 
		   || (loginSceneViewer.lgnPwText3DClick.text == "") 
		   || (loginSceneViewer.lgnAccText3DClick.text == loginSceneViewer.lgnAccText3DClick.strBaseText) 
		   || (loginSceneViewer.lgnPwText3DClick.text == loginSceneViewer.lgnPwText3DClick.strBaseText)) {
			
			loginSceneViewer.LoginErrorSceneSetting();
//			this.errorMessage = "Please fill in all the text fields.";
//			Debug.Log(this.errorMessage);
		}else{
			
			loginSceneViewer.LoginingSceneSetting();
			
			if(loginSceneViewer.keepAccPassClick.isClicked) {
				PlayerPrefs.SetString("userAccount", loginSceneViewer.lgnAccText3DClick.text);
				PlayerPrefs.SetString("userPassword", loginSceneViewer.lgnPwText3DClick.text);
				PlayerPrefs.SetString("checkBoxOnOrNot","On");
				
			}else{
				PlayerPrefs.SetString("userAccount","");
				PlayerPrefs.SetString("userPassword","");
				PlayerPrefs.SetString("checkBoxOnOrNot","Off");
					
			}
			  
			this.LoginStart();
		}	
	}

	void OnConnectedToServer() {
		
		if(this.processState == LoginController.ProcessState.LOGINSTART) {
			networkView.RPC("SendToGameLobby_Login", RPCMode.Server, loginSceneViewer.lgnAccText3DClick.text, loginSceneViewer.lgnPwText3DClick.text);
			this.processState = LoginController.ProcessState.LOGINING;
		}
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			loginSceneViewer = GameObject.FindObjectOfType(typeof(LoginSceneViewer)) as LoginSceneViewer;
			
//			if(this.processState == LoginController.ProcessState.PREPARE) {
//				InitialSceneSetting();
//			}
			
			if(this.processState == LoginController.ProcessState.KICKEDBYGAMELOBBY) {
				loginSceneViewer.LoginErrorSceneSetting();
			}
		}
//		Debug.Log("Login:OnLevelWasLoaded");
	}
	
	void OnFailedToConnect() {
//		Debug.Log("LoginController OnFailedToConnect, Count:" + connectToGameLobbyCount);
		if(this.processState == LoginController.ProcessState.LOGINSTART) {
			if(connectToGameLobbyCount-- > 0) {
				LoginStart();
			}else{
				this.processState = LoginController.ProcessState.LOGINFAIL;
				loginSceneViewer.LoginErrorSceneSetting();
				connectToGameLobbyCount = 5;
			}
		}
	}
	
}