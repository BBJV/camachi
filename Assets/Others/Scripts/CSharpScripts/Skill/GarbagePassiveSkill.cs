using UnityEngine;
using System.Collections;

public class GarbagePassiveSkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Obstacle>().obstacleScale = 2.0f;
		Destroy(this);
	}
}
