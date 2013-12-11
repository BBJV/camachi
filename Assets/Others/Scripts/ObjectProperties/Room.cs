using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour{

	public int roomIndex = 0;
	public String roomName = "";
	public int maxPlayerNumber = 0;
	public GameUserPlayer creator;
	public int msgLimitedNumber = 50;
	public bool isPublic = true;
	
	public List<GameUserPlayer> playersInRoom  = new List<GameUserPlayer>();
	public List<ChatMessage> chatMessages = new List<ChatMessage>();
	
//	public ChatRoomService chatRoomService;
//	
//	void Awake() {
//		chatRoomService = GameObject.FindObjectOfType(typeof(ChatRoomService)) as ChatRoomService;
//	}
//	
	public bool IsCreator(GameUserPlayer p) {
		if(this.creator == p) {
			return true;
		}else {
			return false;
		}
	}
	
	public bool IsCreator(int gupid) {
		if(this.creator.GUPID == gupid) {
			return true;
		}else {
			return false;
		}
	}
	
	public int GetOnePlayerOtherThanLeaver(int leaverGUPID) {
		
		foreach (GameUserPlayer player in this.playersInRoom) {
			if(player.GUPID != leaverGUPID) {
				return player.GUPID;	
			}
		}
		
		return leaverGUPID;
	}
	
	public bool PlayerJoin(GameUserPlayer u) {
		if(playersInRoom.Count < maxPlayerNumber) {
			if(!playersInRoom.Contains(u)){
				playersInRoom.Add(u);
				return true;
			} else {
				return false;
			} 
			
		}else {
			return false;
		}
	}
	
	public bool PlayerLeave(GameUserPlayer u) {
		if(playersInRoom.Contains(u)){
			playersInRoom.Remove(u);
			return true;
		} else {
			return false;
		}
	}
	
	public bool IsPlayerInRoom(GameUserPlayer p) {
		foreach (GameUserPlayer player in playersInRoom) {
			if(p == player) {
				return true;
			}
		}
		return false;
	}
	
	public bool IsPlayerInRoom(int gupid) {
		foreach (GameUserPlayer player in this.playersInRoom) {
			if(gupid == player.GUPID) {
				return true;
			}
		}
		return false;
	}
	
	public GameUserPlayer GetPlayer(int gupid){
		foreach (GameUserPlayer p in playersInRoom) {
			if(p.GUPID == gupid) {
				return p;	
			}
		}
		
		return null;
	}
	
	public bool IsFull() {
		
		if(this.playersInRoom.Count < maxPlayerNumber) {
			return false; 
		}else{
			return true;
		}
	}	
	
	public int GetRemainAmountOfPlayers(){
		return this.maxPlayerNumber - this.playersInRoom.Count;
	}
	
//	public void RemovePlayer(GameObject player) {
//		GameUserPlayer p = player.GetComponent<GameUserPlayer>(); 
//		if(IsPlayerInRoom(p)) {
//			chatRoomService.SendToGameLobby_LeaveChatRoom.LeaveChatRoom(this.roomIndex, p.GUPID, msgInfo);
//		}
//	}
}
