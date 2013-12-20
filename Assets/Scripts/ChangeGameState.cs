using UnityEngine;
using System.Collections;

public class ChangeGameState : MonoBehaviour {
	//public Transform HitTransform;
	public Transform NextStateTransform;
	public int StartTag;
	//Ray ray;
	//RaycastHit hit ;
	// Use this for initialization
	void Action(MyAction action){
		if (StartTag == action.Tag) {
			GameState.ChangeGameState(NextStateTransform);
		}
	}
//	void MoveEnd(){
//		if (Start == Event.WhenMoveEnd) {
//			GameState.ChangeGameState(NextStateTransform);
//		}
//	}
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
