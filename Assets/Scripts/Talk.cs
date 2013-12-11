using UnityEngine;
using System.Collections;

public class Talk : MonoBehaviour {
	private Transform MyTransform;
	// Use this for initialization
	void Start () {
		MyTransform = transform;
	}
	void Play(){
		BroadcastMessage("SoundOn");
	}
	void SoundFinish(){
		MyTransform.parent.BroadcastMessage("ActionFinish",SendMessageOptions.DontRequireReceiver);
	}
}
