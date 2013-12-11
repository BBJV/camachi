using UnityEngine;
using System.Collections;

public class HummerPassiveSkill : MonoBehaviour {

	// Use this for initialization
	void PassThroughStartLine () {
		rigidbody.AddForce(transform.forward * rigidbody.mass * 500);
		transform.root.BroadcastMessage("ShowState",3);
	}
}
