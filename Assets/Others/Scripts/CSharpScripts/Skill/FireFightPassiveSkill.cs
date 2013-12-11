using UnityEngine;
using System.Collections;

public class FireFightPassiveSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<CarProperty>().SetWeakenSlowSkillPercent(0.7f);
		Destroy(this);
	}
}
