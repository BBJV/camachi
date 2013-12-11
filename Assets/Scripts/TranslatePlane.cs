using UnityEngine;
using System.Collections;

public class TranslatePlane : MonoBehaviour {
	private Color OriginalColor;
	private bool IsChange;
	private bool IsChangeToTransparent;
	private Color TempColor;
	private Transform MyTransform;
	private float ColorIndex;
	private float ChangeTime;
	private Camera MainCamera;
	private float OriginalFieldOfView;
	// Use this for initialization
	void Start () {
		MyTransform = transform;
		TempColor = MyTransform.renderer.material.color;
		IsChange = false;
		TempColor = new Color(0.0f,0.0f,0.0f,1.0f);
		MainCamera = Camera.main;
		OriginalFieldOfView = MainCamera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsChange){
			if(IsChangeToTransparent){
				MyTransform.renderer.material.color = MyTransform.renderer.material.color - TempColor * ColorIndex * Time.deltaTime;
				MainCamera.fieldOfView = OriginalFieldOfView;
				if(MyTransform.renderer.material.color.a <= 0){
					MyTransform.renderer.material.color += -1.0f * TempColor * MyTransform.renderer.material.color.a ;
					IsChange = false;
				}
			}else{
				MyTransform.renderer.material.color = MyTransform.renderer.material.color + TempColor * ColorIndex * Time.deltaTime;
				//print ("MyTransform.renderer.material.color = " + MyTransform.renderer.material.color);
				//MainCamera.fieldOfView = 1.0f;
				if(MyTransform.renderer.material.color.a >= 1.0f){
					MainCamera.fieldOfView = 0.015f;
					MyTransform.renderer.material.color = 
						MyTransform.renderer.material.color -= (TempColor * MyTransform.renderer.material.color.a - TempColor);
					IsChange = false;
				}
			}
		}
	}
	//must set time first ,then change transparent
	void SetTime(float time){
		ChangeTime = time;
		ColorIndex = 1.0f / ChangeTime;
	}
	void ChangeTransparent(bool isc2t){
		IsChange = true;
		IsChangeToTransparent = isc2t;
	}
}
