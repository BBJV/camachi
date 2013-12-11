using UnityEngine;
using System.Collections;

public class TranslateScene : MonoBehaviour {
	public Transform TranslatePlane;
	public bool Is2Transparent;
	public float TranslateTime;
	private static string ChangeTransparent = "ChangeTransparent";
	private static string SetTime = "SetTime";
	// Use this for initialization
	void OnEnable () {
		SetTranslateTime();
		Translate();
	}
	void SetTranslateTime(){
		TranslatePlane.BroadcastMessage(SetTime,TranslateTime);
	}
	void Translate(){
		TranslatePlane.BroadcastMessage(ChangeTransparent,Is2Transparent);
	}
}
