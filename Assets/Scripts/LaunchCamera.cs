using UnityEngine;
using System.IO;
using System.Collections;

public class LaunchCamera : MonoBehaviour {
	//private WebCamTexture webcamTexture;
	public CameraDevice MyCameraDevice;
	//public Texture2D tex2D;
	//private Color32[] data;
	public Transform CameraView;
	private float WLRatio;
	//private Texture2D CameraViewTexture;
	/*
	private Rect webcamRect;
	public int webcamX = 0;
	public int webcamY = 0;
	public int webcamWidth = 300;
	public int webcamHeight = 200;
	
	private Rect cameraShotButtonRect;
	public int cameraShotButtonX = 400;
	public int cameraShotButtonY = 400;
	public int cameraShotButtonWidth = 100;
	public int cameraShotButtonHeight = 100;
	*/
	void Start () {
		
		//webcamRect = new Rect(webcamX, webcamY,webcamWidth,webcamHeight);
		//cameraShotButtonRect = new Rect(cameraShotButtonX, cameraShotButtonY,cameraShotButtonWidth,cameraShotButtonHeight);
		
		// Start web cam feed
		//webcamTexture = new WebCamTexture();
		//webcamTexture.Play();
		WLRatio = (float)MyCameraDevice.CameraWidth / (float)MyCameraDevice.CameraHeight;
		/*
		print ("b = " + CameraView.localScale);
		print("webcamTexture.width = " + webcamTexture.width);
		print("webcamTexture.height = " + webcamTexture.height);
		print ("WLRatio = " + WLRatio);
		print ("CameraView.localScale.x = " + CameraView.localScale.x);
		print ("CameraView.localScale.y = " + CameraView.localScale.y);
		print ("(CameraView.localScale.y / CameraView.localScale.x) = " + (CameraView.localScale.y / CameraView.localScale.x));
		print ("s = " + ((CameraView.localScale.y / CameraView.localScale.x) * WLRatio));
		*/
		CameraView.localScale = new Vector3(CameraView.localScale.x * (CameraView.localScale.y / CameraView.localScale.x) * WLRatio ,CameraView.localScale.y,
			CameraView.localScale.z);
		//print ("a = " + CameraView.localScale);
		//print("webcamTexture.width = " + webcamTexture.width);
		//print("webcamTexture.height = " + webcamTexture.height);
	}

	void Update () {
		CameraView.renderer.material.mainTexture = MyCameraDevice.webcamTexture;
		// Do processing of data here.
	}
	void OnEnable(){
		CameraView.gameObject.SetActive(true);
	}
	void OnDisable(){
		if(CameraView){
			CameraView.gameObject.SetActive(false);
		}
	}
	/*
	void OnGUI () {
		GUI.DrawTexture(webcamRect, webcamTexture);
		
		if(GUI.Button(cameraShotButtonRect, "Shot")) {
			data = new Color32[webcamTexture.width * webcamTexture.height];
			tex2D = new Texture2D(webcamTexture.width , webcamTexture.height);
			
			tex2D.SetPixels32(webcamTexture.GetPixels32(data));
			tex2D.Apply();
			
			File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", tex2D.EncodeToPNG());
		}
		
		GUI.Label(new Rect(0,300,100,100), Application.dataPath + "/../SavedScreen.png");
	}
	*/
}
