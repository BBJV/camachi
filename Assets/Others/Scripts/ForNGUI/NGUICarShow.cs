using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class NGUICarShow : MonoBehaviour {

	public int carID;
	public GameObject carGameObject;
	
	public GameObject lockingCar;
	
	public List<CarInsanityCarInfo> ownedCarListInsanityCarInfo;
	
	
	void Awake() {	
		MatchController.OnAfterBoardDrawn += OnAfterBoardDrawn;
	}
	
	void OnDestroy() {
		MatchController.OnAfterBoardDrawn -= OnAfterBoardDrawn;
	}
	
	void OnAfterBoardDrawn() {
//		Debug.Log ("NGUICarShow:OnAfterBoardDrawn");
		string ownedCarList = PlayerPrefs.GetString("OwnedCarList", "0:0:1:POLICE_CAR, 0:0:3:AMBULANCE, 0:0:2:LORRY_TRUCK, 0:0:4:FIRE_FIGHTING_TRUCK, 0:0:5:GARBAGE_TRUCK");
		string[] carListArray = ownedCarList.Split(',');

		ownedCarListInsanityCarInfo = new List<CarInsanityCarInfo>();
		int i = 0;

		foreach(string carInfo in carListArray) {

			if(carInfo != ""){
				string[] carInfoArray = carInfo.Split(':');

				CarInsanityCarInfo c = new CarInsanityCarInfo();
				c.ID = Convert.ToInt32(carInfoArray[0]); //ID
				c.playerID = Convert.ToInt32(carInfoArray[1]); //Player_ID
				c.carID = Convert.ToInt32(carInfoArray[2]); //Car_ID
				c.name = carInfoArray[3]; //name
				
				ownedCarListInsanityCarInfo.Add(c);

				i++;
			}
		}
		
//		Debug.Log ("ownedCarListInsanityCarInfo.Count:"+ownedCarListInsanityCarInfo.Count);
//		carGameObject.SetActiveRecursively(true);
		foreach(CarInsanityCarInfo ownedCar in ownedCarListInsanityCarInfo) {
			if(ownedCar.carID == carID) {
				lockingCar.SetActiveRecursively(false);
			}
		}
	}
	
	void CarTurnLeft (Hashtable args) {
//		Debug.Log ((carID == (int)args["previousCar"] || carID == (int)args["currentCar"]).ToString());
		
		if(carID == (int)args["previousCar"] || carID == (int)args["currentCar"]) {
			carGameObject.SetActiveRecursively(true);
			
			foreach(CarInsanityCarInfo ownedCar in ownedCarListInsanityCarInfo) {
				if(ownedCar.carID == carID) {
					lockingCar.SetActiveRecursively(false);
				}
			}
			
			if(carID == (int)args["currentCar"])
			{
				animation.Play("car_1_R_store");
			}
			else if(carID == (int)args["previousCar"])
			{
				animation.Play("car_1_L_store");
			}
		}else{
			carGameObject.SetActiveRecursively(false);
		}
		
		
		
	}
	
	void CarTurnRight (Hashtable args) {
//		Debug.Log ((carID == (int)args["previousCar"] || carID == (int)args["currentCar"]).ToString());
		
		if(carID == (int)args["previousCar"] || carID == (int)args["currentCar"]) {
			carGameObject.SetActiveRecursively(true);
			
			foreach(CarInsanityCarInfo ownedCar in ownedCarListInsanityCarInfo) {
				if(ownedCar.carID == carID) {
					lockingCar.SetActiveRecursively(false);
				}
			}
			
			if(carID == (int)args["currentCar"])
			{
				animation.Play("car_1_L_store_back");
			}
			else if(carID == (int)args["previousCar"])
			{
				animation.Play("car_1_R_store_back");
			}
		}else{
			carGameObject.SetActiveRecursively(false);
		}
		
		
		
		
		
		
		
		
	}
	
	void ShowCurrentCar (int ID) {
//		print("ID = "+ID);
//		print("carID = "+carID);
		carGameObject.SetActiveRecursively(carID == ID);
//		print("test = "+carID);
	}
}
