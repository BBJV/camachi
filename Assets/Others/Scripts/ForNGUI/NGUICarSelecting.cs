using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NGUICarSelecting : MonoBehaviour {
	
//	MatchController matchController;
	
	public delegate void OnCarSelectingChangedEventHandler(CarInsanityCarInfo car);
	public static event OnCarSelectingChangedEventHandler OnCarSelectingChanged;
	
	public int carCount = 1;
	private int currentCar = 0;
	
	public List<CarInsanityCarInfo> allCarInsanityCarInfo;
	
	void Awake() {
		
//		matchController = GameObject.FindObjectOfType(typeof(MatchController)) as MatchController;
		
		MatchController.OnAfterBoardDrawn += OnAfterBoardDrawn;
	}
	
	void OnDestroy() {
		MatchController.OnAfterBoardDrawn -= OnAfterBoardDrawn;
	}
	
	void Start () {
//		string carList = PlayerPrefs.GetString("CarList", "0:0:1:POLICE_CAR, 0:0:3:AMBULANCE, 0:0:2:LORRY_TRUCK, 0:0:4:FIRE_FIGHTING_TRUCK, 0:0:5:GARBAGE_TRUCK");
//		string ownedCarList = PlayerPrefs.GetString("OwnedCarList");
		
		string allCarList = PlayerPrefs.GetString("AllCarList", "0:0:1:POLICE_CAR:0, 0:0:3:AMBULANCE:0, 0:0:2:LORRY_TRUCK:0, 0:0:4:FIRE_FIGHTING_TRUCK:0, 0:0:5:GARBAGE_TRUCK:0, 0:0:6:GODOFWEALTH_TRUCK:0, 0:0:7:HUMMER_TRUCK:0,0:0:8:MASCOT_TRUCK:0,0:0:9:OLDCLASS_TRUCK:0");
		
		string[] carListArray = allCarList.Split(',');

		allCarInsanityCarInfo = new List<CarInsanityCarInfo>();
//		int i = 0;

		foreach(string carInfo in carListArray) {

			if(carInfo != ""){
				string[] carInfoArray = carInfo.Split(':');

				CarInsanityCarInfo c = new CarInsanityCarInfo();
				c.ID = Convert.ToInt32(carInfoArray[0]); //ID
				c.playerID = Convert.ToInt32(carInfoArray[1]); //Player_ID
				c.carID = Convert.ToInt32(carInfoArray[2]); //Car_ID
				c.name = carInfoArray[3]; //name
				c.isTalentOpened = Convert.ToBoolean(Convert.ToInt32(carInfoArray[4]));
				allCarInsanityCarInfo.Add(c);

//				i++;
			}
		}
	}
	
	public void OnAfterBoardDrawn() {
		
//		Debug.Log ("allCarInsanityCarInfo.Count:"+allCarInsanityCarInfo.Count);
//		Debug.Log("OnAfterBoardDrawn:"+PlayerPrefs.GetString("OwnedCarList"));
		if(PlayerPrefs.GetString("OwnedCarList") != "") {
			string ownedCarList = PlayerPrefs.GetString("OwnedCarList");
		
			string[] carListArray = ownedCarList.Split(',');
	
			foreach(string carInfo in carListArray) {
	
				if(carInfo != ""){
					string[] carInfoArray = carInfo.Split(':');
	
					foreach(CarInsanityCarInfo ac in allCarInsanityCarInfo) {
//						Debug.Log ("ac.carID:"+ac.carID+", Convert.ToInt32(carInfoArray[2]):"+Convert.ToInt32(carInfoArray[2]));
						if(ac.carID == Convert.ToInt32(carInfoArray[2])) {
							ac.isTalentOpened = Convert.ToBoolean(Convert.ToInt32(carInfoArray[4]));
							
						}
					}
					
				}
			}
		}
		
		carCount = allCarInsanityCarInfo.Count;
		
		BroadcastMessage("ShowCurrentCar", allCarInsanityCarInfo[currentCar].carID, SendMessageOptions.DontRequireReceiver);
		
		OnCarSelectingChanged(allCarInsanityCarInfo[currentCar]);
	}
	
	void TurnLeft () {
		int previousCar = currentCar;
		currentCar -= 1;
		if(currentCar < 0)
		{
			currentCar = carCount - 1;
		}
		Hashtable args = new Hashtable();
		args.Add("currentCar", allCarInsanityCarInfo[currentCar].carID);
		args.Add("previousCar", allCarInsanityCarInfo[previousCar].carID);
		
		
		
		BroadcastMessage("CarTurnLeft", args, SendMessageOptions.DontRequireReceiver);
		
		OnCarSelectingChanged(allCarInsanityCarInfo[currentCar]);
		
	}
	
	void TurnRight () {
		int previousCar = currentCar;
		currentCar += 1;
		if(currentCar >= carCount)
		{
			currentCar = 0;
		}
		Hashtable args = new Hashtable();
		args.Add("currentCar", allCarInsanityCarInfo[currentCar].carID);
		args.Add("previousCar", allCarInsanityCarInfo[previousCar].carID);
		
		
		
		BroadcastMessage("CarTurnRight", args, SendMessageOptions.DontRequireReceiver);
		
		OnCarSelectingChanged(allCarInsanityCarInfo[currentCar]);
	}
}
