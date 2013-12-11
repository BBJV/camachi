using UnityEngine;
using System.Collections;

public class YShake : MonoBehaviour {
	public Transform Body;
	public Transform Original;
//	private bool IsShake;
	private bool IsShakeUpward = false;
	//private float NowTime;
	private float NowShakeTime;
	private float Level;
	public float YShakeItL = 0.005f;
	public float YShakeItS = 0.01f;
	void Awake(){
		//Original = Body.localPosition;
	}
	// Use this for initialization
	void Start () {
//		IsShake = false;
		//NowTime = 0.0f;
		IsShakeUpward = false;
		//Original = Body.localPosition;
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		NowShakeTime += Time.deltaTime;
		
		//if(rigidbody.velocity.magnitude > 20.0f){
			//Level = YShakeItL;
		//}else if(rigidbody.velocity.magnitude > 1.0f){
			Level = YShakeItS;
		//}
			if(NowShakeTime >= 0.05f){
				IsShakeUpward = !IsShakeUpward;
				NowShakeTime = 0.0f;
			}
		//}
		if(IsShakeUpward){
			//Body.localPosition =  Original + new Vector3(0.0f,Level,0.0f);
			Body.position =  Original.position + new Vector3(0.0f,Level,0.0f);
		}else{
			Body.position =  Original.position + new Vector3(0.0f,-Level,0.0f);
		}
	}
	
	void YShakeIt () {
		enabled = true;
	}
	void StopYShakeIt(){
		Body.position = Original.position;
		enabled = false;
	}
}
