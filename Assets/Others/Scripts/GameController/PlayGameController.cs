using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayGameController : MonoBehaviour {
	
	public enum ProcessState {
		FREE,

		CONNECTING,
		CONNECTEDTOGAMESERVER,
		
		
		STARTPLAYCOUNTDOWN,
		PLAY,
		ARRIVED,
		BALANCE,
		
		STARTREDIRECT,
		REDIRECTING,
		REDIRECTED,
		REDIRECTERROR,
		
		FINISH,
		MASTERSERVERFAIL,
		
		MATCHCOMPLETED,
	}
	
	public delegate void OnLoadingMapEventHandler();	
	public static event OnLoadingMapEventHandler OnLoadingMap; 
	
	public delegate void OnGameStartEventHandler();	
	public static event OnGameStartEventHandler OnGameStart; 
	
	public delegate void OnShowRecordEventHandler();	
	public static event OnShowRecordEventHandler OnShowRecord;
	
	public delegate void OnShowGainGoldEventHandler(int gainGold, int rank);	
	public static event OnShowGainGoldEventHandler OnShowGainGold;
	
	
	
	public delegate void OnCarArrivedEventHandler(CarInsanityPlayer player, string record, int rankNumber, bool isOwnedPlayer);	
	public static event OnCarArrivedEventHandler OnCarArrived; 
	
	public delegate void OnDisconnectedFromGameServerEventHandler();
	public static event OnDisconnectedFromGameServerEventHandler OnDisconnectedFromGameServer;
	
	public delegate void OnFailedToConnectToGameServerEventHandler();
	public static event OnFailedToConnectToGameServerEventHandler OnFailedToConnectToGameServer;
	
	public PlayGameController.ProcessState processState = PlayGameController.ProcessState.FREE;
	
	public string gameServerIP = "";
	public int gameServerPort = 0;

	private int playingRoomIndex;
	private int playingPlayerNumber;
	private int playingMapID;
	private String playingMatchType;
	private int playingGUPID;
	private int playingPlayerID;
	private int playingPlayerCarID;
	private int playingCarID;
	private String playingPlayerName;
	private int playingIsCreator;
	private int playingIsAI;

	public GameUserPlayer playerInfo;
	
	public RedirectController redirectController;
//	public PlayGameSceneViewer playGameSceneViewer;
	
	public MatchRoom matchRoom;
	private DynamicMaterial dynamicMaterial;
	private CountDownBat countDownBat;
	
	public bool isGameInitialized = false;
	private bool isCountDownAnimaitonStarted = false;
	public bool isDraw = true;
	
//	public Transform[] carPrefabs;
//	public Transform playerCar;
	public int maxConnectToGameServerCount = 5;
	public int connectToGameServerCount = 5;
	
	
	#region GUI
	private GUISkin playGameSkin;
	
//	private float timeUpLimited = 30.0f;
	
//	private bool isShowFinishCountDown = false;
	private bool isFinishPlayCountDown = false;
	private float startPlayCountDown = 0.0f;
	private float finishPlayCountDown = 9.0f;
	
	

//	private string LoadingScreen_1 = "LoadingScreen_1";
//	private string CarIcon_ = "CarIcon_";
//	private string Font_White = "Font_White";
	private string Label_ = "Label_";
	private string _A = "_A";
	private string _B = "_B";
	
	private string strEmpty = "";	
//	private string strRecord = "Record";
//	private string strClose = "Close";
//	private string strSpace = " ";
//	private string strColon = ":";
//	private string strName = "Name";
//	private string strWaitingForOtherPlayers = "waiting for other players...";
//	private string strTheGameIsLoading = "The Game Is Loading...";
//	private string strLoadingEstimatedTime = "Estimated Time: 100 Seconds";
//	private string strLoadingElapsedTime = "Elapsed Time";
//	private string strSeconds = "Seconds";
	private float elapsedTime = 0.0f;

	private Rect loadingScreenWindow;
	
	private Rect recordWindow;
//	private float recordWidht = 400.0f;
//	private float recordHight = 200.0f;

	private float startPlayCountDownX;
	private float startPlayCountDownY;
	private float startPlayCountDownStartX;
	private float startPlayCountDownStartY;
	private float startPlayCountDownTextureWidth = 100.0f;
	private float startPlayCountDownTextureHeight = 100.0f;
	private float startPlayCountDownWidth;
	private float startPlayCountDownHeight;
	
	private float finishPlayCountDownX;
	private float finishPlayCountDownY;
	private float finishPlayCountDownStartX;
	private float finishPlayCountDownStartY;
	private float finishPlayCountDownTextureWidth = 70.0f;
	private float finishPlayCountDownTextureHeight = 70.0f;
	private float finishPlayCountDownWidth;
	private float finishPlayCountDownHeight;
	public AudioClip start1Audio;
	public AudioClip start2Audio;
	
	public delegate void OnGameSceneLoadSuccessEventHandler();
	public static event OnGameSceneLoadSuccessEventHandler OnLoadSuccess;
	
	void OnGUI () {
		
		if(isDraw) {	
			GUI.skin = playGameSkin;
			
//			if(!isGameInitialized) {
//				loadingScreenWindow = new Rect(0, 0, Screen.width, Screen.height);
//				loadingScreenWindow = GUI.Window(1, loadingScreenWindow, drawLoadingScreenWindow, strEmpty, LoadingScreen_1);
//			}
			
//			if(this.processState == PlayGameController.ProcessState.ARRIVED) {
//				recordWindow = new Rect((Screen.width-recordWidht)*0.5f, (Screen.height-recordHight)*0.5f, recordWidht, recordHight);
//				recordWindow = GUI.Window(99, recordWindow, drawArrivedRecordWindow, strRecord);
//				
//				
//			}
//			
//			if(this.processState == PlayGameController.ProcessState.BALANCE) {
//				
//				recordWindow = new Rect((Screen.width - recordWidht)*0.5f, (Screen.height - recordHight)*0.5f, recordWidht, recordHight);
//				recordWindow = GUI.Window(99, recordWindow, drawBalanceRecordWindow, strRecord);
//				
//				return;
//			}
			
			
			if(this.processState == PlayGameController.ProcessState.STARTPLAYCOUNTDOWN) {
				
				startPlayCountDownWidth = startPlayCountDownTextureWidth * Screen.width / 1024;
				startPlayCountDownHeight = startPlayCountDownTextureHeight * Screen.height / 768;	
				
				startPlayCountDownX = (Screen.width - startPlayCountDownWidth) * 0.5f;
				startPlayCountDownY = (Screen.height - startPlayCountDownHeight) * 0.1f;
				
					
					
				GUI.Label(new Rect(startPlayCountDownX, startPlayCountDownY, startPlayCountDownWidth, startPlayCountDownHeight), strEmpty, Label_+this.startPlayCountDown+_A);
				
				return;
			}
			
			if(isFinishPlayCountDown) {
				finishPlayCountDownWidth = finishPlayCountDownTextureWidth * Screen.width / 1024;
				finishPlayCountDownHeight = finishPlayCountDownTextureHeight * Screen.height / 768;	
				
				finishPlayCountDownX = (Screen.width - finishPlayCountDownWidth) * 0.5f;
				finishPlayCountDownY = (Screen.height - finishPlayCountDownHeight) * 0.25f;
				
//				Debug.Log("Convert.ToInt32(this.finishPlayCountDown):"+ Convert.ToInt32(this.finishPlayCountDown));
					
				GUI.Label(new Rect(finishPlayCountDownX, finishPlayCountDownY, finishPlayCountDownWidth, finishPlayCountDownHeight), strEmpty, Label_+Convert.ToInt32(this.finishPlayCountDown)+_B);
				
				return;
			}
		}else{
			GUI.skin = null;
		}
	}
	
//	private void drawLoadingScreenWindow(int id) {
//		GUILayout.BeginHorizontal();					
//		foreach(GameUserPlayer driver in this.matchRoom.GetComponent<Room>().playersInRoom) {
//			
//			GUILayout.BeginVertical();
//				GUILayout.Label(driver.playerName, Font_White);
//				GUILayout.Label(strEmpty, CarIcon_+((Definition.eCarID)driver.GetComponent<CarInsanityPlayer>().selectedCar.carID).ToString(), GUILayout.Height(Screen.height * 0.2f), GUILayout.Width(Screen.height * 0.2f));
//			GUILayout.EndVertical();
//		}
//		GUILayout.EndHorizontal();
//		
//		GUILayout.Label(strTheGameIsLoading);
//		GUILayout.Space(10);
//		GUILayout.Label(strLoadingEstimatedTime);
//		GUILayout.Label(strLoadingElapsedTime + strColon+strSpace + elapsedTime + strSpace+strSeconds);
//		
//	}
	
//	private void drawArrivedRecordWindow(int id) {
//		int c = 1;
//		foreach(KeyValuePair<int, String> kvp in this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordDic()) {
//		
//			GUILayout.BeginHorizontal();
//				GUILayout.Label(c.ToString() + strSpace+strName+strColon + this.matchRoom.GetComponent<Room>().GetPlayer(kvp.Key).playerName + strSpace + strRecord + strColon + kvp.Value);
//			GUILayout.EndHorizontal();
//		}
//		
//		GUILayout.Label(strWaitingForOtherPlayers);
//	}
	
//	private void drawBalanceRecordWindow(int id) {
//		int c = 1;
//		if(this.matchRoom)
//		{
//			foreach(KeyValuePair<int, String> kvp in this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordDic()) {
//				GUILayout.BeginHorizontal();
//					GUILayout.Label(c.ToString() + strSpace+strName+strColon + this.matchRoom.GetComponent<Room>().GetPlayer(kvp.Key).playerName + strSpace + strRecord + strColon + kvp.Value);
//				GUILayout.EndHorizontal();
//			}
//		}
//		
//		if(GUILayout.Button(strClose)) {
//			
//			Destroy(this.matchRoom.gameObject);
//			//this.matchRoom = null;
//			redirectController.RedirectStart();
//		}
//		
//	}
	
	void OnCompletedAMatch() {
		processState = PlayGameController.ProcessState.MATCHCOMPLETED;
		Destroy(this.matchRoom.gameObject);
	}
	
	#endregion
	

	public void StartPreparingForPlay() {
		
//		Debug.Log(PlayerPrefs.GetString("GameType"));
		if(PlayerPrefs.GetString("GameType","Single") == "Network") {
			Network.Disconnect();
		}
		

		this.matchRoom = GameObject.FindObjectOfType(typeof(MatchRoom)) as MatchRoom;
		
		this.playingRoomIndex = this.matchRoom.GetComponent<Room>().roomIndex;
		this.playingPlayerNumber = this.matchRoom.transform.GetChildCount();
		this.playingMapID = this.matchRoom.matchMap;
		this.playingMatchType = this.matchRoom.matchType;
		this.playingGUPID = this.playerInfo.GUPID;
		this.playingPlayerID = this.playerInfo.playerID;
		this.playingPlayerCarID = this.playerInfo.GetComponent<CarInsanityPlayer>().selectedCar.ID;
		this.playingCarID = this.playerInfo.GetComponent<CarInsanityPlayer>().selectedCar.carID;
		this.playingPlayerName = this.playerInfo.playerName;
		this.playingIsAI = Convert.ToInt32(this.playerInfo.isAI);
		
		if(this.playerInfo.GUPID == this.matchRoom.GetComponent<Room>().creator.GUPID) {
			this.playingIsCreator = 1;
		}else{
			this.playingIsCreator = 0;
		}
		
		StartCoroutine(LoadLevelWithProgress(((Definition.eSceneID)this.playingMapID).ToString()));
		
		StartCoroutine(CountingElapsedTime());
		
		//Debug.Log("StartPreparingForPlay");
	}
	
	
	public void SendWin (float driftTime, float useSkill, int beHitTime, int gupid) {
//		Debug.Log("SendWin.GUPID:"+gupid);
		networkView.RPC("SendToGameServer_FinishPlay", RPCMode.Server, playingRoomIndex, driftTime, useSkill, beHitTime, gupid);
		
		if(playingGUPID == gupid) { //may be ai win
			if(this.processState != PlayGameController.ProcessState.BALANCE) {
				this.processState = PlayGameController.ProcessState.ARRIVED;
//				OnShowRecord();
			}
		}
	}
	
	IEnumerator OnLevelWasLoaded(int mapID) {
		if(mapID == this.playingMapID) {
			readyStartPlay = false;
			isDraw = true;
			
			
			this.playGameSkin = Resources.Load("Skin/CarInsanity", typeof(GUISkin)) as GUISkin;
			
			if(PlayerPrefs.GetString("GameType", "Single") == "Network")
			{
				this.processState = PlayGameController.ProcessState.CONNECTING;
				Network.Connect(gameServerIP, gameServerPort);
				enabled = true;
			}
			else
			{
				Room room = this.matchRoom.GetComponent<Room>();
				for(int i = 0; i < 4; i++)
				{
					GameUserPlayer targetPlayer =  room.GetPlayer(i);
					CarInsanityPlayer playerCarInfo = targetPlayer.GetComponent<CarInsanityPlayer>();
					
					GameObject[] startPositionArray =  GameObject.FindGameObjectsWithTag("StartPosition");
					foreach(GameObject position in startPositionArray ) {
						
						if(Convert.ToInt32(position.name) == i + 1) {
//							foreach(Transform cp in carPrefabs) {
//								if(cp.name == ((Definition.eCarID)(targetPlayer.GetComponent<CarInsanityPlayer>().selectedCar.carID)).ToString()) {
//									playerCar = cp;
//								}
//							}
							
							CarProperty tf = Instantiate(Resources.Load(((Definition.eCarID)(playerCarInfo.selectedCar.carID)).ToString(), typeof(CarProperty)), position.transform.position, position.transform.rotation) as CarProperty;
							tf.ownerGUPID = targetPlayer.GUPID;
							tf.playerName = targetPlayer.playerName;
							if(playerCarInfo.selectedCar.isTalentOpened)
							{
								switch(((Definition.eCarID)(playerCarInfo.selectedCar.carID)).ToString())
								{
									case "POLICE_CAR" :
										tf.gameObject.AddComponent<PolicePassiveSkill>();
										break;
									case "LORRY_TRUCK" :
										tf.gameObject.AddComponent<TruckPassiveSkill>();
										break;
									case "AMBULANCE" :
										tf.gameObject.AddComponent<AmbulancePassiveSkill>();
										break;
									case "FIRE_FIGHTING_TRUCK" :
										tf.gameObject.AddComponent<FireFightPassiveSkill>();
										break;
									case "GARBAGE_TRUCK" :
									case "MASCOT_TRUCK" :
										tf.gameObject.AddComponent<GarbagePassiveSkill>();
										break;
									case "OLDCLASS_TRUCK" :
									case "GODOFWEALTH_TRUCK" :
										OldclassPassiveSkill passiveSkill = tf.gameObject.AddComponent<OldclassPassiveSkill>();
										if(targetPlayer.GUPID == playingGUPID) {
											passiveSkill.PassiveSkillInit();
										}
										break;
									case "HUMMER_TRUCK" :
										tf.gameObject.AddComponent<HummerPassiveSkill>();
										break;
								}
							}
//							tf.networkView.viewID = viewID;
			
							if(playingIsCreator == 1) {
								if(targetPlayer.isAI) {
			
									if(tf.GetComponent<CarAI>())
										tf.GetComponent<CarAI>().enabled = true;
									
//									Debug.Log("SendToGameServer_Ready - GUPID:"+targetGUPID+" (target is AI)");
//									networkView.RPC("SendToGameServer_Ready", RPCMode.Server, playingRoomIndex, targetGUPID, tf.networkView.viewID, position.transform.position, position.transform.rotation);
								}
							}
							 
							if(targetPlayer.GUPID == playingGUPID) {
//								Debug.Log("SendToGameServer_Ready - GUPID:"+targetGUPID+"(i m the target)");
//								networkView.RPC("SendToGameServer_Ready", RPCMode.Server, playingRoomIndex, targetGUPID, tf.networkView.viewID, position.transform.position, position.transform.rotation);
							}
							else
							{
//								Debug.Log("SendToGameServer_ReplyInitialData - GUPID:"+targetGUPID);
//								networkView.RPC("SendToGameServer_ReplyInitialData", RPCMode.Server, playingRoomIndex, targetGUPID);
							}
			
							break;
						}	
					}
				}
				Resources.UnloadUnusedAssets();
				
				ReceiveByClientPortal_StartPlayCountDown(5);
//				if(OnLoadSuccess != null)
//				{
//					OnLoadSuccess();
//				}
				enabled = true;
				for(int i = 5; i >= 0; i--)
				{
					startPlayCountDown = i;
					if(i != 0)
					{
						AudioSource.PlayClipAtPoint(start1Audio, Camera.main.transform.position);
					}
					else
					{
						AudioSource.PlayClipAtPoint(start2Audio, Camera.main.transform.position);
					}
					yield return new WaitForSeconds(1);
				}
				enabled = false;
				ReceiveByClientPortal_StartPlay();
			}
		}else{
			this.processState = PlayGameController.ProcessState.FREE;
			this.isGameInitialized = false;
			this.isCountDownAnimaitonStarted = false;
			this.isFinishPlayCountDown = false;
			isDraw = false;
			this.playGameSkin = null;
			
			enabled = false;
		}
	}
	
	void OnConnectedToServer() {
		
		if(this.processState == PlayGameController.ProcessState.CONNECTING) {
//			networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, playingGUPID, playingCarID, playingPlayerName, playingIsCreator, playingPlayerNumber, playingMapID, playingMatchType, playingIsAI, Network.AllocateViewID());
			networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, playingGUPID, playingPlayerID, playingPlayerCarID, playingCarID, playingPlayerName, playingIsCreator, playingPlayerNumber, playingMapID, playingMatchType, playingIsAI, Network.AllocateViewID());
			processState = ProcessState.CONNECTEDTOGAMESERVER;
			//for ai
			if(playingIsCreator == 1) { 
				// i am creator
				foreach(GameUserPlayer d in this.matchRoom.GetComponent<Room>().playersInRoom) {
					if(d.isAI) {
						
//						networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
						networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, 0, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
					}
				}
			}
		}		
	}
	
	#region RPC
	
	#region SendToGameServer
	[RPC]
	public void SendToGameServer_JoinMatch(int roomIndex, int GUPID, int Player_ID, int playerCarID, int CarID, String playerName, int isCreator, int playerNumber, /*int observerNumber,*/ int mapID, String matchType, int isAI, /*int isObserver,*/ NetworkViewID viewID) {
		
	}
	
