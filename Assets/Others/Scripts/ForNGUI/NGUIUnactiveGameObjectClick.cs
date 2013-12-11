using UnityEngine;
using System.Collections;

public class NGUIUnactiveGameObjectClick : MonoBehaviour {
	
	public GameObject targetGameObject;
	
	void OnClick() {
		targetGameObject.SetActiveRecursively(false);
	}
}
