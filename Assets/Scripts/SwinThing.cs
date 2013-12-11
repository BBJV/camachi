using UnityEngine;
using System.Collections;

public class SwinThing : MonoBehaviour {
	public Transform RotateTransform;
	private Transform MyTransform;
	public float MyRotateTime;
	public Vector3 MyRotateAxis;
	public float MyAngle;
	
	public bool IsDecay = true;
	public int SwinTimes;
	public bool IsDisableWhenFinish = true;
	
	private bool IsRotateOK;
	private float RotateIndex;
	private Quaternion ToAngle;
	private Vector3 NowRotateAngle;
	private Vector3 DecayIndex;
	private int NowSwinTimes;
	private bool IsClockWise = true;
	private bool IsInit;
	private Quaternion OriginalQuaternion;
	private Vector3 MyRotateAngle;
	public bool IsPlay;
	
	private static string ActionFinish = "ActionFinish";
	
	// Use this for initialization
	void Start () {
		MyTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsPlay){
			return;
		}
		if(!IsInit){
			IsInit = true;
			if(IsClockWise){
				ToAngle = Quaternion.Euler(NowRotateAngle);
			}else{
				ToAngle = Quaternion.Euler(-NowRotateAngle);
			}
			
			RotateIndex = Quaternion.Angle(RotateTransform.rotation,ToAngle) / MyRotateTime;
			
			//print("RotateIndex= " + RotateIndex);
			if(NowSwinTimes >= SwinTimes){
				
				IsRotateOK = true;
				RotateTransform.rotation = OriginalQuaternion;
				if(IsDisableWhenFinish){
					//MyTransform.gameObject.SetActiveRecursively(false);
					IsPlay = false;
					MyTransform.parent.BroadcastMessage(ActionFinish,SendMessageOptions.DontRequireReceiver);
				}
			}
			IsClockWise = !IsClockWise;
			NowRotateAngle -= DecayIndex;
		}
		if(!IsRotateOK){
			
			RotateTransform.rotation = 
				Quaternion.RotateTowards(RotateTransform.rotation,ToAngle,Time.deltaTime * RotateIndex);
			
			
			if(Quaternion.Angle(RotateTransform.rotation,ToAngle) < 0.1f){
			//if(tempAngle < 0.0f){
				IsInit = false;
				NowSwinTimes++;
			}
		}
	}
	void Play(){
		IsPlay = true;
		Init();
	}
	void Init(){
		NowRotateAngle = MyRotateAngle;
		IsClockWise = true;
		NowSwinTimes = 0;
		IsRotateOK = false;
		IsInit = false;
	}
	void OnEnable(){
		IsPlay = false;
		//IsRotateOK = false;
		//IsInit = false;
		MyRotateAngle = MyRotateAxis * MyAngle;
		
		//NowRotateAngle = MyRotateAngle;
		DecayIndex = MyRotateAngle / SwinTimes;
		//IsClockWise = true;
		//NowSwinTimes = 0;
		MyTransform = transform;
		OriginalQuaternion = RotateTransform.rotation;
		Init();
	}
	void Reset(){
		RotateTransform.rotation = OriginalQuaternion;
	}
}
