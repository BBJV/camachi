using UnityEngine;
using System.Collections;

public class NGUITalentLock : MonoBehaviour {
	
	public UISlicedSprite lockIcon;
	
	
	void Awake() {
		NGUIProductSceneViewer.OnProductViewChanged += OnProductViewChanged;
		
		ProductController.OnTalentOpened += OnTalentOpened;
	}
	
	void OnDestroy() {
		NGUIProductSceneViewer.OnProductViewChanged -= OnProductViewChanged;
		ProductController.OnTalentOpened -= OnTalentOpened;
	}
	
	void OnProductViewChanged(CarInsanityProductInfo car) {
//		Debug.Log ("NGUITalentLock.OnProductViewChanged-isTalentOpened:"+car.isTalentOpened.ToString());
		
		lockIcon.enabled = !car.isTalentOpened;
	}
	
	void OnTalentOpened(CarInsanityProductInfo car) {
		Debug.Log("NGUITalentLock.OnTalentOpened");
		if(lockIcon.enabled) {
			lockIcon.enabled = false;
		}
		
	}
}
