using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NetworkView))]
[RequireComponent (typeof (NetworkRigidbody))]

public class CarNetworkInit : MonoBehaviour {
	private bool isAI = false;
//	void Awake () {
//		networkView.observed = GetComponent<NetworkRigidbody>();
//	}
	
	void Start () {
        // This is our own player
		Transform skillEffect = transform.Find("SkillEffect");
		if(skillEffect)
			skillEffect.gameObject.SetActiveRecursively(false);
		if(PlayerPrefs.GetString("GameType", "Network") == "Single")
		{
			networkView.enabled = false;
		}
		if (networkView.isMine || !networkView.enabled)
		{
//			if(GetComponent<CarB>())
//			{
//				Camera.main.SendMessage("SetTarget", transform);
//			}
			if(GetComponent<CarAI>())
			{
				if(!GetComponent<CarAI>().enabled)
				{
					Camera.main.SendMessage("SetTarget", transform);
				}
				else
				{
					isAI = true;
				}
			}
//			if(GetComponent<NetworkRigidbody>())
//				GetComponent<NetworkRigidbody>().enabled = false;
			
		}
		// This is just some remote controlled player, don't execute direct
		// user input on this
		else
		{
			name += "Remote";
//			if(GetComponent<DriftCar>())
//			{
//				GetComponent<DriftCar>().SetEnableUserInput(false);
//				GetComponent<DriftCar>().throttle = 0.0f;
//			}
//			if(GetComponent<CarB>())
//			{
//				GetComponent<CarB>().SetEnableUserInput(false);
//			}
//			if(GetComponent<NetworkRigidbody>())
//				GetComponent<NetworkRigidbody>().enabled = true;
		}
		StartCoroutine(WaitForStart());
//		Camera.main.GetComponent<UserInterfaceControl>().enabled = false;
		if(GetComponent<NetworkRigidbody>())
			GetComponent<NetworkRigidbody>().enabled = false;
    }
	
	IEnumerator WaitForStart () {
//		yield return null;
		PlayGameController pgs = FindObjectOfType(typeof(PlayGameController)) as PlayGameController;
		if(GetComponent<DriftCar>())
		{
			GetComponent<DriftCar>().SetEnableUserInput(false);
			GetComponent<DriftCar>().throttle = 0.0f;
		}
		if(GetComponent<CarB>())
		{
//			GetComponent<CarB>().SetEnableUserInput(false);
			GetComponent<CarB>().Wait(true);
			GetComponent<CarB>().StartEngine();
//			GetComponent<CarB>().enabled = false;
		}
		
		while(!pgs.IsStartPlay())
		{
//			transform.Rotate(transform.up);
			yield return null;
		}
//		transform.rotation = Quaternion.identity;
		if(!networkView.isMine && networkView.enabled)
		{
			if(GetComponent<NetworkRigidbody>())
				GetComponent<NetworkRigidbody>().enabled = true;
		}
		else
		{
			if(GetComponent<CarB>())
			{
//				if(!isAI)
//				{
//					GetComponent<CarB>().SetEnableUserInput(true);
//					Camera.main.GetComponent<UserInterfaceControl>().enabled = true;
//				}
//				GetComponent<CarB>().enabled = true;
				GetComponent<CarB>().Wait(false);
			}
			if(GetComponent<CarAI>())
			{
				if(isAI)
					GetComponent<CarAI>().enabled = true;
	//			if(!GetComponent<CarAI>().enabled)
	//			{
	//				GetComponent<DriftCar>().SetEnableUserInput(true);
	//				GetComponent<DriftCar>().throttle = 1.0f;
	//			}
			}
		}
		if(!property)
			property = GetComponent<CarProperty>();
		property.SetShowRank(true);
		//Debug.Log(transform.name + " group : " + networkView.group);
	}
	
	private PlayGameController gameState;
	private CarProperty property;
	private CarB carB;
	private CarAI carAI;
	private bool isSendWin = false;
	
//	void OnTriggerEnter(Collider other){
//		if (enabled && networkView.enabled && networkView.isMine && other.tag == "Road")
//		{
//			if(!carB)
//				carB = GetComponent<CarB>();
//			if(carB && carB.round == 3)
//			{
//				if(!gameState)
//					gameState = FindObjectOfType(typeof(PlayGameController)) as PlayGameController;
//				if(!property)
//					property = GetComponent<CarProperty>();
//				if(!carAI)
//					carAI = GetComponent<CarAI>();
//				if(!isSendWin)
//				{
//					if(gameState && !gameState.IsBalanceState()) {
//						networkView.enabled = false;
//						
//						gameState.SendWin(property.GetDriftTime(), property.GetUseEnergy(), property.GetBeHitTime(), property.ownerGUPID);
//						
//						isSendWin = true;
//					}
//					
//				}
//				carB.SetSteer(0);
//				carB.SetThrottle(0);
////				carB.SetEnableUserInput(false);
////				Camera.main.GetComponent<UserInterfaceControl>().enabled = false;
//				carB.Wait(true);
//				carAI.enabled = false;
//				property.SetShowRank(false);
//			}
//		}
//	}
	
	void FinishRound(){
		if (enabled && networkView.enabled && networkView.isMine)
		{
			if(!gameState)
				gameState = FindObjectOfType(typeof(PlayGameController)) as PlayGameController;
			if(!property)
				property = GetComponent<CarProperty>();
			if(!carAI)
				carAI = GetComponent<CarAI>();
			if(!isSendWin)
			{
				if(gameState && !gameState.IsBalanceState()) {
					networkView.enabled = false;
					
					gameState.SendWin(property.GetDriftTime(), property.GetUseEnergy(), property.GetBeHitTime(), property.ownerGUPID);
					
					isSendWin = true;
				}
				
			}
//			carB.SetSteer(0);
//			carB.SetThrottle(0);
//				carB.SetEnableUserInput(false);
//				Camera.main.GetComponent<UserInterfaceControl>().enabled = false;
//			carB.Wait(true);
			carAI.enabled = false;
			property.SetShowRank(false);
		}
	}
}
