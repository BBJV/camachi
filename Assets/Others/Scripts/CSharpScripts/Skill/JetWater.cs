using UnityEngine;
using System.Collections;

public class JetWater : Skill {
	public ParticleEmitter water;
	public SkillSettingRadius[] skillSetting;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(Jet());
	}
	
	IEnumerator Jet () {
		water.emit = true;
		float startTime = 0.0f;
		skillUsing = true;
		while(startTime < skillSetting[skillLevel - 1].skillEffectTimes)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, skillSetting[skillLevel - 1].radius);
			foreach(Collider target in targets)
			{
				if(Vector3.Angle(target.ClosestPointOnBounds(transform.position) - transform.position,transform.forward) < 22.5f)
				{
					
					if(target.transform.root.tag == "car" && 
					   (target.transform.root != transform.root))
					{
						CarProperty _car = target.transform.root.GetComponent<CarProperty>();
						if(!_car.IsProtect())
						{
							Vector3 direction = target.transform.root.position - transform.position;
					        direction = direction.normalized;
							_car.ChangeDirection(direction);		
						}
					}
					else if(target.transform.root.tag == "oil")
					{
						OilSpill oil = target.transform.root.GetComponent<OilSpill>();
						oil.Clean();
					}
					else if(target.transform.root.tag == "barrier")
					{
						Vector3 direction = target.transform.root.position - transform.position;
					    direction = direction.normalized;
						Rigidbody body = target.transform.root.rigidbody;
						body.AddForce(direction * body.mass * 50);
					}
					else if(target.transform.root.tag == "fire")
					{
						Destroy(target.transform.root.gameObject);
					}
				}
			}
			water.localVelocity = new Vector3(0, 0, rigidbody.velocity.magnitude + skillSetting[skillLevel - 1].radius * 2);
			startTime += Time.deltaTime;
			yield return null;
		}
		water.emit = false;
		skillUsing = false;
	}

//	void OnParticleCollision(GameObject other) {
//        Rigidbody body = other.rigidbody;
//		if(other.transform.root.tag == "car")
//		Debug.Log(other.transform.root.name);
//        if (body) {
//            Vector3 direction = other.transform.position - transform.position;
//            direction = direction.normalized;
//            body.AddForce(direction * 200000);
//        }
//    }
}
