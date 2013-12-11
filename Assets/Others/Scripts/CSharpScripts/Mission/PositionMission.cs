using UnityEngine;
using System.Collections;

public class PositionMission : MonoBehaviour {
	public bool inNode = false;
	void OnTriggerEnter (Collider other) {
		if(other.tag == "position")
			inNode = true;
	}
	
	void OnTriggerExit (Collider other) {
		if(other.tag == "position")
			inNode = false;
	}
}
