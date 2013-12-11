using UnityEngine;
using System.Collections;
[AddComponentMenu("Camera-Control/Smooth Follow")]
public class SmoothFollow : MonoBehaviour {

/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 5.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public float xRotationDamping = 3.0f;
	//public bool observer = false;
//	private int carIndex = 0;
	private bool finishGame = false;
	
	// Place the script in the Camera-Control group in the component menu
//	[AddComponentMenu("Camera-Control/Smooth Follow")]
//	partial class SmoothFollow { }
//	public Texture arrowTexture;
	private CarB[] cars;
	private float defaultDistance;
	private float defaultHeight;
	
	void Awake () {
	    // Make the game run as fast as possible in the web player
	    //Application.targetFrameRate = 60;
	}
	
	IEnumerator Start () {
		
		defaultDistance = distance;
		defaultHeight = height;
		if(target)
		{
			SetTarget(target);
		}
		else
		{
			enabled = false;
		}
		args = new Hashtable();
		detectedCars = new ArrayList();
		
		while(cars == null)
		{
			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
			yield return null;
		}
		
	}
	
	float wantedRotationAngle;
	float wantedXRotationAngle;
	
	float wantedHeight;
		
	float currentRotationAngle;
	float currentXRotationAngle;
	Quaternion currentRotation;
	float dis;
	Hashtable args;
	ArrayList detectedCars;
	private string DetectedBackSideCar = "DetectedBackSideCar";
		private string DetectedCars = "detectedCars";
		private string TARGET = "target";
		private string CloseArrow = "CloseArrow";
	void Update () {
		
//		if(observer && Input.GetKeyDown(KeyCode.C))
//		{
//			target = cars[carIndex].transform;
//			carIndex++;
//			if(carIndex >= cars.Length)
//			{
//				carIndex = 0;
//			}
//		}
		
		// Early out if we don't have a target
//		if (!target)
//			return;
		
		// Calculate the current rotation angles
		wantedRotationAngle = target.eulerAngles.y;
		wantedXRotationAngle = target.eulerAngles.x;
		
		wantedHeight = target.position.y + height;
			
		currentRotationAngle = transform.eulerAngles.y;
//		float currentHeight = transform.position.y;
		
		// Calculate the current x-axis rotation angles
		currentXRotationAngle = transform.eulerAngles.x;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		// Damp the rotation around the x-axis
		currentXRotationAngle = Mathf.LerpAngle (currentXRotationAngle, wantedXRotationAngle, xRotationDamping * Time.deltaTime);
		
		// Damp the height
//		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
	
		// Convert the angle into a rotation
		// The quaternion interface uses radians not degrees so we need to convert from degrees to radians
		currentRotation = Quaternion.EulerAngles (currentXRotationAngle * Mathf.Deg2Rad, currentRotationAngle * Mathf.Deg2Rad, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		if(!finishGame)
		{
			transform.position -= currentRotation * Vector3.forward * distance;
			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, wantedHeight, heightDamping * Time.deltaTime), transform.position.z);
		}
		else
		{
			transform.position -= Vector3.forward * distance;
			transform.position = new Vector3(transform.position.x, wantedHeight, transform.position.z);
		}
		
		// Always look at the target
		transform.LookAt (target);
		
//		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, target.localEulerAngles.z);
		
