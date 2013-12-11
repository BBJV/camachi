using UnityEngine;
using System;
using System.Collections;

public class NGUILoginController : MonoBehaviour {

	public enum ProcessState {
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
		WRONGVERSIONNUMBER,
		//BACK,
		FINISH,
		
		PLAYERLOADING,
		PLAYERLOADED,
		PLAYERLOADERROR,
		
		LOGINEDMENU,
		
		STARTREDIRECT,
		REDIRECTING,
		REDIRECTED,
		REDIRECTERROR,
		
		DISCONNECTED,
		ENTEREDLOBBY,
		JOINEDMATCH,
		MATCHSTART,
	}
	
	public delegate void OnLoginedEventHandler();
	public static event OnLoginedEventHandler OnLogined;
	
	public delegate void OnOnLogoutedEventHandler();
	public static event OnOnLogoutedEventHandler OnLogouted;
	
	public delegate void OnLoginedManuEventHandler();
	public static event OnLoginedManuEventHandler OnLoginedManu;
	
	public delegate void OnPlayerLoadedEventHandler(GameUserPlayer player);
	public static event OnPlayerLoadedEventHandler OnPlayerLoaded;
	
	public delegate void OnGoldReloadedEventHandler(int totalGold);
	public static event OnGoldReloadedEventHandler OnGoldReloaded;
	
	public NGUILoginController.ProcessState processState = NGUILoginController.ProcessState.PREPARE;
//	private string errorMessage = "";
	
	public GameUserPlayer carInsanityPlayer;
	public string gameLobbyIP = "127.0.0.1";
	public int gameLobbyPort = 50000;
	public int connectToGameLobbyCount = 0;
	public int maxConnectToGameLobbyCount = 0;
	
	public ProductController productController;
	
	public NGUILoginSceneViewer nguiLoginSceneViewer;
	
	void Awake() {
		PlayGameController.OnDisconnectedFromGameServer += OnDisconnectedFromGameServer;
		PlayGameController.OnFailedToConnectToGameServer += OnFailedToConnectToGameServer;
		LobbyController.OnEnteredLobby += OnEnteredLobby;
		MatchController.OnJoinedMatch += OnJoinedMatch;
		MatchController.OnMatchStarted += OnMatchStarted;
		
	}
	
	void OnDestroy() {
		PlayGameController.OnDisconnectedFromGameServer -= OnDisconnectedFromGameServer;
		PlayGameController.OnFailedToConnectToGameServer -= OnFailedToConnectToGameServer;
		LobbyController.OnEnteredLobby -= OnEnteredLobby;
		MatchController.OnJoinedMatch -= OnJoinedMatch;
		MatchController.OnMatchStarted -= OnMatchStarted;
	}
	
