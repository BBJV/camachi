using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {
	
//	public UISprite alreadyBuy;
//	public UILabel price;
//	public UIButton buyButton;
//	public int productCount = 1;
//	private int currentProduct = 0;
//	private ProductController productController;
	
	
//	void OnDisable()
//	{
//		// Remove all the event handlers
//		ProductController.OnSuccess -= PurchaseSuccessful;
//		ProductController.OnListReceived -= UpdateData;
//
//	}
//	
//	void Start () {
//		// Add event handlers
//		ProductController.OnSuccess += PurchaseSuccessful;
//		ProductController.OnListReceived += UpdateData;
//		
//		
//		productController = FindObjectOfType(typeof(ProductController)) as ProductController;
//		
//	}
//	
//	void UpdateData () {
//		productCount = productController.allCarInsanityProductInfo.Count;
//		BroadcastMessage("ShowCurrentProduct", productController.allCarInsanityProductInfo[currentProduct].ID, SendMessageOptions.DontRequireReceiver);
//	}
//	
//	void TurnLeft () {
//		int previousProduct = currentProduct;
//		currentProduct -= 1;
//		if(currentProduct < 0)
//		{
//			currentProduct = productCount - 1;
//		}
//		Hashtable args = new Hashtable();
//		args.Add("currentProduct", productController.allCarInsanityProductInfo[currentProduct].ID);
//		args.Add("previousProduct", productController.allCarInsanityProductInfo[previousProduct].ID);
//		BroadcastMessage("ProductTurnLeft", args, SendMessageOptions.DontRequireReceiver);
//		ShowCurrentProduct();
//	}
//	
//	void TurnRight () {
//		int previousProduct = currentProduct;
//		currentProduct += 1;
//		if(currentProduct >= productCount)
//		{
//			currentProduct = 0;
//		}
//		Hashtable args = new Hashtable();
//		args.Add("currentProduct", productController.allCarInsanityProductInfo[currentProduct].ID);
//		args.Add("previousProduct", productController.allCarInsanityProductInfo[previousProduct].ID);
//		BroadcastMessage("ProductTurnRight", args, SendMessageOptions.DontRequireReceiver);
//		ShowCurrentProduct();
//	}
//	
//	void ShowCurrentProduct () {
//		alreadyBuy.enabled = productController.allCarInsanityProductInfo[currentProduct].purchased;
//		price.text = productController.allCarInsanityProductInfo[currentProduct].price;
//		buyButton.isEnabled = !productController.allCarInsanityProductInfo[currentProduct].purchased;
////		Debug.Log("currentProduct : " + currentProduct + " productCount :" + productCount);
////		Debug.Log("ID : " + productController.allCarInsanityProductInfo[currentProduct].ID + "\n" + "name : " + productController.allCarInsanityProductInfo[currentProduct].name + "\n" + "price : " + productController.allCarInsanityProductInfo[currentProduct].price + "\n" + "productID : " + productController.allCarInsanityProductInfo[currentProduct].productID + "\n" + "purchased : " + productController.allCarInsanityProductInfo[currentProduct].purchased);
//	}
	
//	public void BuyProduct (int currentProduct) {
//		if(!productController.allCarInsanityProductInfo[currentProduct].purchased)
//		{
//			#if UNITY_IPHONE
//			StoreKitBinding.purchaseProduct(productController.allCarInsanityProductInfo[currentProduct].productID, 1);
//			#endif
//		}
//	}
	
//	void PurchaseSuccessful () {
//		ShowCurrentProduct();
//	}
	
}
