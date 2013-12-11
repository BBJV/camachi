using UnityEngine;
using System.Collections;

public class Swipe : MonoBehaviour {
	public int IngnoreLayer;
	private Vector3 BeforeLocation;
	private static Vector3 DefaultLocation = new Vector3( -1.0f , -1.0f , -1.0f);
	private float SwipeHoriIndex;
	private float SwipeVertIndex;
	private int MyIngnoreLayer;
	Ray ray;
	RaycastHit hit ;
	// Use this for initialization
	void Start () {
		if(IngnoreLayer == -1){
			MyIngnoreLayer = 0;
		}else{
			MyIngnoreLayer = 1 << IngnoreLayer;
			MyIngnoreLayer = ~MyIngnoreLayer;
		}
	}
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast (ray, out hit,Mathf.Infinity,MyIngnoreLayer)) {
				BeforeLocation = Input.mousePosition;
				return;
			}
			if(BeforeLocation.x < 0.0f){
				BeforeLocation = Input.mousePosition;
			}
			//print ("Input.mousePosition.x = "+Input.mousePosition.x);
			//print ("BeforeLocation = "+BeforeLocation.x);
			SwipeHoriIndex = Input.mousePosition.x - BeforeLocation.x;
			SwipeVertIndex = Input.mousePosition.y - BeforeLocation.y;
			BroadcastMessage("SwipeHorizontal",SwipeHoriIndex,SendMessageOptions.DontRequireReceiver);
			BroadcastMessage("SwipeVertical",SwipeVertIndex,SendMessageOptions.DontRequireReceiver);
			/*
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit) && hit.transform == HitTransform) {
				GameState.ChangeGameState(NextStateTransform);
	        }
	        */
		}else if(Input.GetMouseButtonUp(0)){
			
			BeforeLocation = DefaultLocation;
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
	void OnEnable(){
		BeforeLocation = DefaultLocation;
	}
}