	#region RPC
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_Login(string userName, string password, string versionNumber) {
		
	}
	
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_LoadPlayerData(int gupid) {
		
	}
	
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_ReloadGold(int gupid) {
		
	}
	
	
	[RPC]
	public void ReceiveByClientPortal_Login(int gupid, string memberID, NetworkPlayer np, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
//		Debug.Log("ReceiveByClientPortal_Login called, p:"+p +" resultState: "+ resultState+ ". GUPID:"+gupid);
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
//			Debug.Log("resultState ==LoginController.RPCProcessState.SUCCESS");
			
			if(this.processState == NGUILoginController.ProcessState.LOGINING) {
				//Debug.Log("this.processState == NGUILoginController.ProcessState.LOGINING");
				
				if(Network.player == np) {
//					Debug.Log("Network.player == np :true");
					this.carInsanityPlayer.GUPID = gupid;
					this.carInsanityPlayer.memberID = memberID;
					this.carInsanityPlayer.networkPlayer = np;
					
					this.processState = NGUILoginController.ProcessState.LOGINED;
					
					LoadPlayerStart();
				}else{ 
//					Debug.Log("Network.player == np");
				}
			}else{
//				Debug.Log("this.processState != NGUILoginController.ProcessState.LOGINING");
				Network.Disconnect();
				this.processState = NGUILoginController.ProcessState.LOGINFAIL;
				
				nguiLoginSceneViewer.LoginErrorSceneSetting();
			}			
		}else if(resultState == Definition.RPCProcessState.USEREXIST) {
//			Debug.Log("resultState == Definition.RPCProcessState.USEREXIST");
			
			this.processState = NGUILoginController.ProcessState.LOGINFAIL_USEREXIST;
			nguiLoginSceneViewer.AccountIsBeingUsedSceneSetting();
			Network.Disconnect();
		}else if(resultState == Definition.RPCProcessState.FAIL) {
//			Debug.Log("resultState == Definition.RPCProcessState.FAIL");
			Network.Disconnect();
			this.processState = NGUILoginController.ProcessState.LOGINFAIL;
			nguiLoginSceneViewer.LoginFailSceneSetting();
		}else{
//			Debug.Log("XXX");
			Network.Disconnect();
			nguiLoginSceneViewer.LoginErrorSceneSetting();
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
			carInsanityPlayer.money = Convert.ToInt32(playerDataArray[4]);
			
			productController.SendMessage("LoadProductList", gupid, SendMessageOptions.DontRequireReceiver);
//			Application.LoadLevel(Definition.eSceneID.LobbyScene.ToString());
			
			
			this.processState = NGUILoginController.ProcessState.LOGINEDMENU;
			nguiLoginSceneViewer.LoginedMenuSceneSetting();
			
			if( OnPlayerLoaded != null) {
				OnPlayerLoaded(carInsanityPlayer);
			}
			
			if(OnLoginedManu != null) {
				OnLoginedManu();
			}
			
			
			
			if(OnLogined != null) {
				OnLogined();
			}
			
			
			
		}else{
			Network.Disconnect();
//			this.errorMessage = resultState.ToString();
//			Debug.Log(this.errorMessage);			
			nguiLoginSceneViewer.LoadPlayerErrorSceneSetting();
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_GainLoginReward(int gupid, int gold, int totalGold, int p){
//		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(this.carInsanityPlayer.GUPID == gupid) {
			Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
//				Debug.Log("ReceiveByClientPortal_GainLoginReward");
				nguiLoginSceneViewer.GainLoginRewardSceneSetting(gold);	
				carInsanityPlayer.money = totalGold;
				OnGoldReloaded(totalGold);
				
			}
		}else{
//			Debug.Log("ReceiveByClientPortal_KickedByGameLobby : wrong gupid");
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_ReloadGold(int gupid, int gold, int p) {
		
		if(this.carInsanityPlayer.GUPID == gupid) {
			Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				carInsanityPlayer.money = gold;
				OnGoldReloaded(gold);
			}
		}else{

		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_KickedByGameLobby(int gupid, int p) {
		//if() {}
		if(this.carInsanityPlayer.GUPID == gupid) {
			Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {
				Network.Disconnect();
				this.processState = NGUILoginController.ProcessState.KICKEDBYGAMELOBBY;
				
				Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
			}
		}else{
//			Debug.Log("ReceiveByClientPortal_KickedByGameLobby : wrong gupid");
		}
	
	}
	 
	[RPC]
	public void ReceiveByClientPortal_WrongVersionNumber() {
		
		Network.Disconnect();
		this.processState = NGUILoginController.ProcessState.WRONGVERSIONNUMBER;
		
		Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
	}
	#endregion
	
	
	
	private void LoginStart() {
		this.processState = NGUILoginController.ProcessState.LOGINSTART;
		
		nguiLoginSceneViewer.ConnectingToServerSceneSetting();
		
		Network.Connect(gameLobbyIP, gameLobbyPort);


	}
	
	public void LoadPlayerStart() {
		
		if(this.processState == NGUILoginController.ProcessState.LOGINED || this.processState == NGUILoginController.ProcessState.LOGINEDMENU) {
			nguiLoginSceneViewer.LoadingPlayerSceneSetting();
		
			networkView.RPC("SendToGameLobby_LoadPlayerData", RPCMode.Server, this.carInsanityPlayer.GUPID);

			this.processState = NGUILoginController.ProcessState.PLAYERLOADING;
		}else{
			nguiLoginSceneViewer.LoadPlayerErrorSceneSetting();
		}
		
	}
	
	public void Login() {
		
		if((nguiLoginSceneViewer.lgnAccText3DClick.text == "") 
		   || (nguiLoginSceneViewer.lgnPwText3DClick.text == "") 
		   || (nguiLoginSceneViewer.lgnAccText3DClick.text == nguiLoginSceneViewer.lgnAccText3DClick.strBaseText) 
		   || (nguiLoginSceneViewer.lgnPwText3DClick.text == nguiLoginSceneViewer.lgnPwText3DClick.strBaseText)) {
			
			nguiLoginSceneViewer.LoginErrorSceneSetting();
//			this.errorMessage = "Please fill in all the text fields.";
//			Debug.Log(this.errorMessage);
		}else{
			
			if(nguiLoginSceneViewer.nguiKeepAccountAndPasswordClick.uiCheckBox.isChecked) {
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.USERACCOUNT.ToString(), nguiLoginSceneViewer.lgnAccText3DClick.text);
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.USERPASSWORD.ToString(), nguiLoginSceneViewer.lgnPwText3DClick.text);
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.CHECKBOXONORNOT.ToString(),"On");
				
			}else{
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.USERACCOUNT.ToString(),"");
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.USERPASSWORD.ToString(),"");
				PlayerPrefs.SetString(Definition.ePlayerPrefabKey.CHECKBOXONORNOT.ToString(),"Off");
					
			}
			  
			this.LoginStart();
		}	
	}
	
	public void Back() {
		this.processState = NGUILoginController.ProcessState.LOGINEDMENU;
		
		
		Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
	}
	
	public void Logout() {
		Network.Disconnect();
//		Debug.Log ("Logout");
		processState = NGUILoginController.ProcessState.PREPARE;
		OnLogouted();
		nguiLoginSceneViewer.UnloginMenuSceneSetting();
	}
	
	void OnMatchStarted() {
		
		if(PlayerPrefs.GetString("GameType","Single") == "Network") {
			processState = NGUILoginController.ProcessState.MATCHSTART;
		}
		
	}
	
	void OnJoinedMatch() {
		processState = NGUILoginController.ProcessState.JOINEDMATCH;
	}
	
	void OnEnteredLobby() {
		processState = NGUILoginController.ProcessState.ENTEREDLOBBY;
	}
	
	void OnDisconnectedFromGameServer() {
//		Debug.Log ("OnDisconnectedFromGameServer");
		processState = NGUILoginController.ProcessState.DISCONNECTED;
		Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
	}
	
	void OnFailedToConnectToGameServer() {
//		Debug.Log ("OnFailedToConnectToGameServer");
		processState = NGUILoginController.ProcessState.DISCONNECTED;
		Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
	}
	
	void OnDisconnectedFromServer() {
		if((processState == ProcessState.LOGINEDMENU) ||
			(processState == ProcessState.ENTEREDLOBBY) ||
			(processState == ProcessState.JOINEDMATCH)) {
//			Debug.Log ("OnDisconnectedFromServer");
			processState = NGUILoginController.ProcessState.DISCONNECTED;
			Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
		}
	}
	
	void OnConnectedToServer() {
		
		if(this.processState == NGUILoginController.ProcessState.LOGINSTART) {
			networkView.RPC("SendToGameLobby_Login", RPCMode.Server, nguiLoginSceneViewer.lgnAccText3DClick.text, nguiLoginSceneViewer.lgnPwText3DClick.text, Definition.versionNumber);
			this.processState = NGUILoginController.ProcessState.LOGINING;
			
			nguiLoginSceneViewer.LoginingSceneSetting();
		}
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LoginScene_ngui) {
			
			nguiLoginSceneViewer = GameObject.FindObjectOfType(typeof(NGUILoginSceneViewer)) as NGUILoginSceneViewer;
			
//			if(this.processState == NGUILoginController.ProcessState.PREPARE) {
//				InitialSceneSetting();
//			}
			
			if(this.processState == NGUILoginController.ProcessState.KICKEDBYGAMELOBBY) {
				nguiLoginSceneViewer.KickByGameLobbySceneSetting();
//				Debug.Log ("KICKEDBYGAMELOBBY");
				this.processState = NGUILoginController.ProcessState.PREPARE;
				OnLogouted();
			}
			
			if(this.processState == NGUILoginController.ProcessState.WRONGVERSIONNUMBER) {
				nguiLoginSceneViewer.WrongVersionNumberSceneSetting();
//				Debug.Log ("WRONGVERSIONNUMBER");
				this.processState = NGUILoginController.ProcessState.PREPARE;
				OnLogouted();
			}
			
			if(this.processState == NGUILoginController.ProcessState.LOGINEDMENU) {
//				nguiLoginSceneViewer.LoginedMenuSceneSetting();
//				OnLoginedManu();
				LoadPlayerStart();
			}
			
			if(this.processState == NGUILoginController.ProcessState.DISCONNECTED) {
				nguiLoginSceneViewer.FailedToConnectToServerSceneSetting();
//				Debug.Log ("DISCONNECTED");
				this.processState = NGUILoginController.ProcessState.PREPARE;
				OnLogouted();
			}
		}
//		Debug.Log("Login:OnLevelWasLoaded");
	}
	
	void OnFailedToConnect() {
//		Debug.Log("NGUILoginController OnFailedToConnect, Count:" + connectToGameLobbyCount);
		if(this.processState == NGUILoginController.ProcessState.LOGINSTART) {
			if(connectToGameLobbyCount-- > 0) {
				LoginStart();
			}else{
				this.processState = NGUILoginController.ProcessState.LOGINFAIL;
				nguiLoginSceneViewer.FailedToConnectToServerSceneSetting();
				connectToGameLobbyCount = maxConnectToGameLobbyCount;
			}
		}
	}
}
