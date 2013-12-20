using UnityEngine;
using System.Collections;

public class EnableQueue : MonoBehaviour {
//	public static string SetEnableTransform = "SetEnableTransform";
	public ActionEvent ActionStart;
	public int StartActionTag;
	public int EndActionTag;
	void OnEnable () {
		if(ActionStart == ActionEvent.WhenOn){
			MyAction tempaction = new MyAction();
			tempaction.Tag = StartActionTag;
			Action(tempaction);
		}
	}
	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			MyAction tempaction = new MyAction();
			tempaction.Tag = EndActionTag;
			BroadcastMessage(MyAction.ActionString,tempaction);
		}
	}
}
