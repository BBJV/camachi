using UnityEngine;
using System.Collections;

public class NetworkPing : MonoBehaviour {
	
	private Rect lastPingWindow = new Rect(0,0, 300, 50);
	
	void OnGUI() {
		if(Network.peerType == NetworkPeerType.Client) {
			
			lastPingWindow = GUI.Window(100, lastPingWindow, drawLastPingWindow, "");
			
			
		}
	}
	
	void drawLastPingWindow(int id) {
		if(Network.connections[0] != null) {
			GUILayout.Label("Last Ping:"+Network.GetLastPing(Network.connections[0]).ToString()+" ms");
		}
		
	}
}
