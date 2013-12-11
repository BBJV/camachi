using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIProductSceneViewer : MonoBehaviour {
	
	public delegate void OnProductViewChangedEventHandler(CarInsanityProductInfo car);
	public static event OnProductViewChangedEventHandler OnProductViewChanged;
	
	public delegate void OnTalentInfoOpenedEventHandler(CarInsanityProductInfo car);
	public static event OnTalentInfoOpenedEventHandler OnTalentInfoOpened;
	
	public delegate void OnBackToLoginSceneEventHandler();
	public static event OnBackToLoginSceneEventHandler OnBackToLoginScene;
	
	public GameObject buyTalentInfoBoard;
	
	
	public GameObject okMessageBoard;
	public UISprite OKMessageSprite;
	
	private int productCount = 1;
	
	private int currentProduct = 0;
	private int previousProduct = 0;
	public ProductController productController;
	
	private Dictionary<int, int> productSelectionDic = new Dictionary<int, int>();
	
	void Awake() {
		productSelectionDic.Add(0, 2);
		productSelectionDic.Add(1, 0);
		productSelectionDic.Add(2, 4);
		productSelectionDic.Add(3, 1);
		productSelectionDic.Add(4, 3);
		productSelectionDic.Add(5, 6);
		productSelectionDic.Add(6, 8);
		productSelectionDic.Add(7, 7);
		productSelectionDic.Add(8, 5);
		
		// Add event handlers
//		ProductController.OnSuccess += PurchaseSuccessful;
//		ProductController.OnProductLoaded += OnProductLoaded;
		ProductController.OnCarPurchased += OnCarPurchased;
		ProductController.OnFailedToPurchase += OnFailedToPurchase;
		ProductController.OnTalentOpened += OnTalentOpened;
		ProductController.OnFailedToOpenTalent += OnFailedToOpenTalent;
		
		
		productController = FindObjectOfType(typeof(ProductController)) as ProductController;
	}
	
	void OnDisable()
	{
		// Remove all the event handlers
//		ProductController.OnSuccess -= PurchaseSuccessful;
//		ProductController.OnProductLoaded -= OnProductLoaded;
		ProductController.OnCarPurchased -= OnCarPurchased;
		ProductController.OnFailedToPurchase -= OnFailedToPurchase;
		ProductController.OnTalentOpened -= OnTalentOpened;
		ProductController.OnFailedToOpenTalent -= OnFailedToOpenTalent;
	
	}
	
	public void PleaseLoginSceneSetting() {
		OKMessageSprite.spriteName = "pleaseLogin";
		OKMessageSprite.MakePixelPerfect();
		okMessageBoard.SetActiveRecursively(true);
	}
	
	public void OpenTalentInfoBoard() {
		if(!buyTalentInfoBoard.active) {
			buyTalentInfoBoard.SetActiveRecursively(true);
//			Debug.Log("OpenTalentInfoBoard.isTalentOpened:"+productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]].isTalentOpened.ToString());
			OnTalentInfoOpened(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
		}else{
			buyTalentInfoBoard.SetActiveRecursively(false);
		}
		
		
	}
	
//	void OnProductLoaded() {
////		Debug.Log("OnProductLoaded");
//		if(OnProductViewChanged != null) {
//			OnProductViewChanged(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
//		}
//	}
	
	void GoToGarageScene() {
//		currentProduct = 0;
		if(OnProductViewChanged != null) {
			OnProductViewChanged(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
		}
	}
	
	void TurnLeft () {
		int previousProduct = currentProduct;
		currentProduct += 1;
		if(currentProduct >= productController.allCarInsanityProductInfo.Count)
		{
			currentProduct = 0;
		}
//		Debug.Log("TurnRight:"+currentProduct.ToString());
//		Debug.Log ("productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]].name:"+productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]].name);
		
		
		if(OnProductViewChanged != null) {
			OnProductViewChanged(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
		}
		
	}
	
	void TurnRight () {
		
		previousProduct = currentProduct;
		currentProduct -= 1;
		if(currentProduct < 0)
		{
			currentProduct = productController.allCarInsanityProductInfo.Count - 1;
		}
		
//		Debug.Log("TurnLeft:"+currentProduct.ToString());
//		Debug.Log ("productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]].name:"+productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]].name);
		
		if(OnProductViewChanged != null) {
			OnProductViewChanged(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
		}
		
	}
	
	void BackToLoginScene() {
		
//		Debug.Log("BackToLoginScene");
		currentProduct = 0;
		
		OnBackToLoginScene();
	}
	
	
	
	public void BuyProduct () {
		productController.BuyProduct(productSelectionDic[currentProduct]);
	}
	
	public void BuyProductWithGold() {
		productController.BuyProductWithGold(productSelectionDic[currentProduct]);
	}
	
	
//	void PurchaseSuccessful () {
//		if(OnProductViewChanged != null) {
//			OnProductViewChanged(productController.allCarInsanityProductInfo[productSelectionDic[currentProduct]]);
//		}
//	}
	
	void OnCarPurchased(CarInsanityProductInfo car) {
		OKMessageSprite.spriteName = "purchasedSuccess";
		OKMessageSprite.MakePixelPerfect();
		okMessageBoard.SetActiveRecursively(true);
	}
	
	void OnFailedToPurchase(Definition.RPCProcessState r) {
		if(r == Definition.RPCProcessState.NOTENOUGHSTAR) {
			OKMessageSprite.spriteName = "starIsNotEnough";
			OKMessageSprite.MakePixelPerfect();
			okMessageBoard.SetActiveRecursively(true);
		}else{
			OKMessageSprite.spriteName = "loadplayerdataerror";
			OKMessageSprite.MakePixelPerfect();
			okMessageBoard.SetActiveRecursively(true);
		}
	}
	
	void OnTalentOpened(CarInsanityProductInfo car) {
		OKMessageSprite.spriteName = "purchasedSuccess";
		OKMessageSprite.MakePixelPerfect();
		okMessageBoard.SetActiveRecursively(true);
	}
	
	void OnFailedToOpenTalent(Definition.RPCProcessState r) {
		if(r == Definition.RPCProcessState.NOTENOUGHSTAR) {
			OKMessageSprite.spriteName = "starIsNotEnough";
			OKMessageSprite.MakePixelPerfect();
			okMessageBoard.SetActiveRecursively(true);
		}else{
			OKMessageSprite.spriteName = "loadplayerdataerror";
			OKMessageSprite.MakePixelPerfect();
			okMessageBoard.SetActiveRecursively(true);
		}
	}
}
