using UnityEngine;
using System.Collections;

public class RoadTriggerManager : MonoBehaviour {
	Transform MyTransform;
	int ChildCount;
	int[] CountArray;
	// Use this for initialization
	void Start () {
		MyTransform = transform;
		for(int i = 0 ; i < transform.childCount ; i++)
		{
			//MyTransform.GetChild(i).collider.enabled = false;
			MyTransform.GetChild(i).collider.isTrigger = true;
			MyTransform.GetChild(i).name = ""+i;
		}
		ChildCount = MyTransform.GetChildCount();
		CountArray = new int[ChildCount];
		
	}
	

	public void AddTriggerCount(int index){
		CountArray[index] += 1;
		//MyTransform.GetChild(index).renderer.enabled = true;
		//MyTransform.GetChild(index).collider.enabled = true;
		MyTransform.GetChild(index).collider.isTrigger = false;
	}
	public void MinusTriggerCount(int index){
		CountArray[index] -= 1;
		if(CountArray[index] <= 0){
			//MyTransform.GetChild(index).renderer.enabled = false;
			//MyTransform.GetChild(index).collider.enabled = false;
			MyTransform.GetChild(index).collider.isTrigger = true;
			CountArray[index] = 0;
		}
	}
}
