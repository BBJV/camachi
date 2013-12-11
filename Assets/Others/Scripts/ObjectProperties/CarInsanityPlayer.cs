using UnityEngine;
using System;
using System.Collections;

[RequireComponent (typeof(GameUserPlayer))]
public class CarInsanityPlayer : MonoBehaviour {
	
	//public int carID = 0;
	
	
	public CarInsanityCarInfo selectedCar = new CarInsanityCarInfo();
	
	public void Redirect() {
//		Debug.Log("CarInsanityPlayer - Redirect");
		selectedCar = new CarInsanityCarInfo();
	}
}
