using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		CarB car = other.GetComponent<CarB>();
		if(car)
		{
//			car.rigidbody.velocity = car.transform.forward * car.rigidbody.velocity.magnitude * 0.5f;
			car.rigidbody.velocity = new Vector3(car.rigidbody.velocity.x, 0, car.rigidbody.velocity.z);
		}
	}
}
