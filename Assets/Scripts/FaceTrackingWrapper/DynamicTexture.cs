using UnityEngine;
using System.Collections;
using System.IO;

public class DynamicTexture : MonoBehaviour {
	
	
	private Texture2D tex;
	
	
	// Use this for initialization
	void Start () {
		tex = new Texture2D(640,480,TextureFormat.ARGB32,false);
		gameObject.renderer.material.mainTexture = tex;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ApplyTex(Color32[] data) {
		tex.SetPixels32(data);
		tex.Apply(false);
	}
}
