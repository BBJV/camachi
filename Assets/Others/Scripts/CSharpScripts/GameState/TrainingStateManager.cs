using UnityEngine;
using System.Collections;
public enum State{
	RunItem = 0,
	Success,
	Fail,
	AllClear
}
public class TrainingStateManager : MonoBehaviour {
	public TrainingItem[] item;
	public Texture2D T_Sucess;
	public Texture2D T_Fail;
	public Transform car;
	private CarB carb;
//	private UserInterfaceControl userinterfacecontrol;
	private int TrainingIndex;
	//private bool IsEndTraining;
	private State NowState;
	// Use this for initialization
	void Start () {
		//item1.StartAction();
		//print("(GameObject.FindGameObjectWithTacar) = " + (GameObject.FindGameObjectWithTag("car")));
		
		//car.GetComponent<CarB>().Wait(true);
		//transform.parent.GetComponent<CarB>().Wait(true);
		
//		userinterfacecontrol = car.Find("HUD").GetComponent<UserInterfaceControl>();
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl; // change by Vincent
		
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(false);
//		userinterfacecontrol.SetShowLoop(false);
//		userinterfacecontrol.SetShowRank(false);
//		userinterfacecontrol.SetShowTime(false);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
		NowState = State.RunItem;
		TrainingIndex = 0;
		//IsEndTraining = false;
		item[TrainingIndex].StartAction();
	}
	
	// Update is called once per frame
	void Update () {
		/*if(IsEndTraining){
			return;
		}*/
		//print("TrainingIndex= "+TrainingIndex);
		switch(NowState){
			case State.RunItem:
				//item[TrainingIndex].UpdateAction();
				if(item[TrainingIndex].IsEnd()){
					NowState = item[TrainingIndex].GetNextState();
					if(NowState == State.RunItem){
						TrainingIndex++;
						item[TrainingIndex].StartAction();
					}
				}
					break;
			case State.Success:
				if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				{
					foreach (Touch touch in Input.touches) {
							if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
							{
								RunSuccess();
							}
					}
				}
				else
				{
					if(Input.GetMouseButtonDown(0)){
						RunSuccess();
					}
				}
				
					break;
			case State.Fail	:
				if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				{
					foreach (Touch touch in Input.touches) {
							if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
							{
								RunFail();
							}
					}
				}
				else
				{
					if(Input.GetMouseButtonDown(0)){
						RunFail();
					}
				}
					break;
			case State.AllClear	:
					break;
		}
		
	}
	
	void OnGUI(){
		//item1.OnGUI();
		switch(NowState){
			case State.RunItem:
				
					break;
			case State.Success:
				GUI.DrawTexture(new Rect(0.0f,0.0f,T_Sucess.width,T_Sucess.height) ,T_Sucess);
					break;
			case State.Fail	:
				GUI.DrawTexture(new Rect(0.0f,0.0f,T_Fail.width,T_Fail.height),T_Fail);
					break;
			case State.AllClear	:
					break;
		}
	}
	
	private void RunSuccess(){
		TrainingIndex++;
		if(TrainingIndex >= item.Length){
			//(GameObject.FindGameObjectWithTag("car")).GetComponent<CarB>().Wait(false);
			//car.GetComponent<CarB>().Wait(false);
			//IsEndTraining= true;
			NowState = State.AllClear;
		}else{
			NowState = State.RunItem;
			item[TrainingIndex].StartAction();
		}
	}
	
	private void RunFail(){
		TrainingIndex -= item[TrainingIndex].GetBackStep();
		NowState = State.RunItem;
		item[TrainingIndex].StartAction();
	}
}
