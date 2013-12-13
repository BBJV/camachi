using UnityEngine;
using System.Collections;

public class CameraDevice : MonoBehaviour {
	public WebCamTexture webcamTexture;
	public int CameraWidth;
	public int CameraHeight;
//	public WebCamTexture frontTexture;
	// Use this for initialization
	void Start () {
		string frontcamname = null;
		webcamTexture = null;
		WebCamDevice[] webcamDevices = WebCamTexture.devices;
		print ("webcamDevices = " + webcamDevices.Length);
		for(int i = 0 ; i < webcamDevices.Length ; i++){
			if(webcamDevices[i].isFrontFacing){
				frontcamname = webcamDevices[i].name;
			}
		}
		if(frontcamname != null){
			webcamTexture = new WebCamTexture(frontcamname, 400, 400, 10);
		}
//		webcamTexture = new WebCamTexture();
//		webcamTexture.Play();
//		CameraWidth = webcamTexture.width ;
//		CameraHeight = webcamTexture.height;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public bool isSurpportFrontCam(){
		return webcamTexture != null;
	}
}
