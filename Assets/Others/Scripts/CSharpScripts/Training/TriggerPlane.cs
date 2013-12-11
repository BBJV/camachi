using UnityEngine;
using System.Collections;

public class TriggerPlane : MonoBehaviour {
	public int Num;
	private int NowHit;
	// Use this for initialization
	void Start () {
		NowHit = -1;
	}
	
	void  OnTriggerEnter(Collider other){
		NowHit = Num;
	}
	
	public int GetTrig(){
		return NowHit;
	}
	
	public void ResetTrig(){
		NowHit = -1;
	}
}
