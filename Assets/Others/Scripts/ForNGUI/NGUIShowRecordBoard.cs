using UnityEngine;
using System.Collections;

public class NGUIShowRecordBoard : MonoBehaviour {
	
	public GameObject woods;
	
	void Awake() {
		PlayGameController.OnShowRecord += OnShowRecord;
	}
	
	void OnDestroy() {
		PlayGameController.OnShowRecord -= OnShowRecord;
	}
	
	void OnShowRecord() {
		
		if(!animation.enabled) {
			woods.SetActiveRecursively(true);
			animation.enabled = true;
			animation.Play();
		}
	}
}
