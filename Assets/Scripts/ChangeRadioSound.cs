using UnityEngine;
using System.Collections;

public class ChangeRadioSound : MonoBehaviour {
	public Transform MyRadioManager;
	public bool IsNext;
	private static string ChangeSound = "ChangeSound";
	// Use this for initialization
	void Start () {
	
	}
	
	
	void ClickedOn(){
		MyRadioManager.BroadcastMessage(ChangeSound,IsNext,SendMessageOptions.DontRequireReceiver);
	}
	
}
