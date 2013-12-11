//Written by ChunHua 2012/03/05
using UnityEngine;
using System.Collections;


public class SpeedLimit : Skill {
	
	//I added
//	public float timer;
		
	// what's the level of the car
//	public int skillLevel;
	
	//how long the skill can use
	public SkillSettingPercent[] skillSetting;
//	public float skillEffectTime;
//	public float skillStopTime;
	//DriftCar dc = new DriftCar();
	
	//to play the music
	public AudioClip policeCarSiren;
//	public AudioClip snapShot;
	
	//to show the button
//	public Texture2D btnPoliceSkill_photo;
	public GUISkin skin;
	//record the cars in the light
	public ArrayList carArray = new ArrayList();
	
//	ArrayList effectArray = new ArrayList();
	
//	public float radius;
	
//    public bool noGui; 
	
//	public float speedDownPercent = -200.0f;
	
//	public float carSpeed;
	private bool isSpeedLimit = false;
	public GameObject[] effectObjects;
	private SmoothFollow smfCamera;
	private CarProperty[] cars;
	private CarProperty selfCar;
	private ArrayList catchCars = new ArrayList();
	private Vector2 resolutionScale = new Vector2(Screen.width / 1024.0f, Screen.height / 768.0f);
	
	//Draw the button
	void OnGUI ()
	{
		try
		{
			if(isSpeedLimit)
			{
				if(smfCamera && smfCamera.target == transform.root)
				{
					Vector3 screenPos;
	//				int x = 1;
					GUI.skin = skin;
					
					foreach(CarProperty car in cars)
					{
						if(!car){
							continue;
						}
						if(car.transform.root == transform.root || car.CheckSpeedLimit() || (car.rigidbody.velocity.magnitude * 4.0f) < 30 || car.GetRank() > selfCar.GetRank() || catchCars.Contains(car))
						{
							continue;
						}
						screenPos = Camera.main.WorldToScreenPoint(car.transform.position);
						if((screenPos.x < 0 && screenPos.x > Camera.main.pixelWidth) && (screenPos.y < 0 && screenPos.y > Camera.main.pixelHeight) && (screenPos.z <= 0))
						{
							continue;
						}
						
						if(CustomGUI.Button(new Rect(screenPos.x - 50.0f * resolutionScale.x,Screen.height - screenPos.y - 110.0f * resolutionScale.y, 100.0f * resolutionScale.x, 100.0f * resolutionScale.y), "") || Input.GetKeyDown(KeyCode.R))
						{
//							if(selfCar.snapShotAudio)
//							{
//								AudioSource.PlayClipAtPoint(selfCar.snapShotAudio, transform.position);
//							}
							car.SetSpeedLimit(8.3f, skillSetting[skillLevel - 1].effectPercent);
							catchCars.Add(car);
						}
					}
				}
				else if(selfCar.isAI)
				{
					Vector3 screenPos;
					foreach(CarProperty car in cars)
					{
						if(car.transform.root == transform.root || car.CheckSpeedLimit() || (car.rigidbody.velocity.magnitude * 4.0f) < 30 || car.GetRank() > selfCar.GetRank() || catchCars.Contains(car))
						{
							continue;
						}
						screenPos = Camera.main.WorldToScreenPoint(car.transform.position);
						if((screenPos.x < 0 && screenPos.x > Camera.main.pixelWidth) && (screenPos.y < 0 && screenPos.y > Camera.main.pixelHeight) && (screenPos.z <= 0))
						{
							continue;
						}
						car.SetSpeedLimit(8.3f, skillSetting[skillLevel - 1].effectPercent);
						catchCars.Add(car);
					}
				}
			}
			
			
	//		int x = 1;
	//		if(smfCamera && smfCamera.target == transform.root)
	//		{
	//			foreach(Transform cCar in carArray)
	//			{
	//				Vector3 screenPos = Camera.main.WorldToScreenPoint(cCar.position);
	//				if(btnPoliceSkill_photo)
	//					GUI.DrawTexture(new Rect(screenPos.x - 20,Screen.height - screenPos.y - 90, 60, 60), btnPoliceSkill_photo);
	//				
	//				//otherCar=cCar;
	//				CarProperty otherCar = cCar.GetComponent<CarProperty>();
	//				
	//				GUI.Label (new Rect(100 * x, 0,80,80),"car name="+cCar.name+" "+ "Speed = "+ (cCar.rigidbody.velocity.magnitude * 3.6f).ToString("f1")+ " \n");
	//				
	//				
	//				
	//				//if click the button
	//				if(!effectArray.Contains(cCar) && (GUI.Button(new Rect(screenPos.x - 25,Screen.height - screenPos.y - 90, 60, 60), btnPoliceSkill_photo)))
	//				{
	//					
	//					//carSpeed=cCar.rigidbody.velocity.magnitude*-2.0f;
	//	//				otherCar.SpeedUp(speedDownPercent);
	//					otherCar.SetMaxVel(8.3f, timer);
	//					effectArray.Add(cCar);
	//				}
	//				x++;
	//			}
	//		}
	//		foreach(Transform car in effectArray)
	//		{
	//			//Debug.Log(car.name);
	//			carArray.Remove(car);
	//		}
		}
		catch
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
	}

//--------------------------------------------------------------------------------
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		if(!smfCamera)
			smfCamera = Camera.main.GetComponent<SmoothFollow>();
		if(cars == null || cars.Length == 0)
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
		if(!selfCar)
		{
			selfCar = car;
		}
		StartCoroutine(speedLimit(car));	
	}
//--------------------------------------------------------------------------------
	
	
//-----------------------I added-------------------------------------------------	
	IEnumerator speedLimit (CarProperty car) {	
		if(isSpeedLimit)
			yield break;
		isSpeedLimit = true;
		skillUsing = true;
		
		foreach(GameObject effect in effectObjects)
		{
			effect.SetActiveRecursively(true);
		}
		AudioSource sirenAudio = gameObject.AddComponent<AudioSource>();
		sirenAudio.clip = policeCarSiren;
		sirenAudio.loop = true;
		sirenAudio.Play();
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		foreach(GameObject effect in effectObjects)
		{
			effect.SetActiveRecursively(false);
		}
		sirenAudio.Stop();
		Destroy(sirenAudio);
		catchCars.Clear();
		isSpeedLimit = false;
		skillUsing = false;
	}
//-----------------------I added-------------------------------------------------	
	
}