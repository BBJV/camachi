using UnityEngine;
using System;
using System.Collections;

public class GameUserPlayer : MonoBehaviour {	
	
	public string memberID ="";
	public int GUPID = 0;  
	public NetworkPlayer networkPlayer;
	public int joinedGameRoomIndex = 0;
	public int playerID  = 0;
	public int userID = 0;
	public string playerName = ""; 
	public bool isAI = false;
	public string photo = "";
	
	public int money = 0;

	public Definition.ePlayerState playerState = Definition.ePlayerState.OFFLINE;
	
	public float currentPlayGameStateTime = 0.0f;
	public float maxPlayGameStateTime = 600.0f;
	
	public void Redirect(NetworkPlayer np) {
		this.networkPlayer = np;
		this.playerState = Definition.ePlayerState.ONLINE;
		this.joinedGameRoomIndex = 0;
		this.currentPlayGameStateTime  = 0.0f;
	}
}
