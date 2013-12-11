using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

	void Start () {
		PlayGameController.OnLoadSuccess += DestroyObject;
	}
	
	void OnDestroy () {
		PlayGameController.OnLoadSuccess -= DestroyObject;
	}
	
	void DestroyObject () {
		Destroy(gameObject);
	}
}
