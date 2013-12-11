using UnityEngine;
using System.Collections;

public class CameraTake : MonoBehaviour {
	public CameraDevice MyCameraDevice;
	public Texture2D tex2D;
	public Transform FaceTransform;
	public TriggerType MyTrggerType;
	private Color32[] data;
	
	void ClickedOn(){
		if(MyTrggerType == TriggerType.Click){
			Take();
		}
	}
	void Take(){
		data = new Color32[MyCameraDevice.CameraWidth * MyCameraDevice.CameraHeight];
		tex2D = new Texture2D(MyCameraDevice.CameraWidth , MyCameraDevice.CameraHeight);
		
		tex2D.SetPixels32(MyCameraDevice.webcamTexture.GetPixels32(data));
		tex2D.Apply();
		FaceTransform.renderer.material.mainTexture = tex2D;
		//File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", tex2D.EncodeToPNG());
	}
}
