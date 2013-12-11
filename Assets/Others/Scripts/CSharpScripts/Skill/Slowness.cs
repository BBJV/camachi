using UnityEngine;
using System.Collections;

public class Slowness : Skill {
	public float startForce = -50.0f;
	public float speedDownPercent = -90.0f;
	public float effectTime = 10.0f;
	public Color skillColor = Color.blue;
	public AudioClip slowAudio;
	private CarProperty[] cars;
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		if(cars == null || cars.Length == 0)
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
		StartCoroutine(SlowDownSkill(car));
	}
	
	IEnumerator SlowDownSkill (CarProperty self) {
		skillUsing = true;
		if(slowAudio)
			AudioSource.PlayClipAtPoint(slowAudio, self.transform.position);
		try
		{
			foreach(CarProperty car in cars)
			{
				if(car != self)
				{
					car.SpeedUp(startForce);
					car.TorqueUp(speedDownPercent, skillColor);
					car.SendMessage("Slowness", effectTime, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		catch
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
		
		yield return new WaitForSeconds(effectTime);
//		try
//		{
//			foreach(CarProperty car in cars)
//			{
//				if(car != self)
//				{
//					car.TorqueUp(0.0f, Color.white);
//				}
//			}
//		}
//		catch
//		{
//			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
//		}
			
		skillUsing = false;
	}
}
