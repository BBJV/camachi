using UnityEngine;
using System.Collections;

public class RotateSpeedMeter : MonoBehaviour {
	public Vector3 MinAngle;
	public Vector3 MaxAngle;
	public float MaxVelocity;
	private static string Rotate = "Rotate";
	void CarSpeed (float speed) {
		BroadcastMessage(Rotate,MinAngle + (MaxAngle - MinAngle) * speed / (MaxVelocity));
		//print ("Angle = " + (MinAngle + (MaxAngle - MinAngle) * speed / (MaxVelocity)));
	}
}
