using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]
public class DriftCar : MonoBehaviour {
	
	//maximal corner and braking acceleration capabilities
	public float maxCornerAccel = 10.0f;
	public float maxBrakeAccel = 10.0f;
	public float frictionAccel = 2.0f;
	public float fallDownAccel = 5.0f;
	
	//Mass
	public float mass = 1500;
	
	//coefficient of drag
	private float drag = 0f;
	
	//center of gravity height - effects tilting in corners
	public float cogY = 0.0f;
	
	//engine powerband
//	public float minRPM = 700;
	private float maxRPM = 6000;
	
	public float[] gearSpeeds;
	public float[] gearTimes;
	
	//maximum Engine Torque
	private float maxTorque = 400;
	
	//automatic transmission shift points
//	public float shiftDownRPM = 2500;
//	public float shiftUpRPM = 5500;
	
	//gear ratios
//	public float[] gearRatios = {-2.66f, 2.66f, 1.78f, 1.30f, 1.00f};
//	public float finalDriveRatio = 3.4f;
	
	//a basic handling modifier:
	//1.0 understeer
	//0.0 oversteer
	public float handlingTendency = 0.7f;
	
	//graphical wheel objects
	public Transform wheelFR;
	public Transform wheelFL;
	public Transform wheelBR;
	public Transform wheelBL;
	public Transform[] otherWheels;
	//suspension setup
	public float suspensionDistance = 0.3f;
	public float springs = 1000;
	public float dampers = 200;
	public float wheelRadius = 0.45f;
	
	//particle effect for ground dust
	public Transform groundDustEffect;
	
	public bool queryUserInput = true;
	private float engineRPM;
	private float steerVelo = 0.0f;
//	private float brake = 0.0f;
	private float handbrake = 0.0f;
	private float steer = 0.0f;
	private float motor = 0.0f;
//	private float skidTime = 0.0f;
	private bool onGround = false;
	private float cornerSlip = 0.0f;
	private float driveSlip = 0.0f;
	private float wheelRPM;
	private int gear = 1;
	private Skidmarks skidmarks;
	private WheelData[] wheels;
//	private float wheelY = 0.0f;
	public int NextNode = 0;
	public int NowNode = 0;
	private int NodeWeights = 0;
	public int round = 0;
	private float resetTimer = 0.0f;
	public float resetTime = 5.0f;
	private CarAI carAI;
	public bool nonGUI = true;
	public float throttle = 1.0f;
	private float reboundForce = 10.0f;
//	private float speedRatio = 1.0f;
	private bool canBrake = true;
	private bool canSteer = true;
	private float defaultMaxRPM;
	private float defaultMaxTorque;
	private bool timeCount = false;
	private float timeRace = 0.0f;
	private Vector3 allForce;
	private SmoothFollow SMCamera;
	public Texture touchTexture;
	//Functions to be used by external scripts 
	//controlling the car if required
	//===================================================================
	
