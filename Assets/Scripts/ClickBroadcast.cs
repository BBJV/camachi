using UnityEngine;
using System.Collections;

public class ClickBroadcast : MonoBehaviour {
	public int IgnoreLayer;
	
	Ray ray;
	RaycastHit hit ;
	private static string Clicked = "Clicked";
	private int IngnoreLayerShift;
	private Transform RootTransform;
	// Use this for initialization
	void Start () {
		IngnoreLayerShift = 1 << IgnoreLayer;
		IngnoreLayerShift = ~IngnoreLayerShift;
		RootTransform = transform.root;
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit,Mathf.Infinity,IngnoreLayerShift)) {
				//if there is switch function
				RootTransform.BroadcastMessage(Clicked,hit.transform,SendMessageOptions.DontRequireReceiver);
	        }
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
