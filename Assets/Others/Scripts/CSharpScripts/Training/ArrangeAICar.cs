using UnityEngine;
using System.Collections;

public class ArrangeAICar : TrainingItem {

	//public float TimeLimit;
	public Transform AICarPrefab;
	
	private CarB carb;
	private CarB AIcarb;
	private float NowTime;
	
	private bool IsComplete = true;
	
	private CarProperty property;
	private CarProperty AIproperty;
	
//	private UserInterfaceControl userinterfacecontrol;
	
	public override void StartAction() {
		//IsTimeUp = false;
		IsComplete = false; 
		/*AIcarb = AICar.GetComponent<CarB>();
		AIproperty = AICar.GetComponent<CarProperty>();
		
		AICar.gameObject.SetActiveRecursively(true);
		AICar.gameObject.active = true;

		AICar.GetComponent<CarAI>().enabled = false;
		AICar.GetComponent<CarB>().enabled = false;
		AICar.Find("SkillEffect").gameObject.SetActiveRecursively(false);
		AICar.Find("HUD").gameObject.SetActiveRecursively(false);
		
		
		//AICar.transform.rigidbody.useGravity = false;
		//AICar.transform.rigidbody.isKinematic = true;*/
		
			//print("startPosition = "+startPosition);
		
		Transform tf = Instantiate(AICarPrefab, StartPoint.transform.position, StartPoint.transform.rotation) as Transform;
		tf.GetComponent<CarAI>().enabled = false;
		tf.GetComponent<CarB>().Wait(true);
		tf.Find("SkillEffect").gameObject.SetActiveRecursively(false);
		tf.GetComponent<NetworkView>().enabled = false;
		tf.GetComponent<NetworkRigidbody>().enabled = false;
		tf.GetComponent<CarNetworkInit>().enabled = false;
		tf.tag = "TrainingAICar";
		//tf.position = StartPoint.position;
		//tf.rotation = StartPoint.rotation;
	}
	
	// Update is called once per frame
	public override void Update() {
		
		if(IsComplete){
			return;
		}
		IsComplete = true; 
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
