using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
	public ActionEvent ActionStart;
	private Camera MainCamera;
//	public Transform CameraPosition;
	private Transform MainCameraTransform;
	private float RotateIndex;
	public float AnimationTime = 1.5f;
	public Transform RotateTowardTransform;
	public string CallWhenFinish = "RotateEnd";
	private bool isStart = false;
	// Use this for initialization
	void Start () {
		MainCamera = Camera.main;
		MainCameraTransform = MainCamera.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isStart){
			return;
		}

		Quaternion.RotateTowards(MainCameraTransform.rotation,RotateTowardTransform.rotation,RotateIndex * Time.deltaTime);
	}

	void OnEnable () {
		print ("OnEnable here");
		if (ActionStart == ActionEvent.WhenOn) {
			isStart = true;
			RotateIndex = Quaternion.Angle(MainCameraTransform.rotation,RotateTowardTransform.rotation) / AnimationTime;
		}
	}
	void OnDisable () {
		print ("OnDisable here");
		if (ActionStart == ActionEvent.WhenOn) {
			isStart = false;
		}
	}
}
