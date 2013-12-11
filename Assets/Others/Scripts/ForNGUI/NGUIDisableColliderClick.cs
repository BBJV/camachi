using UnityEngine;
using System.Collections;

public class NGUIDisableColliderClick : MonoBehaviour {

	public Collider targetDisableCollider;
	
	void OnClick() {
		targetDisableCollider.enabled  = false;
	}
}
