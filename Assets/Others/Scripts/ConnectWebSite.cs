using UnityEngine;
using System.Collections;

public class ConnectWebSite : MonoBehaviour {
#if UNITY_IPHONE
	void OnClick () {
		StoreKitBinding.LinkITuneConnect("http://www.wgame.com.tw/carinsanity/");
	}
#endif
}
