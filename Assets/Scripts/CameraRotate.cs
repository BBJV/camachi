using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {
	public Transform HorizontalPoint;
	public Vector3 HorizontalAxis;
	public Transform VerticalPoint;
	public Vector3 VerticalAxis;
	
	
	private Camera MainCamera;
	private float HorizontalAngle;
	private float VerticalAngle;
	private float HorizontalAngleIndex;
	private float VerticalAngleIndex;
	// Use this for initialization
	void Start () {
		HorizontalAngleIndex = 1.0f / Screen.width;
		VerticalAngleIndex = 1.0f / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SwipeHorizontal(float swipeindex){
		if(HorizontalPoint){
			HorizontalAngle = HorizontalAngleIndex * swipeindex;
			
			MainCamera.transform.RotateAround(HorizontalPoint.position,HorizontalAxis,HorizontalAngle);
			//MainCamera.transform.LookAt(HorizontalPoint);
		}
	}
	
	void SwipeVertical(float swipeindex){
		if(VerticalPoint){
			VerticalAngle = VerticalAngleIndex * swipeindex;
			
			MainCamera.transform.RotateAround(VerticalPoint.position,VerticalAxis,VerticalAngle);
		}
	}
	
	void OnEnable(){
		MainCamera = Camera.main;
	}
}
