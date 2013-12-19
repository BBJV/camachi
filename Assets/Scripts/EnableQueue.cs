using UnityEngine;
using System.Collections;

public class EnableQueue : MonoBehaviour {
	public static string SetEnableTransform = "SetEnableTransform";
	public ActionEvent MyEvent;
	void OnEnable () {
		if(MyEvent == ActionEvent.WhenOn){
			BroadcastMessage(SetEnableTransform);
		}
	}
	void MoveEnd(){
		if (MyEvent == ActionEvent.WhenMoveEnd) {
			BroadcastMessage(SetEnableTransform);
		}
	}
}
