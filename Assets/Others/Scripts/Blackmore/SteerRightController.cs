using UnityEngine;
using System.Collections;

public class SteerRightController : MonoBehaviour {

	public Transform PressEffect;
	public Transform ControllerStick;
	public float AnimReleaseTime = 1.0f;
	public float AnimRotateAngle = 10.0f;
	private Quaternion Original;
	private Quaternion PressTo;
	private float AnimAngleTime;
	private bool IsPressed;
	private float ReleaseTime;
	private Transform PlayerCar;
	// Use this for initialization
	void Start () {
		AnimAngleTime = AnimRotateAngle / AnimReleaseTime;
		Original = ControllerStick.rotation;
		Vector3 temp = ControllerStick.localRotation.eulerAngles;
		temp.Set(temp.x ,temp.y - AnimRotateAngle,temp.z);
		PressTo = Quaternion.Euler(temp);
		if(PressEffect)
			PressEffect.gameObject.SetActiveRecursively(false);
		PlayerCar = Camera.main.GetComponent<SmoothFollow>().target;
	}
	
	// Update is called once per frame
	void Update () {
		if(!PlayerCar){
			PlayerCar = Camera.main.GetComponent<SmoothFollow>().target;
			return;
		}
		if(!IsPressed && ReleaseTime >= 0.0f){
			ReleaseTime -= Time.deltaTime;
			
			if(ReleaseTime > 0.0f){
				ControllerStick.RotateAroundLocal(Vector3.up, Mathf.Deg2Rad * AnimAngleTime * Time.deltaTime);
				
			}else if(ReleaseTime < 0.0f){
				ControllerStick.localRotation = Original;
			}
		}
	}
	void FixedUpdate(){
		if(!PlayerCar)
			return;
		if(IsPressed){
			PlayerCar.BroadcastMessage("SetPressSteer" , 0.5f);
		}
	}
	
	void OnPress(bool ispressed){
		if(!PlayerCar)
			return;
		if(ispressed){
			IsPressed = true;
			ReleaseTime = AnimReleaseTime;
			if(PressEffect)
				PressEffect.gameObject.SetActiveRecursively(true);
			ControllerStick.rotation = PressTo;
			PlayerCar.BroadcastMessage("SetPressSteer" , 0.5f);
			PlayerCar.BroadcastMessage("SetSteerRight",true);
		}else{
			IsPressed = false;
			if(PressEffect)
				PressEffect.gameObject.SetActiveRecursively(false);
			PlayerCar.BroadcastMessage("SetSteerRight",false);
		}
	}
}
