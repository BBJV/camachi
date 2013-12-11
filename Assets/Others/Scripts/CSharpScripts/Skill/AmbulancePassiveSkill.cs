using UnityEngine;
using System.Collections;

public class AmbulancePassiveSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<CarProperty>().SetWeakenSkillTime(1);
		Destroy(this);
	}
}
