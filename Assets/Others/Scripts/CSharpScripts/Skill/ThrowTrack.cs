using UnityEngine;
using System.Collections;

public class ThrowTrack : MonoBehaviour {
	private CharacterController controller;
	public float speed;
	public float liveTime;
	public AudioClip roadBlockAudio;
	public CarProperty selfCar;
	private float activeTime;
	private string sidebump = "sidebump";
	public Vector3 direction;
	
	IEnumerator Start () {
		activeTime = Time.time;
		direction = transform.forward;
		controller = GetComponent<CharacterController>();
		if(selfCar)
		{
			BroadcastMessage("Initial", selfCar, SendMessageOptions.DontRequireReceiver);
		}
		yield return new WaitForSeconds(liveTime);
		if(gameObject)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		controller.Move((direction + (Vector3.down * 20 * Time.deltaTime)) * speed * Time.deltaTime);
//		controller.SimpleMove(transform.forward * speed);
	}
	
//	void OnTriggerEnter(Collider other) {
//		CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
////		WhirlObstacle whirl = other.transform.root.GetComponent<WhirlObstacle>();
////		if(whirl)
////		{
////			whirl.rigidbody.AddForce(direction * 500);
////			return;
////		}
//		if(collisionCar == selfCar && Time.time - activeTime <= 3.0f)
//		{
//			return;
//		}
//		if(collisionCar)
//		{
//			Hashtable args = new Hashtable();
//			args.Add("second", 1.0f);
//			args.Add("speed", collisionCar.rigidbody.velocity.magnitude * 0.5f);
////			collisionCar.StartCoroutine(collisionCar.SetSlip(1.0f,collisionCar.rigidbody.velocity.magnitude * 0.5f));
//			collisionCar.SendMessage("SetSlip", args, SendMessageOptions.DontRequireReceiver);
//			if(roadBlockAudio)
//				AudioSource.PlayClipAtPoint(roadBlockAudio, collisionCar.transform.position);
//			Destroy(gameObject);
//		}
//    }
	
	void OnHitCar (Vector3 soundPosition) {
		if(roadBlockAudio)
			AudioSource.PlayClipAtPoint(roadBlockAudio, soundPosition);
		Destroy(gameObject);
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.transform.tag == sidebump)
		{
			transform.rotation = Quaternion.LookRotation((direction + hit.normal) * 0.5f);
			direction = transform.forward;
		}
//		else
//		{
//			Rigidbody body = hit.collider.attachedRigidbody;
//	        if (body == null || body.isKinematic)
//	            return;
//	        
//	        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
//	        body.velocity = pushDir * 10;
//		}
    }
}
