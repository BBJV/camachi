using UnityEngine;
using System.Collections;

public class RotateZShake : MonoBehaviour {
//	private bool IsShake;
	private bool IsShakeLeft = false;
	private float NowTime = 0.0f;
	private float NowShakeTime;
	// Use this for initialization
//	void Start () {
////		IsShake = false;
//		NowTime = 0.0f;
//		IsShakeLeft = false;
//	}
	
	// Update is called once per frame
	void LateUpdate () {
//		if(!IsShake){
//			return;
//		}
		NowTime += Time.deltaTime;
		NowShakeTime += Time.deltaTime;
		if(NowTime > 0.5f){
//			IsShake = false;
			enabled = false;
			//transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f));
			NowTime = 0.0f;
			return;
		}else{
			if(NowShakeTime >= 0.05f){
				IsShakeLeft = !IsShakeLeft;
				NowShakeTime = 0.0f;
			}
		}
		if(IsShakeLeft){
			transform.rotation = Quaternion.Euler(
				transform.rotation.eulerAngles + new Vector3(0.0f,0.0f,-1.0f));
		}else{
			transform.rotation = Quaternion.Euler(
				transform.rotation.eulerAngles + new Vector3(0.0f,0.0f,1.0f));
		}
	}
	
	void RotateZShakeIt(){
//		IsShake = true;
		enabled = true;
	}
}
