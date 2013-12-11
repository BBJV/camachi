using UnityEngine;
using System.Collections;

public class ZShake : MonoBehaviour {
	public Transform Body;
	private Vector3 Original;
	private bool IsShake;
	private bool IsShakeForward;
	private float NowTime;
	private float NowShakeTime;
	// Use this for initialization
	void Start () {
		IsShake = false;
		NowTime = 0.0f;
		IsShakeForward = false;
		Original = Body.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsShake){
			return;
		}
		NowTime += Time.deltaTime;
		NowShakeTime += Time.deltaTime;
		if(NowTime > 1.0f){
			IsShake = false;
			Body.localPosition = Original;
			//transform.rotation = Quaternion.Euler(new Vector3(0.0f,0.0f,0.0f));
			NowTime = 0.0f;
			return;
		}else{
			if(NowShakeTime >= 0.05f){
				IsShakeForward = !IsShakeForward;
				NowShakeTime = 0.0f;
			}
		}
		if(IsShakeForward){
			Body.localPosition =  Body.localPosition + new Vector3(0.0f,0.0f,-0.1f);
		}else{
			Body.localPosition =  Body.localPosition + new Vector3(0.0f,0.0f,0.1f);
		}
	}
	
	void ZShakeIt(){
		IsShake = true;
	}
}