	void OnGUI () {
		if(SMCamera.target == transform)
		{
			if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			{
				if(GUI.Button(new Rect(Screen.width - 150 - 50,Screen.height - 100 - 50, 100, 100), "Brake") || GUI.Button(new Rect(100,Screen.height - 100 - 50, 100, 100), "Brake"))
				{
				}
				foreach (Touch touch in Input.touches) {
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						GUI.DrawTexture(new Rect(touch.position.x - touchTexture.width * 0.5f,Screen.height - touch.position.y - touchTexture.height * 0.5f, touchTexture.width, touchTexture.height), touchTexture);
					}
				}
			}
		}
		if(nonGUI)
			return;
		GUI.Label(new Rect(0,0,100,100),"v="+(rigidbody.velocity.magnitude * 3.6f).ToString("f1") + " km/h\ngear= "+gear+"\nrpm= "+engineRPM.ToString("f0"));
		carAI.enabled =  GUI.Toggle(new Rect(0,50,100,100),carAI.enabled,"AI run");
		GUI.Label(new Rect(0,70,100,100),"Max Torque");
//		maxTorque =  GUI.HorizontalSlider (new Rect (80, 80, 100, 30), maxTorque, 400.0f, 1000.0f);
		GUI.Label(new Rect(0,95,100,100),"Max RPM");
//		maxRPM =  GUI.HorizontalSlider (new Rect (80, 105, 100, 30), maxRPM, 5600.0f, 10000.0f);
		throttle = GUI.HorizontalSlider (new Rect (80, 125, 100, 30), throttle, 0.0f, 1.0f);
		GUI.Label(new Rect(Screen.width - 100, 0, 100, 100),"reboundForce");
		reboundForce = GUI.HorizontalSlider(new Rect(Screen.width - 100, 25, 100, 10), reboundForce, 10.0f, 100.0f);
//		GUI.Label(new Rect(Screen.width - 200, 200, 200, 100),"time count = " + timeRace);
		
			
	}
	
	//Setup main camera to follow vehicle
	void SetupCamera() {
		if(Camera.main.GetComponent<SmoothFollow>() != null)
		{
			Camera.main.GetComponent<SmoothFollow>().enabled=true;
			Camera.main.GetComponent<SmoothFollow>().target=transform;
			Camera.main.GetComponent<SmoothFollow>().distance=4;
			Camera.main.GetComponent<SmoothFollow>().height=1;
		}
		Camera.main.transform.parent=null;
	}
	
	//Enable or disable user controls
	public void SetEnableUserInput(bool enableInput)
	{
		queryUserInput=enableInput;
	}
	
	//Car physics
	//===================================================================
	
	//some whee calculation data
	class WheelData{
		public float rotation = 0.0f;
		public WheelCollider coll;
		public Transform graphic;
		public float maxSteerAngle = 0.0f;
		public int lastSkidMark = -1;
		public bool powered = false;
		public bool handbraked = false;
		public Quaternion originalRotation;
	};
	
	void Start () {
		//Destroy existing rigidbody, we don't want anyone to mess with it.
		if(rigidbody)
			Destroy(rigidbody);
		
		//setup rigidbody	
		gameObject.AddComponent<Rigidbody>();
		rigidbody.mass = mass;
		rigidbody.drag = drag;
		rigidbody.angularDrag = 0.05f;
		rigidbody.centerOfMass = new Vector3(rigidbody.centerOfMass.x, cogY, rigidbody.centerOfMass.z);
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
//		rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
	
		//start engine noise
		audio.loop = true;
		audio.Play();
		SMCamera = Camera.main.GetComponent<SmoothFollow>();
		//setup wheels
		wheels=new WheelData[4 + otherWheels.Length];
		for(int i=0;i<4 + otherWheels.Length;i++)
			wheels[i] = new WheelData();
			
		wheels[0].graphic = wheelFL;
		wheels[1].graphic = wheelFR;
		wheels[2].graphic = wheelBL;
		wheels[3].graphic = wheelBR;
		for(int i=0;i < otherWheels.Length;i++)
		{
			wheels[i + 4].graphic = otherWheels[i];
		}
		wheels[0].maxSteerAngle = 30.0f;
		wheels[1].maxSteerAngle = 30.0f;
		wheels[2].powered = true;
		wheels[3].powered = true;
		wheels[2].handbraked = true;
		wheels[3].handbraked = true;
	
		foreach(WheelData w in wheels)
		{
			if(w.graphic == null)
				Debug.Log("You need to assign all four wheels for the car script!");
			if(!w.graphic.transform.IsChildOf(transform))	
				Debug.Log("Wheels need to be children of the Object with the car script");
				
			w.originalRotation = w.graphic.localRotation;
	
			//create collider
			GameObject colliderObject = new GameObject("WheelCollider");
			colliderObject.transform.parent = transform;
			colliderObject.transform.localPosition = w.graphic.localPosition;
			colliderObject.transform.localRotation = w.graphic.localRotation;
			w.coll = colliderObject.AddComponent<WheelCollider>();
			w.coll.suspensionDistance = suspensionDistance;
			JointSpring js = new JointSpring();
			js.spring = springs;
			js.damper = dampers;
//			w.coll.suspensionSpring.spring = springs;
//			w.coll.suspensionSpring.damper = dampers;
			w.coll.suspensionSpring = js;
			//no grip, as we simulate handling ourselves
			WheelFrictionCurve forwardwfc = new WheelFrictionCurve();
			forwardwfc.stiffness = 0;
			WheelFrictionCurve sidewaywfc = new WheelFrictionCurve();
			sidewaywfc.stiffness = 0;
//			w.coll.forwardFriction.stiffness = 0;
//			w.coll.sidewaysFriction.stiffness = 0;
			w.coll.forwardFriction = forwardwfc;
			w.coll.sidewaysFriction = sidewaywfc;
			w.coll.radius = wheelRadius;
		}	
	
		//get wheel height (height forces are applied on)
//		wheelY = wheels[0].graphic.localPosition.y;
		
		//find skidmark object
		skidmarks = FindObjectOfType(typeof(Skidmarks)) as Skidmarks;
		
		//shift to first
		gear = 1;
		defaultMaxRPM = maxRPM;
		defaultMaxTorque = maxTorque;
		carAI = GetComponent<CarAI>();
	}
	
	//update wheel status
	void UpdateWheels()
	{
		//calculate handbrake slip for traction gfx
	 	float handbrakeSlip = handbrake*rigidbody.velocity.magnitude*0.1f;
		if(handbrakeSlip>1)
			handbrakeSlip=1;
			
		float totalSlip = 0.0f;
		onGround=false;
		foreach(WheelData w in wheels)
		{		
			//rotate wheel
			w.rotation += wheelRPM / 60.0f * 360.0f* Time.fixedDeltaTime;
			w.rotation = Mathf.Repeat(w.rotation, 360.0f);		
			w.graphic.localRotation= Quaternion.Euler( w.rotation, w.maxSteerAngle*steer, 0.0f ) * w.originalRotation;
	
			//check if wheel is on ground
			if(w.coll.isGrounded)
				onGround=true;
				
			float slip = cornerSlip+(w.powered?driveSlip:0.0f)+(w.handbraked?handbrakeSlip:0.0f);
			totalSlip += slip;
			
			WheelHit hit;
			WheelCollider c;
			c = w.coll;
			if(c.GetGroundHit(out hit))
			{
				//if the wheel touches the ground, adjust graphical wheel position to reflect springs
//				w.graphic.localPosition.y-=Vector3.Dot(w.graphic.position-hit.point,transform.up)-w.coll.radius;
				float py = w.graphic.localPosition.y;
				py -= Vector3.Dot(w.graphic.position-hit.point,transform.up)-w.coll.radius;
//				w.graphic.localPosition = new Vector3(w.graphic.localPosition.x, py, w.graphic.localPosition.z);
				//create dust on ground if appropiate
				if(slip>0.5f) // && hit.collider.tag=="Dusty"
				{
					if(groundDustEffect)
					{
						groundDustEffect.position=hit.point;
						groundDustEffect.particleEmitter.worldVelocity=rigidbody.velocity*0.5f;
	//					groundDustEffect.particleEmitter.minEmission=(slip-0.5f)*3;
	//					groundDustEffect.particleEmitter.maxEmission=(slip-0.5f)*3;
						groundDustEffect.particleEmitter.minEmission=(slip-0.5f)*2f;
						groundDustEffect.particleEmitter.maxEmission=(slip-0.5f)*2f;
						groundDustEffect.particleEmitter.Emit();								
					}
				}
				
				//and skid marks				
				if(slip>0.75f && skidmarks != null)
					w.lastSkidMark=skidmarks.AddSkidMark(hit.point,hit.normal,(slip-0.75f)*2,w.lastSkidMark);
				else
					w.lastSkidMark=-1;
			}
			else w.lastSkidMark=-1;
		}
		totalSlip/=wheels.Length;
	}
	
	//Automatically shift gears
	void AutomaticTransmission()
	{
//		if(gear>0)
//		{
//			if(engineRPM>shiftUpRPM&&gear<gearRatios.Length-1)
//				gear++;
//			if(engineRPM<shiftDownRPM&&gear>1)
//				gear--;
//		}
		if(gear > 0)
		{
			if(rigidbody.velocity.magnitude * 3.6f > gearSpeeds[gear] && gear < gearSpeeds.Length - 1)
			{
				gear++;
			}
			else if(gear > 1 && rigidbody.velocity.magnitude * 3.6f < gearSpeeds[gear - 1])
			{
				gear--;
			}
		}
	}
	
	//Calculate engine acceleration force for current RPM and trottle
	float CalcEngine()
	{
		//no engine when braking
//		if(brake+handbrake>0.1f)
//		if(handbrake>0.1f)
//		{
//			if(engineRPM >= minRPM)
//				motor=0.0f;
//		}
		
		//if car is airborne, just rev engine
		if(!onGround)
		{
//			engineRPM += (motor-0.3f)*25000.0f*Time.deltaTime;
//			engineRPM = Mathf.Clamp(engineRPM,minRPM,maxRPM);
			return 0.0f;
		}
		else
		{
//			float x;
//			float torqueCurve;
//			float torqueToForceRatio;
			AutomaticTransmission();
//			engineRPM=wheelRPM*gearRatios[gear]*finalDriveRatio;
//			if(gear == 0)
//			{
//				engineRPM = Mathf.Clamp(engineRPM,-maxRPM,-minRPM);
//				if(engineRPM > -maxRPM)
//				{
//					//fake a basic torque curve
//					x = (2*((-engineRPM)/maxRPM)-1);
//				}
//				else
//					//rpm delimiter
//					return 0.0f;
//			}
//			else
//			{
//				engineRPM = Mathf.Clamp(engineRPM,minRPM,maxRPM);
//				if(engineRPM < maxRPM)
//				{
//					//fake a basic torque curve
//					x = (2*(engineRPM/maxRPM)-1);
//				}
//				else
//					//rpm delimiter
//					return 0.0f;
//			}
//			if(engineRPM > -maxRPM && engineRPM < maxRPM)
//			{
//				torqueCurve = 0.5f*(-x*x+2);
//				torqueToForceRatio = gearRatios[gear]*finalDriveRatio/wheelRadius;
//				return motor*maxTorque*torqueCurve*torqueToForceRatio;
//			}
//			else
//				return 0.0f;
			if(rigidbody.velocity.magnitude * 3.6f >= gearSpeeds[gearSpeeds.Length - 1] || (gear == 0 && rigidbody.velocity.magnitude * 3.6f >= -gearSpeeds[0]))
				return 0.0f;
			if(handbrake>0.1f && gear != 0)
				return 0.0f;
			else
			{
				if(gear > 1)
				{
//					Debug.Log(gear + " gear : " + (gearSpeeds[gear] - gearSpeeds[gear - 1]) / gearTimes[gear]);
//					return 0.446f * rigidbody.mass * (gearSpeeds[gear] - gearSpeeds[gear - 1]) / gearTimes[gear];
					return rigidbody.mass * ((gearSpeeds[gear] - gearSpeeds[gear - 1]) / (gearTimes[gear]) * 10 + 9.81f) * Time.fixedDeltaTime;
//					return Time.fixedDeltaTime * rigidbody.mass * rigidbody.velocity.magnitude * 10.1f;
					
				}
				else if(gear == 1)
				{
//					Debug.Log(gear + " gear : " + gearSpeeds[gear]  / gearTimes[gear]);
//					return rigidbody.mass * ((gearSpeeds[1] - (rigidbody.velocity.magnitude * 3.6f)) / (gearTimes[1] * Time.fixedDeltaTime));
					return rigidbody.mass * (gearSpeeds[gear] / (gearTimes[gear]) * 10 + 9.81f) * Time.fixedDeltaTime;
//					return Time.fixedDeltaTime * (rigidbody.mass * ((rigidbody.velocity.magnitude * 10.1f) + gearSpeeds[gear]  / (gearTimes[gear] * gearTimes[gear]) * 43f));
				}
				else
				{
					return rigidbody.mass * (gearSpeeds[gear] / (gearTimes[gear]));
				}
			}
		}
	}
	
	float GetGearTime (int gear) {
		float gearTime;
		if(gear == 0)
		{
			gearTime = gearTimes[gear];
		}
		else
		{
			gearTime = 0.0f;
		}
		for(;gear > 0;gear--)
		{
			gearTime += gearTimes[gear];
		}
		return gearTime;
	}
	
	//Car physics
	//The physics of this car are really a trial-and-error based extension of 
	//basic "Asteriods" physics -- so you will get a pretty arcade-like feel.
	//This may or may not be what you want, for a more physical approach research
	//the wheel colliders
	void HandlePhysics () {
		Vector3 velo=rigidbody.velocity;
		wheelRPM=velo.magnitude*60.0f*0.5f;
	
		rigidbody.angularVelocity=new Vector3(rigidbody.angularVelocity.x,0.0f,rigidbody.angularVelocity.z);
		Vector3 dir = transform.TransformDirection(Vector3.forward);
		Vector3 flatDir= Vector3.Normalize(new Vector3(dir.x,0,dir.z));
		Vector3 flatVelo = new Vector3(velo.x,0,velo.z);
		float rev = Mathf.Sign(Vector3.Dot(flatVelo,flatDir));
		//when moving backwards or standing and brake is pressed, switch to reverse
		if((rev<0||flatVelo.sqrMagnitude<0.5f)&&handbrake>0.1f)
			gear=0;
		if(gear==0)
		{	
			//when in reverse, flip brake and gas
			float tmp = handbrake;
//			handbrake = motor;
			motor = tmp;
			
			//when moving forward or standing and gas is pressed, switch to drive
			if(handbrake==0)
				gear=1;
		}
		Vector3 engineForce = flatDir * CalcEngine();
//		float totalbrake = brake + handbrake*0.5f;
		float totalbrake = handbrake;
		if(totalbrake>1.0f)totalbrake=1.0f;
		Vector3 brakeForce = -flatVelo.normalized * totalbrake * rigidbody.mass * maxBrakeAccel;
		Vector3 frictionForce = -flatVelo.normalized * Mathf.Abs(Vector3.Dot(flatVelo.normalized,transform.right)) * rigidbody.mass * frictionAccel;
		flatDir*=flatVelo.magnitude;
		flatDir=Quaternion.AngleAxis(steer*30.0f,Vector3.up)*flatDir;
		flatDir*=rev;
		float diff=(flatVelo-flatDir).magnitude;
		float cornerAccel=maxCornerAccel;
		if(cornerAccel>diff)cornerAccel=diff;
		Vector3 cornerForce = -(flatVelo-flatDir).normalized * cornerAccel * rigidbody.mass * frictionAccel;
		cornerSlip=Mathf.Pow(cornerAccel/maxCornerAccel,3);
//		rigidbody.AddForceAtPosition(brakeForce+engineForce+cornerForce,(wheels[2].graphic.position + wheels[3].graphic.position) * 0.5f);
//		Debug.DrawRay(transform.position,flatVelo,Color.red);
//		Debug.DrawRay(transform.position,brakeForce,Color.black);
//		Debug.DrawRay(transform.position,engineForce,Color.yellow);
//		Debug.DrawRay(transform.position,cornerForce,Color.green);
//		Debug.DrawRay(transform.position,frictionForce,Color.gray);
//		Debug.DrawRay(transform.position,brakeForce+engineForce+cornerForce+frictionForce,Color.blue);
		allForce = brakeForce+engineForce+cornerForce;
		rigidbody.AddForce((brakeForce+engineForce+cornerForce+frictionForce));
		float handbrakeFactor = 1 + handbrake * 4;
		if(rev<0)
			handbrakeFactor=1;
		float veloSteer=((15 / (2 * velo.magnitude + 1)) + 1) * handbrakeFactor;
		float steerGrip=(1 - handlingTendency * cornerSlip);
		if(rev*steer*steerVelo<0)
			steerGrip=1;
		float maxRotSteer = 2 * Time.fixedDeltaTime * handbrakeFactor * steerGrip;
		
		float fVelo = velo.magnitude;
		float veloFactor = fVelo < 1.0f ? fVelo : Mathf.Pow(velo.magnitude,0.3f);
		float steerVeloInput = rev * steer * veloFactor * 0.5f * Time.fixedDeltaTime * handbrakeFactor;
		if(velo.magnitude<0.1f)
			steerVeloInput=0;
		if(steerVeloInput>steerVelo)
		{
			steerVelo += 0.5f * Time.fixedDeltaTime * veloSteer;
			if(steerVeloInput<steerVelo)
				steerVelo=steerVeloInput;
		}
		else
		{
			steerVelo -= 0.5f*Time.fixedDeltaTime*veloSteer;
			if(steerVeloInput>steerVelo)
				steerVelo=steerVeloInput;
		}
		steerVelo=Mathf.Clamp(steerVelo,-maxRotSteer,maxRotSteer);
		transform.Rotate(Vector3.up*steerVelo*57.295788f);
	}
	
	float oldY = 0.0f;
	void FixedUpdate () {
		//query input axes if necessarry
		if(!carAI.enabled)
//		if(queryUserInput)
		{
//			brake = Mathf.Clamp01(-Input.GetAxis("Vertical"));
			if(Input.GetKey(KeyCode.Escape)){
				Application.Quit();
			}
			if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			{
				foreach (Touch touch in Input.touches) {
					if(touch.position.y < 150 && touch.position.y > 50 && ((touch.position.x > 100 && touch.position.x < 200) || (touch.position.x > Screen.width - 150 - 50 && touch.position.x < Screen.width - 100)))
					{
						if(canBrake)
						{
							if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
							{
								handbrake = 1;
							}
							else
							{
								handbrake = 0;
							}
						}
					}
					else if(canSteer)
					{
						if(touch.position.x > Screen.width * 0.5f)
						{	
							if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
								steer = Mathf.Clamp01(steer + 0.1f);
							else
								steer = 0;
						}
						else
						{
							if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
								steer = Mathf.Clamp(steer - 0.1f, -1.0f, 0.0f);
							else
								steer = 0;
						}
					}
				}
			}
			else
			{
				if(canBrake)
					handbrake = Input.GetKey(KeyCode.LeftShift)?1.0f:0.0f;
				if(canSteer)
					steer = Input.GetAxis("Horizontal");
			}
			
//		 	motor = Mathf.Clamp01(Input.GetAxis("Vertical"));
			motor = throttle;
	 	}
		motor = throttle;
//		else
//		{
//			motor = 0;
//			steer = 0;
//			brake = 0;
//			handbrake = 0;
//		}
	
		//if car is on ground calculate handling, otherwise just rev the engine
	 	if(onGround)
		{
			HandlePhysics();
		}
		else
		{
			if(oldY > transform.position.y)
				transform.position -= new Vector3(0, fallDownAccel * Time.fixedDeltaTime, 0);
			AdjustAirAngular();
		}
		oldY = transform.position.y;
		//wheel GFX
		UpdateWheels();
	
		//engine sounds
//		audio.pitch=0.5f+0.2f*motor+0.8f*engineRPM/maxRPM;
//		audio.volume=0.5f+0.8f*motor+0.2f*engineRPM/maxRPM;
		audio.pitch=0.5f+0.2f*motor+0.8f*(rigidbody.velocity.magnitude * 3.6f / gearSpeeds[gear]);
		audio.volume=0.5f+0.8f*motor+0.2f*(rigidbody.velocity.magnitude * 3.6f / gearSpeeds[gear]);
		Check_If_Car_Is_Flipped();
		if(timeCount)
		{
			timeRace += Time.fixedDeltaTime;
		}
	}
	
	void AdjustAirAngular () {
		if(180 - transform.eulerAngles.x > 0)
			transform.eulerAngles -= new Vector3(Time.fixedDeltaTime * 5 * fallDownAccel, 0, 0);
		else if(180 - transform.eulerAngles.x < 0)
			transform.eulerAngles += new Vector3(Time.fixedDeltaTime * 5 * fallDownAccel, 0, 0);
		if(180 - transform.eulerAngles.z > 0)
			transform.eulerAngles -= new Vector3(0, 0, Time.fixedDeltaTime * 5 * fallDownAccel);
		else if(180 - transform.eulerAngles.z < 0)
			transform.eulerAngles += new Vector3(0, 0, Time.fixedDeltaTime * 5 * fallDownAccel);
		rigidbody.angularVelocity = Vector3.zero;
	}
	
	//Called by DamageReceiver if boat destroyed
	void Detonate()
	{
		//destroy wheels
		foreach(WheelData w in wheels)
			w.coll.gameObject.active=false;
	
		//Mark object no longer a target for homing missiles.
		if(tag=="MissileTarget")
			tag="";
	
		//no more car physics
		enabled=false;
	}
	
	public int GetNextNode(){
		return NextNode;
	}
	
	public int GetWeights () {
		return NodeWeights;
	}
	
	int firstNode = -1;
