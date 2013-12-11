using UnityEngine;
using System.Collections;

public class PressSound : MonoBehaviour {
	public Transform SoundTransform;
	private static string SoundOn = "SoundOn";
	private static string SoundOff = "SoundOff";
	public bool IsEnableClickOn = false;
	public bool IsEnableClickOff = false;
	// Use this for initialization
	void Start () {
	}
	void ClickedOn(){
		//if all false there will be no action,you must call the play sound or stop sound directly
		if(IsEnableClickOn){
			PlaySound();
		}
	}
	void ClickedOff(){
		//if all false there will be no action,you must call the play sound or stop sound directly
		if(IsEnableClickOff){
			StopSound();
		}else if(IsEnableClickOn){
			PlaySound();
		}
	}
	void Pressed(){
		PlaySound();
	}
	void Released(){
		StopSound();
	}
	void PlaySound(){
		SoundTransform.BroadcastMessage(SoundOn);
	}
	void StopSound(){
		SoundTransform.BroadcastMessage(SoundOff);
	}
}
