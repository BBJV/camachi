using UnityEngine;
using System.Collections;

public class SpeedUIController : MonoBehaviour {
	public Transform[] NormalSpeedBar;
	public Transform SpeedUpBar;
	public UISprite TimeMin;
	public UISprite TimeCommon;
	public UISprite TimeSec10;
	public UISprite TimeSec1;
	private CarB PlayerCar;
	private float BarSpeed;
	private float NowTime;
	private float BarTime;
	private float CommonTime;
	private int lastBarCount = 0;
	public string[] TimeUIString;
	// Use this for initialization
	IEnumerator Start () {
		SmoothFollow smCamera = Camera.main.GetComponent<SmoothFollow>();
		while(!smCamera.target)
		{
			yield return null;
		}
		PlayerCar = smCamera.target.GetComponent<CarB>();
		BarSpeed = PlayerCar.gearSpeeds[PlayerCar.gearSpeeds.Length - 1] / NormalSpeedBar.Length;
		SetAllSpeedBarOff();
		NowTime = 0.0f;
		CommonTime = 0.0f;
		if(TimeMin){
			TimeMin.spriteName = TimeUIString[0];
		}
		if(TimeSec10){
			TimeMin.spriteName = TimeUIString[0];
		}
		if(TimeSec1){
			TimeSec1.spriteName = TimeUIString[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerCar && !PlayerCar.IsWaiting){
			BarTime -= Time.deltaTime;
			NowTime += Time.deltaTime;
			CommonTime += Time.deltaTime;
			int barcount = Mathf.CeilToInt(PlayerCar.GetCarSpeed() / BarSpeed) - 1;
			barcount = Mathf.Clamp(barcount, 0, NormalSpeedBar.Length - 1);
			
			if(barcount > lastBarCount)
			{
				for(int i = lastBarCount ; i <= barcount ; i ++){
					NormalSpeedBar[i].gameObject.SetActiveRecursively(true);
				}
			}
			else if(barcount < lastBarCount)
			{
				for(int i = barcount ; i <= lastBarCount ; i ++){
					NormalSpeedBar[i].gameObject.SetActiveRecursively(false);
				}
			}
			else
			{
				if(BarTime >= 0.0f){
					NormalSpeedBar[barcount].gameObject.SetActiveRecursively(false);
				}else if(BarTime >= -0.5f){
					NormalSpeedBar[barcount].gameObject.SetActiveRecursively(true);
				}else{
					BarTime = 0.5f;
				}
			}
			lastBarCount = barcount;
//			for(int i = 0 ; i < NormalSpeedBar.Length ; i ++){
//				if(i < barcount){
//					NormalSpeedBar[i].gameObject.SetActiveRecursively(true);
//				}else if(i == barcount){
//					if(BarTime >= 0.0f){
//						NormalSpeedBar[i].gameObject.SetActiveRecursively(false);
//					}else if(BarTime >= -0.5f){
//						NormalSpeedBar[i].gameObject.SetActiveRecursively(true);
//					}else{
//						BarTime = 0.5f;
//					}
//				}
//				else{
//					NormalSpeedBar[i].gameObject.SetActiveRecursively(false);
//				}
//			}
			if(TimeMin){
				TimeMin.spriteName = TimeUIString[Mathf.FloorToInt(NowTime / 60.0f)];
			}
			if(TimeSec10){
				TimeSec10.spriteName = TimeUIString[Mathf.FloorToInt( (NowTime % 60.0f ) / 10.0f)];
			}
			if(TimeSec1){
				TimeSec1.spriteName = TimeUIString[Mathf.FloorToInt( (NowTime % 60.0f ) % 10.0f)];
			}
		}
	}
	
	void SetAllSpeedBarOff(){
		for(int i = 0 ; i < NormalSpeedBar.Length ; i ++){
			NormalSpeedBar[i].gameObject.SetActiveRecursively(false);
		}
	}
	void SpeedUp(){
		//SpeedUpBar.gameObject.SetActiveRecursively(true);
	}
}
