using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LobbyController : MonoBehaviour {
	
	public enum DrawState {
		FREE,
		ENTER,
		LOBBY,
		QUIT
		
	}
	
	public enum FocusState {
		LOBBY,
		FILTERING,
		MATCHING,
	}
	
	public enum ActionState {
		FREE,
		
		LOADSTART,
		LOADING,
		LOADED,
		LOADERROR,
		
		LOBBYJOINSTART,
		LOBBYJOINING,
		LOBBYJOINED,
		LOBBYJOINERROR,
		
		AUTOMATCHSTART,
		AUTOMATCHING,
		AUTOMATCHED,
		AUTOMATCHERROR,
		
		//ANNOUNCEERROR
		
	}
	
	public delegate void OnEnteredLobbyEventHandler();
	public static event OnEnteredLobbyEventHandler OnEnteredLobby;
	
	public delegate void OnLeaveLobbyEventHandler();
	public static event OnLeaveLobbyEventHandler OnLeaveLobby;
	
//	public delegate void OnAfterLobbyBoardUpEventHandler();
//	public static event OnAfterLobbyBoardUpEventHandler OnAfterLobbyBoardUp;
	
	public delegate void OnAfterLobbyBoardDownEventHandler();
	public static event OnAfterLobbyBoardDownEventHandler OnAfterLobbyBoardDown;
	
	public delegate void OnBeforeLobbyBoardUpEventHandler();
	public static event OnBeforeLobbyBoardUpEventHandler OnBeforeLobbyBoardUp;
	
	public delegate void OnSelectingMapEventHandler();
	public static event OnSelectingMapEventHandler OnSelectingMap;
	
	
	
	
	public LobbyController.DrawState drawState = LobbyController.DrawState.FREE;
	public LobbyController.FocusState focusState = LobbyController.FocusState.LOBBY;
	public LobbyController.ActionState actionState = LobbyController.ActionState.FREE;
	
//	private string errorMsg = "";
	
//	public GUISkin englishSkin;
//	public GUISkin simpleChineseSkin;
//	public GUISkin tranditionalChineseSkin;
//	
	#region GUI
	

	private GUISkin lobbySkin;
	private Definition.eLanguage currentLanguage;
	private string chatInput = "";
	private List<ChatMessage> playerActionBoard = new List<ChatMessage>();
	//private ArrayList messageBoard = new ArrayList();
	
	private int matchFilterMap = 0;
	private int matchFilterMaxPlayerNumber = 0;
	private string strArrayMaxPlayerNumberTwo = "2";
	private string strArrayMaxPlayerNumberThree = "3";
	private string strArrayMaxPlayerNumberFour = "4";
//	private int selectedMaxPlayerNumber = 0;
	
	private int screenX = 0;
	private int screenY = 0;
	private int screenWidth = 1024;
	private int screenHeight = 768;
	
	public bool isDraw = false;
	
	private Vector2 messageScrollPosition;
	private Vector2 playerActionInfoScrollPosition;
	private Vector2 playerListScrollPosition;
	
	private string strEmpty = "";
	private string strSystemName = "System";
	private string Font_White = "Font_White";
	private string strColon = ":";
	private string strLeftParentheses = "(";
	private string strRightParentheses = ")";
	private int playerCount = 0;
	private Color darkTransparent = new Color(0.5f,0.5f,0.5f, 0.5f);
	
//	private string windowLobbyControlName = "Lobby";
	private float windowLobbyX = 0.0f;
	private float windowLobbyY = 0.0f;
	private float windowLobbyWidth = 0.0f;
	private float windowLobbyHeight = 0.0f;
	private float windowLobbyStartX = 0.0f;
	private float windowLobbyStartY = 0.0f;	
	private float windowLobbyTextureWidth = 1024.0f;
	private float windowLobbyTextureHeight = 768.0f;
	private Rect windowLobbyRect;
//	private Rect windowLobbyDarkRect;
	
	private string Window_Matching = "Window_Matching";
//	private string windowMatchingControlName = "Matching";
	private float windowMatchingX = 0.0f;
	private float windowMatchingY = 0.0f;
	private float windowMatchingWidth = 0.0f;
	private float windowMatchingHeight = 0.0f;
	private float windowMatchingStartX = 325.0f;
	private float windowMatchingStartY = 384.0f;	
	private float windowMatchingTextureWidth = 400.0f;
	private float windowMatchingTextureHeight = 199.0f;
	private Rect windowMatchingRect;
	
	
	private Rect matchingWindow;
	
	//Logout button
	private string Button_Logout = "Button_Logout";
	private float buttonLogoutX = 0.0f;
	private float buttonLogoutY = 0.0f;
	private float buttonLogoutWidth = 0.0f;
	private float buttonLogoutHeight = 0.0f;
	private float buttonLogoutStartX = 903.0f;
	private float buttonLogoutStartY = 16.0f;	
	private float buttonLogoutTextureWidth = 101.0f;
	private float buttonLogoutTextureHeight = 83.0f;
	private Rect buttonLogoutRect;
	
	//PlayerInfo button
//	private string Button_PlayerInfo = "Button_PlayerInfo";
//	private float buttonPlayerInfoX = 0.0f;
//	private float buttonPlayerInfoY = 0.0f;
//	private float buttonPlayerInfoWidth = 0.0f;
//	private float buttonPlayerInfoHeight = 0.0f;
//	private float buttonPlayerInfoStartX = 17.0f;
//	private float buttonPlayerInfoStartY = 15.0f;	
//	private float buttonPlayerInfoTextureWidth = 101.0f;
//	private float buttonPlayerInfoTextureHeight = 83.0f;
//	private Rect buttonPlayerInfoRect;
	
	//StartGame button
	private string Button_StartGame = "Button_StartGame";
	private float buttonStartGameX = 0.0f;
	private float buttonStartGameY = 0.0f;
	private float buttonStartGameWidth = 0.0f;
	private float buttonStartGameHeight = 0.0f;
	private float buttonStartGameStartX = 433.0f;
	private float buttonStartGameStartY = 15.0f;	
	private float buttonStartGameTextureWidth = 147.0f;
	private float buttonStartGameTextureHeight = 74.0f;
	private Rect buttonStartGameRect;
	
	// Lobby Banner Group
	private string Lobby_Banner = "Lobby_Banner";
	private float groupLobbyBannerX = 0.0f;
	private float groupLobbyBannerY = 0.0f;
	private float groupLobbyBannerWidth = 0.0f;
	private float groupLobbyBannerHeight = 0.0f;
	private float groupLobbyBannerStartX = 1.0f;
	private float groupLobbyBannerStartY = 0.0f;	
	private float groupLobbyBannerTextureWidth = 1022.0f;
	private float groupLobbyBannerTextureHeight = 163.0f;
	private Rect groupLobbyBannerRect;
	
	//Lobby_Board Group
	private string Lobby_Board = "Lobby_Board";
	private float groupLobbyBoardX = 0.0f;
	private float groupLobbyBoardY = 0.0f;
	private float groupLobbyBoardWidth = 0.0f;
	private float groupLobbyBoardHeight = 0.0f;
	private float groupLobbyBoardStartX = 0.0f;
	private float groupLobbyBoardStartY = 0.0f;	
	private float groupLobbyBoardTextureWidth = 1024.0f;
	private float groupLobbyBoardTextureHeight = 768.0f;
	private Rect groupLobbyBoardRect;
	
	//Board_SysTalk Group
	private string Board_SysTalk = "Board_SysTalk";
	private float groupBoardSysTalkX = 0.0f;
	private float groupBoardSysTalkY = 0.0f;
	private float groupBoardSysTalkWidth = 0.0f;
	private float groupBoardSysTalkHeight = 0.0f;
	private float groupBoardSysTalkStartX = 35.0f;
	private float groupBoardSysTalkStartY = 98.0f;	
	private float groupBoardSysTalkTextureWidth = 653.0f;
	private float groupBoardSysTalkTextureHeight = 661.0f;
	private Rect groupBoardSysTalkRect;
	
	//Board_PlayerList Group
	private string Board_PlayerList = "Board_PlayerList";
	private float groupBoardPlayerListX = 0.0f;
	private float groupBoardPlayerListY = 0.0f;
	private float groupBoardPlayerListWidth = 0.0f;
	private float groupBoardPlayerListHeight = 0.0f;
	private float groupBoardPlayerListStartX = 688.0f;
	private float groupBoardPlayerListStartY = 98.0f;	
	private float groupBoardPlayerListTextureWidth = 307.0f;
	private float groupBoardPlayerListTextureHeight = 661.0f;
	private Rect groupBoardPlayerListRect;
	
	//Ad_ChinaMap 
//	private string Ad_ChinaMap = "Ad_ChinaMap";
	private float labelAdChinaMapX = 0.0f;
	private float labelAdChinaMapY = 0.0f;
	private float labelAdChinaMapWidth = 0.0f;
	private float labelAdChinaMapHeight = 0.0f;
	private float labelAdChinaMapStartX = 62.0f;
	private float labelAdChinaMapStartY = 183.0f;	
	private float labelAdChinaMapTextureWidth = 596.0f;
	private float labelAdChinaMapTextureHeight = 180.0f; 
	private Rect labelAdChinaMapRect;
	
	
	//Board_News 
	private string Board_News = "Board_News";
	private float labelBoardNewsX = 0.0f;
	private float labelBoardNewsY = 0.0f;
	private float labelBoardNewsWidth = 0.0f;
	private float labelBoardNewsHeight = 0.0f;
	private float labelBoardNewsStartX = 57.0f;
	private float labelBoardNewsStartY = 134.0f;	
	private float labelBoardNewsTextureWidth = 606.0f;
	private float labelBoardNewsTextureHeight = 231.0f; 
	private Rect labelBoardNewsRect;
	
	//Board Message
	private float labelBoardMessageX = 0.0f;
	private float labelBoardMessageY = 0.0f;
	private float labelBoardMessageWidth = 0.0f;
	private float labelBoardMessageHeight = 0.0f;
	private float labelBoardMessageStartX = 57.0f;
	private float labelBoardMessageStartY = 378.0f;	
	private float labelBoardMessageTextureWidth = 606.0f;
	private float labelBoardMessageTextureHeight = 302.0f; 
	private Rect labelBoardMessageRect;
	
	//TextField Message
	private float textFieldMessageX = 0.0f;
	private float textFieldMessageY = 0.0f;
	private float textFieldMessageWidth = 0.0f;
	private float textFieldMessageHeight = 0.0f;
	private float textFieldMessageStartX = 57.0f;
	private float textFieldMessageStartY = 690.0f;	
	private float textFieldMessageTextureWidth = 508.0f;
	private float textFieldMessageTextureHeight = 52.0f; 
	private Rect textFieldMessageRect;
	
	//Button Message
	private string Button_Message = "Button_Message";
	private float buttonMessageX = 0.0f;
	private float buttonMessageY = 0.0f;
	private float buttonMessageWidth = 0.0f;
	private float buttonMessageHeight = 0.0f;
	private float buttonMessageStartX = 564.0f;
	private float buttonMessageStartY = 687.0f;	
	private float buttonMessageTextureWidth = 103.0f;
	private float buttonMessageTextureHeight = 58.0f; 
	private Rect buttonMessageRect;
	
	//Tab_AllPlayers TabAllPlayers
	private string Tab_AllPlayers = "Tab_AllPlayers";
	private float labelTabAllPlayersX = 0.0f;
	private float labelTabAllPlayersY = 0.0f;
	private float labelTabAllPlayersWidth = 0.0f;
	private float labelTabAllPlayersHeight = 0.0f;
	private float labelTabAllPlayersStartX = 709.0f;
	private float labelTabAllPlayersStartY = 133.0f;	
	private float labelTabAllPlayersTextureWidth = 128.0f;
	private float labelTabAllPlayersTextureHeight = 46.0f; 
	private Rect labelTabAllPlayersRect;
	
	//Tab_Empty TabEmpty
	private string Tab_Empty = "Tab_Empty";
	private float labelTabEmptyX = 0.0f;
	private float labelTabEmptyY = 0.0f;
	private float labelTabEmptyWidth = 0.0f;
	private float labelTabEmptyHeight = 0.0f;
	private float labelTabEmptyStartX = 839.0f;
	private float labelTabEmptyStartY = 133.0f;	
	private float labelTabEmptyTextureWidth = 128.0f;
	private float labelTabEmptyTextureHeight = 44.0f; 
	private Rect labelTabEmptyRect;
	
	//Board_PlayerBoxList 
	private string Board_PlayerBoxList = "Board_PlayerBoxList";
	private float labelBoardPlayerBoxListX = 0.0f;
	private float labelBoardPlayerBoxListY = 0.0f;
	private float labelBoardPlayerBoxListWidth = 0.0f;
	private float labelBoardPlayerBoxListHeight = 0.0f;
	private float labelBoardPlayerBoxListStartX = 709.0f;
	private float labelBoardPlayerBoxListStartY = 177.0f;	
	private float labelBoardPlayerBoxListTextureWidth = 257.0f;
	private float labelBoardPlayerBoxListTextureHeight = 567.0f; 
	private Rect labelBoardPlayerBoxListRect;
	
	//Area_PlayerBoxList 
	private float areaOtherPlayerBoxListX = 0.0f;
	private float areaOtherPlayerBoxListY = 0.0f;
	private float areaOtherPlayerBoxListWidth = 0.0f;
	private float areaOtherPlayerBoxListHeight = 0.0f;
	private float areaOtherPlayerBoxListStartX = 708.0f;
	private float areaOtherPlayerBoxListStartY = 250.0f;	
	private float areaOtherPlayerBoxListTextureWidth = 255.0f;
	private float areaOtherPlayerBoxListTextureHeight = 494.0f; 
	private Rect areaOtherPlayerBoxListRect;
	private Rect areaOtherPlayerBoxListViewRect;
	
	//Box_OwnPlayer (height diff = 70px)
	private string Box_OwnPlayer = "Box_OwnPlayer";
	private float labelBoxOwnPlayerX = 0.0f;
	private float labelBoxOwnPlayerY = 0.0f;
	private float labelBoxOwnPlayerWidth = 0.0f;
	private float labelBoxOwnPlayerHeight = 0.0f;
	private float labelBoxOwnPlayerStartX = 714.0f;
	private float labelBoxOwnPlayerStartY = 185.0f;	
	private float labelBoxOwnPlayerTextureWidth = 247.0f;
	private float labelBoxOwnPlayerTextureHeight = 64.0f; 
	private Rect labelBoxOwnPlayerRect;
//	private float labelBoxOwnPlayerDiff = 70.0f;
	
	//Box_OtherPlayer (height diff = 70px)
	private string Box_OtherPlayer = "Box_OtherPlayer";
	private float labelBoxOtherPlayerX = 0.0f;
	private float labelBoxOtherPlayerY = 0.0f;
	private float labelBoxOtherPlayerWidth = 0.0f;
	private float labelBoxOtherPlayerHeight = 0.0f;
	private float labelBoxOtherPlayerStartX = 714.0f;
	private float labelBoxOtherPlayerStartY = 255.0f;	
	private float labelBoxOtherPlayerTextureWidth = 247.0f;
	private float labelBoxOtherPlayerTextureHeight = 64.0f; 
	private Rect labelBoxOtherPlayerRect;
	private float labelBoxOtherPlayerDiff = 70.0f;
	
	//Icon_Player  (height diff = 70px)
//	private string Icon_Player = "Icon_Player";
	private float labelIconOwnPlayerX = 0.0f;
	private float labelIconOwnPlayerY = 0.0f;
	private float labelIconOwnPlayerWidth = 0.0f;
	private float labelIconOwnPlayerHeight = 0.0f;
	private float labelIconOwnPlayerStartX = 728.0f;
	private float labelIconOwnPlayerStartY = 192.0f;	
	private float labelIconOwnPlayerTextureWidth = 50.0f;
	private float labelIconOwnPlayerTextureHeight = 50.0f; 
	private Rect labelIconOwnPlayerRect;
	private float labelIconOwnPlayerDiff = 70.0f;
	
	//Icon_OtherPlayer
	private float labelIconOtherPlayerX = 0.0f;
	private float labelIconOtherPlayerY = 0.0f;
	private float labelIconOtherPlayerWidth = 0.0f;
	private float labelIconOtherPlayerHeight = 0.0f;
	private float labelIconOtherPlayerStartX = 728.0f;
	private float labelIconOtherPlayerStartY = 262.0f;	
	private float labelIconOtherPlayerTextureWidth = 50.0f;
	private float labelIconOtherPlayerTextureHeight = 50.0f; 
	private Rect labelIconOtherPlayerRect;
	private float labelIconOtherPlayerDiff = 70.0f;
	
	//labelOwnPlayerNameRect
	private float labelOwnPlayerNameX = 0.0f;
	private float labelOwnPlayerNameY = 0.0f;
	private float labelOwnPlayerNameWidth = 0.0f;
	private float labelOwnPlayerNameHeight = 0.0f;
	private float labelOwnPlayerNameStartX = 791.0f;
	private float labelOwnPlayerNameStartY = 193.0f;	
	private float labelOwnPlayerNameTextureWidth = 150.0f;
	private float labelOwnPlayerNameTextureHeight = 150.0f; 
	private Rect labelOwnPlayerNameRect;
	
	//labelOtherPlayerNameRect (height diff = 70px)
	private float labelOtherPlayerNameX = 0.0f;
	private float labelOtherPlayerNameY = 0.0f;
	private float labelOtherPlayerNameWidth = 0.0f;
	private float labelOtherPlayerNameHeight = 0.0f;
	private float labelOtherPlayerNameStartX = 791.0f;
	private float labelOtherPlayerNameStartY = 264.0f;	
	private float labelOtherPlayerNameTextureWidth = 150.0f;
	private float labelOtherPlayerNameTextureHeight = 150.0f; 
	private Rect labelOtherPlayerNameRect;
	private float labelOtherPlayerNameDiff = 70.0f;
	
	//Board_MatchFiltering
	private string Board_MatchFiltering = "Board_MatchFiltering";
//	private string labelBoardMatchFilteringControlName = "MatchFiltering";
	private float labelBoardMatchFilteringX = 0.0f;
	private float labelBoardMatchFilteringY = 0.0f;
	private float labelBoardMatchFilteringWidth = 0.0f;
	private float labelBoardMatchFilteringHeight = 0.0f;
	private float labelBoardMatchFilteringStartX = 350.0f;
	private float labelBoardMatchFilteringStartY = 102.0f;	
	private float labelBoardMatchFilteringTextureWidth = 321.0f;
	private float labelBoardMatchFilteringTextureHeight = 509.0f;
	private Rect labelBoardMatchFilteringRect;
	
	//Board_SelectMapTitle
	private string Board_SelectMapTitle = "Board_SelectMapTitle";
	private float labelBoardSelectMapTitleX = 0.0f;
	private float labelBoardSelectMapTitleY = 0.0f;
	private float labelBoardSelectMapTitleWidth = 0.0f;
	private float labelBoardSelectMapTitleHeight = 0.0f;
	private float labelBoardSelectMapTitleStartX = 375.0f;
	private float labelBoardSelectMapTitleStartY = 196.0f;	
	private float labelBoardSelectMapTitleTextureWidth = 275.0f;
	private float labelBoardSelectMapTitleTextureHeight = 290.0f; 
	private Rect labelBoardSelectMapTitleRect;
	
	//Button_Map
	//private string Button_Map = "Board_SelectMapTitle";
	private string strMinePit = "MinePit";
	private float buttonMatchFilterMinePitMapX = 0.0f;
	private float buttonMatchFilterMinePitMapY = 0.0f;
	private float buttonMatchFilterMinePitMapWidth = 0.0f;
	private float buttonMatchFilterMinePitMapHeight = 0.0f;
	private float buttonMatchFilterMinePitMapStartX = 381.0f;
	private float buttonMatchFilterMinePitMapStartY = 245.0f;	
	private float buttonMatchFilterMinePitMapTextureWidth = 260.0f;
	private float buttonMatchFilterMinePitMapTextureHeight = 44.0f; 
	private Rect buttonMatchFilterMinePitMapRect;
	
	private string strHighSpeedRoad = "HighSpeedRoad";
	private float buttonMatchFilterHighSpeedRoadMapX = 0.0f;
	private float buttonMatchFilterHighSpeedRoadMapY = 0.0f;
	private float buttonMatchFilterHighSpeedRoadMapWidth = 0.0f;
	private float buttonMatchFilterHighSpeedRoadMapHeight = 0.0f;
	private float buttonMatchFilterHighSpeedRoadMapStartX = 381.0f;
	private float buttonMatchFilterHighSpeedRoadMapStartY = 290.0f;	
	private float buttonMatchFilterHighSpeedRoadMapTextureWidth = 260.0f;
	private float buttonMatchFilterHighSpeedRoadMapTextureHeight = 44.0f; 
	private Rect buttonMatchFilterHighSpeedRoadMapRect;
	
	//BoxMatch Filter MaxPlayerNumber 
	private float boxMatchFilterMaxPlayerNumberX = 0.0f;
	private float boxMatchFilterMaxPlayerNumberY = 0.0f;
	private float boxMatchFilterMaxPlayerNumberWidth = 0.0f;
	private float boxMatchFilterMaxPlayerNumberHeight = 0.0f;
	private float boxMatchFilterMaxPlayerNumberStartX = 381.0f;
	private float boxMatchFilterMaxPlayerNumberStartY = 437.0f;	
	private float boxMatchFilterMaxPlayerNumberTextureWidth = 260.0f;
	private float boxMatchFilterMaxPlayerNumberTextureHeight = 44.0f; 
	private Rect boxMatchFilterMaxPlayerNumberRect;
	
	//Button_MatchFilterConfirm
	private string Button_MatchFilterConfirm = "Button_MatchFilterConfirm";
	private string Button_MatchFilterConfirmDark = "Button_MatchFilterConfirmDark";
	private float buttonMatchFilterConfirmX = 0.0f;
	private float buttonMatchFilterConfirmY = 0.0f;
	private float buttonMatchFilterConfirmWidth = 0.0f;
	private float buttonMatchFilterConfirmHeight = 0.0f;
	private float buttonMatchFilterConfirmStartX = 520.0f;
	private float buttonMatchFilterConfirmStartY = 509.0f;	
	private float buttonMatchFilterConfirmTextureWidth = 125.0f;
	private float buttonMatchFilterConfirmTextureHeight = 60.0f; 
	private Rect buttonMatchFilterConfirmRect;
	
	//Button_MatchFilterCancel
	private string Button_MatchFilterCancel = "Button_MatchFilterCancel";
	private float buttonMatchFilterCancelX = 0.0f;
	private float buttonMatchFilterCancelY = 0.0f;
	private float buttonMatchFilterCancelWidth = 0.0f;
	private float buttonMatchFilterCancelHeight = 0.0f;
	private float buttonMatchFilterCancelStartX = 375.0f;
	private float buttonMatchFilterCancelStartY = 509.0f;	
	private float buttonMatchFilterCancelTextureWidth = 125.0f;
	private float buttonMatchFilterCancelTextureHeight = 60.0f; 
	private Rect buttonMatchFilterCancelRect;
	
	private float bannerStartPoint = -163.0f;
	private float bannerEndPoint = 0.0f;
	//public float bannerCurrentPoint = 0.0f;
	
	private float lobbyBoardStartPoint = 768.0f;
	private float lobbyBoardEndPoint = 0.0f;
	//public float lobbyBoardCurrentPoint = 768.0f;
	
	private float leftBoardStartPoint = -661.0f; // 98 - 759
	private float leftBoardEndPoint = 98.0f;
	//public float leftBoardCurrentPoint = 98.0f;
	
	private float rightBoardStartPoint = -661.0f;
	private float rightBoardEndPoint = 98.0f;
	//public float rightBoardCurrentPoint = 98.0f;
	
	private bool isSelectingMap = false;
	private bool isEnterPlayerBase = false;
	private bool isLeavingLobby = false;
	//private bool isPlayerDataLoaded = false;
	
	private void InitialGUI() {
		
		//lobbyWindow		
		windowLobbyX = (windowLobbyStartX - screenX) * Screen.width / screenWidth;
		windowLobbyY = (windowLobbyStartY - screenY) * Screen.height / screenHeight;
		windowLobbyWidth = windowLobbyTextureWidth * Screen.width / screenWidth;
		windowLobbyHeight = windowLobbyTextureHeight * Screen.height / screenHeight;
		windowLobbyRect = new Rect(windowLobbyX, windowLobbyY, windowLobbyWidth, windowLobbyHeight);
//		windowLobbyDarkRect = new Rect(windowLobbyX, windowLobbyY, windowLobbyWidth, windowLobbyHeight);
		
		//windowMatchFiltering
		labelBoardMatchFilteringX = (labelBoardMatchFilteringStartX - screenX) * Screen.width / screenWidth;
		labelBoardMatchFilteringY = (labelBoardMatchFilteringStartY - screenY) * Screen.height / screenHeight;
		labelBoardMatchFilteringWidth = labelBoardMatchFilteringTextureWidth * Screen.width / screenWidth;
		labelBoardMatchFilteringHeight = labelBoardMatchFilteringTextureHeight * Screen.height / screenHeight;
		labelBoardMatchFilteringRect = new Rect(labelBoardMatchFilteringX, labelBoardMatchFilteringY, labelBoardMatchFilteringWidth, labelBoardMatchFilteringHeight);
		
		//windowMatching
		windowMatchingX = (windowMatchingStartX - screenX) * Screen.width / screenWidth;
		windowMatchingY = (windowMatchingStartY - screenY) * Screen.height / screenHeight;
		windowMatchingWidth = windowMatchingTextureWidth * Screen.width / screenWidth;
		windowMatchingHeight = windowMatchingTextureHeight * Screen.height / screenHeight;
		windowMatchingRect = new Rect(windowMatchingX, windowMatchingY, windowMatchingWidth, windowMatchingHeight);
		
		
		bannerStartPoint = bannerStartPoint * Screen.height / screenHeight;
		bannerEndPoint = bannerEndPoint * Screen.height / screenHeight;
	
		lobbyBoardStartPoint = lobbyBoardStartPoint * Screen.height / screenHeight;
		lobbyBoardEndPoint = lobbyBoardEndPoint * Screen.height / screenHeight;
	
		leftBoardStartPoint = leftBoardStartPoint * Screen.height / screenHeight;
		leftBoardEndPoint = leftBoardEndPoint * Screen.height / screenHeight;
	
		rightBoardStartPoint = rightBoardStartPoint * Screen.height / screenHeight;
		rightBoardEndPoint = rightBoardEndPoint * Screen.height / screenHeight;
		
		
		//LobbyBanner Group
		groupLobbyBannerX = (groupLobbyBannerStartX - screenX) * Screen.width / screenWidth;
		groupLobbyBannerY = (groupLobbyBannerStartY - screenY) * Screen.height / screenHeight;
		groupLobbyBannerWidth = groupLobbyBannerTextureWidth * Screen.width / screenWidth;
		groupLobbyBannerHeight = groupLobbyBannerTextureHeight * Screen.height / screenHeight;
		groupLobbyBannerRect = new Rect(groupLobbyBannerX, groupLobbyBannerY, groupLobbyBannerWidth, groupLobbyBannerHeight);
		groupLobbyBannerRect.y = bannerStartPoint;
		
		//LobbyBoard Group
		groupLobbyBoardX = (groupLobbyBoardStartX - screenX) * Screen.width / screenWidth;
		groupLobbyBoardY = (groupLobbyBoardStartY - screenY) * Screen.height / screenHeight;
		groupLobbyBoardWidth = groupLobbyBoardTextureWidth * Screen.width / screenWidth;
		groupLobbyBoardHeight = groupLobbyBoardTextureHeight * Screen.height / screenHeight;
		groupLobbyBoardRect = new Rect(groupLobbyBoardX, groupLobbyBoardY, groupLobbyBoardWidth, groupLobbyBoardHeight);
		groupLobbyBoardRect.y = lobbyBoardStartPoint;
		
		//BoardSysTalk Group
		groupBoardSysTalkX = (groupBoardSysTalkStartX - screenX) * Screen.width / screenWidth;
		groupBoardSysTalkY = (groupBoardSysTalkStartY - screenY) * Screen.height / screenHeight;
		groupBoardSysTalkWidth = groupBoardSysTalkTextureWidth * Screen.width / screenWidth;
		groupBoardSysTalkHeight = groupBoardSysTalkTextureHeight * Screen.height / screenHeight;
		groupBoardSysTalkRect = new Rect(groupBoardSysTalkX, groupBoardSysTalkY, groupBoardSysTalkWidth, groupBoardSysTalkHeight);
		groupBoardSysTalkRect.y = leftBoardStartPoint;
		
		//BoardPlayerList Group
		groupBoardPlayerListX = (groupBoardPlayerListStartX - screenX) * Screen.width / screenWidth;
		groupBoardPlayerListY = (groupBoardPlayerListStartY - screenY) * Screen.height / screenHeight;
		groupBoardPlayerListWidth = groupBoardPlayerListTextureWidth * Screen.width / screenWidth;
		groupBoardPlayerListHeight = groupBoardPlayerListTextureHeight * Screen.height / screenHeight;
		groupBoardPlayerListRect = new Rect(groupBoardPlayerListX, groupBoardPlayerListY, groupBoardPlayerListWidth, groupBoardPlayerListHeight);
		groupBoardPlayerListRect.y = rightBoardStartPoint;
		
		//logout button
		buttonLogoutX = (buttonLogoutStartX - screenX) * Screen.width / screenWidth;
		buttonLogoutY = (buttonLogoutStartY - screenY) * Screen.height / screenHeight;
		buttonLogoutWidth = buttonLogoutTextureWidth * Screen.width / screenWidth;
		buttonLogoutHeight = buttonLogoutTextureHeight * Screen.height / screenHeight;
		buttonLogoutRect = new Rect(buttonLogoutX, buttonLogoutY, buttonLogoutWidth, buttonLogoutHeight);
		
		//PlayerInfo button
//		buttonPlayerInfoX = (buttonPlayerInfoStartX - screenX) * Screen.width / screenWidth;
//		buttonPlayerInfoY = (buttonPlayerInfoStartY - screenY) * Screen.height / screenHeight;
//		buttonPlayerInfoWidth = buttonPlayerInfoTextureWidth * Screen.width / screenWidth;
//		buttonPlayerInfoHeight = buttonPlayerInfoTextureHeight * Screen.height / screenHeight;
//		buttonPlayerInfoRect = new Rect(buttonPlayerInfoX, buttonPlayerInfoY, buttonPlayerInfoWidth, buttonPlayerInfoHeight);
		
		//StartGame button
		buttonStartGameX = (buttonStartGameStartX - screenX) * Screen.width / screenWidth;
		buttonStartGameY = (buttonStartGameStartY - screenY) * Screen.height / screenHeight;
		buttonStartGameWidth = buttonStartGameTextureWidth * Screen.width / screenWidth;
		buttonStartGameHeight = buttonStartGameTextureHeight * Screen.height / screenHeight;
		buttonStartGameRect = new Rect(buttonStartGameX, buttonStartGameY, buttonStartGameWidth, buttonStartGameHeight);
		
		#region /* In SysTalk Board */
		//AdChinaMap label
		labelAdChinaMapX = (labelAdChinaMapStartX - screenX) * Screen.width / screenWidth;
		labelAdChinaMapY = (labelAdChinaMapStartY - screenY) * Screen.height / screenHeight;
		labelAdChinaMapWidth = labelAdChinaMapTextureWidth * Screen.width / screenWidth;
		labelAdChinaMapHeight = labelAdChinaMapTextureHeight * Screen.height / screenHeight;
		labelAdChinaMapRect = new Rect(labelAdChinaMapX, labelAdChinaMapY, labelAdChinaMapWidth, labelAdChinaMapHeight);
		labelAdChinaMapRect.x = labelAdChinaMapRect.x - groupBoardSysTalkX;
		labelAdChinaMapRect.y = labelAdChinaMapRect.y - groupBoardSysTalkY;
		
		//BoardNews label
		labelBoardNewsX = (labelBoardNewsStartX - screenX) * Screen.width / screenWidth;
		labelBoardNewsY = (labelBoardNewsStartY - screenY) * Screen.height / screenHeight;
		labelBoardNewsWidth = labelBoardNewsTextureWidth * Screen.width / screenWidth;
		labelBoardNewsHeight = labelBoardNewsTextureHeight * Screen.height / screenHeight;
		labelBoardNewsRect = new Rect(labelBoardNewsX, labelBoardNewsY, labelBoardNewsWidth, labelBoardNewsHeight);
		labelBoardNewsRect.x = labelBoardNewsRect.x - groupBoardSysTalkX;
		labelBoardNewsRect.y = labelBoardNewsRect.y - groupBoardSysTalkY;
		
		//BoardMessage label
		labelBoardMessageX = (labelBoardMessageStartX - screenX) * Screen.width / screenWidth;
		labelBoardMessageY = (labelBoardMessageStartY - screenY) * Screen.height / screenHeight;
		labelBoardMessageWidth = labelBoardMessageTextureWidth * Screen.width / screenWidth;
		labelBoardMessageHeight = labelBoardMessageTextureHeight * Screen.height / screenHeight;
		labelBoardMessageRect = new Rect(labelBoardMessageX, labelBoardMessageY, labelBoardMessageWidth, labelBoardMessageHeight);
		labelBoardMessageRect.x = labelBoardMessageRect.x - groupBoardSysTalkX;
		labelBoardMessageRect.y = labelBoardMessageRect.y - groupBoardSysTalkY;
		
		//TextField Message
		textFieldMessageX = (textFieldMessageStartX - screenX) * Screen.width / screenWidth;
		textFieldMessageY = (textFieldMessageStartY - screenY) * Screen.height / screenHeight;
		textFieldMessageWidth = textFieldMessageTextureWidth * Screen.width / screenWidth;
		textFieldMessageHeight = textFieldMessageTextureHeight * Screen.height / screenHeight;
		textFieldMessageRect = new Rect(textFieldMessageX, textFieldMessageY, textFieldMessageWidth, textFieldMessageHeight);
		textFieldMessageRect.x = textFieldMessageRect.x - groupBoardSysTalkX;
		textFieldMessageRect.y = textFieldMessageRect.y - groupBoardSysTalkY;
		
		//Button Message
		buttonMessageX = (buttonMessageStartX - screenX) * Screen.width / screenWidth;
		buttonMessageY = (buttonMessageStartY - screenY) * Screen.height / screenHeight;
		buttonMessageWidth = buttonMessageTextureWidth * Screen.width / screenWidth;
		buttonMessageHeight = buttonMessageTextureHeight * Screen.height / screenHeight;
		buttonMessageRect = new Rect(buttonMessageX, buttonMessageY, buttonMessageWidth, buttonMessageHeight);
		buttonMessageRect.x = buttonMessageRect.x - groupBoardSysTalkX;
		buttonMessageRect.y = buttonMessageRect.y - groupBoardSysTalkY;
		
		#endregion
		
		#region /* In Player List Board */
		//TabAllPlayers label
		labelTabAllPlayersX = (labelTabAllPlayersStartX - screenX) * Screen.width / screenWidth;
		labelTabAllPlayersY = (labelTabAllPlayersStartY - screenY) * Screen.height / screenHeight;
		labelTabAllPlayersWidth = labelTabAllPlayersTextureWidth * Screen.width / screenWidth;
		labelTabAllPlayersHeight = labelTabAllPlayersTextureHeight * Screen.height / screenHeight;
		labelTabAllPlayersRect = new Rect(labelTabAllPlayersX, labelTabAllPlayersY, labelTabAllPlayersWidth, labelTabAllPlayersHeight);
		labelTabAllPlayersRect.x = labelTabAllPlayersRect.x - groupBoardPlayerListX;
		labelTabAllPlayersRect.y = labelTabAllPlayersRect.y - groupBoardPlayerListY;
		
		//TabEmpty label
		labelTabEmptyX = (labelTabEmptyStartX - screenX) * Screen.width / screenWidth;
		labelTabEmptyY = (labelTabEmptyStartY - screenY) * Screen.height / screenHeight;
		labelTabEmptyWidth = labelTabEmptyTextureWidth * Screen.width / screenWidth;
		labelTabEmptyHeight = labelTabEmptyTextureHeight * Screen.height / screenHeight;
		labelTabEmptyRect = new Rect(labelTabEmptyX, labelTabEmptyY, labelTabEmptyWidth, labelTabEmptyHeight);
		labelTabEmptyRect.x = labelTabEmptyRect.x - groupBoardPlayerListX;
		labelTabEmptyRect.y = labelTabEmptyRect.y - groupBoardPlayerListY;
		
		//BoardPlayerBoxList
		labelBoardPlayerBoxListX = (labelBoardPlayerBoxListStartX - screenX) * Screen.width / screenWidth;
		labelBoardPlayerBoxListY = (labelBoardPlayerBoxListStartY - screenY) * Screen.height / screenHeight;
		labelBoardPlayerBoxListWidth = labelBoardPlayerBoxListTextureWidth * Screen.width / screenWidth;
		labelBoardPlayerBoxListHeight = labelBoardPlayerBoxListTextureHeight * Screen.height / screenHeight;
		labelBoardPlayerBoxListRect = new Rect(labelBoardPlayerBoxListX, labelBoardPlayerBoxListY, labelBoardPlayerBoxListWidth, labelBoardPlayerBoxListHeight);
		labelBoardPlayerBoxListRect.x = labelBoardPlayerBoxListRect.x - groupBoardPlayerListX;
		labelBoardPlayerBoxListRect.y = labelBoardPlayerBoxListRect.y - groupBoardPlayerListY;
		
		//AreaOtherPlayerBoxList
		areaOtherPlayerBoxListX = (areaOtherPlayerBoxListStartX - screenX) * Screen.width / screenWidth;
		areaOtherPlayerBoxListY = (areaOtherPlayerBoxListStartY - screenY) * Screen.height / screenHeight;
		areaOtherPlayerBoxListWidth = areaOtherPlayerBoxListTextureWidth * Screen.width / screenWidth;
		areaOtherPlayerBoxListHeight = areaOtherPlayerBoxListTextureHeight * Screen.height / screenHeight;
		areaOtherPlayerBoxListRect = new Rect(areaOtherPlayerBoxListX, areaOtherPlayerBoxListY, areaOtherPlayerBoxListWidth, areaOtherPlayerBoxListHeight);
		areaOtherPlayerBoxListRect.x = areaOtherPlayerBoxListRect.x - groupBoardPlayerListX;
		areaOtherPlayerBoxListRect.y = areaOtherPlayerBoxListRect.y - groupBoardPlayerListY;		
		
		//BoxOwnPlayer
		labelBoxOwnPlayerX = (labelBoxOwnPlayerStartX - screenX) * Screen.width / screenWidth;
		labelBoxOwnPlayerY = (labelBoxOwnPlayerStartY - screenY) * Screen.height / screenHeight;
		labelBoxOwnPlayerWidth = labelBoxOwnPlayerTextureWidth * Screen.width / screenWidth;
		labelBoxOwnPlayerHeight = labelBoxOwnPlayerTextureHeight * Screen.height / screenHeight;
		labelBoxOwnPlayerRect = new Rect(labelBoxOwnPlayerX, labelBoxOwnPlayerY, labelBoxOwnPlayerWidth, labelBoxOwnPlayerHeight);
		labelBoxOwnPlayerRect.x = labelBoxOwnPlayerRect.x - groupBoardPlayerListX;
		labelBoxOwnPlayerRect.y = labelBoxOwnPlayerRect.y - groupBoardPlayerListY;
		
		
		//BoxOtherPlayer
		labelBoxOtherPlayerX = (labelBoxOtherPlayerStartX - screenX) * Screen.width / screenWidth;
		labelBoxOtherPlayerY = (labelBoxOtherPlayerStartY - screenY) * Screen.height / screenHeight;
		labelBoxOtherPlayerWidth = labelBoxOtherPlayerTextureWidth * Screen.width / screenWidth;
		labelBoxOtherPlayerHeight = labelBoxOtherPlayerTextureHeight * Screen.height / screenHeight;
		labelBoxOtherPlayerRect = new Rect(labelBoxOtherPlayerX, labelBoxOtherPlayerY, labelBoxOtherPlayerWidth, labelBoxOtherPlayerHeight);
		labelBoxOtherPlayerRect.x = labelBoxOtherPlayerRect.x - groupBoardPlayerListX;
		labelBoxOtherPlayerRect.y = labelBoxOtherPlayerRect.y - groupBoardPlayerListY;
		labelBoxOtherPlayerDiff = labelBoxOtherPlayerDiff * Screen.height / screenHeight;
		
		//IconOwnPlayer
		labelIconOwnPlayerX = (labelIconOwnPlayerStartX - screenX) * Screen.width / screenWidth;
		labelIconOwnPlayerY = (labelIconOwnPlayerStartY - screenY) * Screen.height / screenHeight;
		labelIconOwnPlayerWidth = labelIconOwnPlayerTextureWidth * Screen.width / screenWidth;
		labelIconOwnPlayerHeight = labelIconOwnPlayerTextureHeight * Screen.height / screenHeight;
		labelIconOwnPlayerRect = new Rect(labelIconOwnPlayerX, labelIconOwnPlayerY, labelIconOwnPlayerWidth, labelIconOwnPlayerHeight);
		labelIconOwnPlayerRect.x = labelIconOwnPlayerRect.x - groupBoardPlayerListX;
		labelIconOwnPlayerRect.y = labelIconOwnPlayerRect.y - groupBoardPlayerListY;
		labelIconOwnPlayerDiff = labelIconOwnPlayerDiff * Screen.height / screenHeight;
		
		//IconOtherPlayer
		labelIconOtherPlayerX = (labelIconOtherPlayerStartX - screenX) * Screen.width / screenWidth;
		labelIconOtherPlayerY = (labelIconOtherPlayerStartY - screenY) * Screen.height / screenHeight;
		labelIconOtherPlayerWidth = labelIconOtherPlayerTextureWidth * Screen.width / screenWidth;
		labelIconOtherPlayerHeight = labelIconOtherPlayerTextureHeight * Screen.height / screenHeight;
		labelIconOtherPlayerRect = new Rect(labelIconOtherPlayerX, labelIconOtherPlayerY, labelIconOtherPlayerWidth, labelIconOtherPlayerHeight);
		labelIconOtherPlayerRect.x = labelIconOtherPlayerRect.x - groupBoardPlayerListX;
		labelIconOtherPlayerRect.y = labelIconOtherPlayerRect.y - groupBoardPlayerListY;
		labelIconOtherPlayerDiff = labelIconOtherPlayerDiff * Screen.height / screenHeight;
		
		//OwnPlayerName
		labelOwnPlayerNameX = (labelOwnPlayerNameStartX - screenX) * Screen.width / screenWidth;
		labelOwnPlayerNameY = (labelOwnPlayerNameStartY - screenY) * Screen.height / screenHeight;
		labelOwnPlayerNameWidth = labelOwnPlayerNameTextureWidth * Screen.width / screenWidth;
		labelOwnPlayerNameHeight = labelOwnPlayerNameTextureHeight * Screen.height / screenHeight;
		labelOwnPlayerNameRect = new Rect(labelOwnPlayerNameX, labelOwnPlayerNameY, labelOwnPlayerNameWidth, labelOwnPlayerNameHeight);
		labelOwnPlayerNameRect.x = labelOwnPlayerNameRect.x - groupBoardPlayerListX;
		labelOwnPlayerNameRect.y = labelOwnPlayerNameRect.y - groupBoardPlayerListY;
		
		//OtherPlayerName
		labelOtherPlayerNameX = (labelOtherPlayerNameStartX - screenX) * Screen.width / screenWidth;
		labelOtherPlayerNameY = (labelOtherPlayerNameStartY - screenY) * Screen.height / screenHeight;
		labelOtherPlayerNameWidth = labelOtherPlayerNameTextureWidth * Screen.width / screenWidth;
		labelOtherPlayerNameHeight = labelOtherPlayerNameTextureHeight * Screen.height / screenHeight;
		labelOtherPlayerNameRect = new Rect(labelOtherPlayerNameX, labelOtherPlayerNameY, labelOtherPlayerNameWidth, labelOtherPlayerNameHeight);
		labelOtherPlayerNameRect.x = labelOtherPlayerNameRect.x - groupBoardPlayerListX;
		labelOtherPlayerNameRect.y = labelOtherPlayerNameRect.y - groupBoardPlayerListY;
		labelOtherPlayerNameDiff = labelOtherPlayerNameDiff * Screen.height / screenHeight;
		
		#endregion
		
		#region /* MatchFiltering*/
		
		//labelBoardSelectMapTitle
		labelBoardSelectMapTitleX = (labelBoardSelectMapTitleStartX - screenX) * Screen.width / screenWidth;
		labelBoardSelectMapTitleY = (labelBoardSelectMapTitleStartY - screenY) * Screen.height / screenHeight;
		labelBoardSelectMapTitleWidth = labelBoardSelectMapTitleTextureWidth * Screen.width / screenWidth;
		labelBoardSelectMapTitleHeight = labelBoardSelectMapTitleTextureHeight * Screen.height / screenHeight;
		labelBoardSelectMapTitleRect = new Rect(labelBoardSelectMapTitleX, labelBoardSelectMapTitleY, labelBoardSelectMapTitleWidth, labelBoardSelectMapTitleHeight);
		
		
		//buttonMatchFilterMinePitMap
		buttonMatchFilterMinePitMapX = (buttonMatchFilterMinePitMapStartX - screenX) * Screen.width / screenWidth;
		buttonMatchFilterMinePitMapY = (buttonMatchFilterMinePitMapStartY - screenY) * Screen.height / screenHeight;
		buttonMatchFilterMinePitMapWidth = buttonMatchFilterMinePitMapTextureWidth * Screen.width / screenWidth;
		buttonMatchFilterMinePitMapHeight = buttonMatchFilterMinePitMapTextureHeight * Screen.height / screenHeight;
		buttonMatchFilterMinePitMapRect = new Rect(buttonMatchFilterMinePitMapX, buttonMatchFilterMinePitMapY, buttonMatchFilterMinePitMapWidth, buttonMatchFilterMinePitMapHeight);
		
		//buttonMatchFilterHighSpeedRoadMap
		buttonMatchFilterHighSpeedRoadMapX = (buttonMatchFilterHighSpeedRoadMapStartX - screenX) * Screen.width / screenWidth;
		buttonMatchFilterHighSpeedRoadMapY = (buttonMatchFilterHighSpeedRoadMapStartY - screenY) * Screen.height / screenHeight;
		buttonMatchFilterHighSpeedRoadMapWidth = buttonMatchFilterHighSpeedRoadMapTextureWidth * Screen.width / screenWidth;
		buttonMatchFilterHighSpeedRoadMapHeight = buttonMatchFilterHighSpeedRoadMapTextureHeight * Screen.height / screenHeight;
		buttonMatchFilterHighSpeedRoadMapRect = new Rect(buttonMatchFilterHighSpeedRoadMapX, buttonMatchFilterHighSpeedRoadMapY, buttonMatchFilterHighSpeedRoadMapWidth, buttonMatchFilterHighSpeedRoadMapHeight);
		
		//boxMatchFilterMaxPlayerNumber
		boxMatchFilterMaxPlayerNumberX = (boxMatchFilterMaxPlayerNumberStartX - screenX) * Screen.width / screenWidth;
		boxMatchFilterMaxPlayerNumberY = (boxMatchFilterMaxPlayerNumberStartY - screenY) * Screen.height / screenHeight;
		boxMatchFilterMaxPlayerNumberWidth = boxMatchFilterMaxPlayerNumberTextureWidth * Screen.width / screenWidth;
		boxMatchFilterMaxPlayerNumberHeight = boxMatchFilterMaxPlayerNumberTextureHeight * Screen.height / screenHeight;
		boxMatchFilterMaxPlayerNumberRect = new Rect(boxMatchFilterMaxPlayerNumberX, boxMatchFilterMaxPlayerNumberY, boxMatchFilterMaxPlayerNumberWidth, boxMatchFilterMaxPlayerNumberHeight);
		
		//buttonMatchFilterMinePitMap
		buttonMatchFilterConfirmX = (buttonMatchFilterConfirmStartX - screenX) * Screen.width / screenWidth;
		buttonMatchFilterConfirmY = (buttonMatchFilterConfirmStartY - screenY) * Screen.height / screenHeight;
		buttonMatchFilterConfirmWidth = buttonMatchFilterConfirmTextureWidth * Screen.width / screenWidth;
		buttonMatchFilterConfirmHeight = buttonMatchFilterConfirmTextureHeight * Screen.height / screenHeight;
		buttonMatchFilterConfirmRect = new Rect(buttonMatchFilterConfirmX, buttonMatchFilterConfirmY, buttonMatchFilterConfirmWidth, buttonMatchFilterConfirmHeight);
		
		//labelBoardSelectMapTitle
		buttonMatchFilterCancelX = (buttonMatchFilterCancelStartX - screenX) * Screen.width / screenWidth;
		buttonMatchFilterCancelY = (buttonMatchFilterCancelStartY - screenY) * Screen.height / screenHeight;
		buttonMatchFilterCancelWidth = buttonMatchFilterCancelTextureWidth * Screen.width / screenWidth;
		buttonMatchFilterCancelHeight = buttonMatchFilterCancelTextureHeight * Screen.height / screenHeight;
		buttonMatchFilterCancelRect = new Rect(buttonMatchFilterCancelX, buttonMatchFilterCancelY, buttonMatchFilterCancelWidth, buttonMatchFilterCancelHeight);
		
		
		#endregion
	}
	
	void Update() {
		if(this.drawState == LobbyController.DrawState.ENTER) {
			
			if(groupLobbyBannerRect.y < bannerEndPoint) {
				groupLobbyBannerRect.y = groupLobbyBannerRect.y + (groupLobbyBannerHeight*Time.deltaTime*3);
				
			}else{
				groupLobbyBannerRect.y = bannerEndPoint;
			}
			
			if(groupLobbyBoardRect.y > lobbyBoardEndPoint) {
				groupLobbyBoardRect.y = groupLobbyBoardRect.y - (groupLobbyBoardHeight*Time.deltaTime*3);
			}else{
				groupLobbyBoardRect.y = lobbyBoardEndPoint;
			}
			
			if((groupLobbyBannerRect.y == bannerEndPoint) && (groupLobbyBoardRect.y == lobbyBoardEndPoint)) {
				if(groupBoardSysTalkRect.y < leftBoardEndPoint) {
					groupBoardSysTalkRect.y = groupBoardSysTalkRect.y + (groupBoardSysTalkHeight * Time.deltaTime*3);
					groupBoardPlayerListRect.y = groupBoardPlayerListRect.y + (groupBoardPlayerListHeight * Time.deltaTime*3);

				}else{
					groupBoardSysTalkRect.y = leftBoardEndPoint;
					groupBoardPlayerListRect.y = leftBoardEndPoint;
					
					this.drawState = LobbyController.DrawState.LOBBY;
					
					OnAfterLobbyBoardDown();
				}
			}
		}
		
		if(this.drawState == LobbyController.DrawState.QUIT) {
			
			if(groupBoardSysTalkRect.y > leftBoardStartPoint) {
				groupBoardSysTalkRect.y = groupBoardSysTalkRect.y - (groupBoardSysTalkHeight*Time.deltaTime*3);
				groupBoardPlayerListRect.y = groupBoardPlayerListRect.y - (groupBoardPlayerListHeight*Time.deltaTime*3);

			}else{
				groupBoardSysTalkRect.y = leftBoardStartPoint;
				groupBoardPlayerListRect.y = leftBoardStartPoint;
			}
			
			if(groupBoardSysTalkRect.y == leftBoardStartPoint) {
				if(groupLobbyBannerRect.y > bannerStartPoint) {
					groupLobbyBannerRect.y = groupLobbyBannerRect.y - (groupLobbyBannerHeight*Time.deltaTime*3);
				}else{
					groupLobbyBannerRect.y = bannerStartPoint;
				}
				
				if(groupLobbyBoardRect.y < lobbyBoardStartPoint) {
					groupLobbyBoardRect.y = groupLobbyBoardRect.y + (groupLobbyBoardHeight*Time.deltaTime*3);

				}else{
					groupLobbyBoardRect.y = lobbyBoardStartPoint;
				}
				
				//
				if((groupLobbyBannerRect.y == bannerStartPoint) && (groupLobbyBoardRect.y == lobbyBoardStartPoint)) {
					
//					OnAfterLobbyBoardUp();
					
					if(isSelectingMap) {
						this.EnteredSelectingMap();
					}
					
					if(isEnterPlayerBase) {
						this.EnterPlayerBase();
					}
					
					if(isLeavingLobby) {
						this.LeftLobby();
					}
				} 
			}
		}
	}
	
	
	void OnGUI() {

		if(isDraw) {	
			GUI.skin = lobbySkin;
			
			if(focusState == LobbyController.FocusState.LOBBY) {
				windowLobbyRect = GUI.Window(0, windowLobbyRect, DrawLobbyWindow, strEmpty);
			}
			
			if(focusState == LobbyController.FocusState.FILTERING) {
				GUI.enabled = false;
				GUI.color = darkTransparent;
				windowLobbyRect = GUI.Window(0, windowLobbyRect, DrawLobbyWindow, strEmpty);
				GUI.enabled = true;
				GUI.color = Color.white;
				
				windowLobbyRect = GUI.Window(2, windowLobbyRect, DrawMatchFilteringWindow, strEmpty);
				GUI.BringWindowToFront(2);
			}
			
			if(focusState == LobbyController.FocusState.MATCHING) {
				GUI.enabled = false;
				GUI.color = darkTransparent;
				windowLobbyRect = GUI.Window(0, windowLobbyRect, DrawLobbyWindow, strEmpty);
				GUI.enabled = true;
				GUI.color = Color.white;
				
				
				windowMatchingRect = GUI.Window(2, windowMatchingRect, DrawMatchingWindow, strEmpty, Window_Matching);
				GUI.FocusWindow(2);
			}
		}else{
			GUI.skin = null;
		}
	}
	
	private void DrawLobbyWindow(int id) {
		
		GUI.BeginGroup(groupLobbyBoardRect, strEmpty, Lobby_Board);
		GUI.EndGroup();

		GUI.BeginGroup(groupBoardSysTalkRect, strEmpty, Board_SysTalk);
			GUI.BeginGroup(labelBoardNewsRect, strEmpty, Board_News);
			GUI.EndGroup();
		
			//News
			GUILayout.BeginArea(labelAdChinaMapRect, strEmpty);
				playerActionInfoScrollPosition = GUILayout.BeginScrollView(playerActionInfoScrollPosition);
				foreach(ChatMessage msg in this.playerActionBoard) {
					GUILayout.Label(msg.sender + strColon + msg.content + strLeftParentheses + msg.dateTime + strRightParentheses);
				}
				
				GUILayout.EndScrollView();
			GUILayout.EndArea();
			
			// Message Board
			GUILayout.BeginArea(labelBoardMessageRect, strEmpty);
				
		
				messageScrollPosition = GUILayout.BeginScrollView(messageScrollPosition);
				
				if(this.lobbyRoom != null) {
					foreach(ChatMessage msg in this.lobbyRoom.chatMessages) {
						if(msg.senderGUPID == this.playerInfo.GUPID) {
							GUILayout.BeginHorizontal();
								GUILayout.Label(msg.sender, Font_White);
								GUILayout.Label(strColon, Font_White);
								GUILayout.Label(msg.content, Font_White);
								GUILayout.FlexibleSpace();		
							GUILayout.EndHorizontal();
						} else {
							GUILayout.BeginHorizontal();
								GUILayout.FlexibleSpace();
								GUILayout.Label(msg.content, Font_White);
								GUILayout.Label(strColon, Font_White);
								GUILayout.Label(msg.sender, Font_White);
							GUILayout.EndHorizontal();
						}
					}
				}
				
				
				GUILayout.EndScrollView();
			GUILayout.EndArea();
		
			//TextField
			this.chatInput = GUI.TextField(textFieldMessageRect, this.chatInput);
		
			if(GUI.Button(buttonMessageRect, strEmpty, Button_Message)) {
				SendMessage(lobbyRoom.roomIndex);
				this.chatInput = strEmpty;
			}
		
		    if(Event.current.type == EventType.keyDown && Event.current.character == '\n' && this.chatInput.Length > 0) {	
				SendMessage(lobbyRoom.roomIndex);
				this.chatInput = strEmpty;
			}
			
			          
		GUI.EndGroup();
		
		GUI.BeginGroup(groupBoardPlayerListRect, strEmpty, Board_PlayerList);
			GUI.Label(labelTabAllPlayersRect, strEmpty, Tab_AllPlayers);
			GUI.Label(labelTabEmptyRect, strEmpty, Tab_Empty);
			GUI.Label(labelBoardPlayerBoxListRect, strEmpty, Board_PlayerBoxList);
			
			//if(isPlayerDataLoaded) {
				//TvCDriver owner =  (TvCDriver)this.lobbyRoom.GetPlayer(this.playerInfo.GetLoginedGameUserPlayerID());
				GUI.Label(labelBoxOwnPlayerRect, strEmpty, Box_OwnPlayer);
				GUI.Label(labelIconOwnPlayerRect, strEmpty, this.playerInfo.photo);		
				GUI.Label(labelOwnPlayerNameRect, this.playerInfo.playerName);
				
				
			//}
			
//			GUILayout.BeginArea(areaOtherPlayerBoxListRect);
				
				if(this.lobbyRoom.transform.GetChildCount() > 8) {
					areaOtherPlayerBoxListViewRect.x = areaOtherPlayerBoxListRect.x;
					areaOtherPlayerBoxListViewRect.y = areaOtherPlayerBoxListRect.y;
					areaOtherPlayerBoxListViewRect.width = areaOtherPlayerBoxListRect.width;
					areaOtherPlayerBoxListViewRect.height = labelBoxOtherPlayerHeight * this.lobbyRoom.transform.GetChildCount();
				}else{
					areaOtherPlayerBoxListViewRect = areaOtherPlayerBoxListRect;				
				}
				
				playerListScrollPosition = GUI.BeginScrollView(areaOtherPlayerBoxListRect, playerListScrollPosition, areaOtherPlayerBoxListViewRect);
				playerCount = 0;
				
				foreach(GameUserPlayer driver in this.lobbyRoom.transform.GetComponentsInChildren<GameUserPlayer>()) {
					if(driver.playerName != this.playerInfo.playerName) {
						GUI.Label(new Rect(labelBoxOtherPlayerRect.x, labelBoxOtherPlayerRect.y + labelBoxOtherPlayerDiff * playerCount, labelBoxOtherPlayerRect.width, labelBoxOtherPlayerRect.height), strEmpty, Box_OtherPlayer);
						GUI.Label(new Rect(labelIconOtherPlayerRect.x, labelIconOtherPlayerRect.y + labelIconOtherPlayerDiff * playerCount, labelIconOtherPlayerRect.width, labelIconOtherPlayerRect.height), strEmpty, driver.photo);
						GUI.Label(new Rect(labelOtherPlayerNameRect.x, labelOtherPlayerNameRect.y + labelOtherPlayerNameDiff * playerCount, labelOtherPlayerNameRect.width, labelOtherPlayerNameRect.height), driver.playerName);
						playerCount++;
					}
					
				}
				GUI.EndScrollView();
//			GUILayout.EndArea();
		
		GUI.EndGroup();
		
		GUI.BeginGroup(groupLobbyBannerRect, strEmpty, Lobby_Banner);
			
			//GUI.enabled = false;
			
//			if(GUI.Button(buttonPlayerInfoRect, strEmpty, Button_PlayerInfo)) {
//				this.drawState = LobbyController.DrawState.QUIT;
//				this.isEnterPlayerBase = true;
//			}
			
			//GUI.enabled = true;	
		
			//Match Arena button
			if(GUI.Button(buttonStartGameRect, strEmpty, Button_StartGame)) {
				//focusState = LobbyController.FocusState.FILTERING;
				
			
				EnterSelectMap();
				
				
			}
		
			if(GUI.Button(buttonLogoutRect, strEmpty, Button_Logout)) {
				this.Back();
			}
		GUI.EndGroup();
		
	}
	
	private void DrawMatchFilteringWindow(int id) {
		GUI.Label(labelBoardMatchFilteringRect, strEmpty, Board_MatchFiltering);
		GUI.Label(labelBoardSelectMapTitleRect, strEmpty, Board_SelectMapTitle);
		
		if(matchFilterMap == (int)Definition.eSceneID.MinePit) {
			GUI.backgroundColor = Color.yellow;
		}
		
			if(GUI.Button(buttonMatchFilterMinePitMapRect, strMinePit)) {
				matchFilterMap = (int)Definition.eSceneID.MinePit;
			}
		
		if(matchFilterMap == (int)Definition.eSceneID.MinePit) {
			GUI.backgroundColor = Color.white;
		}
		
		
		if(matchFilterMap == (int)Definition.eSceneID.HighSpeedRoad) {
			GUI.backgroundColor = Color.yellow;
		}

			if(GUI.Button(buttonMatchFilterHighSpeedRoadMapRect, strHighSpeedRoad)) {
				matchFilterMap = (int)Definition.eSceneID.HighSpeedRoad;
			}
		
		if(matchFilterMap != (int)Definition.eSceneID.HighSpeedRoad) {
			GUI.backgroundColor = Color.white;
		}
		
		GUILayout.BeginArea(boxMatchFilterMaxPlayerNumberRect);
			GUILayout.BeginHorizontal();
				if(matchFilterMaxPlayerNumber == 2) {
					GUI.backgroundColor = Color.yellow;
				}
		
					if(GUILayout.Button(strArrayMaxPlayerNumberTwo)) {
						matchFilterMaxPlayerNumber = 2;
					}
		
				if(matchFilterMaxPlayerNumber == 2) {
					GUI.backgroundColor = Color.white;
				}
		
		//--
				if(matchFilterMaxPlayerNumber == 3) {
					GUI.backgroundColor = Color.yellow;
				}
			
					if(GUILayout.Button(strArrayMaxPlayerNumberThree)) {
						matchFilterMaxPlayerNumber = 3;
					}
				
				if(matchFilterMaxPlayerNumber == 3) {
					GUI.backgroundColor = Color.white;
				}
		
		//--
		
				if(matchFilterMaxPlayerNumber == 4) {
					GUI.backgroundColor = Color.yellow;
				}
			
					if(GUILayout.Button(strArrayMaxPlayerNumberFour)) {
						matchFilterMaxPlayerNumber = 4;
					}
		
				if(matchFilterMaxPlayerNumber == 4) {
					GUI.backgroundColor = Color.white;
				}
			GUILayout.EndHorizontal();
		GUILayout.EndArea();
		
		if(matchFilterMap == 0 || matchFilterMaxPlayerNumber == 0) {
			GUI.Label(buttonMatchFilterConfirmRect, strEmpty, Button_MatchFilterConfirmDark);
		}else{
			if(GUI.Button(buttonMatchFilterConfirmRect, strEmpty, Button_MatchFilterConfirm)) {
				matchController.AutoMatchStart(matchFilterMap, matchFilterMaxPlayerNumber);
				focusState = LobbyController.FocusState.MATCHING;
			}	
		}
	
		if(GUI.Button(buttonMatchFilterCancelRect, strEmpty, Button_MatchFilterCancel)) {
			
			matchFilterMap = 0;
			matchFilterMaxPlayerNumber = 0;
			focusState = LobbyController.FocusState.LOBBY;
		}
	}
	
	void SelectMapMatch (Definition.eSceneID map)
	{
		matchFilterMap = (int)map;
		matchFilterMaxPlayerNumber = 4;
		matchController.AutoMatchStart(matchFilterMap, matchFilterMaxPlayerNumber);
		focusState = LobbyController.FocusState.MATCHING;
	}
	
	private void DrawMatchingWindow(int id) {
		//GUILayout.Label();
	}
	
	#endregion
	
	public NGUILoginController nguiLoginController;
	
	public GameUserPlayer playerInfo;
	public Room lobbyRoom;
	public GameUserPlayer playerPrefab;
	public LobbySceneViewer lobbySceneViewer;
	public MatchController matchController;
	
	void Awake() {
		InitialGUI();
		
		GUISkinLanguageSetting.OnGUISkinLanguageChanged += OnGUISkinLanguageChanged;
	}
	
	void OnDestroy() {
		GUISkinLanguageSetting.OnGUISkinLanguageChanged -= OnGUISkinLanguageChanged;
	}
	
	void OnGUISkinLanguageChanged(Definition.eLanguage l) {
		this.currentLanguage = l;
		
//		Debug.Log ("LobbyController: OnGUISkinLanguageChanged");
		//load GUISkin
		
		
	}
	
	public void EnteredMatchRoom() {
		Application.LoadLevel(Definition.eSceneID.MatchRoomScene.ToString());
	}
	
	public void EnterSelectMap() {
		this.focusState = LobbyController.FocusState.LOBBY;
		this.drawState = LobbyController.DrawState.QUIT;
		
		OnBeforeLobbyBoardUp();
		
		isSelectingMap = true;
	}
	
	void EnteredSelectingMap() {
		//Change by Vincent 2012/10/18
		PlayerPrefs.SetInt("BeforeMapScene", Application.loadedLevel);
		Application.LoadLevel((int)Definition.eSceneID.MapScene);
	}
	
	void EnterPlayerBase() {
		
	}
	
	void LeftLobby() {
		if(OnLeaveLobby != null ) {
			OnLeaveLobby();
		}
		nguiLoginController.Back();
	}
	
	public void Back() {
//		Network.Disconnect();
//		Application.LoadLevel(Definition.eSceneID.LoginScene.ToString());
		this.focusState = LobbyController.FocusState.LOBBY;
		this.drawState = LobbyController.DrawState.QUIT;
		OnBeforeLobbyBoardUp();
		this.isLeavingLobby = true;
		
		if(OnLeaveLobby != null) {
			OnLeaveLobby();
		}
		
	}
	
	public void SendMessage(int roomIndex) {
		//this.actionState = LobbyController.ActionState.MESSAGESENDSTART;
		networkView.RPC("SendToGameLobby_ChatRoomMessage", RPCMode.Server, roomIndex, this.playerInfo.GUPID, this.chatInput);
		//this.actionState = LobbyController.ActionState.MESSAGESENDING;
	}
	
	void OnLevelWasLoaded(int mapIndex) {
		if(mapIndex == (int)Definition.eSceneID.LobbyScene) {
			isSelectingMap = false;
			isEnterPlayerBase = false;
			isLeavingLobby = false;
			//isPlayerDataLoaded = false;
			this.focusState = LobbyController.FocusState.LOBBY;
			this.drawState = LobbyController.DrawState.ENTER;
			isDraw = true;
			
			switch(currentLanguage) {
				case Definition.eLanguage.ENGLISH:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.ENGLISH");
					this.lobbySkin = Resources.Load("Skin/LobbyEnglishLanguage", typeof(GUISkin)) as GUISkin;
					break;
				
				case Definition.eLanguage.SIMPLECHINESE:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.SIMPLECHINESE");
					this.lobbySkin = Resources.Load("Skin/LobbySimpleChineseLanguage",  typeof(GUISkin)) as GUISkin;
					break;
				
				case Definition.eLanguage.TRANDITIONALCHIINESE:
//					Debug.Log("OnLevelWasLoaded:Definition.eLanguage.TRANDITIONALCHIINESE");
					this.lobbySkin = Resources.Load("Skin/LobbyTranditionalChineseLanguage",  typeof(GUISkin)) as GUISkin;
					break;
			}
			
			lobbySceneViewer = GameObject.FindObjectOfType(typeof(LobbySceneViewer)) as LobbySceneViewer;
			
			networkView.RPC("SendToGameLobby_LoadChatRoomPlayer", RPCMode.Server, lobbyRoom.roomIndex, this.playerInfo.GUPID);
			enabled = true;
			
			OnEnteredLobby();
		}else{
			
			this.focusState = LobbyController.FocusState.LOBBY;
			this.drawState = LobbyController.DrawState.FREE;
			isDraw = false;
			this.lobbySkin = null;
//			Resources.UnloadAsset(this.lobbySkin);
			
			
			enabled = false;
		}
		
		if(mapIndex == (int)Definition.eSceneID.MapScene){
			
			
			if(OnSelectingMap != null) {
				OnSelectingMap();
			}
			
		}
		
		//Debug.Log("Lobby:OnLevelWasLoaded");
	}
	
	#region RPC
	
	[RPC]
	public void ReceiveByClientPortal_LoadChatRoomPlayer(int roomIndex, int GUPID, string playerName, string photo, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			 Transform roomTransform = transform.Find(roomIndex.ToString());
			if(roomTransform != null) {
				GameUserPlayer[] playerList = roomTransform.GetComponentsInChildren<GameUserPlayer>();
				foreach(GameUserPlayer child in playerList)
				{
					if(child.playerName == playerName)
					{
						return;
					}
				}
				GameUserPlayer player = GameObject.Instantiate(playerPrefab) as GameUserPlayer;
				player.GUPID = GUPID;
				player.playerName = playerName;
				player.photo = photo;
				player.gameObject.name = playerName;
				player.transform.parent = roomTransform;
				
			}else{
//				Debug.Log("ReceiveByClientPortal_LoadChatRoomPlayer - Room "+roomIndex+" is null.");
			}

		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM) {
//			this.errorMsg = "PLAYERNOTINROOM";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST) {
//			this.errorMsg = "ROOMNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST) {
//			this.errorMsg = "PLAYERNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(this.errorMsg);
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();		
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_JoinChatRoom(int roomIndex, int joinerGUPID, string joinerName, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			Transform roomTransform = transform.Find(roomIndex.ToString());
			if(roomTransform != null) {
				
				Transform playerTransform = roomTransform.FindChild(joinerName.ToString());
				Debug.Log("playerTransform:"+playerTransform);
				if(playerTransform != null) {
					
					GameObject.Destroy(playerTransform.gameObject);
				}
				
				GameUserPlayer player = GameObject.Instantiate(playerPrefab) as GameUserPlayer;
				player.GUPID = joinerGUPID;
				player.playerName = joinerName;
					
				player.gameObject.name = player.GUPID.ToString();
				player.playerState = Definition.ePlayerState.ONLINE;
				
				player.transform.parent = roomTransform;
				
				
				ChatMessage m = new ChatMessage(strSystemName, joinerName+" is joined.", String.Format("{0:d/M/yyyy HH:mm:ss}",System.DateTime.Now), 0);
				roomTransform.GetComponent<Room>().chatMessages.Add(m);
				
			}else{
//				Debug.Log("ReceiveByClientPortal_JoinChatRoom - Room "+roomIndex+" is null.");
			}
		}else if(resultState == Definition.RPCProcessState.PLAYEREXIST) {
//			this.errorMsg = "PLAYEREXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
		}else if(resultState == Definition.RPCProcessState.FULL) {
//			this.errorMsg = "FULL";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST) {
//			this.errorMsg = "ROOMNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST) {
//			this.errorMsg = "PLAYERNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(this.errorMsg);
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();		
		}
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_LeaveChatRoom(int roomIndex, int leaverGUPID, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			Transform roomTransform = transform.Find(roomIndex.ToString());
			if(roomTransform != null) {
				Room r = roomTransform.GetComponent<Room>();
				
				Transform leaverTransform = transform.Find(leaverGUPID.ToString());
				if(leaverTransform != null) {
					GameUserPlayer leaver = leaverTransform.GetComponent<GameUserPlayer>();
					
					
					ChatMessage m = new ChatMessage(strSystemName, leaver.playerName+" is left.", String.Format("{0:d/M/yyyy HH:mm:ss}",System.DateTime.Now), 0);
					r.chatMessages.Add(m);
					
					Destroy(leaver.gameObject);
					
				}else{
//					Debug.Log("ReceiveByClientPortal_LeaveChatRoom - Player "+leaverGUPID+" is null.");
				}
				
				
			}else{
//				Debug.Log("ReceiveByClientPortal_LeaveChatRoom - Room "+roomIndex+" is null.");
			}
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM) {
//			this.errorMsg = "FULL";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST) {
//			this.errorMsg = "ROOMNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST) {
//			this.errorMsg = "PLAYERNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(this.errorMsg);
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();		
		}
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_ChatRoomMessage(int roomIndex, string sender, string txt, string dateTime, int senderGUPID, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			Transform roomTransform = transform.Find(roomIndex.ToString());
			if(roomTransform != null) {
				ChatMessage m = new ChatMessage(sender, txt, dateTime, senderGUPID);
				roomTransform.GetComponent<Room>().chatMessages.Add(m);
				
				messageScrollPosition.y = 999999;
			}else{
//				Debug.Log("ReceiveByClientPortal_ChatRoomMessage - Room "+roomIndex+" is null.");
			}
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTINROOM) {
//			this.errorMsg = "FULL";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
		}else if(resultState == Definition.RPCProcessState.ROOMNOTEXIST) {
//			this.errorMsg = "ROOMNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
			
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST) {
//			this.errorMsg = "PLAYERNOTEXIST";

//			Debug.Log(this.errorMsg);			
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();
			
			
			
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE) {
//			this.errorMsg = "UNAVAILABLE";
//			Debug.Log(this.errorMsg);
			lobbySceneViewer.LoadChatRoomPlayerErrorSceneSetting();	
			
		}
	}
	
	[RPC]
	public void SendToGameLobby_JoinChatRoom(int roomIndex, int gupid) {
		
	}
	
	[RPC]
	public void SendToGameLobby_LoadChatRoomPlayer(int roomIndex, int gupid) {
		
	}
	
	[RPC]
	public void SendToGameLobby_ChatRoomMessage(int roomIndex, int senderGUPID, string message) {
		
	}
	
	
	
	#endregion
}
