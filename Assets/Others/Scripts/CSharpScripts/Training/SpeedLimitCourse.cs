using UnityEngine;
using System.Collections;

public class SpeedLimitCourse : TrainingItem {

	//public float TimeLimit;
	public float LapLimit;
	public Transform car;
	private Transform AICar;
	
	private CarB carb;
	private CarB AIcarb;
//	private float NowTime;
	
	private bool IsComplete = true;
	
	private CarProperty property;
	private CarProperty AIproperty;
	
//	private UserInterfaceControl userinterfacecontrol;
	
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		
//		NowTime = 0.0f;
		
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
		if(carb == null){
			carb = car.GetComponent<CarB>();
		}
		
		carb.SetStartLine(false);
		carb.ResetNowNode();
		property = car.GetComponent<CarProperty>();
		property.SetEnergy(1.0f);
		
		AICar = GameObject.FindWithTag("TrainingAICar").transform;
		AIproperty = AICar.GetComponent<CarProperty>();
		AICar.GetComponent<CarAI>().enabled = true;
		AIcarb = AICar.GetComponent<CarB>();
		AIcarb.Wait(false);
		
		
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(true);
//		userinterfacecontrol.SetShowLoop(false);
//		userinterfacecontrol.SetShowRank(true);
//		userinterfacecontrol.SetShowTime(false);
//		userinterfacecontrol.SetShowSkillBg(true);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
//		userinterfacecontrol.SetShowSkill(0,true);
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
		if(AIproperty.CheckSpeedLimit()){
			IsComplete = true;
			NextState = State.Success;
			
		}
		if(carb.GetRound() >= 1 || AIcarb.GetRound() >= 1){
			IsComplete = true;
			NextState = State.Fail;
			
		}
		/*NowTime += Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,TimeLimit);
		if(NowTime >= TimeLimit){
			IsComplete = true;
			NextState = State.Fail;
		}*/
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
	public override int GetBackStep(){return 3;}
}