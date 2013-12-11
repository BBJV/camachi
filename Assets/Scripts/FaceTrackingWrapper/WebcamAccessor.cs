using UnityEngine;
using System.Collections;

public class WebcamAccessor {

	private WebCamTexture wct;
	
	private int width;
	private int height;
	
	public int Width {
		get {
			return width;
		}
	}
	
	public int Height {
		get {
			return height;
		}
	}
	
	public Color32[] WebcamPixels32 {
		get {
			if(wct != null) {
				return wct.GetPixels32();
			}
			return null;
		}
	}
	
	public WebcamAccessor() {
		wct = null;
		width = 640;
		height = 480;
	}
	
	public bool StartWebcam() {
		return StartWebcam(0);
	}
	
	public bool StartWebcam(int num) {
		return StartWebcam(num, 640, 480, 30);
	}
	
	public bool StartWebcam(int num, int width, int height, int fps) {
		this.width = width;
		this.height = height;
		int count = WebCamTexture.devices.Length;
		if(count <= num) {
			return false;
		}
		wct = new WebCamTexture(WebCamTexture.devices[num].name, width, height, 30);
		if(wct == null) {
			return false;
		}
		wct.Play();
		return true;
	}
	
	public void StopWebcam() {
		wct.Stop();
	}
}
