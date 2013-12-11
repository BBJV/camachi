using UnityEngine;
using System.Collections;

public class RotateOilMeter : MonoBehaviour {
public Vector3 MinAngle;
	public Vector3 MaxAngle;
	public float MaxOil;
	private static string Rotate = "Rotate";
	void CarOil (float oil) {
		BroadcastMessage(Rotate,MinAngle + (MaxAngle - MinAngle) * oil / (MaxOil));
		//print ("Angle = " + (MinAngle + (MaxAngle - MinAngle) * speed / (MaxVelocity)));
	}
}
