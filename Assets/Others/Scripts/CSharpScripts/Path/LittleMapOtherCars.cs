using UnityEngine;
using System.Collections;

public class LittleMapOtherCars : MonoBehaviour {

	public Texture otherCarTexture;
	public GUITexture[] otherCarGUITextures;
	private ArrayList otherCars;
	private Rect textureRect;
	
	void Start () {
		Vector2 size = new Vector2();
		size.x = Screen.width / 1024.0f;
		size.y = Screen.height / 768.0f;
		textureRect = new Rect(otherCarTexture.width * 0.5f * size.x, otherCarTexture.height * 0.5f * size.y, otherCarTexture.width * size.x, otherCarTexture.height * size.y);
	}
	
	void Update () {
		try
		{
			int index = 0;
			foreach(Transform car in otherCars)
			{
				Vector3 pos = camera.WorldToScreenPoint(car.position);
				if(pos.x >= camera.pixelRect.xMin && pos.y >= camera.pixelRect.yMin && pos.x <= camera.pixelRect.xMax && pos.y <= camera.pixelRect.yMax)
				{
					if(otherCarGUITextures[index].texture == null)
					{
						otherCarGUITextures[index].texture = otherCarTexture;
					}
					otherCarGUITextures[index].pixelInset = new Rect(pos.x - textureRect.x, pos.y - textureRect.y, textureRect.width, textureRect.height);
//					GUI.DrawTexture(new Rect(pos.x - 25, (float)Screen.height - pos.y - 25, 50, 50), otherCarTexture);
				}
				index++;
			}
		}
		catch
		{
			CloseGUI();
		}
	}
	
	void DetectedOtherCar (ArrayList cars) {
		otherCars = cars;
		foreach(GUITexture texture in otherCarGUITextures)
		{
			texture.texture = null;
		}
		enabled = true;
	}
	
	void CloseGUI () {
		foreach(GUITexture texture in otherCarGUITextures)
		{
			texture.texture = null;
		}
		enabled = false;
	}
}
