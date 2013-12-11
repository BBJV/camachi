using UnityEngine;
using System.Collections;

public class RotateTempMeter : MonoBehaviour {
public Vector3 MinAngle;
	public Vector3 MaxAngle;
	public float MaxTemp;
	private static string Rotate = "Rotate";
	void CarTemp (float temp) {
		BroadcastMessage(Rotate,MinAngle + (MaxAngle - MinAngle) * temp / (MaxTemp));
		//print ("Angle = " + (MinAngle + (MaxAngle - MinAngle) * speed / (MaxVelocity)));
	}
}
