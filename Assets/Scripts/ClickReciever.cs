using UnityEngine;
using System.Collections;

public class ClickReciever : MonoBehaviour {
	public Transform HitTransform;
	
	public bool IsSwitch = false;
	public bool DefaultSwitch = false;
	private bool NowSwitch;
	
	private static string ClickedOn = "ClickedOn";
	private static string ClickedOff = "ClickedOff";
	
	// Use this for initialization
	void Start () {
		
		NowSwitch = DefaultSwitch;
	}
	void Clicked(Transform hittransform){
		if (hittransform == HitTransform) {
			//if there is switch function
			if(IsSwitch){
				NowSwitch = !NowSwitch;
				if(NowSwitch){
					BroadcastMessage(ClickedOn,SendMessageOptions.DontRequireReceiver);
				}else{
					BroadcastMessage(ClickedOff,SendMessageOptions.DontRequireReceiver);
				}
			}else{
				BroadcastMessage(ClickedOn,SendMessageOptions.DontRequireReceiver);
			}
        }
	}
	/*
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit,Mathf.Infinity,IngnoreLayerShift) && hit.transform == HitTransform) {
				//if there is switch function
				if(IsSwitch){
					NowSwitch = !NowSwitch;
					if(NowSwitch){
						BroadcastMessage(ClickedOn,SendMessageOptions.DontRequireReceiver);
					}else{
						BroadcastMessage(ClickedOff,SendMessageOptions.DontRequireReceiver);
					}
				}else{
					BroadcastMessage(ClickedOn,SendMessageOptions.DontRequireReceiver);
				}
	        }
		}
#else
		foreach (Touch touch in Input.touches) {
	        if (touch.phase == TouchPhase.Began) {
	            ray = Camera.main.ScreenPointToRay(touch.position);
	            if (Physics.Raycast (ray, out hit) && hit.transform == HitTransform) {
					GameState.ChangeGameState(NextStateTransform);
		        }
	        }
	    }
		
#endif
	}
	*/
}
