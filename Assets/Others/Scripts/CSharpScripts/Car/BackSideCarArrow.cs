using UnityEngine;
using System.Collections;

public class BackSideCarArrow : MonoBehaviour {
	
	private ArrayList detectedCars;
	private Transform target;
	public Texture arrowTexture;
	public GUITexture[] arrowGUITetures;
	
	void Update () {
		try
		{
			int index = 0;
			foreach(Transform car in detectedCars)
			{
				if(target == car)
					continue;
				float dis = Vector3.Dot(car.position - target.position,target.forward);
				if(dis < -10 && dis > -30 && Vector3.Distance(car.position,target.position) < 60)
				{
					if(arrowTexture)
					{
						float disX = Vector3.Dot(car.position - target.position,target.right);
//						float posY = Screen.height - arrowTexture.height / (-dis * 0.1f);
						float posX = Mathf.Clamp(Screen.width * 0.5f + disX * 50, 0.0f, Screen.width - arrowTexture.width / (-dis * 0.1f));
						arrowGUITetures[index].texture = arrowTexture;
						arrowGUITetures[index].pixelInset = new Rect(posX, 0, arrowTexture.width / (-dis * 0.1f), arrowTexture.height / (-dis * 0.1f));
//						GUI.DrawTexture(new Rect(posX, posY, arrowTexture.width / (-dis * 0.1f), arrowTexture.height / (-dis * 0.1f)), arrowTexture);
					}
				}
				index++;
			}
		}
		catch
		{
			CloseArrow();
		}
	}
	
	void DetectedBackSideCar (Hashtable args) {
		detectedCars = (ArrayList)args["detectedCars"];
		target = (Transform)args["target"];
		enabled = true;
	}
	
	void CloseArrow () {
		foreach(GUITexture texture in arrowGUITetures)
		{
			texture.texture = null;
		}
		enabled = false;
	}
}
