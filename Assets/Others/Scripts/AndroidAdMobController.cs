using UnityEngine;  
using System.Collections;  
using System;  

public class AndroidAdMobController : MonoBehaviour {  
	
	private static readonly int BOTTOM = 80;
	private static readonly int CENTER_HORIZONTAL = 1;
	
	
	private float baseStartScreenX = 0.0f;
	private float baseStartScreenY = 0.0f;
	private float baseWidthScreenX = 1024.0f;
	private float baseHieghtScreenY = 768.0f;
	
	public float announceAbMobRefX = 0.0f;
	public float announceAbMobRefY = 0.0f;
	
	private float announceAbMobStartX = 64.0f;
	private float announceAbMobStartY = 182.0f;
	
	private float announceAdsWidth = 320.0f;
	private float announceAdsHeight = 70.0f;
	
	public float announcePaddingX = 0.0f;
	public float announcePaddingY = 0.0f;
	
	void Awake() {
		NGUILoginController.OnLoginedManu += OnLoginedManu;
		NGUILoginController.OnLogouted += OnLogouted;
		
		LobbyController.OnEnteredLobby += OnEnteredLobby;
		LobbyController.OnBeforeLobbyBoardUp += OnBeforeLobbyBoardUp;
		PlayGameController.OnLoadSuccess += OnLoadSuccess;
		PlayGameController.OnShowRecord += OnShowRecord;
		PlayGameController.OnLoadingMap += OnLoadingMap;
		
		LobbyController.OnAfterLobbyBoardDown += OnAfterLobbyBoardDown;
		LobbyController.OnSelectingMap += OnSelectingMap;
		
		
	}
	
	void OnDestroy() {
		NGUILoginController.OnLoginedManu -= OnLoginedManu;
		NGUILoginController.OnLogouted -= OnLogouted;
		PlayGameController.OnShowRecord -= OnShowRecord;
		PlayGameController.OnLoadingMap -= OnLoadingMap;
		
		PlayGameController.OnLoadSuccess -= OnLoadSuccess;
		LobbyController.OnBeforeLobbyBoardUp -= OnBeforeLobbyBoardUp;
		LobbyController.OnEnteredLobby -= OnEnteredLobby;
		
		LobbyController.OnAfterLobbyBoardDown -= OnAfterLobbyBoardDown;
		LobbyController.OnSelectingMap -= OnSelectingMap;
	}
	
	IEnumerator ShowAds() {
		yield return new WaitForSeconds(10.0f);
		EnableAds(1.0f);
	}
	
	void Start() {
		announceAbMobRefX = (announceAbMobStartX - baseStartScreenX) * Screen.width / baseWidthScreenX;
		announceAbMobRefY = (announceAbMobStartY - baseStartScreenY) * Screen.height / baseHieghtScreenY;
		
		announcePaddingX = announceAbMobRefX;
		announcePaddingY = announceAbMobRefY;
		
	}
  
	public void OnLogouted() {
//		Debug.Log("OnLogouted");
		
		DisableAds(0.0f);

	}
	
	public void OnLoginedManu() {
		
//		Debug.Log("OnLoginedManu");
		
		SetAdViewAtPosition(CENTER_HORIZONTAL);
		EnableAds(1.0f);
		SetAdViewClickable();
	}
	
	public void OnEnteredLobby() {
		
//		Debug.Log("OnEnteredLobby");
		
		DisableAds(0.0f);

	}
	
	public void OnAfterLobbyBoardDown() {
		
//		Debug.Log("OnAfterLobbyBoardDown");
		
		SetAdViewAtAbsolutePosition((int)announcePaddingX, (int)announcePaddingY, 0, 0);
		EnableAds(1.0f);
		SetAdViewClickable();
		
		StartCoroutine(ShowAds());
		
		
	}

	
	public void OnBeforeLobbyBoardUp() {
		
//		Debug.Log("OnBeforeLobbyBoardUp");
		StopCoroutine("ShowAds");
		DisableAds(0.0f);
		

	}
	
	public void OnSelectingMap() {
		
		if(PlayerPrefs.GetString("GameType") == "Single"){
//			Debug.Log("Single select map");
			DisableAds(0.0f);
		}
	}
	
	public void OnLoadingMap() {
		
//		Debug.Log("OnLoadingMap");
//		
//		SetAdViewAtPosition(BOTTOM|CENTER_HORIZONTAL);
//		EnableAds(1.0f);
//		SetAdViewUnclickable();
	}
	
	public void OnLoadSuccess() {
		
//		Debug.Log("OnLoadSuccess");
//		DisableAds(0.0f);

	}
	
	public void OnShowRecord() {
		
//		Debug.Log("OnShowRecord");
		SetAdViewAtPosition(BOTTOM|CENTER_HORIZONTAL);
		EnableAds(1.0f);
		SetAdViewClickable();
	}
	
