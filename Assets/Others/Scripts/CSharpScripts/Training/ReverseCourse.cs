using UnityEngine;
using System.Collections;

public class ReverseCourse : TrainingItem {

	public float TimeLimit;
	public float ReverseTimeNeed;
	public Transform car;
	public GameObject trigger;
	
	private CarB carb;
	private float NowTime;
	private float ReverseTime;
	//private TriggerPlane triggerPlane;
	private bool IsComplete = true;
//	private UserInterfaceControl userinterfacecontrol;
	private GameCountDownTimer CountDownTimer;
	public override void Start()
	{
		NowTime = 0.0f;
		//triggerPlane = trigger.GetComponent<TriggerPlane>();
	}
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		NowTime = TimeLimit;
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
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
//		userinterfacecontrol.SetShowSkillBg(false);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
//		userinterfacecontrol.SetShowBrake(true);
	}
	
	// Update is called once per frame
	public override void Update() {
		if(carb == null){
			carb = car.GetComponent<CarB>();
			return;
		}
		if(IsComplete){
			carb.SetReverseSound(false);
			return;
		}
		if(carb.IsCarReverse()){
			ReverseTime += Time.deltaTime;
			
			if(ReverseTime >= ReverseTimeNeed){
				IsComplete = true;
				NextState = State.Success;
				
				return;
			}
		}else{
			ReverseTime = 0.0f;
		}
		
		NowTime -= Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,TimeLimit);
		CountDownTimer.SetNowTime(NowTime);
		if(NowTime <= 0.0f){
			IsComplete = true;
			NextState = State.Fail;
			return;
			//IsTimeUp = true;
			//IsStart = false;
		}
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
//	public override void OnGUI () {
//		if(!IsComplete){
////			int nowtime = (int)NowTime;
//		}
//	}
}