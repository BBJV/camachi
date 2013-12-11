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
	bool IsInit;
	// Use this for initialization
	void Start () {
		MainCamera = Camera.main;
		MainCameraTransform = MainCamera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsInit){
			IsInit = true;
			MoveIndex = Vector3.Magnitude(MainCameraTransform.position - CameraPosition.position) / AnimationTime;
			RotateIndex = Quaternion.Angle(MainCameraTransform.rotation,CameraPosition.rotation) / AnimationTime;
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
			transform.root.BroadcastMessage("ChangeState",MyState.State_Run);
		}
		
		if(LookAtTransform){
			MainCameraTransform.LookAt(LookAtTransform);
		}
	}
	void OnEnable(){
		IsInit = false;
	}
}
