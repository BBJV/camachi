using UnityEngine;
using System.Collections;

public class ParticleEmitSetting : MonoBehaviour {
	public int emitTime = 1;
	// Use this for initialization
	void Start () {
		GetComponent<ParticleEmitter>().Emit(emitTime);
	}
}
