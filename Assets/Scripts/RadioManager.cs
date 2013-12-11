using UnityEngine;
using System.Collections;

public class RadioManager : MonoBehaviour {
	public Transform[] SoundTransform;
	public Transform MyPressSoundTransform;
	private bool IsTurnOn = false;
	private int NowSound;
	private PressSound MyPressSound;
	private string PlaySound = "PlaySound";
	private string StopSound = "StopSound";
	// Use this for initialization
	void Start () {
		NowSound = 0;
		MyPressSound = MyPressSoundTransform.GetComponent<PressSound>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ClickedOn(){
		IsTurnOn = true;
	}
	void ClickedOff(){
		IsTurnOn = false;
	}
	void ChangeSound(bool isnext){
		if(IsTurnOn){
			if(isnext){
				NowSound++;
				if(NowSound >= SoundTransform.Length){
					NowSound = 0;
				}
			}else{
				NowSound--;
				if(NowSound < 0){
					NowSound = SoundTransform.Length - 1;
				}
			}
			MyPressSoundTransform.BroadcastMessage(StopSound);
			MyPressSound.SoundTransform = SoundTransform[NowSound];
			MyPressSoundTransform.BroadcastMessage(PlaySound);
		}
	}
}
