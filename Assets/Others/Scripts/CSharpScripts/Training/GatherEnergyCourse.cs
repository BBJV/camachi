using UnityEngine;
using System.Collections;

public class GatherEnergyCourse : TrainingItem {

	public float TimeLimit;
	public Transform car;
	
	private CarB carb;
	private float NowTime;
	
	private bool IsComplete = true;
	
	private CarProperty property;
	
//	private UserInterfaceControl userinterfacecontrol;
	private GameCountDownTimer CountDownTimer;
	public override void Start()
	{	
		
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
		carb.ResetNowNode();
		CountDownTimer = MyGameCountDownTimer.GetComponent<GameCountDownTimer>();
		CountDownTimer.IsShowCountDown = true;
		CountDownTimer.SetNowTime(NowTime);
		
		property = car.GetComponent<CarProperty>();
		property.SetEnergy(0.0f);
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(true);
//		userinterfacecontrol.SetShowLoop(true);
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
			return;
		}

		/*if(IsEnterCurve){
			if(!carb.IsCarDrift()){
				IsComplete = true;
				NextState = State.Fail;
				return;
			}
			if(EndTriggerPlane.GetTrig() != -1){
				IsComplete = true;
				NextState = State.Success;
				return;
			}
		}else{
			if(StartTriggerPlane.GetTrig() != -1){
				//print("IsEnterCurve = true;");
				IsEnterCurve = true;
			}
		}*/
		if(property.CheckEnergy() >= 3.0f){
			IsComplete = true;
			NextState = State.Success;
		}
		
		if(carb.GetRound() >= 1){
			IsComplete = true;
			NextState = State.Fail;
		}
		NowTime -= Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,TimeLimit);
		CountDownTimer.SetNowTime(NowTime);
		if(NowTime <= 0.0f){
			IsComplete = true;
			NextState = State.Fail;
			
		}
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
	public override void OnGUI () {
		base.OnGUI();
		/*if(ShowTurnSign){
			GUI.DrawTexture(TurnSign_Co , LeftTurnSign);
		}*/
	}
}