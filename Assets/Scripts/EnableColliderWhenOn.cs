using UnityEngine;
using System.Collections;

public class EnableColliderWhenOn : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
	void OnEnable(){
		
		BroadcastMessage(SetEnable, true);
	}
}
