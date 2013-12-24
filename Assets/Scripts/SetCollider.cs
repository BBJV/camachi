using UnityEngine;
using System.Collections;

public class SetCollider : MonoBehaviour {
	public bool isEnableCollider = false;
	public Transform MyCollider;
	public ActionEvent MyEvent;
	public int StartActionTag;
	public int EndActionTag = -1;
	void Action(MyAction action){
		if (action.Tag == StartActionTag) {
			print("SetCollider = " + MyCollider.name);
			MyCollider.GetComponent<Collider> ().enabled = isEnableCollider;
			if(EndActionTag >= 0){
				MyAction tempaction = new MyAction();
				tempaction.Tag = EndActionTag;
				BroadcastMessage (MyAction.ActionString, tempaction);
			}
		}
	}
	void OnEnable () {
		if (MyEvent == ActionEvent.WhenOn) {
			//				BroadcastMessage (SetEnable, true);
			MyAction tempaction = new MyAction();
			tempaction.Tag = StartActionTag;
			Action(tempaction);
		}
	}
}
