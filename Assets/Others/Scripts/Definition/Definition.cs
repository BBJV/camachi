using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Definition {
	
	public static string versionNumber = "1.1.1";
	
	public enum RPCProcessState {
		UNAVAILABLE,
		FAIL,
		SUCCESS,
		PLAYERNAMEDUPLICATE,
		WRONGPASSWORD,
		USEREXIST,
		USERNOTEXIST,
		PLAYEREXIST,
		PLAYERNOTEXIST,
		PLAYERINROOM,
		PLAYERNOTINROOM,
		ROOMNOTEXIST,
		ROOMEXIST,
		FULL,
		NOTFRIEND,
		EMPTY,
		NOTENOUGHSTAR,
	}
	
	public enum eCarID {
		POLICE_CAR = 1,
		LORRY_TRUCK = 2,	
		AMBULANCE = 3,
		FIRE_FIGHTING_TRUCK = 4,
		GARBAGE_TRUCK = 5,
		
		GODOFWEALTH_TRUCK = 6,
		HUMMER_TRUCK = 7,
		MASCOT_TRUCK = 8,
		OLDCLASS_TRUCK = 9,
	}
	
	public enum eSceneID {
		Main = 0,
//		LoginScene = 1,
		LoginScene_ngui = 1,
		LobbyScene = 2,
		MapScene = 3,
		MatchRoomScene = 4,
		MinePit = 5,
		HighSpeedRoad = 6,
		JapanNoLight = 7,
		Loading = 8,
	}
	public enum ePlayerState {
		OFFLINE,
		ONLINE,
		PLAYINGGAME,
	}
	
	public enum ePlayerPrefabKey {
		USERACCOUNT,
		USERPASSWORD,
		CHECKBOXONORNOT,
	}
	
	public enum eLanguage {
		ENGLISH,
		SIMPLECHINESE,
		TRANDITIONALCHIINESE,
	}
	
	public enum eCa{
		
	}
	
	//CLIENT
	//when player shut down, clear room list
	
	//redirect to lobby
	
	
	//LOBBY
	//reserved data when playing game
	
	//
}
