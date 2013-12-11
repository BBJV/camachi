using UnityEngine;
using System.Collections;

public class TireAttack : MonoBehaviour {
	private CarProperty selfCar;
	private float activeTime;
	
	void Initial (CarProperty car) {
		activeTime = Time.time;
		selfCar = car;
	}
	
	void OnTriggerEnter(Collider other) {
		CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
//		WhirlObstacle whirl = other.transform.root.GetComponent<WhirlObstacle>();
//		if(whirl)
//		{
//			whirl.rigidbody.AddForce(direction * 500);
//			return;
//		}
		if(collisionCar == selfCar && Time.time - activeTime <= 3.0f)
		{
			return;
		}
		if(collisionCar)
		{
			Hashtable args = new Hashtable();
			args.Add("second", 1.0f);
			args.Add("speed", collisionCar.rigidbody.velocity.magnitude * 0.5f);
//			collisionCar.StartCoroutine(collisionCar.SetSlip(1.0f,collisionCar.rigidbody.velocity.magnitude * 0.5f));
			collisionCar.SendMessage("SetSlip", args, SendMessageOptions.DontRequireReceiver);
			collisionCar.HitBySkill(3.0f);
			SendMessageUpwards("OnHitCar", collisionCar.transform.position, SendMessageOptions.DontRequireReceiver);
		}
    }
}
