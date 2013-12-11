using UnityEngine;
using System.Collections;

public class MeshColliderController : MonoBehaviour {
	
	private ArrayList onRoadObjects = new ArrayList();
	private int count = 0;
	void Use () {
//		if(!onRoadObjects.Contains(onRoadObject))
//		{
//			onRoadObjects.Add(onRoadObject);
//		}
//		collider.enabled = true;
//		renderer.enabled = true;
		
		count += 1;
		collider.isTrigger = false;
	}
	
	void UnUse () {
//		if(new ArrayList(((GameObject[])args["ignoreRoads"])).Contains(gameObject))
//		{
//			return;
//		}
//		
//		if(onRoadObjects.Contains((Transform)args["onRoadObject"]))
//		{
//			onRoadObjects.Remove((Transform)args["onRoadObject"]);
//			onRoadObjects.Remove(null);
//			if(onRoadObjects.Count == 0)
//			{
//				collider.enabled = false;
////				renderer.enabled = false;
//			}
//		}
////		usedCount = Mathf.Clamp(usedCount - 1, 0, usedCount);
////		if(usedCount == 0)
////		{
////			collider.enabled = false;
//////			renderer.enabled = false;
////		}
		
		count = Mathf.Clamp(count - 1, 0, count);
		if(count == 0){
			collider.isTrigger = true;
		}
	}
}
