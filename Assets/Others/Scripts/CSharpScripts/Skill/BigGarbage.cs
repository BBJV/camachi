using UnityEngine;
using System.Collections;

public class BigGarbage : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if(other.transform.root.tag == "car")
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter (Collision collision) {
		if(collision.transform.root.tag == "car")
		{
			CarProperty car = collision.transform.root.GetComponent<CarProperty>();
			if(car)
				car.AddBarrierTime();
			Destroy(gameObject);
		}
	}
}
