using UnityEngine;
using System.Collections;

public class DeviceChange : MonoBehaviour {
	public Transform[] ManageGameObject;
	// Use this for initialization
	void Start () {
		for(int i = 0 ; i < ManageGameObject.Length ; i++){
			Destroy(ManageGameObject[i].gameObject); 
		}
	}
}
