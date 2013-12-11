//  OnTouchDown.cs
//  Allows "OnMouseDown()" events to work on the iPhone.
//  Attack to the main camera.



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTouchDown : MonoBehaviour {
	private RaycastHit hit;
	private Ray ray;
	
	#if UNITY_IPHONE || UNITY_ANDROID
	void Update () {
        // Code for OnMouseDown in the iPhone and Android. Unquote to test.
		
		hit = new RaycastHit();
		
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began)) {
				
				// Construct a ray from the current touch coordinates
				
				ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				
				if(Physics.Raycast(ray, out hit)) {
                	hit.transform.gameObject.SendMessage("OnMouseDown");
				}
			}
		}
	}
	#endif
}
