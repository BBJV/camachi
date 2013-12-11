using UnityEngine;
using System.Collections;

public class RedirectController : MonoBehaviour {
	
	private enum ProcessState {
		FREE,
		UNAVAILABLE,
		FINISH,

		STARTREDIRECT,
		REDIRECTING,
		REDIRECTED,
		REDIRECTERROR,
		REDIRECTFAIL
	}
	
	private RedirectController.ProcessState processState = RedirectController.ProcessState.FREE;
//	private string errorMsg = "";
	
	public GameUserPlayer carInsanityPlayer;
	public string gameLobbyIP = "127.0.0.1";
	public int gameLobbyPort = 50000;
	public int connectToGameLobbyCount = 0;
	public int maxConnectToGameLobbyCount = 0;
	
	
	void RedirectStart() {
//		Network.Disconnect();
		this.processState = RedirectController.ProcessState.STARTREDIRECT;
		Network.Connect(gameLobbyIP, gameLobbyPort);
		
	}
	
	[RPC]
	public void SendToGameLobby_Redirect(int GUPID, int playerID) {
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_Redirect(int gupid, int p) {
		
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
//		Debug.Log("Redirect:"+resultState.ToString());
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			
			
			this.carInsanityPlayer.networkPlayer = Network.player;
			this.carInsanityPlayer.GetComponent<CarInsanityPlayer>().selectedCar = new CarInsanityCarInfo();
			
			Application.LoadLevel(Definition.eSceneID.LobbyScene.ToString());
			this.processState = RedirectController.ProcessState.FREE;
		}else{
//			this.errorMsg = resultState.ToString();
//			Debug.Log(errorMsg);
			this.processState = RedirectController.ProcessState.REDIRECTFAIL;
			
			Network.Disconnect();
			Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
			
		}
	}

	void OnCompletedAMatch() {
		Network.Disconnect();
		RedirectStart();
	}
	

	void OnConnectedToServer() {
		
		if(this.processState == RedirectController.ProcessState.STARTREDIRECT){
			
			networkView.RPC("SendToGameLobby_Redirect", RPCMode.Server, this.carInsanityPlayer.GUPID, this.carInsanityPlayer.playerID);
			
			this.processState = RedirectController.ProcessState.REDIRECTING;
			
			
		}
	}
	
	void OnFailedToConnect() {
//		Debug.Log("RedirectController OnFailedToConnect, Count:" + connectToGameLobbyCount);
		if(this.processState == RedirectController.ProcessState.STARTREDIRECT) {
			if(connectToGameLobbyCount-- > 0) {
				RedirectStart();
			}else{
				this.processState = RedirectController.ProcessState.REDIRECTERROR;
				Application.LoadLevel(Definition.eSceneID.LoginScene_ngui.ToString());
				connectToGameLobbyCount = maxConnectToGameLobbyCount;
			}
		}
	}
	

}
