using UnityEngine;
using System.Collections;

public class TranslateThing : MonoBehaviour {
	public Transform TranslateTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	void Translate (Vector3 trans) {
		TranslateTransform.Translate(trans,Space.World);
	}
}
