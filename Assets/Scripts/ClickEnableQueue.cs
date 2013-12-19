using UnityEngine;
using System.Collections;

public class ClickEnableQueue : MonoBehaviour {
	public static string SetEnableTransform = "SetEnableTransform";
	void ClickedOn(){
		BroadcastMessage(SetEnableTransform);
	}
}
