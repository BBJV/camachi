using UnityEngine;
using System.Collections;

public class CameraDevice : MonoBehaviour {
	public WebCamTexture webcamTexture;
	public int CameraWidth;
	public int CameraHeight;
	// Use this for initialization
	void Start () {
		webcamTexture = new WebCamTexture();
		webcamTexture.Play();
		CameraWidth = webcamTexture.width ;
		CameraHeight = webcamTexture.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