		try
		{
			bool isDetectedCar = false;
			//ArrayList detectedCars = new ArrayList();
			detectedCars.Clear();
			foreach(CarB car in cars)
			{
				if(target == car.transform)
					continue;
				dis = Vector3.Dot(car.transform.position - target.position,target.forward);
				if(dis < -10 && dis > -30 && Vector3.Distance(car.transform.position,target.position) < 60)
				{
					detectedCars.Add(car.transform);
					isDetectedCar = true;
				}
			}
			
			if(isDetectedCar)
			{
				//Hashtable args = new Hashtable();
				args.Clear();
				args.Add(DetectedCars, detectedCars);
				args.Add(TARGET, target);
				SendMessage(DetectedBackSideCar, args, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				SendMessage(CloseArrow, SendMessageOptions.DontRequireReceiver);
			}
		}
		catch
		{
			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
		}
	}
	
//	void OnGUI () {
//		try
//		{
//			foreach(CarB car in cars)
//			{
//				if(target == car.transform)
//					continue;
//				float dis = Vector3.Dot(car.transform.position - target.position,target.forward);
//				if(dis < -10 && dis > -30 && Vector3.Distance(car.transform.position,target.position) < 60)
//				{
//					if(arrowTexture)
//					{
//						float disX = Vector3.Dot(car.transform.position - target.position,target.right);
//						float posY = Screen.height - arrowTexture.height / (-dis * 0.1f);
//		//				float posX = Mathf.Clamp(pos.x - arrowTexture.width / (-dis * 0.2f), 0.0f, Screen.width - arrowTexture.width / (-dis * 0.1f));
//						float posX = Mathf.Clamp(Screen.width * 0.5f + disX * 50, 0.0f, Screen.width - arrowTexture.width / (-dis * 0.1f));
//						GUI.DrawTexture(new Rect(posX, posY, arrowTexture.width / (-dis * 0.1f), arrowTexture.height / (-dis * 0.1f)), arrowTexture);
//					}
//				}
//			}
//		}
//		catch
//		{
//			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//		}
//	}
	
	public void SetCars () {
		cars = FindObjectsOfType(typeof(CarB)) as CarB[];
	}
	public float GetOriginalHeight(){
		return defaultHeight + (target.collider.bounds.size.y * 3);
	}
	void SetTarget (Transform t) {
		target = t;
		distance = defaultDistance + target.collider.bounds.size.z;
		height = defaultHeight + (target.collider.bounds.size.y * 3);
		
		
//		float wantedRotationAngle = target.eulerAngles.y;
//		float wantedXRotationAngle = target.eulerAngles.x;
//		float wantedHeight = target.position.y + height;
//			
//		float currentRotationAngle = transform.eulerAngles.y;
//		float currentHeight = transform.position.y;
//		
//		// Calculate the current x-axis rotation angles
//		float currentXRotationAngle = transform.eulerAngles.x;
//		
//		// Damp the rotation around the y-axis
//		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
//		// Damp the rotation around the x-axis
//		currentXRotationAngle = Mathf.LerpAngle (currentXRotationAngle, wantedXRotationAngle, xRotationDamping * Time.deltaTime);
//		
//		// Damp the height
////		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
//	
//		// Convert the angle into a rotation
//		// The quaternion interface uses radians not degrees so we need to convert from degrees to radians
//		Quaternion currentRotation = Quaternion.EulerAngles (currentXRotationAngle * Mathf.Deg2Rad, currentRotationAngle * Mathf.Deg2Rad, 0);
//		
//		// Set the position of the camera on the x-z plane to:
//		// distance meters behind the target
//		transform.position = target.position;
//		if(!finishGame)
//		{
//			transform.position -= currentRotation * Vector3.forward * distance;
//			// Set the height of the camera
//			transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, wantedHeight, heightDamping * Time.deltaTime), transform.position.z);
//		}
//		else
//		{
//			transform.position -= Vector3.forward * distance;
//			transform.position = new Vector3(transform.position.x, wantedHeight, transform.position.z);
//		}
//		
//		transform.parent = target;
		enabled = true;
	}
	
	void ShowCup () {
		distance = -distance - target.collider.bounds.size.z;
		height = height * 2.5f;
		finishGame = true;
		target.GetComponent<CarAI>().enabled = true;
	} 
	
	void SetOriginalHeight (Transform tf) {
		if(tf == target)
		{
			height = GetOriginalHeight();
		}
	}
	
	void RotationDampingLow (Transform tf) {
		if(tf == target)
		{
			rotationDamping = 0.1f;
		}
	}
	
	void RotationDampingHigh (Transform tf) {
		if(tf == target)
		{
			rotationDamping = 3.0f;
		}
	}
}
