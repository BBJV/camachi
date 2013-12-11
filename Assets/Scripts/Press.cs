using UnityEngine;
using System.Collections;

public class Press : MonoBehaviour {
	public Transform HitTransform;
	public int IgnoreLayer;
	Ray ray;
	RaycastHit hit ;
	private static string Pressed = "Pressed";
	private static string Released = "Released";
	private int IngnoreLayerShift;
	// Use this for initialization
	void Start () {
	IngnoreLayerShift = 1 << IgnoreLayer;
	IngnoreLayerShift = ~IngnoreLayerShift;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit,Mathf.Infinity,IngnoreLayerShift) && hit.transform == HitTransform) {
				BroadcastMessage(Pressed,SendMessageOptions.DontRequireReceiver);
				
	        }
		}else if(Input.GetMouseButtonUp(0)){
			BroadcastMessage(Released,SendMessageOptions.DontRequireReceiver);
		}
#else
		/*
		foreach (Touch touch in Input.touches) {
	        if (touch.phase == TouchPhase.Began) {
	            ray = Camera.main.ScreenPointToRay(touch.position);
	            if (Physics.Raycast (ray, out hit) && hit.transform == HitTransform) {
					GameState.ChangeGameState(NextStateTransform);
		        }
	        }
	    }
		*/
#endif
	}
}
