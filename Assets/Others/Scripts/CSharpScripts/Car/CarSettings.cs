using UnityEngine;
using System.Collections;

public class CarSettings : MonoBehaviour {
	public Vector3 dragMultiplier = new Vector3(2, 5, 1);
	public Transform centerOfMass;
	public Transform[] frontWheels;
	public Transform[] rearWheels;
	
	public float[] gearSpeeds;
	public float[] gearTime;
	public float[] gearAcc;
	
	public float ForceFactor = 64.0f;
	public float mass = 1500.0f; 
	public float BrakeForceFactor = 1.0f;
	
	public float DriftXDrag = 0.0f;
}