    private void EnableAds(float enableTime) {  
 
	#if UNITY_ANDROID  
        AndroidJNI.AttachCurrentThread();  
        // first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        
		//Debug.Log("obj_Activity = " + obj_Activity);  

        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr startAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "EnableAds", "(F)V");  
        
		//Debug.Log("m_startAdsMethod = " + startAdsMethod);  

		if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false) {  

			//Debug.Log("Activity IS a OurAppNameActivity");  
  
            jvalue[] myArray = new jvalue[1];
			myArray[0].f = enableTime;
            AndroidJNI.CallVoidMethod(obj_Activity, startAdsMethod, myArray);  
        }  
 
	#else  
  
//        m_adShowing = true;  
 
	#endif //UNITY_ANDROID  
  
    }  
  
    private void DisableAds(float disableTime)  {  
 
	#if UNITY_ANDROID  
  
		AndroidJNI.AttachCurrentThread();  
		// first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr stopAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "DisableAds", "(F)V");  
 
        if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false)  {  
            jvalue[] myArray = new jvalue[1];
			myArray[0].f = disableTime;
            AndroidJNI.CallVoidMethod(obj_Activity, stopAdsMethod,myArray);  
        }  
 
	#else //UNITY_ANDROID  
  
//        m_adShowing = false;  
 
	#endif //UNITY_ANDROID  
  
    }  
	
	private void SetAdViewClickable()  {  
 
	#if UNITY_ANDROID  
  
		AndroidJNI.AttachCurrentThread();  
		// first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr stopAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "SetAdViewClickable", "()V");  
 
        if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false)  {  
            jvalue[] myArray = new jvalue[1];  
            AndroidJNI.CallVoidMethod(obj_Activity, stopAdsMethod,myArray);  
        }  
 
	#else //UNITY_ANDROID  
  
//        m_adShowing = false;  
 
	#endif //UNITY_ANDROID  
  
    }  
	
	private void SetAdViewUnclickable()  {  
 
	#if UNITY_ANDROID  
  
		AndroidJNI.AttachCurrentThread();  
		// first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr stopAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "SetAdViewUnclickable", "()V");  
 
        if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false)  {  
            jvalue[] myArray = new jvalue[1];  
            AndroidJNI.CallVoidMethod(obj_Activity, stopAdsMethod,myArray);  
        }  
 
	#else //UNITY_ANDROID  
  
//        m_adShowing = false;  
 
	#endif //UNITY_ANDROID  
  
    }  
	
	private void SetAdViewAtPosition(int position)  {  
 
	#if UNITY_ANDROID  
  
		AndroidJNI.AttachCurrentThread();  
		// first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr stopAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "SetAdViewAtPosition", "(I)V");  
 
        if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false)  {  
            jvalue[] myArray = new jvalue[1];
			myArray[0].i = position;
            AndroidJNI.CallVoidMethod(obj_Activity, stopAdsMethod,myArray);  
        }  
 
	#else //UNITY_ANDROID  
  
//        m_adShowing = false;  
 
	#endif //UNITY_ANDROID  
  
    } 
	
	private void SetAdViewAtAbsolutePosition(int paddingLeft, int paddingTop, int paddingRight, int paddingBottom)  {  
 
	#if UNITY_ANDROID  
  
		AndroidJNI.AttachCurrentThread();  
		// first we try to find our main activity..  
  
        IntPtr cls_Activity = AndroidJNI.FindClass("com/unity3d/player/UnityPlayer");  
        IntPtr fid_Activity = AndroidJNI.GetStaticFieldID(cls_Activity, "currentActivity", "Landroid/app/Activity;");  
        IntPtr obj_Activity = AndroidJNI.GetStaticObjectField(cls_Activity, fid_Activity);  
        IntPtr cls_OurAppNameActivityClass = AndroidJNI.FindClass("com/wintek/CarInsanity/CarInsanityAdMobActivity"); //this has to be changed  
        IntPtr stopAdsMethod = AndroidJNI.GetMethodID(cls_OurAppNameActivityClass, "SetAdViewAtAbsolutePosition", "(IIII)V");  
 
        if (AndroidJNI.IsInstanceOf(obj_Activity, cls_OurAppNameActivityClass) != false)  {  
            jvalue[] myArray = new jvalue[4];
			myArray[0].i = paddingLeft;
			myArray[1].i = paddingTop;
			myArray[2].i = paddingRight;
			myArray[3].i = paddingBottom;
            AndroidJNI.CallVoidMethod(obj_Activity, stopAdsMethod,myArray);  
        }  
 
	#else //UNITY_ANDROID  
  
//        m_adShowing = false;  
 
	#endif //UNITY_ANDROID  
  
    } 
  
}  
