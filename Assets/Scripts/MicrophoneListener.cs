using UnityEngine;
using System.Collections;

public class MicrophoneListener : MonoBehaviour {
	public Mip MyMip;
	public float TriggerLoud;
	public string LoudOnBroadcastMessage;
	public string LoudOffBroadcastMessage;
	//public Transform TestTransform;
	private bool IsLitenSomthing;
	private float WaitListenTime;
	// Use this for initialization
	void Start () {
		IsLitenSomthing = false;
	}
	
	// Update is called once per frame
	void Update () {
		//print ("MyMip.GetAveragedVolume() = " + MyMip.GetNowVolume());
		if(MyMip.GetNowVolume() >= TriggerLoud){
			//print ("MyMip.GetAveragedVolume() = " + MyMip.GetNowVolume());
			BroadcastMessage(LoudOnBroadcastMessage);
			//TestTransform.gameObject.SetActive(true);
			IsLitenSomthing = true;
			WaitListenTime = 0.0f;
		}else{
			if(IsLitenSomthing){
				WaitListenTime += Time.deltaTime;
				if(WaitListenTime <= 1.0f){
					return;
				}else{
					WaitListenTime = 0.0f;
				}
			}
			IsLitenSomthing = false;
			BroadcastMessage(LoudOffBroadcastMessage);
			//TestTransform.gameObject.SetActive(false);
		}
		
	}
}
