using UnityEngine;
using System.Collections;

public class SetTransform : MonoBehaviour {
	public bool IsEnable;
	public Transform MyTrnsform;
	public bool DisableWhenDisable = false;
	public int StartActionTag;
	void Action(MyAction action){
		if (StartActionTag == action.Tag) {
			MyTrnsform.gameObject.SetActive(IsEnable);
		}
	}
	void OnDisable(){
		if(MyTrnsform){
			MyTrnsform.gameObject.SetActive(!DisableWhenDisable);
		}
	}
}
