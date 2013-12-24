using UnityEngine;
using System.Collections;

public class DisableCollider : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
//	public ActionEvent MyEvent;
	public ActionEvent MyEvent;
	public int StartActionTag;
	public int EndActionTag = -1;
	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			print("DisableCollider");
			if(EndActionTag >= 0){
				MyAction tempaction = new MyAction();
				tempaction.Tag = EndActionTag;
				BroadcastMessage (MyAction.ActionString, tempaction);
			}
		}
	}
}
