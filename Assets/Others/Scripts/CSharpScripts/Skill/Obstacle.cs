using UnityEngine;
using System.Collections;


//need to use with OnTriggerEnterha.cs
public class Obstacle : Skill {

	//I added
//	public float timer;
	
	
	// what's the level of the car
//	public int skillLevel;
	
	//how long the skill can use
//	public float skillEffect;
//	public float skillLiveTime;
	public SkillSettingPercent[] skillSetting;
	//DriftCar dc = new DriftCar();
	
	//the position the obstacle appear
	public Transform targetPosition;
	
	//the obstacle
	public WhirlObstacle groundObstacle;
	public float obstacleScale = 1.0f;
	public AudioClip roadBlockAudio;

	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		obstacle(car);	
	}
	
	
	void obstacle (CarProperty car) {	
//-----------------------I added-------------------------------------------------		
//		carlevel2 = dc.carLevel;
		//groundObstacle=GameObject.Find("groundObstacle");
		
		//skills play level1~level5
		//Change by Vincent
//		switch (skillLevel)
//		{
//			//how long the skill can play
//			case 1:
//				skillEffect= -40.0f;
//				skillLiveTime = 20.0f;
//				break;
//			case 2:
//				skillEffect= -40.0f;
//				skillLiveTime = 25.0f;
//				break;
//			case 3:
//				skillEffect= -20.0f;
//				skillLiveTime = 30.0f;
//				break;
//			case 4:
//				skillEffect= -25.0f;
//				skillLiveTime = 35.0f;
//				break;
//			case 5:
//				skillEffect= -30.0f;
//				skillLiveTime = 40.0f;
//				break;
//		}
			
		//the car which is effected by the skill it can't stop itself
		//car.CanBrake(false);
		
		// if skill played the car will light
		skillUsing = true;
		//Debug.LogError("OnTriggerEnterha");
		if(roadBlockAudio)
		{
			AudioSource.PlayClipAtPoint(roadBlockAudio, targetPosition.position);
		}
		WhirlObstacle newGroundObstacle = Instantiate(groundObstacle, targetPosition.position, targetPosition.rotation) as WhirlObstacle;
		if(obstacleScale != 1.0f)
		{
			newGroundObstacle.transform.localScale = newGroundObstacle.transform.localScale * obstacleScale;
			Vector3 position = newGroundObstacle.transform.position;
			position.y += obstacleScale * 0.5f;
			newGroundObstacle.transform.position = position;
		}
		newGroundObstacle.selfCar = car;
		
		//finish the skill after 5 seconds
		newGroundObstacle.liveTime = skillSetting[skillLevel - 1].skillEffectTimes;
		newGroundObstacle.effectPercent = skillSetting[skillLevel - 1].effectPercent;
		skillUsing = false;
	}
//-----------------------I added-------------------------------------------------		
	
}
