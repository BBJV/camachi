using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {
	public Transform SoundTransform;
	private static string SoundOn = "SoundOn";
	//private static string SoundOff = "SoundOff";
	private bool IsPlayed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsPlayed)
			SoundTransform.BroadcastMessage(SoundOn);
		IsPlayed = true;
	}
	void OnEnable(){
		IsPlayed = false;
	}
}
