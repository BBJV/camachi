using UnityEngine;
using System.Collections;

public class ContDownTimer : TrainingItem {
	public Texture2D[] Num;
	public float CountDownSecs;
	public Transform car;
	private CarB carb;
	
	private float NowTime;
	//private bool IsTimeUp;
	private bool IsComplete = true;
	private Rect Co;
	
	public override void Start()
	{
		Co = new Rect((Screen.width - Num[0].width) / 2 ,
		              (Screen.height - Num[0].height) / 2 ,Num[0].width , Num[0].height);
	}
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		ResetTime();
	}
	
	// Update is called once per frame
	public override void Update() {
		if(IsComplete){
			
			return;
		}
		if(carb == null){
			carb = car.GetComponent<CarB>();
			return;
		}
		
		NowTime -= Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,CountDownSecs);
		if(NowTime <= 0.0f){
			NowTime = 0.0f;
			IsComplete = true;
			carb.Wait(false);
			//IsTimeUp = true;
			//IsStart = false;
		}
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
	public override void OnGUI () {
		if(!IsComplete){
			int nowtime = (int)NowTime;
			GUI.DrawTexture(Co , Num[nowtime]);
		}
	}
	
	private void ResetTime()
	{
		NowTime = CountDownSecs;
	}
}
