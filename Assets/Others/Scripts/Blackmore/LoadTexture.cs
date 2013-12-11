using UnityEngine;
using System.Collections;

public class LoadTexture : MonoBehaviour {
	public string[] TextureName;
	// Use this for initialization
	void Start () {
		for(int i = 0 ; i < TextureName.Length ; i++){
			transform.renderer.materials[i].mainTexture = Resources.Load(TextureName[i]) as Texture;
		}
	}
}
