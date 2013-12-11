//GarbageCollection Skill
//Written by ChunHua 2012/03/02

using UnityEngine;
using System.Collections;


public class GarbageCollection : Skill {
	

	public Transform targetPosition;
	private ArrayList eats = new ArrayList();
	private class EatedCar {
		public GameObject car;
		public float time;
	}
	
	
	//I added
	public float timer;
	
	//to show the button
	public Texture2D clickMe;
	
	//record the cars in the light
	public ArrayList carArray = new ArrayList();
	
	//record the cars which are eaten
	public ArrayList eatCars = new ArrayList();
	
	public float radius;
	
	// what's the level of the car
	public int carlevel2;
	
	//how long the skill can use
	public float skillEffectTime;

	
	//change the car's size
	public static float size = 1.0f;
	
	public float originalSize = 1.0f;
	
	public CarProperty _car;
	
	
//	DriftCar dc = new DriftCar();
	
	ArrayList effectArray = new ArrayList();
	
	int temp = 0;
	//Draw the button
	void OnGUI ()
	{	
		
		foreach(Transform t in carArray)
		{
			Vector3 screenPos = Camera.main.WorldToScreenPoint(t.position);
			GUI.DrawTexture(new Rect(screenPos.x - 20,Screen.height - screenPos.y - 20, 30, 30), clickMe);
			
			//if click the button
			if((CustomGUI.Button(new Rect(screenPos.x - 20,Screen.height - screenPos.y - 20, 30, 30), "click")))
			{
			    effectArray.Remove(t);
				effectArray.Add(t);
				
				//when the car eat other cars it will change its size
				 size=size+0.5f;
		
				
				t.gameObject.SetActiveRecursively(false);
				EatedCar eat = new EatedCar();
				eat.car = t.gameObject;
				temp++;
				eat.time = Time.time + temp * 5;
//				Debug.Log(eat.time + " timeNow :" + Time.time);
				eats.Add(eat);
//				GameObject.Find("truckCamera").camera.enabled = false;
//			    GameObject.Find("Camera").camera.enabled = false;
				_car.ChangeSize(size);
				
				//let eatCars = the cars are eaten	
//				eatCars.Add(t);
			}
		}
			foreach(Transform car in effectArray)
		{
			//Debug.Log(car.name);
			carArray.Remove(car);
		}

	}
	
	

	 public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(garbageCollection(car));
	}
	
