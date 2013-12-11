using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]
public class AutoCar : MonoBehaviour {
	int NowNode = 0;
	public float speed = 10.0f;
	public float distance = 3.0f;
	public float speedRate = 1.0f;
	public float centerY = 0.35f;
	public float radius = 0.5f;
//	private CharacterController controller;
//	private BoxCollider boxCollider;
	private CarB carB;
	private Transform[] frontWheels;
	private Transform[] rearWheels;
	public CarSettings carSettings;
	private CharacterController character;
	private Quaternion lastRot = Quaternion.identity;
	
	void Start () {
//		controller = GetComponent<CharacterController>();
//		boxCollider = GetComponent<BoxCollider>();
		carB = GetComponent<CarB>();
		carB.enabled = false;
		if(carB)
		{
			carB.enabled = false;
			WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>();
			foreach(WheelCollider w in wheels)
			{
				Destroy(w.gameObject);
			}
		}
		if(!rigidbody)
		{
			Rigidbody rigid = gameObject.AddComponent<Rigidbody>();
			rigid.mass = 1500;
			rigid.angularDrag = 1000;
		}
		frontWheels = carSettings.frontWheels;
		rearWheels = carSettings.rearWheels;
		SetupWheelColliders();
//		character = gameObject.AddComponent<CharacterController>();
//		character.height = 1.0f;
//		character.center = new Vector3(0, centerY, 0);
//		CharacterController cc = GetComponent<CharacterController>();
//		cc.detectCollisions = false;
//		carB = transform.root.GetComponent<CarB>();
//		controller.enabled = true;
//		boxCollider.enabled = false;
		
	}
	
//	void OnEnable () {
//		if(controller)
//			controller.enabled = true;
//		if(boxCollider)
//			boxCollider.enabled = false;
//		if(carB)
//		{
//			carB.enabled = false;
//			WheelCollider[] wheels = GetComponentsInChildren<WheelCollider>();
//			foreach(WheelCollider w in wheels)
//			{
//				Destroy(w.gameObject);
//			}
//		}
//	}
	
//	void OnDisable () {
//		controller.enabled = false;
//		boxCollider.enabled = true;
//		if(carB)
//			carB.enabled = true;
//	}
	
	// Update is called once per frame
	Node target;
	void Update () {
//        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
//        Vector3 forward = transform.TransformDirection(Vector3.forward);
//        float curSpeed = speed * Input.GetAxis("Vertical");
        carB.Wait(true);
		Transform nextNode = PathNodeManager.SP.getNode(carB.NextNode);
		Node next = nextNode.GetComponent<Node>();
		Vector3 targetPosition = new Vector3(next.informations[0].position.x, transform.position.y, next.informations[0].position.z);
//		Vector3 targetDir = next.informations[0].veloDir;
//		Quaternion rhs = transform.rotation;
		foreach(Node.CarInformation info in target.informations)
		{
			if(Vector3.Dot(info.position - transform.position, transform.forward) <= 0 && Vector3.Distance(transform.position,info.position) < distance)
			{
				continue;
			}
			else
			{
				targetPosition = new Vector3(info.position.x, transform.position.y, info.position.z);
//				targetDir = info.veloDir;
//				speed = info.velo;
//				rhs = info.rotation;
				break;
			}
		}
//		Vector3 targetPosition = new Vector3(nextNode.position.x, transform.position.y, nextNode.position.z);
		Vector3 direction = targetPosition - transform.position;
		Debug.DrawRay(transform.position, direction);
		
//		transform.Translate(direction.normalized * speed * Time.deltaTime,Space.World);
//		transform.LookAt(targetPosition);
		Quaternion rhs = Quaternion.LookRotation(direction);
		float angle = Quaternion.Angle(lastRot, rhs);
		
//		if(angle > 5.0f)
//			carB.SetThrottle(-1.0f);
//		else
//			carB.SetThrottle(1.0f);
		transform.localRotation = Quaternion.Slerp(lastRot, rhs, Time.deltaTime * angle);
		lastRot = transform.localRotation;
		transform.Translate(transform.forward * Mathf.Clamp((speed - angle), 0.0f, speed) * Time.deltaTime, Space.World);
//		Debug.Log("velo : " + rigidbody.velocity.magnitude + " speed : " + Mathf.Clamp((speed - angle), 0.0f, speed));
//		character.SimpleMove(targetDir * speed);
//		if(Vector3.Distance(transform.position,targetPosition) < distance)
//		{
//			NextNode += 1;
//			if(NextNode >= PathNodeManager.SP.GetNodeLength()){
//				NextNode = 0;
//			}
//		}
//		speed += speedRate * Time.deltaTime;
	}
	
	void  SetupWheelColliders (){
			
//		int wheelCount = 0;
		
		foreach(Transform t in frontWheels)
		{
			SetupWheel(t);
		}
		
		foreach(Transform t in rearWheels)
		{
			SetupWheel(t);
		}
	}
	
	void SetupWheel (Transform wheelTransform){
		 GameObject go = new GameObject(wheelTransform.name + " Collider");
		go.transform.position = wheelTransform.position;
		go.transform.parent = transform;
		go.transform.rotation = wheelTransform.rotation;
		go.layer = LayerMask.NameToLayer("Wheel");
		WheelCollider wc = go.AddComponent(typeof(WheelCollider)) as WheelCollider;
		wc.radius = wheelTransform.renderer.bounds.size.y / 2;
	}
	
	void  OnTriggerEnter(Collider other){
			
		Node collNode = other.transform.GetComponent<Node>();
		if(!collNode)
		{
			return;
		}
		
		NowNode = collNode.GetIndex();
		
		target = PathNodeManager.SP.getNode(NowNode).GetComponent<Node>();
	}
}