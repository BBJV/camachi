using UnityEngine;
using System.Collections;

public class PolicePassiveSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Dash dash = GetComponent<Dash>();
		dash.effectTime += 3;
		Destroy(this);
	}
}