//-----------------------I added-------------------------------------------------	
	IEnumerator garbageCollection (CarProperty car) {			
//		carlevel2 = dc.carLevel;

		_car = car;
		//skills play level1~level5
		if(carlevel2==1)
		{
			//how long the skill can play
			skillEffectTime=4.5f;

			// if skill played the car will light
			GameObject.Find("policePointLight").light.enabled = true;
			carArray.Clear();
			while(skillEffectTime > 0)
			{
				
				Collider[] targets = Physics.OverlapSphere(transform.position, radius);
				foreach(Collider target in targets)
				{
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						carArray.Remove(target.transform.root);
						carArray.Add(target.transform.root);
					}
				}
				foreach(Transform eCar in effectArray)
				{
					carArray.Remove(eCar);
				}
				skillEffectTime -= Time.deltaTime;
				yield return null;
			}
			carArray.Clear();
			
			
//		    yield return new WaitForSeconds(skillEffectTime);
//		    GameObject.Find("truckCamera").camera.enabled = true;
//			GameObject.Find("Camera").camera.enabled = true;
			GameObject.Find("policePointLight").light.enabled = false;
			_car.ChangeSize(1.0f);
			
			//let the cars which were eaten to show up again
			while(eats.Count > 0)
			{
				foreach(EatedCar ec in eats)
				{
//					Debug.Log(ec.car.name + ":" + ec.time);
					if(Time.time - ec.time - skillEffectTime >= 0)
					{
						ec.car.SetActiveRecursively(true);
						ec.car.transform.position= targetPosition.position;
						eatCars.Add(ec);
					}
					yield return null;
				}
				foreach(EatedCar ec in eatCars)
				{
					eats.Remove(ec);
				}
				eatCars.Clear();
			}
			
//			foreach(Transform c in eatCars)
//			{
//				c.gameObject.SetActiveRecursively(true);
//				c.transform.position= targetPosition.position;
//			}
//			eatCars.Clear();
			
		}
		
				
		if(carlevel2==2)
		{
			//how long the skill can play
			skillEffectTime=5.0f;

			// if skill played the car will light
			GameObject.Find("policePointLight").light.enabled = true;
			carArray.Clear();
			while(skillEffectTime > 0)
			{
				
				Collider[] targets = Physics.OverlapSphere(transform.position, radius);
				foreach(Collider target in targets)
				{
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						carArray.Remove(target.transform.root);
						carArray.Add(target.transform.root);
					}
				}
				skillEffectTime -= Time.deltaTime;
				yield return null;
			}
			carArray.Clear();
			
			
		    yield return new WaitForSeconds(skillEffectTime);
		    GameObject.Find("truckCamera").camera.enabled = true;
			GameObject.Find("Camera").camera.enabled = true;
			GameObject.Find("policePointLight").light.enabled = false;
			_car.ChangeSize(1.0f);
			
			//let the cars which were eaten to show up again
			foreach(Transform c in eatCars)
			{
				c.gameObject.SetActiveRecursively(true);
				c.transform.position= targetPosition.position;
			}
			eatCars.Clear();
	
		}
		
		if(carlevel2==3)
		{
			//how long the skill can play
			skillEffectTime=5.5f;

			// if skill played the car will light
			GameObject.Find("policePointLight").light.enabled = true;
			carArray.Clear();
			while(skillEffectTime > 0)
			{
				
				Collider[] targets = Physics.OverlapSphere(transform.position, radius);
				foreach(Collider target in targets)
				{
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						carArray.Remove(target.transform.root);
						carArray.Add(target.transform.root);
					}
				}
				skillEffectTime -= Time.deltaTime;
				yield return null;
			}
			carArray.Clear();

			
		    yield return new WaitForSeconds(skillEffectTime);
		    GameObject.Find("truckCamera").camera.enabled = true;
			GameObject.Find("Camera").camera.enabled = true;
			GameObject.Find("policePointLight").light.enabled = false;
			_car.ChangeSize(1.0f);
			
			//let the cars which were eaten to show up again
			foreach(Transform c in eatCars)
			{
				c.gameObject.SetActiveRecursively(true);
				c.transform.position= targetPosition.position;
			}
			eatCars.Clear();
	
		}
		
		if(carlevel2==4)
		{
			//how long the skill can play
			skillEffectTime=6.0f;

			// if skill played the car will light
			GameObject.Find("policePointLight").light.enabled = true;
			carArray.Clear();
			while(skillEffectTime > 0)
			{
				
				Collider[] targets = Physics.OverlapSphere(transform.position, radius);
				foreach(Collider target in targets)
				{
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						carArray.Remove(target.transform.root);
						carArray.Add(target.transform.root);
					}
				}
				skillEffectTime -= Time.deltaTime;
				yield return null;
			}
			carArray.Clear();
			
			
		    yield return new WaitForSeconds(skillEffectTime);
		    GameObject.Find("truckCamera").camera.enabled = true;
			GameObject.Find("Camera").camera.enabled = true;
			GameObject.Find("policePointLight").light.enabled = false;
			_car.ChangeSize(1.0f);
			
			//let the cars which were eaten to show up again
			foreach(Transform c in eatCars)
			{
				c.gameObject.SetActiveRecursively(true);
				c.transform.position= targetPosition.position;
			}
			eatCars.Clear();

		}
		
		if(carlevel2==5)
		{
			//how long the skill can play
			skillEffectTime=6.5f;

			// if skill played the car will light
			GameObject.Find("policePointLight").light.enabled = true;
			carArray.Clear();
			while(skillEffectTime > 0)
			{
				
				Collider[] targets = Physics.OverlapSphere(transform.position, radius);
				foreach(Collider target in targets)
				{
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						carArray.Remove(target.transform.root);
						carArray.Add(target.transform.root);
					}
				}
				skillEffectTime -= Time.deltaTime;
				yield return null;
			}
			carArray.Clear();
			
			
		    yield return new WaitForSeconds(skillEffectTime);
		    GameObject.Find("truckCamera").camera.enabled = true;
			GameObject.Find("Camera").camera.enabled = true;
			GameObject.Find("policePointLight").light.enabled = false;
			_car.ChangeSize(1.0f);
			
			//let the cars which were eaten to show up again
			foreach(Transform c in eatCars)
			{
				c.gameObject.SetActiveRecursively(true);
				c.transform.position= targetPosition.position;
			}
			eatCars.Clear();
	
		}
	
	}
//-----------------------I added-------------------------------------------------		



	
	
//----------------------------------------------------------------------------------	
	
}
