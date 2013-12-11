using UnityEngine;
using System.Collections;

public class AddContainer : MonoBehaviour {
	public GameObject container;
	// Use this for initialization
	void Start () {
		container = Instantiate(container,transform.position,transform.rotation) as GameObject;
		ConfigurableJoint configJoint = container.GetComponent<ConfigurableJoint>();
		if(configJoint)
			configJoint.connectedBody = rigidbody;
		else
		{
			container.hingeJoint.connectedBody = rigidbody;
		}
	}
}
