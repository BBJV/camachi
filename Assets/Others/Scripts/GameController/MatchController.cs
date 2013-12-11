using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MatchController : MonoBehaviour {
	
	private enum DrawState {
		FREE,
		ENTER,
		MATCHROOM,
		QUIT,
	}
	
	private enum ActionState {
		FREE,
		
		CARLISTLOADSTART,
		CARLISTLOADING,
		CARLISTLOADED,
		CARLISTLOADERROR,
		
		CARSELECTSTART,
		CARSELECTING,
		CARSELECTED,
		CARSELECTERROR,
		
		STARTSTART,
		STARTING,
		STARTED,
		STARTERROR,
		
		LEAVESTART,
		LEAVING,
		LEFT,
		LEAVEERROR,
		
		ABDICATEERROR,	
		
		MESSAGEERROR,		
	}
	
	public delegate void OnAfterOnGUIEventHandler();	
	public static event OnAfterOnGUIEventHandler OnAfterBoardDrawn; 
	
	public delegate void OnCarSelectedEventHandler();
	public static event OnCarSelectedEventHandler OnCarSelected;
	
	public delegate void OnAllPlayerSelectCarEventHandler();
	public static event OnAllPlayerSelectCarEventHandler OnAllPlayerSelectCar;
	
	public delegate void OnLeaveFromMatchRoomEventHandler();
	public static event OnLeaveFromMatchRoomEventHandler OnLeaveFromMatchRoom;
	
	public delegate void OnJoinedMatchEventHandler();
	public static event OnJoinedMatchEventHandler OnJoinedMatch;
	
	public delegate void OnMatchStartedEventHandler();
	public static event OnMatchStartedEventHandler OnMatchStarted;
	
	private MatchController.DrawState drawState = MatchController.DrawState.FREE;
//	private MatchController.ActionState actionState = MatchController.ActionState.FREE;
	
	private List<CarInsanityCarInfo> allCarInsanityCarInfo = new List<CarInsanityCarInfo>();
	private CarInsanityCarInfo selectedCarInsanityCarInfo = new CarInsanityCarInfo();
	private Dictionary<string, bool> carAvailableDic = new Dictionary<string, bool>();
	private List<string> carAvailableKeyList;
//	private string errorMsg = "";	
	
	public GameUserPlayer playerInfo;
	public MatchRoom matchRoom;
	public GameUserPlayer playerPrefab;
	public MatchRoom matchRoomPrefab;
	
	public LobbySceneViewer lobbySceneViewer;
	public MatchRoomSceneViewer matchRoomSceneViewer;
	
	public PlayGameController playGameController;
	public LobbyController lobbyController;
	public bool isDraw = false;
	private bool isLockedOn = false;
	
	private bool isStartPlay = false;
	private bool isLeaving = false;
	
//	private string strTest = "Test";
	
//	public GUISkin englishSkin;
//	public GUISkin simpleChineseSkin;
//	public GUISkin tranditionalChineseSkin;
	
	#region GUI
	
	
	private bool isLockOnButtonEnable = false;
//	private int carCountX = 0;
//	private int carCountY = 0;
	private int playerCount = 0;
	private int selectingCarTotalTime = 20;
//	private int currentCarSelectingTime = 0;
	private int tenDigit = 0;
	private int digit = 0;

	private Vector2 messageScrollPosition;
	private Vector2 carSelectScrollPosition;
	
	private string strEmpty = "";
//	private string strAddAI = "Add AI";
	private string strIconCar = "Icon_Black";
	private string strBoardPlayerType = "Board_Player";
	
	private string Button_AddAI = "Button_AddAI";
	private string Button_AddAI_Dark = "Button_AddAI_Dark";
	
	private string Font_White = "Font_White";
//	private string strPlayerStateWord = "";
//	private string strChoosingState = "Choosing";
//	private string strChosenState = "OK";
	private Rect matchRoomWindow;	
	private GUISkin matchRoomSkin;	
	private Definition.eLanguage currentLanguage;
	

	private float screenX = 0;
	private float screenY = 0;
	private int screenWidth = 1024;
	private int screenHeight = 768;
	
	// PlayerActionInfoBG box
	
//	private string Background_PlayerActionInfo = "Background_PlayerActionInfo";
//	private float boxPlayerActionInfoBGX = 0.0f;
//	private float boxPlayerActionInfoBGY = 0.0f;
//	private float boxPlayerActionInfoBGWidth = 0.0f;
//	private float boxPlayerActionInfoBGHeight = 0.0f;
//	private float boxPlayerActionInfoBGStartX = 505.0f;
//	private float boxPlayerActionInfoBGStartY = 10.0f;	
//	private float boxPlayerActionInfoBGTextureWidth = 321.0f;
//	private float boxPlayerActionInfoBGTextureHeight = 98.0f;
//	private Rect boxPlayerActionInfoBGRect;
	
	
	//Logout button
	private string Button_Logout = "Button_Back";
	private float buttonLogoutX = 0.0f;
	private float buttonLogoutY = 0.0f;
	private float buttonLogoutWidth = 0.0f;
	private float buttonLogoutHeight = 0.0f;
	private float buttonLogoutStartX = 903.0f;
	private float buttonLogoutStartY = 16.0f;	
	private float buttonLogoutTextureWidth = 101.0f;
	private float buttonLogoutTextureHeight = 83.0f;
	private Rect buttonLogoutRect;
	
	// Room Banner Group
	private string Room_Banner = "Room_Banner";
	private float groupRoomBannerX = 0.0f;
	private float groupRoomBannerY = 0.0f;
	private float groupRoomBannerWidth = 0.0f;
	private float groupRoomBannerHeight = 0.0f;
	private float groupRoomBannerStartX = 1.0f;
	private float groupRoomBannerStartY = 0.0f;	
	private float groupRoomBannerTextureWidth = 1022.0f;
	private float groupRoomBannerTextureHeight = 163.0f;
	private Rect groupRoomBannerRect;
	
	//Digit Label
	private string Label_Digit_ = "Label_Digit_";
	private float labelDigitX = 0.0f;
	private float labelDigitY = 0.0f;
	private float labelDigitWidth = 0.0f;
	private float labelDigitHeight = 0.0f;
	private float labelDigitStartX = 516.0f; 
	private float labelDigitStartY = 26.0f;
	private float labelDigitTextureWidth = 31.0f;
	private float labelDigitTextureHeight = 43.0f;
	private Rect labelDigitRect;
	
	//TenDigit Label
	private float labelTenDigitX = 0.0f;
	private float labelTenDigitY = 0.0f;
	private float labelTenDigitWidth = 0.0f;
	private float labelTenDigitHeight = 0.0f;
	private float labelTenDigitStartX = 484.0f; 
	private float labelTenDigitStartY = 26.0f;
	private float labelTenDigitTextureWidth = 31.0f;
	private float labelTenDigitTextureHeight = 43.0f;
	private Rect labelTenDigitRect;
	
	//Room_Board Group
	private string Room_Board = "Room_Board";
	private float groupRoomBoardX = 0.0f;
	private float groupRoomBoardY = 0.0f;
	private float groupRoomBoardWidth = 0.0f;
	private float groupRoomBoardHeight = 0.0f;
	private float groupRoomBoardStartX = 0.0f;
	private float groupRoomBoardStartY = 0.0f;	
	private float groupRoomBoardTextureWidth = 1024.0f;
	private float groupRoomBoardTextureHeight = 768.0f;
	private Rect groupRoomBoardRect;
	
	//Board_Left Group
	private string Board_Left = "Board_Left";
	private float groupBoardLeftX = 0.0f;
	private float groupBoardLeftY = 0.0f;
	private float groupBoardLeftWidth = 0.0f;
	private float groupBoardLeftHeight = 0.0f;
	private float groupBoardLeftStartX = 35.0f;
	private float groupBoardLeftStartY = 98.0f;	
	private float groupBoardLeftTextureWidth = 311.0f;
	private float groupBoardLeftTextureHeight = 661.0f;
	private Rect groupBoardLeftRect;
	
	//Board_Right Group
	private string Board_Right = "Board_Right";
	private float groupBoardRightX = 0.0f;
	private float groupBoardRightY = 0.0f;
	private float groupBoardRightWidth = 0.0f;
	private float groupBoardRightHeight = 0.0f;
	private float groupBoardRightStartX = 341.0f;
	private float groupBoardRightStartY = 101.0f;	
	private float groupBoardRightTextureWidth = 647.0f;
	private float groupBoardRightTextureHeight = 659.0f;
	private Rect groupBoardRightRect;
	
	//Board_PlayerList
	private string Board_PlayerList = "Board_PlayerList";
	private float labelBoardPlayerListX = 0.0f;
	private float labelBoardPlayerListY = 0.0f;
	private float labelBoardPlayerListWidth = 0.0f;
	private float labelBoardPlayerListHeight = 0.0f;
	private float labelBoardPlayerListStartX = 58.0f;
	private float labelBoardPlayerListStartY = 133.0f;	
	private float labelBoardPlayerListTextureWidth = 257.0f;
	private float labelBoardPlayerListTextureHeight = 534.0f;
	private Rect labelBoardPlayerListRect;
	
	//Board_Owner_0
	private string Board_Owner_Dark = "Board_Owner_Dark";
	private string Board_Owner = "Board_Owner_0";
	private string Board_Owner_ = "Board_Owner_";

	//Board_Player
	private string Board_Player = "Board_Player";
	private string Board_Player_Dark = "Board_Player_Dark";
	private float labelBoardPlayerX = 0.0f;
	private float labelBoardPlayerY = 0.0f;
	private float labelBoardPlayerWidth = 0.0f;
	private float labelBoardPlayerHeight = 0.0f;
	private float labelBoardPlayerStartX = 68.0f;
	private float labelBoardPlayerStartY = 185.0f;	
	private float labelBoardPlayerTextureWidth = 246.0f;
	private float labelBoardPlayerTextureHeight = 117.0f;
	private Rect labelBoardPlayerRect;
	private float labelBoardPlayerDiff = 120.0f;
	
	//Board_PlayerUnavailable
	
	
	//Label_PlayerName
	private float labelPlayerNameX = 0.0f;
	private float labelPlayerNameY = 0.0f;
	private float labelPlayerNameWidth = 0.0f;
	private float labelPlayerNameHeight = 0.0f;
	private float labelPlayerNameStartX = 185.0f;
	private float labelPlayerNameStartY = 195.0f;	
	private float labelPlayerNameTextureWidth = 146.0f;
	private float labelPlayerNameTextureHeight = 34.0f;
	private Rect labelPlayerNameRect;
	private float labelPlayerNameDiff = 120.0f;
	
	//Label_StateWord
	private float labelStateWordX = 0.0f;
	private float labelStateWordY = 0.0f;
	private float labelStateWordWidth = 0.0f;
	private float labelStateWordHeight = 0.0f;
	private float labelStateWordStartX = 185.0f;
	private float labelStateWordStartY = 261.0f;	
	private float labelStateWordTextureWidth = 146.0f;
	private float labelStateWordTextureHeight = 34.0f;
	private Rect labelStateWordRect;
	private float labelStateWordDiff = 120.0f;
	
	//Button_LockOn
	private string Button_LockOn = "Button_LockOn";
	private float buttonLockOnX = 0.0f;
	private float buttonLockOnY = 0.0f;
	private float buttonLockOnWidth = 0.0f;
	private float buttonLockOnHeight = 0.0f;
	private float buttonLockOnStartX = 56.0f;
	private float buttonLockOnStartY = 674.0f;	
	private float buttonLockOnTextureWidth = 269.0f;
	private float buttonLockOnTextureHeight = 60.0f;
	private Rect buttonLockOnRect;
	
	//Board_CarList
//	private string Board_CarList = "Board_CarList";
//	private float labelBoardCarListX = 0.0f;
//	private float labelBoardCarListY = 0.0f;
//	private float labelBoardCarListWidth = 0.0f;
//	private float labelBoardCarListHeight = 0.0f;
//	private float labelBoardCarListStartX = 366.0f;
//	private float labelBoardCarListStartY = 135.0f;	
//	private float labelBoardCarListTextureWidth = 596.0f;
//	private float labelBoardCarListTextureHeight = 365.0f;
	private Rect labelBoardCarListRect;
	
	//Board_MapInfo
	private string MapInfo_ = "MapInfo_";
	private float labelBoardMapInfoX = 0.0f;
	private float labelBoardMapInfoY = 0.0f;
	private float labelBoardMapInfoWidth = 0.0f;
	private float labelBoardMapInfoHeight = 0.0f;
	private float labelBoardMapInfoStartX = 367.0f;
	private float labelBoardMapInfoStartY = 515.0f;	
	private float labelBoardMapInfoTextureWidth = 595.0f;
	private float labelBoardMapInfoTextureHeight = 225.0f;
	private Rect labelBoardMapInfoRect;
	
	//Icon_Black
	private string Icon_Black = "Icon_Black";
	private float iconBlackX = 0.0f;
	private float iconBlackY = 0.0f;
	private float iconBlackWidth = 0.0f;
	private float iconBlackHeight = 0.0f;
	private float iconBlackStartX = 79.0f;
	private float iconBlackStartY = 192.0f;	
	private float iconBlackTextureWidth = 102.0f;
	private float iconBlackTextureHeight = 102.0f;
	private Rect iconBlackRect;
	private float iconBlackDiff = 120.0f;
	
	//Icon_Car
	private string Icon_Car = "Icon_Car_";
	private float iconCarX = 0.0f;
	private float iconCarY = 0.0f;
	private float iconCarWidth = 0.0f;
	private float iconCarHeight = 0.0f;
	private float iconCarStartX = 395.0f;
	private float iconCarStartY = 185.0f;	
	private float iconCarTextureWidth = 75.0f;
	private float iconCarTextureHeight = 75.0f;
	private Rect iconCarRect;
	private float iconCarDiffX = 120.0f; // 108
	private float iconCarDiffY = 120.0f; // 104
	
	private float bannerStartPoint = -163.0f;
	private float bannerEndPoint = 0.0f;
	//public float bannerCurrentPoint = 0.0f;
	
	private float roomBoardStartPoint = 768.0f;
	private float roomBoardEndPoint = 0.0f;
	//public float roomBoardCurrentPoint = 768.0f;
	
	private float leftBoardStartPoint = -661.0f; // 98 - 759
	private float leftBoardEndPoint = 98.0f;
	//public float leftBoardCurrentPoint = 98.0f;
	
	private float rightBoardStartPoint = -661.0f;
	private float rightBoardEndPoint = 98.0f;
	//public float rightBoardCurrentPoint = 98.0f;

	
	void Awake() {
		
		carAvailableDic.Add(Definition.eCarID.POLICE_CAR.ToString(), false);
		carAvailableDic.Add(Definition.eCarID.LORRY_TRUCK.ToString(), false);
		carAvailableDic.Add(Definition.eCarID.AMBULANCE.ToString(), false);
		carAvailableDic.Add(Definition.eCarID.FIRE_FIGHTING_TRUCK.ToString(), false);
		carAvailableDic.Add(Definition.eCarID.GARBAGE_TRUCK.ToString(), false);
		
		carAvailableKeyList = new List<string>(carAvailableDic.Keys);
		
		
		InitialGUI();
		
		NGUILoginController.OnLogined += OnLogined;
		NGUICarSelecting.OnCarSelectingChanged += OnCarSelectingChanged;
		GUISkinLanguageSetting.OnGUISkinLanguageChanged += OnGUISkinLanguageChanged;
	}
	
	void OnDestroy() {
		NGUILoginController.OnLogined -= OnLogined;
		NGUICarSelecting.OnCarSelectingChanged -= OnCarSelectingChanged;
		GUISkinLanguageSetting.OnGUISkinLanguageChanged -= OnGUISkinLanguageChanged;
	}
	
	private float temp = 0.0f;
	private int n = 0;
	
	void Update() {
		Board_Owner = Board_Owner_+ n;
		
		n += (int)temp;
		temp = (temp >= 1) ? 0.0f : temp + Time.deltaTime * 10;
		if(n > 5) {
			n = 0;
		}
		if(this.drawState == MatchController.DrawState.ENTER) {
			if(groupRoomBannerRect.y < bannerEndPoint) {
				groupRoomBannerRect.y = groupRoomBannerRect.y + (groupRoomBannerHeight*Time.deltaTime*3);
				
			}else{
				groupRoomBannerRect.y = bannerEndPoint;
			}
			
			if(groupRoomBoardRect.y > roomBoardEndPoint) {
				groupRoomBoardRect.y = groupRoomBoardRect.y - (groupRoomBoardHeight*Time.deltaTime*3);
			}else{
				groupRoomBoardRect.y = roomBoardEndPoint;
			}
			
			if((groupRoomBannerRect.y == bannerEndPoint) && (groupRoomBoardRect.y == roomBoardEndPoint)) {
				if(groupBoardLeftRect.y < leftBoardEndPoint) {
					groupBoardLeftRect.y = groupBoardLeftRect.y + (groupBoardLeftHeight * Time.deltaTime*3);
					groupBoardRightRect.y = groupBoardRightRect.y + (groupBoardRightHeight * Time.deltaTime*3);
				}else{
					groupBoardLeftRect.y = leftBoardEndPoint;
					groupBoardRightRect.y = leftBoardEndPoint;
					
					this.drawState = MatchController.DrawState.MATCHROOM;
					
					if(OnAfterBoardDrawn != null) {
						OnAfterBoardDrawn();
					}
				}
			}
		}
		
		if(this.drawState == MatchController.DrawState.QUIT) {
			
			if(groupBoardLeftRect.y > leftBoardStartPoint) {
				groupBoardLeftRect.y = groupBoardLeftRect.y - (groupBoardLeftHeight*Time.deltaTime*3);
				groupBoardRightRect.y = groupBoardRightRect.y - (groupBoardRightHeight*Time.deltaTime*3);
			}else{
				groupBoardLeftRect.y = leftBoardStartPoint;
				groupBoardRightRect.y = leftBoardStartPoint;
			}
			
			if(groupBoardLeftRect.y == leftBoardStartPoint) {
				if(groupRoomBannerRect.y > bannerStartPoint) {
					groupRoomBannerRect.y = groupRoomBannerRect.y - (groupRoomBannerHeight*Time.deltaTime*3);

				}else{
					groupRoomBannerRect.y = bannerStartPoint;
				}
				
				if(groupRoomBoardRect.y < roomBoardStartPoint) {
					groupRoomBoardRect.y = groupRoomBoardRect.y + (groupRoomBoardHeight*Time.deltaTime*3);

				}else{
					groupRoomBoardRect.y = roomBoardStartPoint;
				}

				if((groupRoomBannerRect.y == bannerStartPoint) && (groupRoomBoardRect.y == roomBoardStartPoint)) {
					if(isStartPlay) {
						
						this.StartPreparingForPlay();
						
					}else if(isLeaving) {
						this.Back();
					}
				} 
			}
		}
	}
	
	void InitialGUI() {
		
		bannerStartPoint = bannerStartPoint * Screen.height / screenHeight;
		bannerEndPoint = bannerEndPoint * Screen.height / screenHeight;
//		bannerCurrentPoint = bannerCurrentPoint * Screen.height / screenHeight;
	
		roomBoardStartPoint = roomBoardStartPoint * Screen.height / screenHeight;
		roomBoardEndPoint = roomBoardEndPoint * Screen.height / screenHeight;
//		roomBoardCurrentPoint = roomBoardCurrentPoint * Screen.height / screenHeight;
	
		leftBoardStartPoint = leftBoardStartPoint * Screen.height / screenHeight;
		leftBoardEndPoint = leftBoardEndPoint * Screen.height / screenHeight;
//		leftBoardCurrentPoint = leftBoardCurrentPoint * Screen.height / screenHeight;
	
		rightBoardStartPoint = rightBoardStartPoint * Screen.height / screenHeight;
		rightBoardEndPoint = rightBoardEndPoint * Screen.height / screenHeight;
//		rightBoardCurrentPoint = rightBoardCurrentPoint * Screen.height / screenHeight;
		
		//boxPlayerActionInfoBG
//		boxPlayerActionInfoBGX = (boxPlayerActionInfoBGStartX - screenX) * Screen.width / screenWidth;
//		boxPlayerActionInfoBGY = (boxPlayerActionInfoBGStartY - screenY) * Screen.height / screenHeight;
//		boxPlayerActionInfoBGWidth = boxPlayerActionInfoBGTextureWidth * Screen.width / screenWidth;
//		boxPlayerActionInfoBGHeight = boxPlayerActionInfoBGTextureHeight * Screen.height / screenHeight;
//		boxPlayerActionInfoBGRect = new Rect(boxPlayerActionInfoBGX, boxPlayerActionInfoBGY, boxPlayerActionInfoBGWidth, boxPlayerActionInfoBGHeight);
		
		//RoomBanner Group		
		groupRoomBannerX = (groupRoomBannerStartX - screenX) * Screen.width / screenWidth;
		groupRoomBannerY = (groupRoomBannerStartY - screenY) * Screen.height / screenHeight;
		groupRoomBannerWidth = groupRoomBannerTextureWidth * Screen.width / screenWidth;
		groupRoomBannerHeight = groupRoomBannerTextureHeight * Screen.height / screenHeight;
		groupRoomBannerRect = new Rect(groupRoomBannerX, groupRoomBannerY, groupRoomBannerWidth, groupRoomBannerHeight);
		groupRoomBannerRect.y = bannerStartPoint;
		
		//RoomBoard Group
		groupRoomBoardX = (groupRoomBoardStartX - screenX) * Screen.width / screenWidth;
		groupRoomBoardY = (groupRoomBoardStartY - screenY) * Screen.height / screenHeight;
		groupRoomBoardWidth = groupRoomBoardTextureWidth * Screen.width / screenWidth;
		groupRoomBoardHeight = groupRoomBoardTextureHeight * Screen.height / screenHeight;
		groupRoomBoardRect = new Rect(groupRoomBoardX, groupRoomBoardY, groupRoomBoardWidth, groupRoomBoardHeight);
		groupRoomBoardRect.y = roomBoardStartPoint;
		
		//BoardLeft Group
		groupBoardLeftX = (groupBoardLeftStartX - screenX) * Screen.width / screenWidth;
		groupBoardLeftY = (groupBoardLeftStartY - screenY) * Screen.height / screenHeight;
		groupBoardLeftWidth = groupBoardLeftTextureWidth * Screen.width / screenWidth;
		groupBoardLeftHeight = groupBoardLeftTextureHeight * Screen.height / screenHeight;
		groupBoardLeftRect = new Rect(groupBoardLeftX, groupBoardLeftY, groupBoardLeftWidth, groupBoardLeftHeight);
		groupBoardLeftRect.y = leftBoardStartPoint;
		
		//BoardRight Group
		groupBoardRightX = (groupBoardRightStartX - screenX) * Screen.width / screenWidth;
		groupBoardRightY = (groupBoardRightStartY - screenY) * Screen.height / screenHeight;
		groupBoardRightWidth = groupBoardRightTextureWidth * Screen.width / screenWidth;
		groupBoardRightHeight = groupBoardRightTextureHeight * Screen.height / screenHeight;
		groupBoardRightRect = new Rect(groupBoardRightX, groupBoardRightY, groupBoardRightWidth, groupBoardRightHeight);
		groupBoardRightRect.y = rightBoardStartPoint;

		//logout button
		buttonLogoutX = (buttonLogoutStartX - screenX) * Screen.width / screenWidth;
		buttonLogoutY = (buttonLogoutStartY - screenY) * Screen.height / screenHeight;
		buttonLogoutWidth = buttonLogoutTextureWidth * Screen.width / screenWidth;
		buttonLogoutHeight = buttonLogoutTextureHeight * Screen.height / screenHeight;
		buttonLogoutRect = new Rect(buttonLogoutX, buttonLogoutY, buttonLogoutWidth, buttonLogoutHeight);
		
//		//Back button
//		buttonBackX = (buttonBackStartX - screenX) * Screen.width / screenWidth;
//		buttonBackY = (buttonBackStartY - screenY) * Screen.height / screenHeight;
//		buttonBackWidth = buttonBackTextureWidth * Screen.width / screenWidth;
//		buttonBackHeight = buttonBackTextureHeight * Screen.height / screenHeight;
//		buttonBackRect = new Rect(buttonBackX, buttonBackY, buttonBackWidth, buttonBackHeight);
		
		labelDigitX = (labelDigitStartX - screenX) * Screen.width / screenWidth;
		labelDigitY = (labelDigitStartY - screenY) * Screen.height / screenHeight;
		labelDigitWidth = labelDigitTextureWidth * Screen.width / screenWidth;
		labelDigitHeight = labelDigitTextureHeight * Screen.height / screenHeight;
		labelDigitRect = new Rect(labelDigitX, labelDigitY, labelDigitWidth, labelDigitHeight);
		
		labelTenDigitX = (labelTenDigitStartX - screenX) * Screen.width / screenWidth;
		labelTenDigitY = (labelTenDigitStartY - screenY) * Screen.height / screenHeight;
		labelTenDigitWidth = labelTenDigitTextureWidth * Screen.width / screenWidth;
		labelTenDigitHeight = labelTenDigitTextureHeight * Screen.height / screenHeight;
		labelTenDigitRect = new Rect(labelTenDigitX, labelTenDigitY, labelTenDigitWidth, labelTenDigitHeight);
		
		#region /* Left Board in Room */
		//BoardPlayerList label
		labelBoardPlayerListX = (labelBoardPlayerListStartX - screenX) * Screen.width / screenWidth;
		labelBoardPlayerListY = (labelBoardPlayerListStartY - screenY) * Screen.height / screenHeight;
		labelBoardPlayerListWidth = labelBoardPlayerListTextureWidth * Screen.width / screenWidth;
		labelBoardPlayerListHeight = labelBoardPlayerListTextureHeight * Screen.height / screenHeight;
		labelBoardPlayerListRect = new Rect(labelBoardPlayerListX, labelBoardPlayerListY, labelBoardPlayerListWidth, labelBoardPlayerListHeight);
		labelBoardPlayerListRect.x = labelBoardPlayerListRect.x - groupBoardLeftX;
		labelBoardPlayerListRect.y = labelBoardPlayerListRect.y - groupBoardLeftY;
		
//		//BoardCreator0 label
//		labelBoardCreator0X = (labelBoardCreator0StartX - screenX) * Screen.width / screenWidth;
//		labelBoardCreator0Y = (labelBoardCreator0StartY - screenY) * Screen.height / screenHeight;
//		labelBoardCreator0Width = labelBoardCreator0TextureWidth * Screen.width / screenWidth;
//		labelBoardCreator0Height = labelBoardCreator0TextureHeight * Screen.height / screenHeight;
//		labelBoardCreator0Rect = new Rect(labelBoardCreator0X, labelBoardCreator0Y, labelBoardCreator0Width, labelBoardCreator0Height);
//		labelBoardCreator0Rect.x = labelBoardCreator0Rect.x - groupBoardLeftX;
//		labelBoardCreator0Rect.y = labelBoardCreator0Rect.y - groupBoardLeftY;
		
		//BoardPlayer label
		labelBoardPlayerX = (labelBoardPlayerStartX - screenX) * Screen.width / screenWidth;
		labelBoardPlayerY = (labelBoardPlayerStartY - screenY) * Screen.height / screenHeight;
		labelBoardPlayerWidth = labelBoardPlayerTextureWidth * Screen.width / screenWidth;
		labelBoardPlayerHeight = labelBoardPlayerTextureHeight * Screen.height / screenHeight;
		labelBoardPlayerRect = new Rect(labelBoardPlayerX, labelBoardPlayerY, labelBoardPlayerWidth, labelBoardPlayerHeight);
		labelBoardPlayerRect.x = labelBoardPlayerRect.x - groupBoardLeftX;
		labelBoardPlayerRect.y = labelBoardPlayerRect.y - groupBoardLeftY;
		labelBoardPlayerDiff = labelBoardPlayerDiff * Screen.height / screenHeight;
		
		//PlayerName label
		labelPlayerNameX = (labelPlayerNameStartX - screenX) * Screen.width / screenWidth;
		labelPlayerNameY = (labelPlayerNameStartY - screenY) * Screen.height / screenHeight;
		labelPlayerNameWidth = labelPlayerNameTextureWidth * Screen.width / screenWidth;
		labelPlayerNameHeight = labelPlayerNameTextureHeight * Screen.height / screenHeight;
		labelPlayerNameRect = new Rect(labelPlayerNameX, labelPlayerNameY, labelPlayerNameWidth, labelPlayerNameHeight);
		labelPlayerNameRect.x = labelPlayerNameRect.x - groupBoardLeftX;
		labelPlayerNameRect.y = labelPlayerNameRect.y - groupBoardLeftY;
		labelPlayerNameDiff = labelPlayerNameDiff * Screen.height / screenHeight;
		
		//StateWord label
		labelStateWordX = (labelStateWordStartX - screenX) * Screen.width / screenWidth;
		labelStateWordY = (labelStateWordStartY - screenY) * Screen.height / screenHeight;
		labelStateWordWidth = labelStateWordTextureWidth * Screen.width / screenWidth;
		labelStateWordHeight = labelStateWordTextureHeight * Screen.height / screenHeight;
		labelStateWordRect = new Rect(labelStateWordX, labelStateWordY, labelStateWordWidth, labelStateWordHeight);
		labelStateWordRect.x = labelStateWordRect.x - groupBoardLeftX;
		labelStateWordRect.y = labelStateWordRect.y - groupBoardLeftY;
		labelStateWordDiff = labelStateWordDiff * Screen.height / screenHeight;
		
		// Icon_Black
		iconBlackX = (iconBlackStartX - screenX) * Screen.width / screenWidth;
		iconBlackY = (iconBlackStartY - screenY) * Screen.height / screenHeight;
		iconBlackWidth = iconBlackTextureWidth * Screen.width / screenWidth;
		iconBlackHeight = iconBlackTextureHeight * Screen.height / screenHeight;
		iconBlackRect = new Rect(iconBlackX, iconBlackY, iconBlackWidth, iconBlackHeight);
		iconBlackRect.x = iconBlackRect.x - groupBoardLeftX;
		iconBlackRect.y = iconBlackRect.y - groupBoardLeftY;
		iconBlackDiff = iconBlackDiff * Screen.height / screenHeight;
		
		
		
		//Button_LockOn
		buttonLockOnX = (buttonLockOnStartX - screenX) * Screen.width / screenWidth;
		buttonLockOnY = (buttonLockOnStartY - screenY) * Screen.height / screenHeight;
		buttonLockOnWidth = buttonLockOnTextureWidth * Screen.width / screenWidth;
		buttonLockOnHeight = buttonLockOnTextureHeight * Screen.height / screenHeight;
		buttonLockOnRect = new Rect(buttonLockOnX, buttonLockOnY, buttonLockOnWidth, buttonLockOnHeight);
		buttonLockOnRect.x = buttonLockOnRect.x - groupBoardLeftX;
		buttonLockOnRect.y = buttonLockOnRect.y - groupBoardLeftY;
		#endregion
		
		#region /* Right Board in Room */
		//BoardCarList label
//		labelBoardCarListX = (labelBoardCarListStartX - screenX) * Screen.width / screenWidth;
//		labelBoardCarListY = (labelBoardCarListStartY - screenY) * Screen.height / screenHeight;
//		labelBoardCarListWidth = labelBoardCarListTextureWidth * Screen.width / screenWidth;
//		labelBoardCarListHeight = labelBoardCarListTextureHeight * Screen.height / screenHeight;
//		labelBoardCarListRect = new Rect(labelBoardCarListX, labelBoardCarListY, labelBoardCarListWidth, labelBoardCarListHeight);
//		labelBoardCarListRect.x = labelBoardCarListRect.x - groupBoardRightX;
//		labelBoardCarListRect.y = labelBoardCarListRect.y - groupBoardRightY;
//		
		// Icon_Car
		iconCarX = (iconCarStartX - screenX) * Screen.width / screenWidth;
		iconCarY = (iconCarStartY - screenY) * Screen.height / screenHeight;
		iconCarWidth = iconCarTextureWidth * Screen.width / screenWidth;
		iconCarHeight = iconCarTextureHeight * Screen.height / screenHeight;
		iconCarRect = new Rect(iconCarX, iconCarY, iconCarWidth, iconCarHeight);
		iconCarRect.x = iconCarRect.x - groupBoardRightX;
		iconCarRect.y = iconCarRect.y - groupBoardRightY;
		iconCarDiffX = iconCarDiffX * Screen.width / screenWidth;
		iconCarDiffY = iconCarDiffY * Screen.height / screenHeight;
		
		//Board_MapInfo
		labelBoardMapInfoX = (labelBoardMapInfoStartX - screenX) * Screen.width / screenWidth;
		labelBoardMapInfoY = (labelBoardMapInfoStartY - screenY) * Screen.height / screenHeight;
		labelBoardMapInfoWidth = labelBoardMapInfoTextureWidth * Screen.width / screenWidth;
		labelBoardMapInfoHeight = labelBoardMapInfoTextureHeight * Screen.height / screenHeight;
		labelBoardMapInfoRect = new Rect(labelBoardMapInfoX, labelBoardMapInfoY, labelBoardMapInfoWidth, labelBoardMapInfoHeight);
		labelBoardMapInfoRect.x = labelBoardMapInfoRect.x - groupBoardRightX;
		labelBoardMapInfoRect.y = labelBoardMapInfoRect.y - groupBoardRightY;
		
		#endregion
	}
	
	void OnGUI() {
		
		if(isDraw) {	
			GUI.skin = matchRoomSkin;
			
			GUI.BeginGroup(groupRoomBoardRect, strEmpty, Room_Board);
			GUI.EndGroup();
			
			GUI.BeginGroup(groupBoardLeftRect, strEmpty, Board_Left);
				GUI.Label(labelBoardPlayerListRect, strEmpty, Board_PlayerList);
			
				playerCount = 0;
			
				foreach(GameUserPlayer u in this.matchRoom.GetComponent<Room>().playersInRoom) {

					//is owner
					if(this.playerInfo.GUPID == u.GUPID) {
						
						if(selectedCarInsanityCarInfo.carID == 0) {
							
							strIconCar = Icon_Black;
//							strPlayerStateWord = strChoosingState;
						}else{
							if(u.GetComponent<CarInsanityPlayer>().selectedCar.carID != 0) {
							
								strIconCar = Icon_Car + u.GetComponent<CarInsanityPlayer>().selectedCar.carID;
//								strPlayerStateWord = strChosenState;
							}else{
								
								strIconCar = Icon_Car + selectedCarInsanityCarInfo.carID;
//								strPlayerStateWord = strChoosingState;
							}
						}
					}else{ // is other
						
						if(u.GetComponent<CarInsanityPlayer>().selectedCar.carID != 0) {
							
							strIconCar = Icon_Car + u.GetComponent<CarInsanityPlayer>().selectedCar.carID;
//							strPlayerStateWord = strChosenState;
						}else{
							
							strIconCar = Icon_Black;
//							strPlayerStateWord = strChoosingState;
						}
					}
				
					if(this.playerInfo.GUPID == u.GUPID) {
						if(!isLockedOn) {
							strBoardPlayerType = Board_Owner;
						}else{
							strBoardPlayerType = Board_Owner_Dark;
						}
					}else{
						if(u.GetComponent<CarInsanityPlayer>().selectedCar.carID != 0) {
							strBoardPlayerType = Board_Player_Dark;
						}else{
							strBoardPlayerType = Board_Player;
						}
						
					}
					
					
					GUI.Label(new Rect(labelBoardPlayerRect.x, labelBoardPlayerRect.y + labelBoardPlayerDiff * playerCount, labelBoardPlayerRect.width, labelBoardPlayerRect.height), strEmpty, strBoardPlayerType);
					GUI.Label(new Rect(iconBlackRect.x, iconBlackRect.y + iconBlackDiff * playerCount, iconBlackRect.width, iconBlackRect.height), strEmpty, strIconCar);
					
					GUI.Label(new Rect(labelPlayerNameRect.x, labelPlayerNameRect.y + labelPlayerNameDiff * playerCount, labelPlayerNameRect.width, labelPlayerNameRect.height), u.playerName, Font_White);
//					GUI.Label(new Rect(labelStateWordRect.x, labelStateWordRect.y + labelStateWordDiff * playerCount, labelStateWordRect.width, labelStateWordRect.height), strPlayerStateWord, Font_White);
				
					
					playerCount++;

				}
			

				for(int i = 0; i < (this.matchRoom.GetComponent<Room>().maxPlayerNumber - this.matchRoom.GetComponent<Room>().playersInRoom.Count); i++  ) {
					if(this.matchRoom.GetComponent<Room>().IsCreator(this.playerInfo.GUPID)) {
						if(GUI.Button(new Rect(labelBoardPlayerRect.x, labelBoardPlayerRect.y + labelBoardPlayerDiff * playerCount, labelBoardPlayerRect.width, labelBoardPlayerRect.height), strEmpty, Button_AddAI)) {
							StartAddAI();
						}
					}else{
						GUI.Label(new Rect(labelBoardPlayerRect.x, labelBoardPlayerRect.y + labelBoardPlayerDiff * playerCount, labelBoardPlayerRect.width, labelBoardPlayerRect.height), strEmpty, Button_AddAI_Dark);
					}
					
				}
			

				if(this.matchRoom.IsFull()) {
			
					if(selectedCarInsanityCarInfo.carID != 0) {
						if(!isLockedOn) {
							isLockOnButtonEnable = true;
						}else{
							isLockOnButtonEnable = false;
						}
					}else{
						isLockOnButtonEnable = false;
					}
				}else{
					isLockOnButtonEnable = false;
				}

				GUI.enabled = isLockOnButtonEnable;
				if(GUI.Button(buttonLockOnRect, strEmpty, Button_LockOn)) {
					StartCarSelect();
				}
				GUI.enabled = true;
			GUI.EndGroup();
			
			
			
			GUI.BeginGroup(groupBoardRightRect, strEmpty, Board_Right);
			
//				GUI.Label(labelBoardCarListRect, strEmpty, Board_CarList);
//			
//				carCountX = 0;
//				carCountY = 0;
//				
//				foreach(CarInsanityCarInfo carInfo in allCarInsanityCarInfo) {
//					
//					if(GUI.Button(new Rect(iconCarRect.x+iconCarDiffY*carCountX, iconCarRect.y+iconCarDiffY*carCountY, iconCarRect.width, iconCarRect.height), strEmpty, Icon_Car+carInfo.carID)) {							
//						selectedCarInsanityCarInfo = carInfo;
//					}
//					
//					carCountX++;
//					
//					if(carCountX == 5) {
//						carCountX = 0;
//						carCountY++;
//					}
//				}
	
				GUI.Label(labelBoardMapInfoRect, strEmpty, MapInfo_+matchRoom.matchMap.ToString());
			GUI.EndGroup();
			
			GUI.BeginGroup(groupRoomBannerRect, strEmpty, Room_Banner);
				
				if(this.matchRoom.IsFull()) {
					
				
					GUI.Label(labelTenDigitRect, strEmpty, Label_Digit_+tenDigit);
					GUI.Label(labelDigitRect, strEmpty, Label_Digit_+digit);
				}
			
				if(GUI.Button(buttonLogoutRect, strEmpty, Button_Logout)) {
					StartLeaveRoom();
				}

			GUI.EndGroup();

		}else{
			GUI.skin = null;
		}
	}
	#endregion
	
	
	void OnGUISkinLanguageChanged(Definition.eLanguage l) {
//		Debug.Log ("MatchController: OnGUISkinLanguageChanged");
		this.currentLanguage = l;
	}
	
	public void StartLeaveRoom() {
		if(PlayerPrefs.GetString("GameType") == "Network") {
			networkView.RPC("SendToGameLobby_LeaveMatchRoom", RPCMode.Server, this.matchRoom.GetComponent<Room>().roomIndex, this.playerInfo.GUPID);
		}else{
			selectedCarInsanityCarInfo = new CarInsanityCarInfo();
			isLockedOn = false;
			isStartPlay = false;
			StopCoroutine("StartCountDown");
//			this.actionState = MatchController.ActionState.LEFT;
			this.drawState = MatchController.DrawState.QUIT;
			this.isLeaving = true;
			
			OnLeaveFromMatchRoom();
		}
	}
	
	public void StartingMatch() {
		this.drawState = MatchController.DrawState.QUIT;
		this.isStartPlay = true;		
	}
	
	public void StartPreparingForPlay() {
		this.drawState = MatchController.DrawState.FREE;
//		isDraw = false;
		
		
		OnMatchStarted();
		playGameController.StartPreparingForPlay();
		
	}
	
	public void Back() {
		
		this.drawState = MatchController.DrawState.FREE;
		isDraw = false;
		Destroy(matchRoom.gameObject);
		Application.LoadLevel((int)Definition.eSceneID.MapScene);
		
//		if(PlayerPrefs.GetString("GameType", "Single") == "Network") {
//			this.drawState = MatchController.DrawState.FREE;
//			isDraw = false;
//			
//			Destroy(matchRoom.gameObject);
//			Application.LoadLevel(Definition.eSceneID.LobbyScene.ToString());
//			
//			
//			
//		}else{
//			this.drawState = MatchController.DrawState.FREE;
//			isDraw = false;
//			Destroy(matchRoom.gameObject);
//			Application.LoadLevel((int)Definition.eSceneID.MapScene);
//		}
		
		
	}
	
	public void OnCarSelectingChanged(CarInsanityCarInfo car) {
		
		foreach(CarInsanityCarInfo ownedCar in allCarInsanityCarInfo) {
			if(ownedCar.carID == car.carID) {
				selectedCarInsanityCarInfo = ownedCar;
				return;
			}
		}
		
		selectedCarInsanityCarInfo = new CarInsanityCarInfo();
		
		
	}
	
	public void StartAddAI() {
		if(PlayerPrefs.GetString("GameType", "Single") == "Network") {
			networkView.RPC("SendToGameLobby_AddAI", RPCMode.Server, this.matchRoom.GetComponent<Room>().roomIndex, this.playerInfo.GUPID);
		}
	}
	
	public void StartCarSelect() {
//		this.actionState = MatchController.ActionState.CARSELECTSTART;
		if(PlayerPrefs.GetString("GameType", "Single") == "Network")
		{
			networkView.RPC("SendToGameLobby_CarSelect", RPCMode.Server, this.matchRoom.GetComponent<Room>().roomIndex, this.playerInfo.GUPID, this.selectedCarInsanityCarInfo.carID, (this.selectedCarInsanityCarInfo.isTalentOpened) ? 1 : 0);
		}
		else
		{
			foreach(CarInsanityCarInfo c in allCarInsanityCarInfo) {
				if(c.carID == this.selectedCarInsanityCarInfo.carID) {
					this.matchRoom.GetCarInsanityPlayer(0).selectedCar = c;
					
					OnCarSelected();
				}
			}
			
			isLockedOn = false;
			isStartPlay = false;
			selectedCarInsanityCarInfo = new CarInsanityCarInfo();
			StopCoroutine("StartCountDown");
			
			this.StartingMatch();
			
			OnAllPlayerSelectCar();
		}
//		this.actionState = MatchController.ActionState.CARSELECTING;
	}
	
	public void AutoMatchStart(int matchFilterMap, int matchFilterMaxPlayerNumber) {
		if(PlayerPrefs.GetString("GameType", "Single") == "Network")
		{
			networkView.RPC("SendToGameLobby_AutoMatch", RPCMode.Server,  this.playerInfo.GUPID, matchFilterMap, matchFilterMaxPlayerNumber);
		}
		else
		{
			matchRoom = CreateMatchRoom(0, "Single", 4, matchFilterMap, "Single", this.playerInfo);
				
			lobbyController.EnteredMatchRoom();
			
			JoinMatchRoom(0, "Single", matchFilterMap, "Single", 4, 0, "player", 0, "player", 0);
			
//			string carList = PlayerPrefs.GetString("CarList", "0:0:1:POLICE_CAR, 0:0:3:AMBULANCE, 0:0:2:LORRY_TRUCK, 0:0:4:FIRE_FIGHTING_TRUCK, 0:0:5:GARBAGE_TRUCK");
			string carList = PlayerPrefs.GetString("OwnedCarList", "0:0:1:POLICE_CAR:0, 0:0:3:AMBULANCE:0, 0:0:2:LORRY_TRUCK:0, 0:0:4:FIRE_FIGHTING_TRUCK:0, 0:0:5:GARBAGE_TRUCK:0");
			string[] carListArray = carList.Split(',');

			allCarInsanityCarInfo = new List<CarInsanityCarInfo>();

			foreach(string carInfo in carListArray) {

				if(carInfo != ""){
					string[] carInfoArray = carInfo.Split(':');

					CarInsanityCarInfo c = new CarInsanityCarInfo();
					c.ID = Convert.ToInt32(carInfoArray[0]); //ID
					c.playerID = Convert.ToInt32(carInfoArray[1]); //Player_ID
					c.carID = Convert.ToInt32(carInfoArray[2]); //Car_ID
					c.name = carInfoArray[3]; //name
					c.isTalentOpened = Convert.ToBoolean(Convert.ToInt32(carInfoArray[4])); //isTalentOpened
					
					allCarInsanityCarInfo.Add(c);
				}
			}
			for(int i = 1; i < 4; i++)
			{
				JoinMatchRoom(0, "Single", matchFilterMap, "Single", 4, i, "AI" + i.ToString(), 0, "player", 1);
				
				this.matchRoom.GetCarInsanityPlayer(i).selectedCar = allCarInsanityCarInfo[UnityEngine.Random.Range(0, allCarInsanityCarInfo.Count)];
			}
		}
	}
	
	public MatchRoom CreateMatchRoom(int roomIndex, string roomName, int maxPlayerNumber, int matchMap, string matchType, GameUserPlayer creator) {
	
		matchRoom = GameObject.Instantiate(matchRoomPrefab, Vector3.zero, Quaternion.identity) as MatchRoom;
		matchRoom.matchMap = matchMap;
		matchRoom.matchType = matchType;
		matchRoom.GetComponent<Room>().roomIndex = roomIndex;
		matchRoom.GetComponent<Room>().roomName = roomName;
		matchRoom.GetComponent<Room>().maxPlayerNumber = maxPlayerNumber;
		matchRoom.GetComponent<Room>().creator = creator;
		
		matchRoom.name = matchRoom.GetComponent<Room>().roomIndex.ToString();
		matchRoom.transform.parent = gameObject.transform;
		
		return matchRoom;

	}
	
	public void JoinMatchRoom(int roomIndex, string roomName, int matchMap, string matchType, int maxPlayerNumber, int joinerGUPID, string joinerName, int creatorGUPID, string creatorName, int isAI) {
		//lobby sends data of all player that in the match

		GameUserPlayer joiner = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameUserPlayer;
		joiner.GUPID = joinerGUPID;
		joiner.playerName = joinerName;
		joiner.isAI = Convert.ToBoolean(isAI);
		
		Transform roomTransform = transform.Find(roomIndex.ToString());
		
		if(roomTransform == null) {
			
//			Debug.Log ("matchRoom == null");
			GameUserPlayer creator = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameUserPlayer;
			joiner.GUPID = creatorGUPID;
			joiner.playerName = creatorName;
			joiner.isAI = false;
			
			matchRoom = CreateMatchRoom(roomIndex, roomName, maxPlayerNumber, matchMap, matchType, creator);
		
		}
		
		if(!this.matchRoom.GetComponent<Room>().IsPlayerInRoom(joinerGUPID)) {

			this.matchRoom.PlayerJoin(joiner);			
			joiner.transform.parent = matchRoom.transform;
			
			if(this.matchRoom.IsFull()) {
				
				StartCoroutine("StartCountDown");
				if(PlayerPrefs.GetString("GameType", "Single") == "Network")
				{
					//if i m a creator, random choose a car for AI
					if(this.matchRoom.GetComponent<Room>().IsCreator(this.playerInfo.GUPID)) {
						foreach(GameUserPlayer u in this.matchRoom.GetComponent<Room>().playersInRoom) {
							if(u.isAI) {
								
								int randomCarForAI = UnityEngine.Random.Range(1, carAvailableKeyList.Count+1);
								networkView.RPC("SendToGameLobby_CarSelect", RPCMode.Server, this.matchRoom.GetComponent<Room>().roomIndex, u.GUPID, randomCarForAI, 0);
							}
						}
					}
				}
			}
			
		}else{
//			Debug.Log("Player "+joinerName+" is in the room");
		}
	}
	
	private IEnumerator StartCountDown() {
		for(int i = selectingCarTotalTime; i >= 0; i-- ) {

			tenDigit = i / 10;
			digit = i % 10;	
			yield return new WaitForSeconds(1.0f);
		}
//		Debug.Log("isLockedOn : " + isLockedOn + " isStartPlay : " + isStartPlay);
		if(!isLockedOn) {

			selectedCarInsanityCarInfo.carID = allCarInsanityCarInfo[UnityEngine.Random.Range(0, allCarInsanityCarInfo.Count)].carID;
			
			StartCarSelect();
		}
		yield return null;
	}
	
	private	IEnumerator PlayOwnerBoard2DAnimation() {
		int n = 0;
		while(!isLockedOn) {
//			Debug.Log(n);
			Board_Owner = Board_Owner_+ n;
			yield return new WaitForSeconds(0.1f);
			
			n++;
			
			if(n > 5) {
				n = 0;
			}
		}
		yield return null;
	}
	
	public void OnLogined() {
//		Debug.Log ("Called");
		networkView.RPC("SendToGameLobby_LoadCarList", RPCMode.Server, this.playerInfo.GUPID, this.playerInfo.playerID);
	}
	
	private void LoadCarListStart() {
//		this.actionState = MatchController.ActionState.CARLISTLOADSTART;
		if(PlayerPrefs.GetString("GameType", "Single") == "Network")
		{
			networkView.RPC("SendToGameLobby_LoadCarList", RPCMode.Server, this.playerInfo.GUPID, this.playerInfo.playerID);
		}
//		this.actionState = MatchController.ActionState.CARLISTLOADING;
		if(PlayerPrefs.GetString("GameType", "Single") == "Single")
		{
//			this.actionState = MatchController.ActionState.CARLISTLOADED;
		}
	}
	
	void OnLevelWasLoaded(int mapIndex) {
//		Debug.Log(strTest);
		
		if(mapIndex == (int)Definition.eSceneID.MatchRoomScene) {
			
			this.drawState = MatchController.DrawState.ENTER;
			isDraw = true;
			
			switch(currentLanguage) {
				case Definition.eLanguage.ENGLISH:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.ENGLISH");
					this.matchRoomSkin = Resources.Load("Skin/RoomEnglishLanguage", typeof(GUISkin)) as GUISkin;
					break;
				
				case Definition.eLanguage.SIMPLECHINESE:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.SIMPLECHINESE");
					this.matchRoomSkin = Resources.Load("Skin/RoomSimpleChineseLanguage",  typeof(GUISkin)) as GUISkin;
					break;
				
				case Definition.eLanguage.TRANDITIONALCHIINESE:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.TRANDITIONALCHIINESE");
					this.matchRoomSkin = Resources.Load("Skin/RoomTranditionalChineseLanguage",  typeof(GUISkin)) as GUISkin;
					break;
			}
			
			matchRoomSceneViewer = GameObject.FindObjectOfType(typeof(MatchRoomSceneViewer)) as MatchRoomSceneViewer;
			lobbySceneViewer = GameObject.FindObjectOfType(typeof(LobbySceneViewer)) as LobbySceneViewer;
			
			LoadCarListStart();
			enabled = true;
			
			if(PlayerPrefs.GetString("GameType") == "Network") {
				OnJoinedMatch();
			}
			
		}else{
			enabled = false;
			this.drawState = MatchController.DrawState.FREE;
			isDraw = false;
			this.matchRoomSkin = null;
		}
		
//		Debug.Log("Match:OnLevelWasLoaded");
	}
	
	#region RPC
	[RPC]
	public void SendToGameLobby_AutoMatch(int requestPlayerGUPID, int matchFilterMap, int matchFilterMaxPlayerNumber) {
		
	}
	
	[RPC]
	public void SendToGameLobby_LeaveMatchRoom(int roomIndex, int GUPID) {
		
	}
	
	[RPC]
	public void SendToGameLobby_LoadCarList(int GUPID, int playerID) {
		
	}
	
	[RPC]
	public void SendToGameLobby_CarSelect(int roomIndex, int GUPID, int car_ID, int isTalentOpened) {
		
	}
	
	[RPC]
	public void SendToGameLobby_AddAI(int roomIndex, int GUPID) {
		
	}
	
	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_CreateMatchRoom(int gupid, int roomIndex, string roomName, int maxPlayerNumber, int gameMap, string matchType, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		if(resultState == Definition.RPCProcessState.SUCCESS) {			
			if(this.playerInfo.GUPID == gupid) {
				
				matchRoom = CreateMatchRoom(roomIndex, roomName, maxPlayerNumber, gameMap, matchType, this.playerInfo);
				
				lobbyController.EnteredMatchRoom();
				
				
			}else{
//				this.errorMsg = "FAIL. You are not the correct member.";
//				Debug.Log(errorMsg);
				lobbySceneViewer.CreateMatchErrorSceneSetting();
			}
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			lobbySceneViewer.CreateMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			lobbySceneViewer.CreateMatchErrorSceneSetting();
		}else{
//			Debug.Log(p.ToString());
			lobbySceneViewer.CreateMatchErrorSceneSetting();
		}
	}
	
	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_JoinMatchRoom(int roomIndex, string roomName, int matchMap, string matchType, int maxPlayerNumber, int joinerGUPID, string joinerName, int creatorGUPID, string creatorName, int isAI, int p, NetworkMessageInfo msgInfo) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
 		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			
			JoinMatchRoom(roomIndex, roomName, matchMap, matchType, maxPlayerNumber, joinerGUPID, joinerName, creatorGUPID, creatorName, isAI);
			
			if(joinerGUPID == this.playerInfo.GUPID) {
				
				lobbyController.EnteredMatchRoom();
			}
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			lobbySceneViewer.JoinMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERINROOM){
			
//			this.errorMsg = "PLAYERINROOM";
//			Debug.Log(errorMsg);
			lobbySceneViewer.JoinMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.FULL){
			
//			this.errorMsg = "FULL";
//			Debug.Log(errorMsg);	
			lobbySceneViewer.JoinMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST){
			
//			this.errorMsg = "ROOMNOTEXIST";
//			Debug.Log(errorMsg);
			lobbySceneViewer.JoinMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			lobbySceneViewer.JoinMatchErrorSceneSetting();
		}else{
			
			lobbySceneViewer.JoinMatchErrorSceneSetting();
//			Debug.Log(p.ToString());

		}
	}
	
	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_AutoMatch(int requestPlayer, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
 
		if(this.playerInfo.GUPID == requestPlayer) {
			if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
				lobbySceneViewer.AutoMatchErrorSceneSetting();
			}
		}else{
//			this.errorMsg = "FAIL. You are not the correct member.";
//			Debug.Log(errorMsg);
			lobbySceneViewer.AutoMatchErrorSceneSetting();
		}
		
	}
	
	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_LeaveMatchRoom(int roomIndex, int leaverGUPID, int p, NetworkMessageInfo msgInfo) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
