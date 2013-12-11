using UnityEngine;
using System.Collections;

public class ScaleThing : MonoBehaviour {
	public Transform ScaleTransform;
	private Transform MyTransform;
	//public float MyScaleTime;
	public float MyScale;
	public Vector3 MyScaleVector;
	public float ScaleTime;
	public bool IsRepeat = true;
	
	private float NowScale;
	private float ScaleIndex;
	private bool IsScaleOK;
	private Vector3 OriginalScale;
	//private int NowScaleTimes;
	// Use this for initialization
	void Start () {
		MyTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsScaleOK){
			NowScale += ScaleIndex * Time.deltaTime;
			ScaleTransform.localScale += ScaleIndex * Time.deltaTime * MyScaleVector;
			//NowScaleTimes ++;
			
			if(NowScale > MyScale){
				if(IsRepeat){
					NowScale = MyScale;
					ScaleIndex = -ScaleIndex;
				}else{
					IsScaleOK = true;
					ScaleTransform.localScale = OriginalScale;
				}
			}else if(NowScale < 0.0f){
				if(IsRepeat){
					NowScale = 0.0f;
					ScaleIndex = -ScaleIndex;
				}else{
					IsScaleOK = true;
					ScaleTransform.localScale = OriginalScale;
				}
			}
		}
	}
	
	void OnEnable(){
		//MyScaleTime = 0.0f;
		OriginalScale = ScaleTransform.localScale;
		NowScale = 0.0f;
		IsScaleOK = false;
		ScaleIndex = MyScale / ScaleTime;
		//NowScaleTimes = 0 ;
	}
}
