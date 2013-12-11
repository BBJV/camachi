using UnityEngine;
using System.Collections;

public class NetworkInit : MonoBehaviour {

	void Start () {
		if(!networkView.viewID.isMine)
		{
			GetComponent<DriftCar>().SetEnableUserInput(false);
		}
	}
}
