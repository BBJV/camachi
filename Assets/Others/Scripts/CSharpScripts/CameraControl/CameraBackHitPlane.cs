using UnityEngine;
using System.Collections;

public class CameraBackHitPlane : MonoBehaviour {

	private Camera MainCamera;
	private Transform target;
	public Transform CameraPosition;
	private bool IsHit;
	// Use this for initialization
	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().target;
		IsHit = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsHit){
			MainCamera.transform.LookAt(target);
		}
	}
	
	void  OnTriggerEnter(Collider other){
		print("OnTriggerEnter");
		if(other.transform.Equals(target))
		{
			MainCamera.transform.position = CameraPosition.position;
			MainCamera.GetComponent<SmoothFollow>().target = null;
			IsHit = true;
			MainCamera.transform.LookAt(target);
			
		}
	}
}
