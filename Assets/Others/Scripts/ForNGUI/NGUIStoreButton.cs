using UnityEngine;
using System.Collections;

public class NGUIStoreButton : MonoBehaviour {
	
	public GameObject loginedStoreButton;
	
	void Awake() {
		ProductController.OnProductLoadedFromLobby += OnProductLoadedFromLobby;
		loginedStoreButton.SetActiveRecursively(false);
	}
	
	void OnDestroy() {
		ProductController.OnProductLoadedFromLobby -= OnProductLoadedFromLobby;
	}
	
	void OnProductLoadedFromLobby() {
		loginedStoreButton.SetActiveRecursively(true);
	}
}
