using UnityEngine;
using System.Collections;

public class ChangeInState : MonoBehaviour {
	public MyState toInState;
	public int StartActionTag;
	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			transform.root.BroadcastMessage("ChangeState",toInState);
		}
	}
}
