using UnityEngine;
using System.Collections;

public class NGUIEnableColliderClick : MonoBehaviour {

	public Collider targetDisableCollider;
	
	void OnClick() {
		targetDisableCollider.enabled  = true;
	}
}
