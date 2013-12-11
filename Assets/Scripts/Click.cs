using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	public Transform HitTransform;
	public int IgnoreLayer;
	public bool IsSwitch = false;
	public bool DefaultSwitch = false;
	private bool NowSwitch;
	Ray ray;
	RaycastHit hit ;
	private static string ClickedOn = "ClickedOn";
	private static string ClickedOff = "ClickedOff";
	private int IngnoreLayerShift;
	// Use this for initialization
	void Start () {
		IngnoreLayerShift = 1 << IgnoreLayer;
		IngnoreLayerShift = ~IngnoreLayerShift;
		NowSwitch = DefaultSwitch;
	}
	
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
//					GameState.ChangeGameState(NextStateTransform);
		        }
	        }
	    }
		
#endif
	}
}
