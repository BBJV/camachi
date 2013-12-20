using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
	public Transform SoundTransform;
	private static string SoundOn = "SoundOn";
	//private static string SoundOff = "SoundOff";
	private bool IsPlayed;
	public ActionEvent ActionStart;
	public int StartActionTag;
//	public int EndActionTag;
	// Use this for initialization
	void Start () {
	
	}

	void OnEnable(){
		if(ActionStart == ActionEvent.WhenOn){
			MyAction tempaction = new MyAction();
			tempaction.Tag = StartActionTag;
			Action(tempaction);
		}
	}
	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			SoundTransform.BroadcastMessage(SoundOn);
		}
	}
}
