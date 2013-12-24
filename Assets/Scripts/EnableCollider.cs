using UnityEngine;
using System.Collections;

public class EnableCollider : MonoBehaviour {
	public static string SetEnable = "SetEnableCollider";
	public ActionEvent MyEvent;
	public int StartActionTag;
	public int EndActionTag;
	void OnEnable () {
		if (MyEvent == ActionEvent.WhenOn) {
//				BroadcastMessage (SetEnable, true);
			MyAction tempaction = new MyAction();
			tempaction.Tag = StartActionTag;
			Action(tempaction);
		}
	}
	void Action(MyAction action){
		if(action.Tag == StartActionTag){
			if(EndActionTag >= 0){
				MyAction tempaction = new MyAction();
				tempaction.Tag = EndActionTag;
				BroadcastMessage (MyAction.ActionString, tempaction);
			}
		}
	}
}
