using UnityEngine;
using System.Collections;

public class LittleMap : MonoBehaviour {
	private CarB[] cars;
//	public Texture texture;
	public Texture otherCarTexture;
	public GUITexture[] otherCarGUITextures;
	private Rect textureRect;
	private SmoothFollow SFCamera;
//	public Texture selfTexture;
//	public Texture miniMapTexture;
//	private Vector2 scale;
	void Start () {
		cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//		Debug.Log("MiniMap Start cars.Length : " + cars.Length);
		SFCamera = Camera.main.GetComponent<SmoothFollow>();
		Vector2 size = new Vector2();
		size.x = Screen.width / 1024.0f;
		size.y = Screen.height / 768.0f;
		textureRect = new Rect(otherCarTexture.width * 0.5f * size.x, otherCarTexture.height * 0.5f * size.y, otherCarTexture.width * size.x, otherCarTexture.height * size.y);
//		scale = new Vector2(Screen.width / 1024.0f, Screen.height / 768.0f);
	}
	
	public void SetCars () {
		cars = FindObjectsOfType(typeof(CarB)) as CarB[];
		if(!SFCamera)
		{
			SFCamera = Camera.main.GetComponent<SmoothFollow>();
		}
		SFCamera.SetCars();
//		Debug.Log("MiniMap SetCars cars.Length : " + cars.Length);
	}
	
	void Update () {
		if(SFCamera.target)
		{
			transform.position = new Vector3(SFCamera.target.position.x, transform.position.y, SFCamera.target.position.z);
			transform.rotation = Quaternion.Euler(90, SFCamera.target.eulerAngles.y, 0);
		}
		
		try
		{
//			bool isDetectedCar = false;
//			ArrayList detectedCars = new ArrayList();
			int index = 0;
			foreach(CarB car in cars)
			{
				if(car.transform != SFCamera.target)
				{
					Vector3 pos = camera.WorldToScreenPoint(car.transform.position);
					if(pos.x >= camera.pixelRect.xMin && pos.y >= camera.pixelRect.yMin && pos.x <= camera.pixelRect.xMax && pos.y <= camera.pixelRect.yMax)
					{
//						detectedCars.Add(car.transform);
//						isDetectedCar = true;
						if(otherCarGUITextures[index].texture == null)
						{
							otherCarGUITextures[index].texture = otherCarTexture;
						}
						otherCarGUITextures[index].pixelInset = new Rect(pos.x - textureRect.x, pos.y - textureRect.y, textureRect.width, textureRect.height);
						index++;
					}
					else
					{
						otherCarGUITextures[index].texture = null;
					}
				}
			}
			
//			if(isDetectedCar)
//			{
//				SendMessage("DetectedOtherCar", detectedCars, SendMessageOptions.DontRequireReceiver);
//			}
//			else
//			{
//				SendMessage("CloseGUI", SendMessageOptions.DontRequireReceiver);
//			}
		}
		catch
		{
//			Debug.LogError("MiniMap foreach broken");
			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//			Debug.LogError("MiniMap new cars.Length : " + cars.Length);
		}
	}
	
//	void OnGUI () {
//		try
//		{
////			GUI.DrawTexture(new Rect(801.0f * scale.x, -8.0f * scale.y, miniMapTexture.width * scale.x, miniMapTexture.height * scale.y), miniMapTexture);
//			foreach(CarB car in cars)
//			{
//	//			Debug.Log(car.name);
//				if(car)
//				{
//					Vector3 pos = camera.WorldToScreenPoint(car.transform.position);
//					if(pos.x >= camera.pixelRect.x && Screen.height - pos.y <= camera.pixelHeight)
//					{
//						if(car.transform == SFCamera.target)
//						{
////							GUI.DrawTexture(new Rect(pos.x - 25, (float)Screen.height - pos.y - 25, 50, 50), selfTexture);
//						}
//						else
//						{
//							GUI.DrawTexture(new Rect(pos.x - 25, (float)Screen.height - pos.y - 25, 50, 50), texture);
//						}
//					}
//				}
//			}
//		}
//		catch
//		{
//			Debug.LogError("MiniMap foreach broken");
//			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//			Debug.LogError("MiniMap new cars.Length : " + cars.Length);
//		}
//	}
}