//	void  OnTriggerEnter ( Collider collInfo  ){
//		Node collNode = collInfo.transform.GetComponent<Node>();
//		if(!collNode)
//			return;
//		bool isCorner = PathNodeManager.SP.IsCornerNode(NowNode);
//		if(NowNode == collNode.GetIndex())
//		{
//			NodeWeights += collNode.GetWeight();
//			NowNode = collNode.GetIndex() + 1;
//			if(firstNode == -1)
//			{
//				firstNode = NowNode - 1;
//				timeCount = true;
//			}
//			else if(firstNode == NowNode - 1)
//			{
//				timeCount = false;
//			}
//		}
//		if(NowNode >= PathNodeManager.SP.GetNodeLength()){
//			NowNode = 0;
//		}
//		NextNode = NowNode;
//		
//		while(PathNodeManager.SP.IsLimitNode(NextNode) && 
//		      ((isCorner && rigidbody.velocity.magnitude < PathNodeManager.SP.MinSpeedNode(NextNode)) ||
//		       (!isCorner && rigidbody.velocity.magnitude > PathNodeManager.SP.MaxSpeedNode(NextNode))))
//		{
//			NodeWeights += PathNodeManager.SP.GetWeight(NextNode);
//			NextNode = NextNode + 1;
//			if(NextNode >= PathNodeManager.SP.GetNodeLength()){
//				NextNode = 0;
//			}
//		}
//		
//	}
	
	void  OnCollisionEnter (Collision collision){
		Node collNode = collision.transform.GetComponent<Node>();
		if(!collNode)
			return;
//		bool isCorner = PathNodeManager.SP.IsCornerNode(NowNode);
		int tempNode = collNode.GetIndex();
		if(firstNode == -1)
		{
			NowNode = tempNode;
			firstNode = tempNode;
		}
		if(NowNode > tempNode)
		{
			NodeWeights -= 1;
		}
		else if(NowNode < tempNode)
		{
			NodeWeights += 1;
		}
		NowNode = tempNode;
		if(firstNode == NowNode && NodeWeights > 0)
		{
			round += 1;
			NodeWeights = 0;
		}
		NextNode = NowNode + 1;
		if(NextNode >= PathNodeManager.SP.GetNodeLength()){
			NextNode = 0;
		}
		
	}
	
	void FlipCar()
	{
		transform.rotation = Quaternion.LookRotation(transform.forward);
		Vector3 flipPosition;
//		if(NodeWeights > 1 && NowNode - 1 < 0)
//			flipPosition = PathNodeManager.SP.getNode(PathNodeManager.SP.GetNodeLength() - 1).position;
//		else
//			flipPosition = PathNodeManager.SP.getNode(NowNode - 1).position;
		flipPosition = PathNodeManager.SP.getNode(NowNode).position;
		transform.position = flipPosition + Vector3.up * 0.5f;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		resetTimer = 0;
		motor = 0;
	}
	
	public void SetThrottle(float t){
//	 	motor = Mathf.Clamp01(t);
//		motor = throttle;
		throttle = t;
	}
	
	public float GetThrottle(){
		return motor;
	}
	
	public void SetSteer(float s){
		if(canSteer)
			steer = s;
	}
	
	public void SetHandBrake(float hb)
	{
		if(canBrake)
			handbrake = hb;
	}
	
	void Check_If_Car_Is_Flipped()
	{
		if(transform.localEulerAngles.z > 80 && transform.localEulerAngles.z < 280)
			resetTimer += Time.deltaTime;
		else
			resetTimer = 0;
		
		if(resetTimer > resetTime)
			FlipCar();
	}
	
