using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {
	public Transform target;
	private Vector3 deltaMove = new Vector3(0,0,10);
//	private int x = 0;
	void Update () {
		transform.RotateAround(target.position + deltaMove, Vector3.up, (30 + Random.Range(0, 10)) * Time.deltaTime);
		deltaMove.x = deltaMove.x + Time.deltaTime * 2f;
		transform.LookAt(target);
	}
}
