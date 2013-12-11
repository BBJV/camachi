using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductController : MonoBehaviour {
	
	// Events and delegates

	public delegate void OnProductListReceivedSuccessEventHandler();
	public delegate void OnFailedPurchaseEventHandler();
	

	public static event OnProductListReceivedSuccessEventHandler OnListReceived;
	public static event OnFailedPurchaseEventHandler OnFailedPurchase;
	
	public delegate void OnTalentOpenedEventHandler(CarInsanityProductInfo car);
	public static event OnTalentOpenedEventHandler OnTalentOpened;
	
	public delegate void OnFailedToOpenTalentEventHandler(Definition.RPCProcessState r);
	public static event OnFailedToOpenTalentEventHandler OnFailedToOpenTalent;
	
	public delegate void OnCarPurchasedEventHandler(CarInsanityProductInfo car);
	public static event OnCarPurchasedEventHandler OnCarPurchased;
	
	public delegate void OnFailedToPurchaseEventHandler(Definition.RPCProcessState r);
	public static event OnFailedToPurchaseEventHandler OnFailedToPurchase;
	
	public delegate void OnProductLoadedFromLobbyEventHandler();
	public static event OnProductLoadedFromLobbyEventHandler OnProductLoadedFromLobby;

	public GameUserPlayer playerInfo;
	
	public MatchController matchController;
	
	
	public List<CarInsanityProductInfo> allCarInsanityProductInfo = new List<CarInsanityProductInfo>();
	
	private int isTest = 0;
	private int _gupid;
	
#if UNITY_IPHONE
	void Start()
	{
//		if(level != (int)Definition.eSceneID.LoginScene)
//		{
//			return;
//		}
		// Listens to all the StoreKit events.  All event listeners MUST be removed before this object is disposed!
		StoreKitManager.purchaseSuccessful += purchaseSuccessful;
//		StoreKitManager.purchaseCancelled += purchaseCancelled;
//		StoreKitManager.purchaseFailed += purchaseFailed;
//		StoreKitManager.receiptValidationFailed += receiptValidationFailed;
//		StoreKitManager.receiptValidationRawResponseReceived += receiptValidationRawResponseReceived;
//		StoreKitManager.receiptValidationSuccessful += receiptValidationSuccessful;
		StoreKitManager.productListReceived += productListReceived;
//		StoreKitManager.productListRequestFailed += productListRequestFailed;
//		StoreKitManager.restoreTransactionsFailed += restoreTransactionsFailed;
//		StoreKitManager.restoreTransactionsFinished += restoreTransactionsFinished;
	}
	
	
	void OnDestroy()
	{
		// Remove all the event handlers
		StoreKitManager.purchaseSuccessful -= purchaseSuccessful;
//		StoreKitManager.purchaseCancelled -= purchaseCancelled;
//		StoreKitManager.purchaseFailed -= purchaseFailed;
//		StoreKitManager.receiptValidationFailed -= receiptValidationFailed;
//		StoreKitManager.receiptValidationRawResponseReceived -= receiptValidationRawResponseReceived;
//		StoreKitManager.receiptValidationSuccessful -= receiptValidationSuccessful;
		StoreKitManager.productListReceived -= productListReceived;
//		StoreKitManager.productListRequestFailed -= productListRequestFailed;
//		StoreKitManager.restoreTransactionsFailed -= restoreTransactionsFailed;
//		StoreKitManager.restoreTransactionsFinished -= restoreTransactionsFinished;
	}
	
	void productListReceived( List<StoreKitProduct> productList )
	{
//		Debug.Log("Get Product List Received");
		foreach(CarInsanityProductInfo productInfo in allCarInsanityProductInfo)
		{
			foreach( StoreKitProduct product in productList )
			{
				if( productInfo.productID == product.productIdentifier)
				{
					productInfo.price = product.currencySymbol + " " + product.price;
					productInfo.description = product.description;
					break;
				}
			}
		}
		if(OnListReceived != null)
		{
			OnListReceived();
		}
	}
	
	void purchaseSuccessful( string productIdentifier, string receipt, int quantity )
	{
		networkView.RPC("SendToGameLobby_BuyProduct", RPCMode.Server, _gupid, productIdentifier, receipt, isTest);
	}
	
#endif
	
	
	
	void LoadProductList (int gupid) {
		_gupid = gupid;
//		Debug.Log("SendToGameLobby_LoadProductList");
		networkView.RPC("SendToGameLobby_LoadProductList", RPCMode.Server, gupid);
	}
	
	string GetProductList () {
		string result = "";
		foreach(CarInsanityProductInfo product in allCarInsanityProductInfo)
		{
			result = result + product.productID + ",";
		}
		result = result.TrimEnd(',');
		return result;
	}
	
	void GetProductData () {
		#if UNITY_IPHONE
		StoreKitBinding.requestProductData(GetProductList());
		#endif
	}
	
	public void BuyProduct (int carID) {
		if(!allCarInsanityProductInfo[carID-1].purchased)
		{
			#if UNITY_IPHONE
			StoreKitBinding.purchaseProduct(allCarInsanityProductInfo[carID].productID, 1);
			#endif
			
		}
	}
	
	public void BuyProductWithGold(int carID) {
		Debug.Log("c:"+allCarInsanityProductInfo.Count.ToString()+", carID:"+carID);
		if(!allCarInsanityProductInfo[carID-1].purchased) {
			networkView.RPC("SendToGameLobby_BuyProductWithGold", RPCMode.Server, playerInfo.GUPID, carID);
		}
	}
	
	public void OpenTalent(int carID) {
		networkView.RPC("SendToGameLobby_OpenTalent", RPCMode.Server, this.playerInfo.GUPID, carID);
	}
	
	[RPC]
	public void SendToGameLobby_LoadProductList(int gupid) {
		
	}
	
	[RPC]
	public void SendToGameLobby_BuyProduct(int gupid, string productID, string receipt, int isTest) {
		
	}
	
	[RPC]
	public void SendToGameLobby_BuyProductWithGold(int gupid, int carID) {
		
	}
	
	[RPC] //Define for game lobby's RPC
	public void SendToGameLobby_OpenTalent(int gupid, int carID) {
		
	}
	
	[RPC]
	public void ReceiveByClientPortal_LoadProductList(int gupid, string productList, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			
			string[] productListArray = productList.Split(',');
			allCarInsanityProductInfo.Clear();

			foreach(string productInfo in productListArray) {

				if(productInfo != ""){
					string[] productInfoArray = productInfo.Split(':');

					CarInsanityProductInfo product = new CarInsanityProductInfo();
					product.ID = System.Convert.ToInt32(productInfoArray[0]); //ID
					product.productID = productInfoArray[1]; //productID
					product.name = productInfoArray[2]; //name
					product.talentPrice = productInfoArray[3];
					product.goldPrice = productInfoArray[4];
					product.purchased = System.Convert.ToBoolean(productInfoArray[5]); //purchased
					product.isTalentOpened = System.Convert.ToBoolean(productInfoArray[6]); //isTalentOpened
					
					allCarInsanityProductInfo.Add(product);
//					Debug.Log("ID:"+product.ID+", productID:" +product.productID+ ", name:"+product.name+", TPrice:"+product.talentPrice+", GPrice:"+product.goldPrice+", purchased:"+product.purchased.ToString()+", talentOpen:"+product.isTalentOpened.ToString());
				}
			}
			
			if(OnProductLoadedFromLobby != null) {
				OnProductLoadedFromLobby();
			}
			
			
			GetProductData();
			
			
			
			
		}else if(resultState == Definition.RPCProcessState.UNAVAILABLE){
//			Debug.Log("UNAVAILABLE");
		}else if(resultState == Definition.RPCProcessState.PLAYERNOTEXIST){
//			Debug.Log("USERNOTEXIST");
		}else{
//			Debug.Log(p);
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_BuyProduct(int gupid, int carID, int p) {
		Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
		
		if(resultState == Definition.RPCProcessState.SUCCESS) {
			//reload product list
			networkView.RPC("SendToGameLobby_LoadProductList", RPCMode.Server, gupid);
			
			//reload Owned Car List
			matchController.SendMessage("LoadCarListStart", SendMessageOptions.DontRequireReceiver);
			
			allCarInsanityProductInfo[carID-1].purchased = true;
			
			if(OnCarPurchased != null) {
				OnCarPurchased(allCarInsanityProductInfo[carID-1]);
			}
		
		}else{
			if(OnFailedToPurchase != null) {
				OnFailedToPurchase(resultState);
			}
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_OpenTalent(int gupid, int carID, int p,  NetworkMessageInfo msgInfo) {
		if(this.playerInfo.GUPID == gupid) {
			Definition.RPCProcessState resultState = (Definition.RPCProcessState)p;
			
			if(resultState == Definition.RPCProcessState.SUCCESS) {

				allCarInsanityProductInfo[carID-1].isTalentOpened = true;

				OnTalentOpened(allCarInsanityProductInfo[carID-1]);
				
				
			}else {
				OnFailedToOpenTalent(resultState);
			}
		}else{
			Debug.Log("ReceiveByClientPortal_OpenTalent : wrong gupid");
		}
	}
}
