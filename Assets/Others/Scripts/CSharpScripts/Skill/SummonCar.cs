//Written by ChunHua 2012/02/26
using UnityEngine;
using System.Collections;


public class SummonCar : Skill {
	
	//Skills' level-- level one to level five
	public SkillSettingPercent[] skillSetting;
	public GameObject[] level1;
	public GameObject[] level2;
	public GameObject[] level3;
	public GameObject[] level4;
	public GameObject[] level5;


	//I added
	public float timer;
	
	
	// what's the level of the car
	public int carlevel2;
	
	//how long the skill can use
	public float skillEffectTime;

	
//	DriftCar dc = new DriftCar();
	

	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(Summon(car));	
	}
	
//-----------------------I added-------------------------------------------------	
	IEnumerator Summon (CarProperty car) 
    {			
//		carlevel2 = dc.carLevel;

		
		//skills play level1~level5
		if(carlevel2==1)
		{
			//how long the skill can play
			skillEffectTime=5.0f;
	        car.Protect(true);
			//for truck skill when you play the skill other car's camera will change
			
			
			foreach(GameObject summon in level1)
			{				
 			  summon.SetActiveRecursively(true);	
			}
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			car.Protect(false);
			foreach(GameObject summon in level1)
			{	
 			  summon.SetActiveRecursively(false);	
			}	
		}
		
		
		//skill level2
		if(carlevel2==2)
		{
			//how long the skill can play
			skillEffectTime=8.0f;
			 car.Protect(true);
			foreach(GameObject summon in level2)
			{
			 summon.SetActiveRecursively(true);	
			}
			
			//finish the skill after 8 seconds
			yield return new WaitForSeconds(skillEffectTime);
			car.Protect(false);
			foreach(GameObject summon in level2)
			{				
 			  summon.SetActiveRecursively(false);		
			}
		}
		
		
		//skill level3
		if(carlevel2==3)
		{
			//how long the skill can play
			skillEffectTime=8.0f;
			car.Protect(true);
	
			foreach(GameObject summon in level3)
			{
			  summon.SetActiveRecursively(true);			
			}
			
			//finish the skill after 8 seconds
			yield return new WaitForSeconds(skillEffectTime);
			car.Protect(false);
			foreach(GameObject summon in level3)
			{			
 			  summon.SetActiveRecursively(false);		
			}
		}
		
		//skill level4
		if(carlevel2==4)
		{
			//how long the skill can play
			skillEffectTime=10.0f;
			car.Protect(true);
			foreach(GameObject summon in level4)
			{
		    	summon.SetActiveRecursively(true);			
			}
			
			//finish the skill after 10 seconds
			yield return new WaitForSeconds(skillEffectTime);
			car.Protect(false);
			foreach(GameObject summon in level4)
			{			
 			  summon.SetActiveRecursively(false);	
			}
		}
		
		
		//skill level5
		if(carlevel2==5)
		{
			//how long the skill can play
			skillEffectTime=10.0f;
						
			car.Protect(true);					
			foreach(GameObject summon in level5)
			{				
			 summon.SetActiveRecursively(true);
			}
		
			//finish the skill after 10 seconds
		    yield return new WaitForSeconds(skillEffectTime);
			car.Protect(false);
			
			foreach(GameObject summon in level5)
			{			
 			  summon.SetActiveRecursively(false);		
			}
				
		}
		
	}
//-----------------------I added-------------------------------------------------		
	


	
	
	
	
}
