using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {
	public float Velocity;
	public float Acceleration = 50.0f;
	public float MaxVelocity = 30.0f;
	
	public bool IsOn;
	
	private Transform RootTransform;
	private static string YShakeIt = "YShakeIt";
	private static string StopYShakeIt = "StopYShakeIt";
	private static string CarSpeed = "CarSpeed";
	
	private static string CarOil = "CarOil";
	private float Oil;
	private float OilTime;
	
	private static string CarTemp = "CarTemp";
	private float Temp;
	private float TempTime;
	// Use this for initialization
	void Start () {
		IsOn = false;
		Velocity = 0.0f;
		RootTransform = transform.root;
		Oil = 100.0f;
		OilTime = 0.0f;
		Temp = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		TempTime += Time.deltaTime;
		if(IsOn){
			OilTime += Time.deltaTime;
			
			Accelerate(true);
			if(OilTime > 2.0f){
				Oil -= 1.0f;
				OilTime = 0.0f;
			}
			if(Oil < 0.0f){
				Oil = 100.0f;
			}
			
			if(TempTime > 1.0f){
				Temp += 1.0f;
				TempTime = 0.0f;
			}
		}else{
			Accelerate(false);
			if(TempTime > 1.0f){
				Temp -= 1.0f;
				TempTime = 0.0f;
			}
		}
		if(Temp >= 50.0f){
			Temp = 50.0f;
		}else if(Temp < 0.0f){
			Temp = 0.0f;
		}
		RootTransform.BroadcastMessage(CarSpeed,Velocity,SendMessageOptions.DontRequireReceiver);
		RootTransform.BroadcastMessage(CarOil,Oil,SendMessageOptions.DontRequireReceiver);
		RootTransform.BroadcastMessage(CarTemp,Temp,SendMessageOptions.DontRequireReceiver);
	}
	void Accelerate(bool isspeedup){
		if(isspeedup){
			Velocity += Acceleration * Time.deltaTime;
		}else{
			Velocity -= Acceleration * Time.deltaTime;
		}
		
		if(Velocity >= MaxVelocity){
			Velocity = MaxVelocity;
		}else if(Velocity < 0.0f){
			Velocity = 0.0f;
		}
	}
	void ClickedOn(){
		IsOn = true;
		Camera.main.BroadcastMessage(YShakeIt,SendMessageOptions.DontRequireReceiver);
	}
	void ClickedOff(){
		Camera.main.BroadcastMessage(StopYShakeIt,SendMessageOptions.DontRequireReceiver);
		IsOn = false;
	}
}
