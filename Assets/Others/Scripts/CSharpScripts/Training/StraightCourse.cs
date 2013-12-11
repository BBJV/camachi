using UnityEngine;
using System.Collections;

public class StraightCourse : TrainingItem {

	public float TimeLimit;
	public Transform car;
	public GameObject trigger;
	
	private CarB carb;
	private float NowTime;
	private TriggerPlane triggerPlane;
	private bool IsComplete = true;
	
//	private UserInterfaceControl userinterfacecontrol;
	private GameCountDownTimer CountDownTimer;
	public override void Start()
	{
		NowTime = 0.0f;
		triggerPlane = trigger.GetComponent<TriggerPlane>();
	}
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		NowTime = TimeLimit;
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
		if(carb == null){
			carb = car.GetComponent<CarB>();
		}
		carb.SetStartLine(false);
		CountDownTimer = MyGameCountDownTimer.GetComponent<GameCountDownTimer>();
		CountDownTimer.IsShowCountDown = true;
		CountDownTimer.SetNowTime(NowTime);
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(false);
//		userinterfacecontrol.SetShowLoop(false);
//		userinterfacecontrol.SetShowRank(false);
//		userinterfacecontrol.SetShowTime(false);
//		userinterfacecontrol.SetShowTime(false);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
//		userinterfacecontrol.SetShowBrake(true);
	}
	
	// Update is called once per frame
	public override void Update() {
		if(IsComplete){
			return;
		}
		if(triggerPlane.GetTrig() != -1){
			IsComplete = true;
			NextState = State.Success;
			return;
		}
		NowTime -= Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,TimeLimit);
		CountDownTimer.SetNowTime(NowTime);
		if(NowTime <= 0.0f){
			IsComplete = true;
			NextState = State.Fail;
			//IsTimeUp = true;
			//IsStart = false;
		}
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
//	public override void OnGUI () {
//		if(!IsComplete){
//			int nowtime = (int)NowTime;
//		}
//	}
}
