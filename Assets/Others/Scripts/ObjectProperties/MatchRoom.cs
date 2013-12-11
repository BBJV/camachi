using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Room))]
public class MatchRoom : MonoBehaviour {
	
	public Room room;
	
	public int matchMap = 0;
	public String matchType = "";

	public bool isAllSelected = false;
	public bool isMatchStart = false;

	public float currentAIJoinTime = -5.0f;
	public float AIJoinTime = 10.0f;

	public int onlyPlayerCount = 0;
	public int onlyAICount = 0;
	
	void Awake() {
		if(room == null) {
			room = GetComponent<Room>();
		}
	}
	
	public bool IsAllSelected() {
		return isAllSelected;
	}

	public bool PlayerJoin(GameUserPlayer u) {
		
		if((onlyAICount + onlyPlayerCount) < room.maxPlayerNumber) {	
			if(room.PlayerJoin(u)) {
				if(u.isAI) {
					onlyAICount++;
				}else{
					onlyPlayerCount++;
				}
				
				return true;
			}else{
				return false;
			}			
		}else {
			return false;
		}
	}
	
	public bool PlayerLeave(GameUserPlayer u) {

		if(room.PlayerLeave(u)){
			if(u.isAI) {
				onlyAICount--;
			}else{
				onlyPlayerCount--;
			}
			
			return true;
		} else {
			return false;
		}
	}
	
	public int GetPlayersCount() {
		return (onlyAICount+onlyPlayerCount);
	}
	
	public bool IsFull() {
		
		if((onlyAICount+onlyPlayerCount) < room.maxPlayerNumber) {
			return false; 
		}else{
			return true;
		}
	}
	
	public int GetRemainAmountOfPlayers(){
		return room.maxPlayerNumber - (onlyPlayerCount+onlyAICount);
	}
	
	public int GetOnePlayerOtherThanLeaver(int leaverGUPID) {
		
		foreach (GameUserPlayer player in room.playersInRoom) {
			if((player.GUPID != leaverGUPID) && (!player.isAI)) {
				return player.GUPID;	
			}
		}
		
		return leaverGUPID;
	}
	
	public CarInsanityPlayer GetCarInsanityPlayer(int GUPID) {
		return room.GetPlayer(GUPID).GetComponent<CarInsanityPlayer>();
	}
}
