using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class StoreKitGUIManager : MonoBehaviour
{
#if UNITY_IPHONE
//	private ProductController productController;
	
//	void Start () {
//		productController = FindObjectOfType(typeof(ProductController)) as ProductController;
//	}
	
	void OnGUI()
	{
		GUI.depth = -6;
		float yPos = 10.0f;
		float xPos = 20.0f;
		float width = 210.0f;
		
		if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Get Can Make Payments" ) )
		{
//			bool canMakePayments = StoreKitBinding.canMakePayments();
//			Debug.Log( "StoreKit canMakePayments: " + canMakePayments );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Get Product Data" ) )
		{
			// comma delimited list of product ID's from iTunesConnect.  MUST match exactly what you have there!
//			string productIdentifiers = "com.wintek.babyPic11, com.wintek.babyPic2, com.wintek.girlPic1,com.wintek.girlPic2";
			string productIdentifiers = "com.wintek.anotherProduct, com.wintek.CarInsanity.anotherProduct";
//			string productIdentifiers = productController.GetProductList();
			StoreKitBinding.requestProductData( productIdentifiers );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Restore Completed Transactions" ) )
		{
			StoreKitBinding.restoreCompletedTransactions();
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Validate Receipt" ) )
		{
			// grab the transactions, then just validate the first one
			List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();
			if( transactionList.Count > 0 )
				StoreKitBinding.validateReceipt( transactionList[0].base64EncodedTransactionReceipt, true );
		}
		
		// Second column
		xPos += xPos + width;
		yPos = 10.0f;
		if( GUI.Button( new Rect( xPos, yPos, width, 40 ), "Purchase Product 1" ) )
		{
			StoreKitBinding.purchaseProduct( "testProduct", 1 );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Purchase Product 2" ) )
		{
			StoreKitBinding.purchaseProduct( "anotherProduct", 1 );
		}


		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Purchase Subscription" ) )
		{
			StoreKitBinding.purchaseProduct( "sevenDays", 1 );
		}
		
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Validate Subscription" ) )
		{
			// grab the transactions and if we have a subscription in there validate it
			List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();
			foreach( var t in transactionList )
			{
				if( t.productIdentifier == "sevenDays" )
				{
					StoreKitBinding.validateAutoRenewableReceipt( t.base64EncodedTransactionReceipt, "YOUR_SECRET_FROM_ITC", true );
					break;
				}
			}
		}

		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Get Saved Transactions" ) )
		{
//			List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();
			
			// Print all the transactions to the console
//			Debug.Log( "\ntotal transaction received: " + transactionList.Count );
			
//			foreach( StoreKitTransaction transaction in transactionList )
//				Debug.Log( transaction.ToString() + "\n" );
		}
		
		if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), "Test WWW" ) )
		{
			List<StoreKitTransaction> transactionList = StoreKitBinding.getAllSavedTransactions();
			
			if( transactionList.Count > 0 )
				StartCoroutine(ValidReceipt(transactionList[0].base64EncodedTransactionReceipt));
		}
		// Second column
		xPos += xPos + width;
		yPos = -40.0f;
		
//		string[] products = productController.GetProductList().Split(',');
//		foreach(string product in products)
//		{
//			if( GUI.Button( new Rect( xPos, yPos += 50, width, 40 ), product ) )
//			{
//				StoreKitBinding.purchaseProduct( product, 1 );
//			}
//		}
	}
	
	IEnumerator ValidReceipt (string receipt) {
		string url = "https://sandbox.itunes.apple.com/verifyReceipt";
		string jsonString = "{ \"receipt-data\":\"" + receipt + "\"}";
		System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
//		string jsonString = MiniJSON.Json.Serialize(jsonDic);
		Hashtable headers = new Hashtable();
		headers.Add("Content-Type", "application/x-www-form-urlencoded");
		headers.Add("Content-Length", jsonString.Length);
        WWW www = new WWW(url, encoding.GetBytes(jsonString), headers);
        yield return www;
//        Debug.Log(www.text);
	}
#endif
}
