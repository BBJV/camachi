using UnityEngine;
using System.Collections;

public class MicrophoneRecord : MonoBehaviour {
	public Mip MyMip;
	void StartRecord(){
		MyMip.StartRecord();
	}
	void StopRecord(){
		MyMip.StopRecord();
	}
}
