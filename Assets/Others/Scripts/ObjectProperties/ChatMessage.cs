using UnityEngine;
using System;
using System.Collections;

public class ChatMessage {
	public String sender = "";
	public String content = "";
	public String dateTime = "";
	//public String chatRoom;
	public int senderGUPID;
	
	public ChatMessage(String sender, String txt, String dateTime, int senderGUPID) {
		this.sender = sender;
		this.content = txt;
		this.dateTime = dateTime;
		this.senderGUPID = senderGUPID;
	}
	
//	public bool isMine() {
//		if(networkPlayer == Network.player) {
//			return true;
//		} else {
//			return false;
//		}
//	}
}
