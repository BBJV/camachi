using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnPlayerConnected(NetworkPlayer player) {
		//playerCount ++;
		//Debug.Log("Player " + playerCount + " connected from " + player.ipAddress + ":" + player.port);
	}
	//Server function
	void OnPlayerDisconnected(NetworkPlayer player) {
		//playerCount --;
	}
}

