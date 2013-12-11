using UnityEngine;
using System.Collections;

public class StoreProduct : MonoBehaviour {
	
	public int productID;
	public GameObject product;
	
	void ProductTurnLeft (Hashtable args) {
//		Debug.Log ((productID == (int)args["previousProduct"] || productID == (int)args["currentProduct"]).ToString());
		product.SetActiveRecursively(productID == (int)args["previousProduct"] || productID == (int)args["currentProduct"]);
		if(productID == (int)args["currentProduct"])
		{
			animation.Play("car_1_R_store");
		}
		else if(productID == (int)args["previousProduct"])
		{
			animation.Play("car_1_L_store");
		}
	}
	
	void ProductTurnRight (Hashtable args) {
//		Debug.Log ((productID == (int)args["previousProduct"] || productID == (int)args["currentProduct"]).ToString());
		product.SetActiveRecursively(productID == (int)args["previousProduct"] || productID == (int)args["currentProduct"]);
		if(productID == (int)args["currentProduct"])
		{
			animation.Play("car_1_L_store_back");
		}
		else if(productID == (int)args["previousProduct"])
		{
			animation.Play("car_1_R_store_back");
		}
	}
	
	void ShowCurrentProduct (int ID) {
		print("ID = "+ID);
		print("productID = "+productID);
		product.SetActiveRecursively(productID == ID);
		print("test = "+productID);
	}
}
