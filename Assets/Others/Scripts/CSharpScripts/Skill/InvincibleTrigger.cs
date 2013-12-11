using UnityEngine;
using System.Collections;

public class InvincibleTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
		if(collisionCar && !other.isTrigger)
		{
			Hashtable args = new Hashtable();
			args.Add("second", 1.0f);
			args.Add("speed", rigidbody.velocity.magnitude * 0.5f);
			collisionCar.SendMessage("SetSlip", args, SendMessageOptions.DontRequireReceiver);
			collisionCar.HitBySkill(2.0f);
		}
    }
}
