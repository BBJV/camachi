using UnityEngine;
using System.Collections;

public class DynamicMaterial : MonoBehaviour {
	public Texture[] DMArray;
	public int[] DMIndex;
	public float[] DMT;//time between 2 texture,if length > 0,it will ignore ALT
	public float ALT;//the total dynamic time
	private float DeltaTime;
	private float NowTime;
	private int NowIndex;
	
	
	// Use this for initialization
	void Start () {
		NowTime = 0.0f;
		DeltaTime = ALT / DMIndex.Length;
		NowIndex = 0;
		
		//Added by Milk
		renderer.material.SetTexture("_MainTex",DMArray[NowIndex]);
	}
	
//	// Update is called once per frame
	void Update () {
		NowTime += Time.deltaTime;
		if(DMT.Length > 0){
			DeltaTime = DMT[NowIndex];
		}
		if(NowTime >= DeltaTime){
			NowIndex++;
			NowTime = 0.0f;
			if(NowIndex >= DMIndex.Length){
				NowIndex = 0;
			}
			renderer.material.SetTexture("_MainTex",DMArray[DMIndex[NowIndex]]);
		}
	}
	
	public void SetCountdownRender(int NowTime) {
//		Debug.Log("NowTime:"+ NowTime);
		if(NowTime == 0) {
			NowTime = 6; // number 6 for "GO"
		}
		
		renderer.material.SetTexture("_MainTex",DMArray[NowTime]);
	}
	
}

