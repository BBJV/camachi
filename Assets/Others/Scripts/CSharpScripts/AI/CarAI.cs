using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {

//	private int NextNode = -1;
//	private Transform CarTransform;
//	private DriftCar CarScriptInstance;
	private CarB CarBScript;
	public LayerMask ignoreLayers = -1;
	public float detectScale = 0.5f;
	public float driftRefer = 0.8f;
	public float raycastRadius = 1.0f;
//	private AVOID_ACTION MyAvoidAction;
	private int Index;
	private Vector3 targetPosition;
	private float maxThrottle = 1.0f;
	private Vector3 oldPosition;
	private CarProperty property;
	private bool isTurnBack = false;
	private bool isCheckStick = false;
	float angle;
	float angularVelo;
	// Constructor
//	public CarAIProtoType (int index) {
//		NextNode = 0;
//		MyAvoidAction = AVOID_ACTION.None;
//		Index = index;
//	}
	
	void Start () {
//		CarScriptInstance = GetComponent<DriftCar>();
		property = GetComponent<CarProperty>();
//		if(CarScriptInstance)
//			CarScriptInstance.SetEnableUserInput(false);
		CarBScript = GetComponent<CarB>();
//		if(CarBScript)
//			CarBScript.SetEnableUserInput(false);
//		SetCarTransform(transform);
		ignoreLayers = 1 << 2;
		ignoreLayers = ignoreLayers | (1 << 20);
		ignoreLayers = ~ignoreLayers;
		Throttle(1.0f);
		oldPosition = transform.position;
//		StartCoroutine(CheckNode());
//		StartCoroutine(UseSkill());
//		SendMessage("SetAIRandomLevel",SendMessageOptions.DontRequireReceiver);
	}
	
	// Update is called once per frame
//	IEnumerator ApplyCarRun () {
////		Transform node;
////		if(CarBScript)
////		{
////			if(NextNode != CarBScript.GetNextNode())
////			{
////				NextNode = CarBScript.GetNextNode();
////				node = PathNodeManager.SP.getNode(NextNode);
////				Vector3 newPosition = Random.insideUnitSphere;
////				targetPosition = node.position + new Vector3(newPosition.x,0,newPosition.z);
////				if(PathNodeManager.SP.IsCornerNode(NextNode))
////				{
////					targetPosition = node.TransformPoint(PathNodeManager.SP.GetCorner(NextNode));
////				}
////			}
////		}
////		if(CarScriptInstance)
////		{
////			if(NextNode != CarScriptInstance.GetNextNode())
////			{
////				NextNode = CarScriptInstance.GetNextNode();
////	//			while(PathNodeManager.SP.IsLimitNode(NextNode) && 
////	//		      ((rigidbody.velocity.magnitude < PathNodeManager.SP.MinSpeedNode(NextNode)) ||
////	//			       (rigidbody.velocity.magnitude > PathNodeManager.SP.MaxSpeedNode(NextNode))))
////	//			{
////	//				NextNode = NextNode + 1;
////	//				if(NextNode >= PathNodeManager.SP.GetNodeLength()){
////	//					NextNode = 0;
////	//				}
////	//			}
////	//			while(Vector3.Distance(CarTransform.position,PathNodeManager.SP.getNode(NextNode).position) < 20)
////	//			{
////	//				NextNode += 1;
////	//				if(NextNode >= PathNodeManager.SP.GetNodeLength()){
////	//					NextNode = 0;
////	//				}
////	//			}
////				node = PathNodeManager.SP.getNode(NextNode);
////				Vector3 newPosition = Random.insideUnitSphere;
////				targetPosition = node.position + new Vector3(newPosition.x,0,newPosition.z);
////				if(PathNodeManager.SP.IsCornerNode(NextNode))
////				{
////					targetPosition = node.TransformPoint(PathNodeManager.SP.GetCorner(NextNode));
////				}
////			}
////		}
////		
////		Debug.DrawLine(transform.position,targetPosition);
////		Vector3 temp = targetPosition - CarTransform.position;
////		Vector3 roadDir = CarTransform.TransformDirection(Vector3.right);
////		Vector3 cardir = CarTransform.TransformDirection(Vector3.forward);
////		RaycastHit hit;
////		float tempsteer = 0.0f;
////		float tempthrottle = 0.0f;
////		float tempDrift = 0.0f;
//////		if(MyAvoidAction == AVOID_ACTION.None){
////			tempsteer = Vector3.Dot(roadDir, temp.normalized) * 2;
//////		}
////		Vector3 leftPoint = new Vector3(0,0,0);
////		Vector3 rightPoint = new Vector3(0,0,0);
////		bool crash = false;
//////		if(MyAvoidAction == AVOID_ACTION.None || MyAvoidAction == AVOID_ACTION.Brake)
//////		{
//////			if(transform.name == "policeCar")
//////		Debug.Log("rigidbody.velocity.magnitude: " + rigidbody.velocity.magnitude);
//////		if(Physics.Raycast(CarTransform.position, rigidbody.velocity.normalized,out hit, rigidbody.velocity.magnitude,ignoreLayers) && hit.transform.root != transform.root)
////		if(rigidbody && Physics.SphereCast(CarTransform.position, raycastRadius, rigidbody.velocity.normalized,out hit, rigidbody.velocity.magnitude * detectScale,ignoreLayers) && hit.transform.root != transform.root)
//////		if(Physics.Linecast(CarTransform.position, new Vector3(targetPosition.x, CarTransform.position.y, targetPosition.z), ignoreLayers))
////		{
////			Debug.Log("Raycat : " + hit.transform.name);
////			if(Physics.Raycast(CarTransform.position, Quaternion.AngleAxis(30,Vector3.up) * rigidbody.velocity.normalized,out hit, rigidbody.velocity.magnitude * detectScale,ignoreLayers) && hit.transform.root != transform.root)
//////			if(Physics.Raycast(CarTransform.position, Quaternion.AngleAxis(45,Vector3.up) * cardir,out hit, rigidbody.velocity.magnitude * detectScale,ignoreLayers) && hit.transform.root != transform.root) //&& (hit.transform.tag == "sidebump" || hit.transform.tag == "car")
////			{
////				targetPosition = (targetPosition + -((CarTransform.right + CarTransform.forward) * detectScale) * rigidbody.velocity.magnitude * Time.deltaTime);
////				Debug.DrawLine(CarTransform.position,hit.point,Color.red);
////				rightPoint = hit.point - CarTransform.position;
//////				if(transform.name == "policeCar")
//////		Debug.Log(Vector3.Dot(roadDir, rightPoint));
//////				tempsteer -= 1 - Mathf.Clamp01(Vector3.Dot(roadDir, rightPoint.normalized));
//////				if(transform.name == "policeCar")
//////			Debug.Log(Vector3.Dot(roadDir, rightPoint.normalized));
//////		Debug.Log("right:" + tempsteer);
//////				Debug.Log(hit.transform.name);
////				crash = true;
////			}
////			if(Physics.Raycast(CarTransform.position, Quaternion.AngleAxis(-30,Vector3.up) * rigidbody.velocity.normalized,out hit, rigidbody.velocity.magnitude * detectScale,ignoreLayers) && hit.transform.root != transform.root)
//////			if(Physics.Raycast(CarTransform.position, Quaternion.AngleAxis(-45,Vector3.up) * cardir,out hit, rigidbody.velocity.magnitude * detectScale,ignoreLayers) && hit.transform.root != transform.root) //&& (hit.transform.tag == "sidebump" || hit.transform.tag == "car") 
////			{
////			targetPosition = (targetPosition + ((CarTransform.right + CarTransform.forward) * detectScale) * rigidbody.velocity.magnitude * Time.deltaTime);
////				Debug.DrawLine(CarTransform.position,hit.point,Color.red);
////				leftPoint = hit.point - CarTransform.position;
//////				if(transform.name == "policeCar")
//////		Debug.LogError(tempsteer);
//////				tempsteer += 1 - Mathf.Clamp01(Vector3.Dot(roadDir, leftPoint.normalized));
//////				if(transform.name == "policeCar")
//////		Debug.Log("left:" + tempsteer);
////				crash = true;
////			}
////		}
//////		}
//////		if(
//////		   (rigidbody.velocity.magnitude < 10 && Vector3.Dot(roadDir, leftPoint) < 1 && (leftPoint != Vector3.zero) && (rightPoint != Vector3.zero) && 
//////		   Mathf.Abs(Vector3.Dot(roadDir, leftPoint) + Vector3.Dot(roadDir, rightPoint)) < 0.25))
//////		{
//////			Debug.Log("Vector3.Dot(roadDir, leftPoint):" + Vector3.Dot(roadDir, leftPoint) + " Mathf.Abs(Vector3.Dot(roadDir, leftPoint) + Vector3.Dot(roadDir, rightPoint)):" + Mathf.Abs(Vector3.Dot(roadDir, leftPoint) + Vector3.Dot(roadDir, rightPoint)));
//////			tempthrottle += -0.1f;
//////			tempsteer = Vector3.Dot(roadDir, temp) / 5.0f;
//////			MyAvoidAction = AVOID_ACTION.Brake;
//////		}
//////		else
//////		{
//////			tempthrottle += 0.1f;
//////			MyAvoidAction = AVOID_ACTION.None;
//////		}
////		
//////		maxThrottle = 1 - Mathf.Abs(tempsteer);
//////		if(!crash)
//////		{
//////			tempthrottle += 0.1f;
//////			MyAvoidAction = AVOID_ACTION.None;
//////		}
//////		else
//////			maxThrottle = maxThrottle - 0.3f;
//////		Vector3 relativeVelocity = CarTransform.InverseTransformDirection(CarTransform.rigidbody.velocity);
//////		if(relativeVelocity.z > 20.0f)
//////			maxThrottle = Mathf.Clamp01(maxThrottle);
//////		else
//////			maxThrottle = Mathf.Clamp(maxThrottle, 0.5f, 1.0f);
//////		Throttle(tempthrottle);
//////		if(transform.name == "policeCar")
//////		Debug.Log(tempsteer);
////		tempsteer = Mathf.Clamp(tempsteer, -1.0f, 1.0f);
////		if(rigidbody && rigidbody.velocity.magnitude <= 3.0f && (Physics.Raycast(CarTransform.position, transform.forward, out hit, 3.0f, ignoreLayers) && hit.transform.root != transform.root) && (Mathf.Abs(Vector3.Dot(roadDir, leftPoint) + Vector3.Dot(roadDir, rightPoint)) < 0.25))
////		{
////			Throttle(-1.0f);
////		}
////		else
////		{
////			Throttle(1.0f);
////		}
////		Steer(tempsteer);
////		
////		if(rigidbody && (tempsteer >= driftRefer || tempsteer <= -driftRefer) && rigidbody.velocity.magnitude > 20 && Vector3.Dot(rigidbody.velocity, CarTransform.forward) > 0)
////			Drift(true);
////		else
////			Drift(false);
//		while(true)
//		{
//			if(!CarBScript.IsWait())
//			{
//				yield return StartCoroutine(CheckNode());
//			}
//			else
//			{
//				yield return null;
//			}
////			yield return StartCoroutine(CheckStick());
//			property.AddEnergy(0.1f * Time.deltaTime);
////			yield return null;
//		}
//	}
	
	IEnumerator CheckStick () {
		if(isCheckStick)
		{
			yield break;
		}
		isCheckStick = true;
		while(oldPosition == transform.position)
		{
			switch(Random.Range(0, 4))
			{
				case 0:
					Steer(1.0f);
					Throttle(1.0f);
					break;
				case 1:
					Steer(-1.0f);
					Throttle(1.0f);
					break;
				case 2:
					Steer(1.0f);
					Throttle(-1.0f);
					break;
				case 3:
					Steer(-1.0f);
					Throttle(-1.0f);
					break;
			}
			yield return null;
		}
		Throttle(1.0f);
		isCheckStick = false;
	}
	
	Node nodeNext;
	int nowNode;
	void FixedUpdate () {
		property.UseSkill();
		if(!CarBScript.IsWait() && !isCheckStick && !isTurnBack && !property.CheckSlip() && CarBScript.GetIsCanSteer())
		{
			if(CarBScript)
			{
				if(nowNode != CarBScript.NowNode)
				{
//					NextNode = CarBScript.GetNextNode();
					nowNode = CarBScript.NowNode;
					nodeNext = PathNodeManager.SP.GetNode(nowNode);
				}
			}
			Node.CarInformation tempInfo = PathNodeManager.SP.GetNode(CarBScript.NextNode).informations[0];
			Vector3 temp = tempInfo.veloDir;
			targetPosition = tempInfo.position;
			bool canTrans = false;
			if(nodeNext)
			{
				foreach(Node.CarInformation info in nodeNext.informations)
				{
//					Debug.DrawRay(info.position, info.veloDir);
					if(Vector3.Dot(info.position - transform.position, temp) <= 0)
					{
						continue;
					}
					
					if(Vector3.Distance(info.position, transform.position) > detectScale)
					{
						temp = targetPosition - transform.position;
					}
					else
					{
						tempInfo = info;
						canTrans = true;
					}
					break;
				}
			}
//				Debug.DrawRay(transform.position,rigidbody.velocity,Color.red);
			
	//		Vector3 temp = targetPosition - CarTransform.position;
//			if(Vector3.Dot(rigidbody.velocity, transform.forward) < 0)
//			{
//				Debug.DrawRay(transform.position,rigidbody.velocity,Color.black);
//			}
			if(canTrans) // && Vector3.Dot(rigidbody.velocity, transform.forward) >= 0
			{
//				Throttle(1.0f);
				if(rigidbody.velocity.magnitude > tempInfo.velo - 20)
				{
	//				transform.position = tempInfo.position;
					transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, tempInfo.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
				}
				else
				{
					Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
					Vector3 selfVeloDir = rotate * rigidbody.velocity.normalized;
					float tempsteer = Vector3.Dot(tempInfo.veloDir, selfVeloDir);
					Steer(tempsteer);
					if(Vector3.Dot(rigidbody.velocity, transform.forward) < 0 && rigidbody.velocity.magnitude < 5)
					{
						transform.Rotate(transform.up, Vector3.Angle(rigidbody.velocity, transform.forward));
					}
				}
				
				rigidbody.velocity = rigidbody.velocity.magnitude * tempInfo.veloDir;
				if(Vector3.Angle(rigidbody.velocity, transform.forward) > 25)
				{
					CarBScript.SetDrift();
				}
				if(!CarBScript.IsCarMaxSpeed())
				{
					rigidbody.AddForce(rigidbody.velocity * 10);
				}
//				Debug.DrawRay(transform.position,rigidbody.velocity,Color.red);
			}
			else
			{
//				Vector3 roadDir = CarTransform.TransformDirection(Vector3.right);
//				Vector3 cardir = CarTransform.TransformDirection(Vector3.forward);
//				RaycastHit hit;
				float tempsteer = 0.0f;
//				float tempthrottle = 0.0f;
//				float tempDrift = 0.0f;
				
				Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
				Vector3 selfVeloDir = rotate * rigidbody.velocity.normalized;
				tempsteer = Vector3.Dot(temp, selfVeloDir);
		//		Debug.Log("rigidbody.velocity.normalized : " + rigidbody.velocity.normalized + " temp : " + temp + " tempsteer : " + tempsteer + " selfVeloDir : " + selfVeloDir);
				tempsteer = Mathf.Clamp(tempsteer, -1.0f, 1.0f);
		//		if(rigidbody && rigidbody.velocity.magnitude <= 3.0f && (Physics.Raycast(CarTransform.position, transform.forward, out hit, 3.0f, ignoreLayers) && hit.transform.root != transform.root) && (Mathf.Abs(Vector3.Dot(roadDir, leftPoint) + Vector3.Dot(roadDir, rightPoint)) < 0.25))
		//		{
		//			Throttle(-1.0f);
		//		}
		//		else
		//		{
		//			Throttle(1.0f);
		//		}
		//		transform.LookAt(targetPosition);
		//		Steer(tempsteer);
				int layerMask =  1 << 2;
				layerMask = layerMask | (1 << 12);
				layerMask = ~layerMask;
	//			Debug.Log(rigidbody.velocity.magnitude);
				if(rigidbody.velocity.magnitude < 1f && Physics.Raycast(transform.position, transform.forward, 5.0f, layerMask) && !isTurnBack && !isCheckStick)
				{
					StartCoroutine(TurnBack(tempsteer));
	//				Debug.Log("back");
				}
				
				if(oldPosition == transform.position && !isTurnBack && !isCheckStick)
				{
					StartCoroutine(CheckStick());
				}
		//		else
		//		{
		//			Throttle(1.0f);
		//		}
				
				if(rigidbody && !CarBScript.IsCarDrift() && (tempsteer >= driftRefer || tempsteer <= -driftRefer) && rigidbody.velocity.magnitude > 20)
				{
					Steer(Mathf.Clamp(tempsteer * 10, -1.0f, 1.0f));
					Drift(true);
				}
				else
				{
					Steer(tempsteer);
					Drift(false);
				}
	//			Debug.Log(rigidbody.velocity * rigidbody.mass * 100 * Time.deltaTime);
	//			rigidbody.AddForce(rigidbody.velocity * rigidbody.mass * 100 * Time.deltaTime);
//				Debug.DrawRay(transform.position,rigidbody.velocity,Color.blue);
			}
		}
	}
	
	IEnumerator TurnBack (float steer) {
		if(isTurnBack)
		{
			yield break;
		}
		isTurnBack = true;
		int layerMask =  1 << 2;
		layerMask = layerMask | (1 << 12);
		layerMask = ~layerMask;
		while(oldPosition != transform.position && Physics.Raycast(transform.position, transform.forward, 10.0f, layerMask))
		{
			Steer(steer);
//			Throttle(-1.0f);
			CarBScript.Brake();
			if(Vector3.Dot(transform.forward, rigidbody.velocity) < 0)
			{
				rigidbody.velocity *= 1.1f;
			}
			transform.rotation *= Quaternion.AngleAxis(steer, Vector3.up);
			yield return null;
		}
		rigidbody.velocity = transform.forward * 2;
//		Throttle(1.0f);
		CarBScript.UnBrake();
		isTurnBack = false;
	}
	
	IEnumerator UseSkill () {
		while (true)
		{
			if(this.enabled)
			{
//				int randomIndex = Random.Range(0, 4);
//				while(property.CheckEnergy() < randomIndex)
//				{
//					yield return null;
//				}
				property.UseSkill();
			}
			yield return new WaitForSeconds(1.0f);
		}
	}
	
	void Throttle(float throttle){
		if(CarBScript)
		{
			CarBScript.SetThrottle(throttle);
		}
//		if(CarScriptInstance)
//		{
//			CarScriptInstance.SetThrottle(Mathf.Clamp(CarScriptInstance.GetThrottle() + throttle, -1.0f, maxThrottle));
//		}
	}
	
	void Steer(float steer){
		if(CarBScript)
		{
			if(CarBScript.GetThrottle() < 0)
				steer = -steer;
			CarBScript.SetSteer(steer);
		}
//		if(CarScriptInstance)
//		{
//			if(CarScriptInstance.GetThrottle() < 0) // if on turn back time
//				steer = -steer;
//			CarScriptInstance.SetSteer(steer);
//		}
	}
	
	void Drift(bool isDrift){
		if(CarBScript)
		{
			if(isDrift)
			{
				CarBScript.SetThrottle(-1f);
			}
			else
			{
				CarBScript.SetThrottle(1f);
			}
		}
//		if(CarScriptInstance)
//		{
//			if(isDrift)
//				CarScriptInstance.SetHandBrake(1);
//			else
//				CarScriptInstance.SetHandBrake(0);
//		}
	}
	
//	void UseItem(){
//		for(int i = 0 ; i < CarScriptInstance.Props.Length ; i++){
//			CarScriptInstance.UseProps(i);
//		}
//	}
	
//	public void SetCarTransform(Transform transform){
//		CarTransform = transform;
//	}
	
//	void OnCollisionEnter (Collision other) {
//		foreach(ContactPoint contactPoint in other.contacts)
//		{
//			Vector3 hitPoint = contactPoint.point - transform.position;
//			Steer(Mathf.Clamp(Vector3.Dot(transform.right, hitPoint), -1.0f, 1.0f));
//		}
//	}
	
//	public void SetCarInstance(Car car){
//		CarScriptInstance = car;
//	}
}
