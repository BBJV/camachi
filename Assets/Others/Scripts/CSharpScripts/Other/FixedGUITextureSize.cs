using UnityEngine;
using System.Collections;

public class FixedGUITextureSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GUITexture[] guis = FindObjectsOfType(typeof(GUITexture)) as GUITexture[];
		Vector2 size = new Vector2();
		size.x = Screen.width / 1024.0f;
		size.y = Screen.height / 768.0f;
		foreach(GUITexture gui in guis)
		{
			gui.pixelInset = new Rect(gui.pixelInset.x * size.x, gui.pixelInset.y * size.y, gui.pixelInset.width * size.x, gui.pixelInset.height * size.y);
		}
	}
}
