using UnityEngine;
using System.Collections;


public class GiveAlice : Skill {
	


	//I added
	public float timer;
	
	
	// what's the level of the car
	public int carlevel2;
	
	//how long the skill can use
	public float skillEffectTime;
	
		//to play the music
	public AudioSource giveAliceMusic;
	
	public Transform showGarbage;
	
//	DriftCar dc = new DriftCar();
	

	
	
	

	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(giveAlice(car));	
	}
	
	
	IEnumerator giveAlice (CarProperty car) {	
//-----------------------I added-------------------------------------------------		
//		carlevel2 = dc.carLevel;
		
		

		
		//skills play level1~level5
		if(carlevel2==1)
		{
			//how long the skill can play
			skillEffectTime=10.0f;
			
			//when start the skill the music will play
			giveAliceMusic.Play();
			
		    
			GameObject.Find("policePointLight").light.enabled = true;
			showGarbage.gameObject.active=true;
			
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			
			//when the skillEffectTime is over stop the music
			giveAliceMusic.Stop();
			showGarbage.gameObject.active=false;
			GameObject.Find("policePointLight").light.enabled = false;
		
	
		}
		
		
		//skill level2
		if(carlevel2==2)
		{
			//how long the skill can play
			skillEffectTime=11.0f;
			
			//when start the skill the music will play
			giveAliceMusic.Play();
			
		    
			GameObject.Find("policePointLight").light.enabled = true;
			showGarbage.gameObject.active=true;
			
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			
			//when the skillEffectTime is over stop the music
			giveAliceMusic.Stop();
			showGarbage.gameObject.active=false;
			GameObject.Find("policePointLight").light.enabled = false;

		}
		
		
		//skill level3
		if(carlevel2==3)
		{
			//how long the skill can play
			skillEffectTime=12.0f;
			
			//when start the skill the music will play
			giveAliceMusic.Play();
			
		    
			GameObject.Find("policePointLight").light.enabled = true;
			showGarbage.gameObject.active=true;
			
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			
			//when the skillEffectTime is over stop the music
			giveAliceMusic.Stop();
			showGarbage.gameObject.active=false;
			GameObject.Find("policePointLight").light.enabled = false;

		}
		
		//skill level4
		if(carlevel2==4)
		{
			//how long the skill can play
			skillEffectTime=13.0f;
			
			//when start the skill the music will play
			giveAliceMusic.Play();
			
		    
			GameObject.Find("policePointLight").light.enabled = true;
			showGarbage.gameObject.active=true;
			
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			
			//when the skillEffectTime is over stop the music
			giveAliceMusic.Stop();
			showGarbage.gameObject.active=false;
			GameObject.Find("policePointLight").light.enabled = false;

		}
		
		
		//skill level5
		if(carlevel2==5)
		{
			//how long the skill can play
			skillEffectTime=14.0f;
			
			//when start the skill the music will play
			giveAliceMusic.Play();
			
		    
			GameObject.Find("policePointLight").light.enabled = true;
			showGarbage.gameObject.active=true;
			
			
			//finish the skill after 5 seconds
			yield return new WaitForSeconds(skillEffectTime);
			
			//when the skillEffectTime is over stop the music
			giveAliceMusic.Stop();
			showGarbage.gameObject.active=false;
			GameObject.Find("policePointLight").light.enabled = false;

				
		}
		
	}
//-----------------------I added-------------------------------------------------		
	


	
	
	
	
}
