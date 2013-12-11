using UnityEngine;
using System.Collections;

public class RoadTrigger : MonoBehaviour {
	private static string Road = "road";
	private static string Wall = "wall";
	private static string AddTriggerCount = "AddTriggerCount";
	private static string MinusTriggerCount = "MinusTriggerCount";
	
	void OnTriggerEnter(Collider other){
//		Transform temp = other.transform.parent;
//		print("temp = "+temp.name);
//		if(temp && (temp.name.Equals(Road) || temp.name.Equals(Wall))){
//			other.transform.parent.SendMessage(AddTriggerCount,int.Parse(other.name.Replace("Object", "")), SendMessageOptions.DontRequireReceiver);
//		}
		other.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
	}
	void OnTriggerExit(Collider other){
//		Transform temp = other.transform.parent;
//		if(temp && (temp.name.Equals(Road) || temp.name.Equals(Wall))){
//			other.transform.parent.SendMessage(MinusTriggerCount,int.Parse(other.name.Replace("Object", "")), SendMessageOptions.DontRequireReceiver);
//		}
		other.SendMessage("UnUse", SendMessageOptions.DontRequireReceiver);
	}
}
