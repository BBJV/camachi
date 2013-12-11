using UnityEngine;
using System.Collections;

public class CarShadow : MonoBehaviour {
	private static string RoadCollider = "roadcollider";
	private Transform Car;
	// Use this for initialization
	void Start () {
		Car = transform.parent;
	}
	
	// Update is called once per frame
//	void Update () {
//		RaycastHit hit;
//		
//		if(Physics.Raycast(Car.position, -Car.up, out hit)){
//			
//			if(hit.transform.name.Equals(RoadCollider)){
//				//transform.rotation = Car.rotation;
//				//transform.up = hit.normal;
//				//transform.position.Set(transform.position.x,hit.point.y,transform.position.z);// = hit.point;
//				MyTransform.gameObject.SetActiveRecursively(true);
//				MyTransform.position = hit.point;
//				MyTransform.position += transform.up * 0.05f;
//				//transform.localPosition.Set(transform.localPosition.x,transform.localPosition.y + 1.0f,
//				//	transform.localPosition.z);
//				
//			}
//		}else{
//			MyTransform.gameObject.SetActiveRecursively(false);
//		}
//	}
	
	void OpenShadow (Vector3 hitPoint) {
		if(hitPoint != Vector3.zero)
		{
			transform.renderer.enabled = true;
			transform.position = hitPoint;
			transform.position += transform.up * 0.05f;
		}
		else
		{
			transform.renderer.enabled = false;
		}
	}
}
