using UnityEngine;
using System.Collections;

public class DisableColliderWhenClick : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
	void ClickedOn(){
		BroadcastMessage(SetEnable, false);
	}
}
