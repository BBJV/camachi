using UnityEngine;
using System.Collections;

public class TruckPassiveSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<CarProperty>().canAbsorbSkill = true;
		Destroy(this);
	}
}
