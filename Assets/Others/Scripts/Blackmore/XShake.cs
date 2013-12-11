using UnityEngine;
using System.Collections;

public class XShake : MonoBehaviour {
	public Transform Body;
	private Vector3 Original;
	private bool IsShakeLeft;
	private bool IsShake;
	//private bool IsLeft;
	//private float NowTime;
	private float NowShakeTime;
	public float ShakeLevel = 0.01f;
	// Use this for initialization
	void Start () {
		IsShakeLeft = false;
		//NowTime = 0.0f;
		Original = Body.localPosition;
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
//		if(!IsShake){
//			return;
//		}
		
		//NowTime += Time.deltaTime;
		NowShakeTime += Time.deltaTime;
		/*if(NowTime > 1.0f){
			IsShakeLeft = false;
			Body.localPosition = Original;
			//transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f));
			NowTime = 0.0f;
			return;
		}else{*/
			if(NowShakeTime >= 0.03f){
				IsShakeLeft = !IsShakeLeft;
				NowShakeTime = 0.0f;
			}
		//}
		/*
		if(IsLeft){
			Body.localPosition =  Original + new Vector3(0.02f,0.0f,0.0f);
		}else{
			Body.localPosition =  Original + new Vector3(-0.02f,0.0f,0.0f);
		}
		*/
		if(IsShakeLeft){
			Body.localPosition =  Original + new Vector3(ShakeLevel,0.0f,0.0f);
		}else{
			Body.localPosition =  Original + new Vector3(-ShakeLevel,0.0f,0.0f);
		}
		
		
	}
	
	void XShakeIt(){
		//IsLeft = left;
//		IsShake = true;
		enabled = true;
	}
	void StopXShakeIt(){
		//IsLeft = left;
		Body.localPosition = Original;
//		IsShake = false;
		enabled = false;
	}
}
