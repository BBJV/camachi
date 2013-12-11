using UnityEngine;
using System.Collections;

public class DriftCourse : TrainingItem {

	public float TimeLimit;
	public float ShowTurnTimeLimit;
	public Transform car;
	public GameObject CurveStartTrigger;
	public GameObject CurveEndTrigger;
	public GameObject TurnSignTrigger;
	public Texture2D LeftTurnSign;
	public float scale;
	
	private CarB carb;
	private float NowTime;
	private TriggerPlane StartTriggerPlane;
	private TriggerPlane EndTriggerPlane;
	private TriggerPlane TurnSignTriggerPlane;
	private bool IsComplete = true;
	private float ShowTurnTime;
	bool IsEnterCurve;
	bool ShowTurnSign;
	
	private Rect TurnSign_Co;
	
//	private UserInterfaceControl userinterfacecontrol;
	
	private GameCountDownTimer CountDownTimer;
	public override void Start()
	{	
		StartTriggerPlane = CurveStartTrigger.GetComponent<TriggerPlane>();
		EndTriggerPlane = CurveEndTrigger.GetComponent<TriggerPlane>();
		TurnSignTriggerPlane = TurnSignTrigger.GetComponent<TriggerPlane>();
		
		TurnSign_Co = new Rect((Screen.width - LeftTurnSign.width * scale) / 2 ,
		                       (Screen.height - LeftTurnSign.height * scale) / 2- LeftTurnSign.height * scale ,
		                       -LeftTurnSign.width * scale , LeftTurnSign.height * scale);
	}
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		IsEnterCurve = false;
		ShowTurnSign = false;
		StartTriggerPlane.ResetTrig();
		EndTriggerPlane.ResetTrig();
		TurnSignTriggerPlane.ResetTrig();
		NowTime = TimeLimit;
		ShowTurnTime = 0.0f;
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
		
		CountDownTimer = MyGameCountDownTimer.GetComponent<GameCountDownTimer>();
		CountDownTimer.SetNowTime(NowTime);
		CountDownTimer.IsShowCountDown = true;
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(true);
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
		if(carb == null){
			carb = car.GetComponent<CarB>();
			return;
		}
		if(IsComplete){
			return;
		}
		if(ShowTurnSign){
			ShowTurnTime += Time.deltaTime;
			if(ShowTurnTime >= ShowTurnTimeLimit){
				ShowTurnSign = false;
			}
		}else{
			if(TurnSignTriggerPlane.GetTrig() != -1){
				ShowTurnSign = true;
			}
		}
		if(IsEnterCurve){
			if(!carb.IsCarDrift()){
				IsComplete = true;
				NextState = State.Fail;
				ShowTurnSign = false;
				return;
			}
			if(EndTriggerPlane.GetTrig() != -1){
				IsComplete = true;
				NextState = State.Success;
				ShowTurnSign = false;
				return;
			}
		}else{
			if(StartTriggerPlane.GetTrig() != -1){
				//print("IsEnterCurve = true;");
				IsEnterCurve = true;
			}
		}
		
		NowTime -= Time.deltaTime;
		NowTime = Mathf.Clamp(NowTime,0.0f,TimeLimit);
		CountDownTimer.SetNowTime(NowTime);
		if(NowTime <= 0.0f){
			IsComplete = true;
			NextState = State.Fail;
			ShowTurnSign = false;
			//IsTimeUp = true;
			//IsStart = false;
		}
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
	public override void OnGUI () {
		if(ShowTurnSign){
			GUI.DrawTexture(TurnSign_Co , LeftTurnSign);
		}
	}
}