//	[RPC]
//	public void SendToGameServer_JoinMatch(int roomIndex, int GUPID, int CarID, String playerName, int isCreator, int playerNumber, /*int observerNumber,*/ int mapID, String matchType, int isAI, /*int isObserver,*/ NetworkViewID viewID) {
//		
//	}
	
	[RPC]
	public void SendToGameServer_Ready(int roomIndex, int GUPID, NetworkViewID nvid, Vector3 posistion, Quaternion rotation) {
		
	}
	
	[RPC]
	public void SendToGameServer_FinishPlay(int roomIndex, float driftTime, float useSkill, int beHitTime, int GUPID) {
		
	}
	
	[RPC]
	public void SendToGameServer_ReplyInitialData(int roomIndex, int GUPID) {
		
	}
	
	[RPC]
	public void SendToGameServer_AIHandOffInitial(int roomIndex, int gupid, int AIgupid, NetworkViewID vid) {
		
	}
	
	[RPC]
	public void SendToGameServer_ReplyAIHandOffInitial(int roomIndex, int AIgupid, NetworkViewID vid) {
		
	}
	
	[RPC]
	public void SendToGameServer_StartSendAllPlayerInitialData(int roomIndex) {
		
	}
	
	
	#endregion
	
	#region ReceiveByClientPortal
	[RPC]
	public void ReceiveByClientPortal_InitialData(int targetGUPID, int startPosition, NetworkViewID viewID) {
//		Debug.Log("ReceiveByClientPortal_InitialData - GUPID:"+targetGUPID+", StartPosition:"+startPosition);
		GameUserPlayer targetPlayer =  this.matchRoom.GetComponent<Room>().GetPlayer(targetGUPID);
		CarInsanityPlayer playerCarInfo = targetPlayer.GetComponent<CarInsanityPlayer>();
		
		GameObject[] startPositionArray =  GameObject.FindGameObjectsWithTag("StartPosition");

		foreach(GameObject position in startPositionArray ) {
			
			if(Convert.ToInt32(position.name) == startPosition) {
//				Debug.Log("startPosition:"+startPosition);
//				foreach(Transform cp in carPrefabs) {
//					if(cp.name == ((Definition.eCarID)(targetPlayer.GetComponent<CarInsanityPlayer>().selectedCar.carID)).ToString()) {
//						playerCar = cp;
//					}
//				}
				
				CarProperty tf = Instantiate(Resources.Load(((Definition.eCarID)(playerCarInfo.selectedCar.carID)).ToString(), typeof(CarProperty)), position.transform.position, position.transform.rotation) as CarProperty;
				tf.ownerGUPID = targetPlayer.GUPID;
				tf.playerName = targetPlayer.playerName;
				if(playerCarInfo.selectedCar.isTalentOpened)
				{
					switch(((Definition.eCarID)(playerCarInfo.selectedCar.carID)).ToString())
					{
						case "POLICE_CAR" :
							tf.gameObject.AddComponent<PolicePassiveSkill>();
							break;
						case "LORRY_TRUCK" :
							tf.gameObject.AddComponent<TruckPassiveSkill>();
							break;
						case "AMBULANCE" :
							tf.gameObject.AddComponent<AmbulancePassiveSkill>();
							break;
						case "FIRE_FIGHTING_TRUCK" :
							tf.gameObject.AddComponent<FireFightPassiveSkill>();
							break;
						case "GARBAGE_TRUCK" :
						case "MASCOT_TRUCK" :
							tf.gameObject.AddComponent<GarbagePassiveSkill>();
							break;
						case "OLDCLASS_TRUCK" :
						case "GODOFWEALTH_TRUCK" :
							OldclassPassiveSkill passiveSkill = tf.gameObject.AddComponent<OldclassPassiveSkill>();
							if(targetPlayer.GUPID == playingGUPID) {
								passiveSkill.PassiveSkillInit();
							}
							break;
						case "HUMMER_TRUCK" :
							tf.gameObject.AddComponent<HummerPassiveSkill>();
							break;
					}
				}
				tf.networkView.viewID = viewID;

				if(playingIsCreator == 1) {
					if(targetPlayer.isAI) {

						if(tf.GetComponent<CarAI>())
							tf.GetComponent<CarAI>().enabled = true;
						
//						Debug.Log("SendToGameServer_Ready - GUPID:"+targetGUPID+" (target is AI)");
						networkView.RPC("SendToGameServer_Ready", RPCMode.Server, playingRoomIndex, targetGUPID, tf.networkView.viewID, position.transform.position, position.transform.rotation);
					}
				}
				 
				if(targetPlayer.GUPID == playingGUPID) {
//					Debug.Log("SendToGameServer_Ready - GUPID:"+targetGUPID+"(i m the target)");
					networkView.RPC("SendToGameServer_Ready", RPCMode.Server, playingRoomIndex, targetGUPID, tf.networkView.viewID, position.transform.position, position.transform.rotation);
				}
				else
				{
//					Debug.Log("SendToGameServer_ReplyInitialData - GUPID:"+targetGUPID);
					networkView.RPC("SendToGameServer_ReplyInitialData", RPCMode.Server, playingRoomIndex, targetGUPID);
				}

				return;
			}	
		}
		Resources.UnloadUnusedAssets();
	}
	
	[RPC]
	public void ReceiveByClientPortal_Ready(int gupid, NetworkViewID nvid, Vector3 pos, Quaternion rot) {
//		Debug.Log("ReceiveByClientPortal_Ready");
		CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
		
		foreach(CarProperty cp in cars) {
			if(cp.ownerGUPID == gupid) {
				cp.transform.position = pos;
				cp.transform.rotation = rot;

				if(gupid == this.playerInfo.GUPID) {

//					if(countDownBat == null) {
//						countDownBat = Camera.main.GetComponentInChildren<CountDownBat>();
//					}
//		
//					countDownBat.SetStartFlyIn();
					
				}else{
					cp.networkView.viewID = nvid;
				}
				
				return;
			}
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_StartPlay() {
//		Debug.Log("ReceiveByClientPortal_StartPlay");
		this.matchRoom.isMatchStart = true;
		readyStartPlay = false;
		if(countDownBat == null) {
			countDownBat = Camera.main.GetComponentInChildren<CountDownBat>();
		}
		
//		countDownBat.SetStartFlyOut();
		
		LittleMap mapCamera = FindObjectOfType(typeof(LittleMap)) as LittleMap;
		mapCamera.SetCars();
		
		RankManager rankManager = FindObjectOfType(typeof(RankManager)) as RankManager;
		rankManager.SetRanksCar();
		this.processState = PlayGameController.ProcessState.PLAY;
		
		if(OnGameStart != null) {
			OnGameStart();
		}
		
		
	}
	
//	private bool isOpenMapCamera = false;
	private bool readyStartPlay = false;
	[RPC]
	public void ReceiveByClientPortal_StartPlayCountDown(float second) {
		if(!readyStartPlay)
		{
			readyStartPlay = true;
			if(OnLoadSuccess != null)
			{
				OnLoadSuccess();
			}
		}
		this.processState = PlayGameController.ProcessState.STARTPLAYCOUNTDOWN;
		this.startPlayCountDown = second;
		if(PlayerPrefs.GetString("GameType", "Single") == "Network")
		{
			if(second == 0)
			{
				AudioSource.PlayClipAtPoint(start2Audio, Camera.main.transform.position);
			}
			else
			{
				AudioSource.PlayClipAtPoint(start1Audio, Camera.main.transform.position);
			}
		}
		isGameInitialized = true;
		
		if(!isCountDownAnimaitonStarted) {
			if(countDownBat == null) {
				countDownBat = Camera.main.GetComponentInChildren<CountDownBat>();
			}
	
//			countDownBat.SetStartFlyIn();
			isCountDownAnimaitonStarted = true;
		}
		
		
		
		if(dynamicMaterial == null) {
			dynamicMaterial = Camera.main.GetComponentInChildren<DynamicMaterial>();
		}
//		dynamicMaterial.SetCountdownRender((int)this.startPlayCountDown);
//		if(!isOpenMapCamera)
//		{
//			LittleMap mapCamera = FindObjectOfType(typeof(LittleMap)) as LittleMap;
//			mapCamera.SetCars();
//			isOpenMapCamera = true;
//		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_FinishPlay(int GUPID, string timeRecord, int gainGold) {
//		Debug.Log ("ReceiveByClientPortal_FinishPlay - GUPID:"+GUPID+", Record:"+timeRecord);
		
//		Debug.Log("FinishPlay");
		if(!isFinishPlayCountDown) {
			StartCoroutine(FinishPlayCountDown());
		}
		
//		isShowFinishCountDown = true;
		
		this.matchRoom.GetComponent<CarInsanityRecorder>().AddRecord(GUPID, timeRecord);
		
//		Debug.Log ("OnCarArrived - GUPID:"+GUPID+", Record:"+timeRecord+", rank:"+this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount()+", isOwned:" + (playingGUPID == GUPID).ToString() );
		
		OnCarArrived(this.matchRoom.GetCarInsanityPlayer(GUPID), timeRecord, this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount(), playingGUPID == GUPID);
		
		
		
		if(GUPID == playingGUPID) { // i m finish
			
//			isShowFinishCountDown = false;
			
			this.processState = PlayGameController.ProcessState.ARRIVED;
//			OnShowRecord();
			OnShowGainGold(gainGold, this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount());
			
			int c = 0;
			foreach(KeyValuePair<int, string> kp in this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordDic()) {
				c++;
				if(kp.Key == playingGUPID) {
					Camera.main.BroadcastMessage("ShowCup", c, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			
//			//stop synchronize
//			CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
//			foreach(CarProperty cp in cars) {
//				if(cp.ownerGUPID == playingGUPID) {
//					cp.networkView.enabled = false;
//					break;
//				}
//			}
		
		}
			
		//start balance when all player arrived
		if(this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount() == this.matchRoom.GetComponent<Room>().maxPlayerNumber) {
//			Debug.Log("ReceiveByClientPortal_FinishPlay - GetRecordCount():" + this.tvcMatchRoom.GetRecordCount() + ", GetMaxPlayerNumber():"+ this.tvcMatchRoom.GetMaxPlayerNumber());
			StopAllCarAndNetworkSynchron();
			this.processState = PlayGameController.ProcessState.BALANCE;
			OnShowRecord();
		}
		
	}	
	
	
//	[RPC]
//	public void ReceiveByClientPortal_FinishPlay(int GUPID, string timeRecord) {
////		Debug.Log ("ReceiveByClientPortal_FinishPlay - GUPID:"+GUPID+", Record:"+timeRecord);
//		
////		Debug.Log("FinishPlay");
//		if(!isFinishPlayCountDown) {
//			StartCoroutine(FinishPlayCountDown());
//		}
//		
////		isShowFinishCountDown = true;
//		
//		this.matchRoom.GetComponent<CarInsanityRecorder>().AddRecord(GUPID, timeRecord);
//		
////		Debug.Log ("OnCarArrived - GUPID:"+GUPID+", Record:"+timeRecord+", rank:"+this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount()+", isOwned:" + (playingGUPID == GUPID).ToString() );
//		
//		OnCarArrived(this.matchRoom.GetCarInsanityPlayer(GUPID), timeRecord, this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount(), playingGUPID == GUPID);
//		
//		
//		
//		if(GUPID == playingGUPID) { // i m finish
//			
////			isShowFinishCountDown = false;
//			
//			this.processState = PlayGameController.ProcessState.ARRIVED;
////			OnShowRecord();
////			OnShowGainGold(gainGold, this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount());
//			
//			int c = 0;
//			foreach(KeyValuePair<int, string> kp in this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordDic()) {
//				c++;
//				if(kp.Key == playingGUPID) {
//					Camera.main.BroadcastMessage("ShowCup", c, SendMessageOptions.DontRequireReceiver);
//					break;
//				}
//			}
//			
////			//stop synchronize
////			CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
////			foreach(CarProperty cp in cars) {
////				if(cp.ownerGUPID == playingGUPID) {
////					cp.networkView.enabled = false;
////					break;
////				}
////			}
//		
//		}
//			
//		//start balance when all player arrived
//		if(this.matchRoom.GetComponent<CarInsanityRecorder>().GetRecordCount() == this.matchRoom.GetComponent<Room>().maxPlayerNumber) {
////			Debug.Log("ReceiveByClientPortal_FinishPlay - GetRecordCount():" + this.tvcMatchRoom.GetRecordCount() + ", GetMaxPlayerNumber():"+ this.tvcMatchRoom.GetMaxPlayerNumber());
//			StopAllCarAndNetworkSynchron();
//			this.processState = PlayGameController.ProcessState.BALANCE;
//			OnShowRecord();
//		}
//		
//	}	
	
	
	[RPC]
	public void ReceiveByClientPortal_DestroyObject(NetworkViewID NVID) {
//		Debug.Log("ReceiveByClientPortal_DestroyObject - NetworkViewID:"+NVID);
		CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
		foreach(CarProperty cp in cars) {
			if(cp.networkView.viewID == NVID) {
//				Debug.Log("ReceiveByClientPortal_DestroyObject - gameObject:" + cp.gameObject);
				this.matchRoom.GetComponent<Room>().maxPlayerNumber -= 1;
				
				Destroy(cp.gameObject);
				
				break;
			}
		}
	}

//	[RPC]
//	public void ReceiveByClientPortal_FinishPlayCountDown(float second) {
//		Debug.Log("ReceiveByClientPortal_FinishPlayCountDown: " + second);
//		this.finishPlayCountDown = second;
//	}
	 
	[RPC]
	public void ReceiveByClientPortal_TimeUp() {
//		Debug.Log("TimeUp");
		StopAllCarAndNetworkSynchron();
		this.processState = PlayGameController.ProcessState.BALANCE;
		OnShowRecord();
	}
	
	[RPC]
	public void ReceiveByClientPortal_AIHandOff(int gupid, int AIgupid) {
//		Debug.Log(gupid+" GUPID - ReceiveByClientPortal_AIHandOff: "+AIgupid);
		//set creator to this.tvcDriver
		
		CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
		foreach(CarProperty cp in cars) {
			if(AIgupid == cp.ownerGUPID) {
				cp.networkView.enabled = false;
			}
		}
		
		if(playingGUPID == gupid) {
			playingIsCreator = 1;
			networkView.RPC("SendToGameServer_AIHandOffInitial", RPCMode.Server, playingRoomIndex, playingGUPID, AIgupid, Network.AllocateViewID());
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_AIHandOffInitial(int AIgupid, NetworkViewID vid, NetworkMessageInfo msgInfo) {
		
//		Debug.Log("ReceiveByClientPortal_AIHandOffInitial - AIgupid: "+AIgupid+ ", ViewID: " + vid);
		
		CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
		foreach(CarProperty cp in cars) {
			if(AIgupid == cp.ownerGUPID) {
				
				cp.networkView.viewID = vid;
				cp.networkView.enabled = true;
				
				networkView.RPC("SendToGameServer_ReplyAIHandOffInitial", RPCMode.Server, playingRoomIndex, AIgupid, vid);		
				return;
			}
		}
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_AIOn(int AIgupid, NetworkViewID vid, NetworkMessageInfo msgInfo) {
		
//		Debug.Log("ReceiveByClientPortal_AIOn - AIgupid: "+AIgupid+ ", ViewID: " + vid);
		
		CarProperty[] cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[] ;
		foreach(CarProperty cp in cars) {
			if(AIgupid == cp.ownerGUPID) {
				
				cp.GetComponent<NetworkRigidbody>().enabled = false;
				cp.GetComponent<CarAI>().enabled = true;
				cp.GetComponent<CarB>().IsWaiting = false;
				
				cp.networkView.viewID = vid;
				cp.networkView.enabled = true;
				
				//networkView.RPC("SendToGameServer_ReplyAIHandOffInitial", RPCMode.Server, playingRoomIndex, AIgupid, vid);		
				return;
			}
		}
	}
	
	public bool IsAnyAIs() {
		foreach(GameUserPlayer d in this.matchRoom.GetComponent<Room>().playersInRoom) {
			if(d.isAI) {
				return true;
			}
		}
		
		return false;
	}
	
	public bool IsStartPlay() {
		if(this.processState == PlayGameController.ProcessState.PLAY) {
			return true;
		}else{
			return false;
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_CreatorHandOff(int creatorGUPID) {
		
		playingPlayerNumber--;
		
		if(playingGUPID == creatorGUPID) {
			playingIsCreator = 1;
			if(IsAnyAIs()) {
				foreach(GameUserPlayer d in this.matchRoom.GetComponent<Room>().playersInRoom) {
					if(d.isAI) {
						//Debug.Log("SendToGameServer_JoinMatch RPC for AI:"+d.GetPlayerName());
//						networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
						networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, 0, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
					}
				}
			}else{ // no AI
				networkView.RPC("SendToGameServer_StartSendAllPlayerInitialData", RPCMode.Server, playingRoomIndex);
			}
		} 
	}
	
	[RPC]
	public void ReceiveByClientPortal_CreatorOn(int creatorGUPID) {
		
		playingPlayerNumber--;
		
		if(playingGUPID == creatorGUPID) {
			playingIsCreator = 1;
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_SendAIJoinMatch(int creatorGUPID) {
				
		if((playingGUPID == creatorGUPID) && (playingIsCreator == 1)) {
			foreach(GameUserPlayer d in this.matchRoom.GetComponent<Room>().playersInRoom) {
				if(d.isAI) {
					//Debug.Log("SendToGameServer_JoinMatch RPC for AI:"+d.GetPlayerName());
//					networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
					networkView.RPC("SendToGameServer_JoinMatch", RPCMode.Server, playingRoomIndex, d.GUPID, 0, 0, 0, d.playerName, 0, playingPlayerNumber, playingMapID, playingMatchType, 1, Network.AllocateViewID());
				}
			}
		} 
	}

	#endregion

	#endregion
	
	private void StopAllCarAndNetworkSynchron() {
		
//		Network.Disconnect();
		
		CarB[] carBs = FindObjectsOfType(typeof(CarB)) as CarB[];
		foreach(CarB car in carBs)
		{
			car.SetSteer(0);
			car.SetThrottle(0);
			car.Wait(true);
			((UserInterfaceControl)FindObjectOfType(typeof(UserInterfaceControl))).enabled = false;
			//car.networkView.enabled = false;
		}
	}
	
	IEnumerator FinishPlayCountDown() {
		isFinishPlayCountDown = true;
//		Debug.Log("FinishPlayCountDown");
		finishPlayCountDown = 9.0f;
		while(finishPlayCountDown > 0.0f) {
			finishPlayCountDown = finishPlayCountDown - 1.0f;
			
			yield return new WaitForSeconds(1.0f);
		}
		
		isDraw = false;
		
//		isShowFinishCountDown = false;
	}
	
	private bool onLoad = false;
	IEnumerator LoadLevelWithProgress (String levelToLoad) {
		
		OnLoadingMap();
		
		//Add by Vincent 2012/11/13
	    if(onLoad)
		{
			yield break;
		}
		onLoad = true;
		Application.backgroundLoadingPriority = ThreadPriority.High;
		AsyncOperation async = Application.LoadLevelAsync((int)Definition.eSceneID.Loading);
	    while (!async.isDone) {
//	        this.loadingProgress += 8.0f;
//			if(this.loadingProgress > 100.0f) {
//				this.loadingProgress = 100.0f;	
//			}
//			//Debug.LogError((async.progress *100.0f).ToString());
	        yield return null ;
	    }
		
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		
		yield return new WaitForSeconds(5.0f);
		Application.LoadLevel(levelToLoad);
		onLoad = false;
//	    while (true) {
//
//	        yield return null ;
//	    }
	}
	
	IEnumerator CountingElapsedTime() {
		//Debug.Log("CountingElapsedTime");
		while(!isGameInitialized && elapsedTime < 100.0f ) {
			elapsedTime = elapsedTime + 1.0f;
			//Debug.Log("elapsedTime:"+elapsedTime);
			
			yield return new WaitForSeconds(1.0f);
		}
		
		
		elapsedTime = 0.0f;
	}
	
	#region State Method
	
//	public override void FinishRun() {
//		
//		this.Free();
//		base._context.SetGameFlowState(GetComponent("RedirectState") as RedirectState);
//		base._context.GetGameFlowState().StartRun(this);
//		
//		Application.LoadLevel("EmptyScene");
//	}
	
//	public override void Login() {
//		this.Free();
//		base._context.GetGameFlowState().StartRun(this);
//		
//		Application.LoadLevel("EmptyScene");
//	}
	
	#endregion
	
	
	
	void OnDisconnectedFromServer() {

		if(this.enabled) {
			if((this.processState == PlayGameController.ProcessState.ARRIVED) ||
			   (this.processState == PlayGameController.ProcessState.BALANCE))
			{
				
				//disconnected on finished game
				
//				NGUIRecordCloseButtonClick button = GameObject.FindObjectOfType(typeof(NGUIRecordCloseButtonClick)) as NGUIRecordCloseButtonClick;
//				button.BroadcastMessage("OnClick", SendMessageOptions.DontRequireReceiver);
				OnCompletedAMatch();
				redirectController.SendMessage("RedirectStart", SendMessageOptions.DontRequireReceiver);
				
			}else if((this.processState == PlayGameController.ProcessState.STARTPLAYCOUNTDOWN) ||
			  		 (this.processState == PlayGameController.ProcessState.CONNECTEDTOGAMESERVER) ||
					 (this.processState == PlayGameController.ProcessState.PLAY)){
				
				//disconnected on playing
				OnDisconnectedFromGameServer();
			}else{
				
			}
		}
	}
	
	void OnFailedToConnect() {
		if(this.processState == PlayGameController.ProcessState.CONNECTING) {
			
			if(connectToGameServerCount-- > 0) {
				Network.Connect(gameServerIP, gameServerPort);
				
			}else{
//				playGameSceneViewer.ConnectionFailSceneSetting();
				connectToGameServerCount = maxConnectToGameServerCount;
				
				OnFailedToConnectToGameServer();

			}
			
		}
	}
	
	public bool IsBalanceState () {
		return this.processState == PlayGameController.ProcessState.BALANCE;
	}
	
	public void ShowRecord() {
		OnShowRecord();
	}
}
