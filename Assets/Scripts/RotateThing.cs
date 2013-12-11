using UnityEngine;
using System.Collections;
/*
 * this class is for a known start rotation to a end rotation,
 * not rotate for an angle
 * */

public class RotateThing : MonoBehaviour {
	public Transform RotateTransform;
	public TriggerType MyTriggerType = TriggerType.OnEnable;
	private Transform MyTransform;
	public float MyRotateTime;
	public Vector3 MyRotateAngle;
	public string FinishBBroadcastMessageName = null;
	private bool IsRotateOK;
	private float RotateIndex;
	private Quaternion ToAngle;
	bool IsInit;
	private Quaternion OriginalQuaternion;
	// Use this for initialization
	void Start () {
		MyTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsInit){
			IsInit = true;
			ToAngle = Quaternion.Euler(MyRotateAngle);
			RotateIndex = Quaternion.Angle(RotateTransform.rotation,ToAngle) / MyRotateTime;
			if(RotateIndex == 0.0f){
				return;
			}
			//print ("Quaternion.Angle(RotateTransform.rotation,ToAngle) = " + Quaternion.Angle(RotateTransform.rotation,ToAngle));
			//print ("RotateIndex = " + RotateIndex);
		}
		if(!IsRotateOK){
			RotateTransform.rotation = 
				Quaternion.RotateTowards(RotateTransform.rotation,ToAngle,Time.deltaTime * RotateIndex);
			//print("RotateTransform.rotation = " + RotateTransform.rotation);
			if(Quaternion.Angle(RotateTransform.rotation,ToAngle) < 0.1f){
				IsRotateOK = true;
				if(FinishBBroadcastMessageName != null){
					BroadcastMessage(FinishBBroadcastMessageName,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	void Rotate(Vector3 rotateAngle){
		IsRotateOK = false;
		IsInit = false;
		OriginalQuaternion = RotateTransform.rotation;
		MyRotateAngle = rotateAngle;
	}
	void OnEnable(){
		IsRotateOK = true;
		IsInit = true;
		if(MyTriggerType == TriggerType.OnEnable){
			Rotate(MyRotateAngle);
		}
	}
	
}
