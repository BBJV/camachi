using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public Transform CameraPosition;
	private Camera MainCamera;
	private Transform MainCameraTransform;
	private float MoveIndex;
	private float RotateIndex;
	public float AnimationTime = 1.5f;
	public Transform LookAtTransform;
	public ActionEvent MyActionEvent;
	public int StartActionTag;
	public int EndActionTag;
	bool IsStart;
//	public string CallWhenFinish = "ChangeState";
//	bool IsInit;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(!IsStart){
			return;
		}
		if(Vector3.SqrMagnitude(MainCameraTransform.position - CameraPosition.position) > 0.01f){
			MainCameraTransform.position = Vector3.MoveTowards(MainCameraTransform.position,CameraPosition.position,MoveIndex * Time.deltaTime);
			if(RotateIndex != 0.0f){
				MainCameraTransform.rotation = 
				Quaternion.RotateTowards(MainCameraTransform.rotation,CameraPosition.rotation,RotateIndex * Time.deltaTime);
			}
		}else{
			MainCameraTransform.position = CameraPosition.position;
			MainCameraTransform.rotation = CameraPosition.rotation;
			if(EndActionTag >= 0){
				MyAction tempaction = new MyAction();
				tempaction.Tag = EndActionTag;
				transform.root.BroadcastMessage(MyAction.ActionString,tempaction);
			}
		}
		
		if(LookAtTransform){
			MainCameraTransform.LookAt(LookAtTransform);
		}
	}
	void OnEnable(){
		if(MyActionEvent == ActionEvent.WhenOn){
			MyAction tempaction = new MyAction();
			tempaction.Tag = StartActionTag;
			Action(tempaction);
		}
	}

	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			MainCamera = Camera.main;
			MainCameraTransform = MainCamera.transform;
			MoveIndex = Vector3.Magnitude(MainCameraTransform.position - CameraPosition.position) / AnimationTime;
			RotateIndex = Quaternion.Angle(MainCameraTransform.rotation,CameraPosition.rotation) / AnimationTime;
			IsStart = true;
		}
	}
}
