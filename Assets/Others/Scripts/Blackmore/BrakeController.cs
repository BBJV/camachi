using UnityEngine;
using System.Collections;

public class BrakeController : MonoBehaviour {
	public Transform PressEffect;
	public Transform Brake;
	public float AnimReleaseTime = 1.0f;
	public float AnimRotateAngle = 30.0f;
	private Quaternion Original;
	private Quaternion PressTo;
	private float AnimAngleTime;
	private bool IsPressed;
	private float ReleaseTime;
	private Transform PlayerCar;
	// Use this for initialization
	IEnumerator Start () {
		AnimAngleTime = AnimRotateAngle / AnimReleaseTime;
		Original = Brake.rotation;
		Vector3 temp = Brake.localRotation.eulerAngles;
		temp.Set(Brake.localRotation.eulerAngles.x - AnimRotateAngle,Brake.localRotation.eulerAngles.y,Brake.localRotation.eulerAngles.z);
		PressTo = Quaternion.Euler(temp);
		if(PressEffect)
			PressEffect.gameObject.SetActiveRecursively(false);
		SmoothFollow smCamera = Camera.main.GetComponent<SmoothFollow>();
		while(!smCamera.target)
		{
			yield return null;
		}
		PlayerCar = smCamera.target;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PlayerCar && !IsPressed && ReleaseTime >= 0.0f){
			ReleaseTime -= Time.deltaTime;
			
			if(ReleaseTime > 0.0f){
				Brake.RotateAroundLocal(Vector3.right,- Mathf.Deg2Rad * AnimAngleTime * Time.deltaTime);
				
			}else if(ReleaseTime < 0.0f){
				Brake.localRotation = Original;
			}
		}
		
	}
//	void FixedUpdate(){
//		if(PlayerCar)
//		{
//			if(IsPressed){
//				PlayerCar.BroadcastMessage("Brake");
//			}else{
//				PlayerCar.BroadcastMessage("UnBrake");
//			}
//		}
//	}
	void OnPress(bool ispressed){
		
		if(PlayerCar)
		{
			if(ispressed){
				IsPressed = true;
				ReleaseTime = AnimReleaseTime;
				if(PressEffect)
					PressEffect.gameObject.SetActiveRecursively(true);
				Brake.rotation = PressTo;
				StartCoroutine(HolaBrake());
			}else{
				IsPressed = false;
				if(PressEffect)
					PressEffect.gameObject.SetActiveRecursively(false);
			}
		}
	}
	
	IEnumerator HolaBrake () {
		while(IsPressed)
		{
			PlayerCar.SendMessage("Brake", SendMessageOptions.DontRequireReceiver);
			yield return null;
		}
		PlayerCar.SendMessage("UnBrake", SendMessageOptions.DontRequireReceiver);
	}
}
