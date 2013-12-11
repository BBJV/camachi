using UnityEngine;
using System.Collections;

public class NGUITalentOpenSummit : MonoBehaviour {

	public CarInsanityProductInfo currentCar;
	
	public ProductController productController;
	
//	public BoxCollider boxCollider;
	public GameObject talentOpenSummitButton;
	public BoxCollider talentOpenSummitButtonBoxCollider;
	
	void Awake() {
		NGUIProductSceneViewer.OnTalentInfoOpened += OnTalentInfoOpened;
		ProductController.OnTalentOpened += OnTalentOpened;
		ProductController.OnCarPurchased += OnCarPurchased;
		ProductController.OnFailedToPurchase += OnFailedToPurchase;
		
		productController = GameObject.FindObjectOfType(typeof(ProductController)) as ProductController;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnTalentInfoOpened -= OnTalentInfoOpened;
		ProductController.OnTalentOpened -= OnTalentOpened;
		ProductController.OnCarPurchased -= OnCarPurchased;
		ProductController.OnFailedToPurchase -= OnFailedToPurchase;
	}
	
	void OnTalentInfoOpened(CarInsanityProductInfo car) {
		currentCar = car;
//		Debug.Log("NGUITalentOpenSummit.OnTalentInfoOpened.isTalentOpened:"+currentCar.isTalentOpened.ToString());
		
		
		if(!currentCar.purchased) {
			talentOpenSummitButton.SetActiveRecursively(true);
			talentOpenSummitButtonBoxCollider.enabled = false;
		}else{
			if(currentCar.isTalentOpened) {
				talentOpenSummitButton.SetActiveRecursively(false);
			}else{
				
				talentOpenSummitButton.SetActiveRecursively(true);
				talentOpenSummitButtonBoxCollider.enabled = true;
			}
		}
	}
	
	void OnTalentOpened(CarInsanityProductInfo car) {
		talentOpenSummitButton.SetActiveRecursively(false);
	}
	
	void OnFailedToOpenTalent(Definition.RPCProcessState resultState) {
		
		talentOpenSummitButton.SetActiveRecursively(true);
	}
	
	void OnCarPurchased(CarInsanityProductInfo car) {
		talentOpenSummitButtonBoxCollider.enabled = true;
	}
	
	void OnFailedToPurchase(Definition.RPCProcessState resultState) {
		talentOpenSummitButton.SetActiveRecursively(true);
	}
	
	void OnClick(){
		productController.OpenTalent(currentCar.ID);
		talentOpenSummitButton.SetActiveRecursively(false);
	}
}
