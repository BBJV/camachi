using UnityEngine;
using System.Collections;

public class NGUIDestroyOnClick : MonoBehaviour {
	
	public MonoBehaviour component;
	
	void OnClick() {
		Destroy(component);
		Destroy(this);
		
	}
}
