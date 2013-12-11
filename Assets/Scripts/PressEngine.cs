using UnityEngine;
using System.Collections;

public class PressEngine : MonoBehaviour {
	public Transform RoadManager;
	private bool IsEngineStart;
	private static string SetEngine = "SetEngine";
	// Use this for initialization
	void Start () {
	
	}
	
	void ClickedOn(){
		RoadManager.BroadcastMessage(SetEngine);
	}
	void OnEnable(){
	}
}
