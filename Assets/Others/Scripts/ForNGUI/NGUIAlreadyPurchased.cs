using UnityEngine;
using System.Collections;

public class NGUIAlreadyPurchased : MonoBehaviour {

	public UISprite alreadyPurchased;
	
	void Awake() {
		NGUIProductSceneViewer.OnProductViewChanged += OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene += OnBackToLoginScene;
		ProductController.OnCarPurchased += OnCarPurchased;
		ProductController.OnFailedToPurchase += OnFailedToPurchase;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnProductViewChanged -= OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene -= OnBackToLoginScene;
		ProductController.OnCarPurchased -= OnCarPurchased;
		ProductController.OnFailedToPurchase -= OnFailedToPurchase;
	}
	
	void OnProductViewChanged(CarInsanityProductInfo car) {
		alreadyPurchased.enabled = car.purchased;
	}
	
	void OnCarPurchased(CarInsanityProductInfo car) {
		alreadyPurchased.enabled = car.purchased;
	}
	
	void OnFailedToPurchase(Definition.RPCProcessState resultState) {
		
	}
	
	void OnBackToLoginScene() {
		alreadyPurchased.enabled = false;
	}
}
