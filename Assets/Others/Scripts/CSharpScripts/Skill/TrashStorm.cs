using UnityEngine;
using System.Collections;


public class TrashStorm : Skill {
	

	//I added
	public float timer;
	
	
	// what's the level of the car
	public int carlevel2;
	
	//how long the skill can use
	public float skillEffectTime;

	
//	DriftCar dc = new DriftCar();
	
	//the position the obstacle appear
	public Transform targetPosition;
	
	//the obstacle
	public Transform groundObstacle;
	
	public Transform screenGarbage;
	


	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(trashStorm(car));	
	}
	
	
	IEnumerator trashStorm (CarProperty car) {	
//-----------------------I added-------------------------------------------------		
//		carlevel2 = dc.carLevel;
		

		//skills play level1~level5
		if(carlevel2==1)
		{
			//how long the skill can play
			skillEffectTime=10.0f;
			
			//the car which is effected by the skill it can't stop itself
			car.CanBrake(false);
			
			// if skill played the car will light
//			GameObject.Find("groundObstacle").active = true;
			groundObstacle.gameObject.active=true;
			groundObstacle.transform.position=targetPosition.position;
		

			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			groundObstacle.gameObject.active=false;
			screenGarbage.gameObject.active=false;
 	
		}
		
		
		//skill level2
		if(carlevel2==2)
		{
				//how long the skill can play
			skillEffectTime=12.0f;
			
			//the car which is effected by the skill it can't stop itself
			car.CanBrake(false);
			
			// if skill played the car will light
//			GameObject.Find("groundObstacle").active = true;
			groundObstacle.gameObject.active=true;
			groundObstacle.transform.position=targetPosition.position;
		

			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			groundObstacle.gameObject.active=false;
			screenGarbage.gameObject.active=false;

		}
		
		
		//skill level3
		if(carlevel2==3)
		{
					//how long the skill can play
			skillEffectTime=14.0f;
			
			//the car which is effected by the skill it can't stop itself
			car.CanBrake(false);
			
			// if skill played the car will light
//			GameObject.Find("groundObstacle").active = true;
			groundObstacle.gameObject.active=true;
			groundObstacle.transform.position=targetPosition.position;
		

			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			groundObstacle.gameObject.active=false;
			screenGarbage.gameObject.active=false;
			

		}
		
		//skill level4
		if(carlevel2==4)
		{
					//how long the skill can play
			skillEffectTime=16.0f;
			
			//the car which is effected by the skill it can't stop itself
			car.CanBrake(false);
			
			// if skill played the car will light
//			GameObject.Find("groundObstacle").active = true;
			groundObstacle.gameObject.active=true;
			groundObstacle.transform.position=targetPosition.position;
		

			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			groundObstacle.gameObject.active=false;
			screenGarbage.gameObject.active=false;
 
		}
		
		
		//skill level5
		if(carlevel2==5)
		{
					//how long the skill can play
			skillEffectTime=18.0f;
			
			//the car which is effected by the skill it can't stop itself
			car.CanBrake(false);
			
			// if skill played the car will light
//			GameObject.Find("groundObstacle").active = true;
			groundObstacle.gameObject.active=true;
			groundObstacle.transform.position=targetPosition.position;
		

			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			groundObstacle.gameObject.active=false;
			screenGarbage.gameObject.active=false;

				
		}
//-----------------------I added-------------------------------------------------		
		
	}
//-----------------------I added-------------------------------------------------		
	


	
	
	
	
}
