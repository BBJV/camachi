using UnityEngine;
using System.Collections;

public class SetCollider : MonoBehaviour {
	public Transform MyCollider;
	
	void SetEnableCollider(bool enable){
		MyCollider.GetComponent<Collider>().enabled = enable;
	}
}
