using UnityEngine;
using System.Collections;

public class RaceCourse : TrainingItem {

	//public float TimeLimit;
	public float LapLimit;
	public Transform car;
	private Transform AICar;
	
	private CarB carb;
	private CarB AIcarb;
//	private float NowTime;
	
	private bool IsComplete = true;
	
	private CarProperty property;
//	private CarProperty AIproperty;
	
//	private UserInterfaceControl userinterfacecontrol;
	public override void Start()
	{	
		
	}
	public override void StartAction() {
		//IsTimeUp = false;
		print("StartAction");
		IsComplete = false; 
		
//		NowTime = 0.0f;
		
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
		if(carb == null){
			carb = car.GetComponent<CarB>();
		}
		carb.SetStartLine(false);
		carb.ResetNowNode();
		//print("car round = "+carb.GetRound());
		property = car.GetComponent<CarProperty>();
		property.SetEnergy(0.0f);
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(true);
//		userinterfacecontrol.SetShowLoop(true);
//		userinterfacecontrol.SetShowRank(true);
//		userinterfacecontrol.SetShowTime(false);
//		userinterfacecontrol.SetShowSkillBg(true);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
//		userinterfacecontrol.SetShowSkill(0,true);
//		userinterfacecontrol.SetShowBrake(true);
		
		AICar = GameObject.FindWithTag("TrainingAICar").transform;
//		AIproperty = AICar.GetComponent<CarProperty>();
		AICar.GetComponent<CarAI>().enabled = true;
		AIcarb = AICar.GetComponent<CarB>();
		AIcarb.Wait(false);
		
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
		}
		print("AIcarb.GetRound() = " +AIcarb.GetRound());
		print("carb.GetStartLine = " +carb.GetStartLine());
		print("carb.GetRound = " +carb.GetRound());
		print("carb.GetWeights = " +carb.GetWeights());*/
		if(AIcarb.GetRound() > LapLimit){
			IsComplete = true;
				NextState = State.Fail;
		}
		if(carb.GetRound() > LapLimit){
			if(property.GetRank() == 1){
				IsComplete = true;
				NextState = State.Success;
				
			}else{
				IsComplete = true;
				NextState = State.Fail;
				
			}
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
}