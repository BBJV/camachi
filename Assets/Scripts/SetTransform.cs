using UnityEngine;
using System.Collections;

public class SetTransform : MonoBehaviour {
	public bool IsEnable;
	public Transform MyTrnsform;
	public bool DisableWhenDisable = false;
	void SetEnableTransform(){
		MyTrnsform.gameObject.SetActive(IsEnable);
	}
	void OnDisable(){
		if(MyTrnsform){
			MyTrnsform.gameObject.SetActive(!DisableWhenDisable);
		}
	}
}