// 		Debug.Log("ReceiveByClientPortal_LeaveMatchRoom");
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			if((this.matchRoom != null)) {
				if(this.matchRoom.GetComponent<Room>().roomIndex == roomIndex) {

					if(this.playerInfo.GUPID != leaverGUPID) {
			
						if(this.matchRoom.GetComponent<Room>().IsPlayerInRoom(leaverGUPID)) {
							
							GameUserPlayer leaver = this.matchRoom.GetComponent<Room>().GetPlayer(leaverGUPID);
							 
							this.matchRoom.PlayerLeave(leaver);
							Destroy(leaver.gameObject);
							StopCoroutine("StartCountDown");
						}
						
					}else{
//						Debug.Log("you are left");							
						
//						Destroy(matchRoom.gameObject);
						
						selectedCarInsanityCarInfo = new CarInsanityCarInfo();
						isLockedOn = false;
						isStartPlay = false;
						StopCoroutine("StartCountDown");
//						this.actionState = MatchController.ActionState.LEFT;
						this.drawState = MatchController.DrawState.QUIT;
						this.isLeaving = true;
						
						OnLeaveFromMatchRoom();
					}
					
				}else{
//					this.errorMsg = "Game index not match!";
//					Debug.Log(errorMsg);
					matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
				}				
			}else{

//				this.errorMsg = "Game not available (it is null)!";
//				Debug.Log(errorMsg);
				matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
			}
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM){
//			this.errorMsg = "PLAYERNOTINROOM";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST){
			
//			this.errorMsg = "ROOMNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
			
		}else{
//			Debug.Log(p.ToString());
			matchRoomSceneViewer.LeaveMatchErrorSceneSetting();
		}
	}

	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_Abdicate(int roomIndex, int requestGUPID, int p, NetworkMessageInfo msgInfo) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			if((this.matchRoom != null)) {
				if (this.matchRoom.GetComponent<Room>().roomIndex == roomIndex){
					
					if(this.matchRoom.GetComponent<Room>().IsPlayerInRoom(requestGUPID)){
						
						GameUserPlayer requester = this.matchRoom.GetComponent<Room>().GetPlayer(requestGUPID);
						this.matchRoom.GetComponent<Room>().creator = requester;
					
					}
					
				}else{
//					this.errorMsg = "Game index not match!";
//					Debug.Log(errorMsg);
					matchRoomSceneViewer.AbdicateErrorSceneSetting();
				}
			}else{
//				this.errorMsg = "Game not available (it is null)!";
//				Debug.Log(errorMsg);
				matchRoomSceneViewer.AbdicateErrorSceneSetting();
			}
		}else if(resultState == Definition.RPCProcessState.FAIL){
//			this.errorMsg = "FAIL";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM){
			
//			this.errorMsg = "PLAYERNOTINROOM";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST){
			
//			this.errorMsg = "ROOMNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}else{
//			Debug.Log(p.ToString());
			matchRoomSceneViewer.AbdicateErrorSceneSetting();
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_LoadCarList(int gupid, string carList, int p, NetworkMessageInfo msgInfo) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			
			if(this.playerInfo.GUPID == gupid) {
//				Debug.Log("LoadCarList:"+carList);
				PlayerPrefs.SetString("OwnedCarList", carList);
				string[] carListArray = carList.Split(',');

				allCarInsanityCarInfo = new List<CarInsanityCarInfo>();
				int i = 0;

				foreach(string carInfo in carListArray) {

					if(carInfo != ""){
						string[] carInfoArray = carInfo.Split(':');

						CarInsanityCarInfo c = new CarInsanityCarInfo();
						c.ID = Convert.ToInt32(carInfoArray[0]); //ID
						c.playerID = Convert.ToInt32(carInfoArray[1]); //Player_ID
						c.carID = Convert.ToInt32(carInfoArray[2]); //Car_ID
						c.name = carInfoArray[3]; //name
//						Debug.Log("ReceiveByClientPortal_LoadCarList:"+carInfoArray[4]);
						c.isTalentOpened = Convert.ToBoolean(Convert.ToInt32(carInfoArray[4])); //isTalentOpened
						
						allCarInsanityCarInfo.Add(c);

						i++;
					}
				}
				
//				this.actionState = MatchController.ActionState.CARLISTLOADED;
			}else{
//				this.errorMsg = "FAIL. You are not the correct member.";
//				Debug.Log(errorMsg);
								
				matchRoomSceneViewer.LoadCarErrorSceneSetting();
			}
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LoadCarErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "USERNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.LoadCarErrorSceneSetting();
			
		}else{
//			Debug.Log(p.ToString());
			matchRoomSceneViewer.LoadCarErrorSceneSetting();
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_SelectCar(int roomIndex, int selectGUPID, int car_ID, int isTalentOpened, int p) {
//		Debug.Log("ReceiveByClientPortal_SelectCar - GUPID:"+selectGUPID+", Car:"+car_ID);
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			
			if((this.matchRoom != null)) {
				if (this.matchRoom.GetComponent<Room>().roomIndex == roomIndex){
					
					if(this.matchRoom.GetComponent<Room>().IsPlayerInRoom(selectGUPID)){
						
						if(selectGUPID == this.playerInfo.GUPID) {
							isLockedOn = true;
							foreach(CarInsanityCarInfo c in allCarInsanityCarInfo) {
								if(c.carID == car_ID) {
									this.matchRoom.GetCarInsanityPlayer(selectGUPID).selectedCar = c;
									this.playerInfo.GetComponent<CarInsanityPlayer>().selectedCar = c;
								}
							}
							
//							this.actionState = MatchController.ActionState.CARSELECTED;
							
							OnCarSelected();
						}else{
							CarInsanityCarInfo othersCar = new CarInsanityCarInfo();
							
							othersCar.carID = car_ID;
							othersCar.isTalentOpened = System.Convert.ToBoolean(isTalentOpened);
							othersCar.name = ((Definition.eCarID)car_ID).ToString();
							this.matchRoom.GetCarInsanityPlayer(selectGUPID).selectedCar = othersCar;
							
						}

					}else{
//						this.errorMsg = "player is not in the room!";
//						Debug.Log(errorMsg);
						matchRoomSceneViewer.SelectCarErrorSceneSetting();
					}
					
				}else{

//					this.errorMsg = "Game index not match!";
//					Debug.Log(errorMsg);
					matchRoomSceneViewer.SelectCarErrorSceneSetting();
				}
			}else{

//				this.errorMsg = "Game not available (it is null)!";
//				Debug.Log(errorMsg);
				matchRoomSceneViewer.SelectCarErrorSceneSetting();
			}
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM){
			
//			this.errorMsg = "PLAYERNOTINROOM";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST){
			
//			this.errorMsg = "ROOMNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.FAIL){
			
//			this.errorMsg = "FAIL";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
		}else{
//			Debug.Log(p.ToString());
			matchRoomSceneViewer.SelectCarErrorSceneSetting();
		}
	}

	[RPC] //Define for client's RPC
	public void ReceiveByClientPortal_StartMatch(int gupid, int roomIndex, string gameServerIP, int gameServerPort, int p) {
//		Debug.Log("ReceiveByClientPortal_StartMatch - IP:"+gameServerIP+", Port:"+gameServerPort);
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			if(this.playerInfo.GUPID == gupid) {
				// 1. check game index
				// 2. check target user
				if((this.matchRoom!= null)) {
					if (this.matchRoom.GetComponent<Room>().roomIndex == roomIndex){
						
						isLockedOn = false;
						isStartPlay = false;
						selectedCarInsanityCarInfo = new CarInsanityCarInfo();
						StopCoroutine("StartCountDown");
						playGameController.gameServerIP = gameServerIP;
						playGameController.gameServerPort = gameServerPort;
						
						this.StartingMatch();

						OnAllPlayerSelectCar();
						
					}else{
						
//						this.errorMsg = "Game index not match!";
//						Debug.Log(errorMsg);
						matchRoomSceneViewer.StartMatchErrorSceneSetting();
					}
				}else{
					
//					this.errorMsg = "Game not available (it is null)!";
//					Debug.Log(errorMsg);
					matchRoomSceneViewer.StartMatchErrorSceneSetting();
				}
			}else{
//				this.errorMsg = "You are not the correct member.";
//				Debug.Log(errorMsg);
				matchRoomSceneViewer.StartMatchErrorSceneSetting();
			}
			
			
		}else if(resultState == Definition.RPCProcessState.FAIL){
//			this.errorMsg = "FAIL";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM){
			
//			this.errorMsg = "PLAYERNOTINROOM";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST){
			
//			this.errorMsg = "ROOMNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
			
//			this.errorMsg = "PLAYERNOTEXIST";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
			
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(errorMsg);
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
		}else{
//			Debug.Log(p.ToString());
			matchRoomSceneViewer.StartMatchErrorSceneSetting();
		}
	}

	
	#endregion
	

	
}
