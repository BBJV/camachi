using UnityEngine;
using System.Collections;

public class EnableCollider : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
	public ActionEvent MyEvent;
	void OnEnable () {
		if (MyEvent == ActionEvent.WhenOn) {
				BroadcastMessage (SetEnable, true);
		}
	}
}
