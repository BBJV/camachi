using UnityEngine;
using System.Collections;

public class ClickChangeGameState : MonoBehaviour {
	//public Transform HitTransform;
	public Transform NextStateTransform;
	//Ray ray;
	//RaycastHit hit ;
	// Use this for initialization
	void Start () {
	
	}
	void ClickedOn(){
		GameState.ChangeGameState(NextStateTransform);
	}
	/*
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		 if (Input.GetButtonDown("Fire1")) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit) && hit.transform == HitTransform) {
				GameState.ChangeGameState(NextStateTransform);
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
