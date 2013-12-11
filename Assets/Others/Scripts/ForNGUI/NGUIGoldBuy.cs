using UnityEngine;
using System.Collections;

public class NGUIGoldBuy : MonoBehaviour {
	
//	public BoxCollider goldBuyButtonCollider;
	public string animationName;
	
	private ProductController productController;
	
	private CarInsanityProductInfo currentCar;
	
	private float animationTime = 0.0f;
	
//	void Update() {
//		Debug.Log(animation[animationName].time.ToString());
//	}
	
	void Awake() {
		NGUIProductSceneViewer.OnProductViewChanged += OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene += OnBackToLoginScene;
		ProductController.OnCarPurchased += OnCarPurchased;
		ProductController.OnFailedToPurchase += OnFailedToPurchase;
		
		productController = GameObject.FindObjectOfType(typeof(ProductController)) as ProductController;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnProductViewChanged -= OnProductViewChanged;
		NGUIProductSceneViewer.OnBackToLoginScene -= OnBackToLoginScene;
		ProductController.OnCarPurchased -= OnCarPurchased;
		ProductController.OnFailedToPurchase -= OnFailedToPurchase;
	}
	
	void OnProductViewChanged(CarInsanityProductInfo car) {
//		Debug.Log ("NGUIGoldBuy.OnProductViewChanged:"+car.purchased.ToString());
		
		currentCar = car;
		
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
		animation[animationName].speed = -1.0f;
		animation.Play();
	}
	
	void OnFailedToPurchase(Definition.RPCProcessState resultState) {
		
	}
	
	
	void OnBackToLoginScene() {
		animation[animationName].speed = -1.0f;
		animation.Play();
	}

	void OnClick() {
		GetComponent<BoxCollider>().enabled = false;
		productController.BuyProductWithGold(currentCar.ID);
	}
	
	
}
