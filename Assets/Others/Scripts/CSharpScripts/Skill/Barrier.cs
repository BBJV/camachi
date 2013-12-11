using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
	void OnTriggerEnter (Collider other) {
		if(gameObject.active)
		{
			if(other.transform.root.tag == "car" && other.transform.root != transform.root)
			{
				//need to change to sendmassage type
				other.transform.root.rigidbody.AddTorque(other.transform.up * 80000 * other.rigidbody.mass);
				transform.parent.gameObject.SetActiveRecursively(false);
			}
		}
	}
	
	void OnEnable () {
		GetComponent<BoxCollider>().enabled = true;
	}
	
	void OnDisable () {
		GetComponent<BoxCollider>().enabled = false;
	}
}
