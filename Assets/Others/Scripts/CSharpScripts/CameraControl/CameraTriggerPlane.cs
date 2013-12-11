using UnityEngine;
using System.Collections;
//for short the camera clip plane to better render performance
public class CameraTriggerPlane : MonoBehaviour {
	public int FarClipPlane = 100;
	private Camera MainCamera;
	private Transform target;
	// Use this for initialization
	void Start () {
		MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().target;
	}
	
	// Update is called once per frame
	void Update () {
		if(!target){
			target = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollow>().target;
		}
	}
	
	void  OnTriggerEnter(Collider other){
		if(other.transform.Equals(target))
		{
			MainCamera.farClipPlane = FarClipPlane;
		}
	}
	
}