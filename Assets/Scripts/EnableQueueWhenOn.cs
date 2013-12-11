using UnityEngine;
using System.Collections;

public class EnableQueueWhenOn : MonoBehaviour {
public static string SetEnableTransform = "SetEnableTransform";
	void OnEnable () {
		BroadcastMessage(SetEnableTransform);
	}
}
