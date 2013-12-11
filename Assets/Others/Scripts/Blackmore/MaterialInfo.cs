using UnityEngine;
using System.Collections;

public class MaterialInfo : MonoBehaviour {
	public Color Info;
	
	void  OnTriggerEnter (Collider other) {
		other.transform.root.BroadcastMessage("MyChangeColor", Info, SendMessageOptions.DontRequireReceiver);
	}
	
	void  OnTriggerExit (Collider other) {
		other.transform.root.BroadcastMessage("ResetColor", SendMessageOptions.DontRequireReceiver);
	}
}
