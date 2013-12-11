using UnityEngine;
using System.Collections;

public class BumperCar : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(transform.root.name == "policeCar")
//		Debug.Log(other.transform.name);
		transform.root.rigidbody.velocity = -transform.root.rigidbody.velocity;
	}
}
