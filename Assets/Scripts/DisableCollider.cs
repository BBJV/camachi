using UnityEngine;
using System.Collections;

public class DisableCollider : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
	public ActionEvent MyEvent;
	void ClickedOn(){
		if (MyEvent == ActionEvent.WhenClick) {
				BroadcastMessage (SetEnable, false);
		}
	}
}