//	public void SetSpeedRatio (float percent)
//	{
//		speedRatio = percent;
//	}
	
	public void SetMaxRPM (float percent)
	{
		maxRPM = defaultMaxRPM * percent;
	}
	
	public void SetMaxTorque (float percent)
	{
		maxTorque = defaultMaxTorque * percent;
	}
	
	public void SetCanBrake (bool isCanBrake)
	{
		canBrake = isCanBrake;
	}
	
	public void SetCanSteer (bool isCanSteer)
	{
		canSteer = isCanSteer;
	}
	
	public bool CheckSpeed () {
//		return rigidbody.velocity.magnitude * 60 < (maxRPM / finalDriveRatio * gearRatios[gearRatios.Length - 1] * wheelRadius * 2 * Mathf.PI);
		return rigidbody.velocity.magnitude * 3.6f < gearSpeeds[gearSpeeds.Length - 1];
	}
	
	public Vector3 GetForce () {
		return allForce;
	}
//	void OnCollisionStay(Collision collision) {
//		if(collision.transform.tag == "Road")
//			return;
////		float angle = Vector3.Angle(collision.contacts[0].point - transform.position,transform.forward);
////		if(newForce.x != 0)
////			newForce.z = -newForce.z;
//		
////		Vector3 newForce = collision.contacts[0].point - transform.position;
//		Vector3 newForce = new Vector3();
//		foreach(ContactPoint cp in collision.contacts)
//		{
//			newForce += cp.point - transform.position;
//		}
//		newForce.Normalize();
////		newForce = Vector3.Scale(newForce,transform.forward);
////		newForce = transform.InverseTransformDirection(newForce);
//		newForce = Vector3.Reflect(newForce,transform.right);
//		newForce.y = 0;
//		float angle = Vector3.Angle(newForce,transform.forward);
//		Debug.DrawRay(transform.position,newForce * 50);
//		if(transform.name == "policeCar")
//		{
//			Debug.LogError("newForce: " + newForce + " angle: " + angle + "point: " + (collision.contacts[0].point) + " angle1: " + (Vector3.Angle(transform.forward,collision.contacts[0].point - transform.position)).ToString() + " angle2: " + angle + " magnitude: " + collision.relativeVelocity.magnitude + " relativeVelocity: " + collision.relativeVelocity + " velocity: " + rigidbody.velocity);
//		}
//		if(angle < 25 || angle > 155)
//			rigidbody.velocity = -newForce * reboundForce;
//		else
//			rigidbody.velocity = newForce * reboundForce;
//	}
}
