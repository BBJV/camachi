using UnityEngine;
using System.Collections;

public class DisableCollider : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
//	public ActionEvent MyEvent;
	public int ActionTag;
	void Action(MyAction action){
		if (ActionTag == action.Tag) {
				BroadcastMessage (SetEnable, false);
		}
	}
}
