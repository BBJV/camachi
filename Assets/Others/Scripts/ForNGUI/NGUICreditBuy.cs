using UnityEngine;
using System.Collections;

public class NGUICreditBuy : MonoBehaviour {
	
//	public BoxCollider creditBuyButtonColloder;
	
	public string animationName;
	
	private float animationTime;
	
	private bool isProductLoadedFromStore = false;
	
	void Awake() {
		NGUIProductSceneViewer.OnProductViewChanged += OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene += OnBackToLoginScene;
		ProductController.OnCarPurchased += OnCarPurchased;
		ProductController.OnFailedToPurchase += OnFailedToPurchase;
		ProductController.OnListReceived += OnProductListReceivedFromStore;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnProductViewChanged -= OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene -= OnBackToLoginScene;
		ProductController.OnCarPurchased -= OnCarPurchased;
		ProductController.OnFailedToPurchase -= OnFailedToPurchase;
		ProductController.OnListReceived -= OnProductListReceivedFromStore;
	}
	
	void OnProductListReceivedFromStore() {
		isProductLoadedFromStore = true;
	}
	
	void OnProductViewChanged(CarInsanityProductInfo car) {
		if(!isProductLoadedFromStore) {
			return;
		}
		
		if(!car.purchased) {
			
			animation[animationName].time = animationTime;
			animation[animationName].speed = 1.0f;
			animation.Play();
			animationTime = animation[animationName].length;
		
			
			
		}else{
			animation[animationName].time = animationTime;
			animation[animationName].speed = -1.0f;
			animation.Play();
			animationTime = 0.0f;
			
		}
	}
	
	void OnCarPurchased(CarInsanityProductInfo car) {
		animation[animationName].time = animation[animationName].length;
		animation[animationName].speed = - 1.0f;
		animation.Play();
	}
	
	void OnFailedToPurchase(Definition.RPCProcessState resultState) {
		
	}
	
	void OnBackToLoginScene() {
		animation[animationName].speed = -1.0f;
		animation.Play();
	}
}
