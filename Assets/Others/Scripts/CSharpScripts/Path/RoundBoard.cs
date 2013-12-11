using UnityEngine;
using System.Collections;

public class RoundBoard : MonoBehaviour {
	public Texture[] textures;
	
	public void Round (int round) {
		renderer.material.mainTexture = textures[Mathf.Clamp(round - 1, 0, textures.Length - 1)];
	}
}
