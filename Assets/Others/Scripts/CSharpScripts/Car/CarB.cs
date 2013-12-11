using UnityEngine;
using System.Collections;

public class CarB : MonoBehaviour {
public Detonator DriftFx;
public Texture touchTexture;
public Transform BrakeLight;
float suspensionRange = 0.1f;
float suspensionDamper = 50;
//float suspensionDamper = 10.0f;
float suspensionSpringFront = 18500;
float suspensionSpringRear = 9000;
//float suspensionSpringFront = 2000;
//float suspensionSpringRear = 2000;
public CarSettings MyCarSettings;
//public Texture2D Tex_Yahoo;
	//public
private bool IsPlayer;
private string WallCollider = "wallcollider";
private string MaterialCollider = "MaterialCollider";
//private string MyChangeColor = "MyChangeColor";
//	private string ResetColor = "ResetColor";
private Vector3 dragMultiplier = new Vector3(2, 5, 1);
private Transform centerOfMass;
private Transform[] frontWheels;
private Transform[] rearWheels;
private float wheelRadius = 0.4f;
//private float mass = 1500.0f;
public float[] gearSpeeds;
private float[] gearTime;
private float[] gearAcc;
private float ForceFactor = 16.0f;
private float BrakeForceFactor = 1.0f;
private float BrakeForce;
private float DriftXDrag;
	//public end
float throttle = 0;
private float steer = 0;
private bool  handbrake = false;
private float drag = 0.0f;
private Wheel[] wheels;
private WheelFrictionCurve wfc;

float topSpeed = 0.0f;
int numberOfGears = 5;
private float topSpeedSqr = 0.0f;
	
int maximumTurn = 15;
int minimumTurn = 10;

public float resetTime = 2.0f;
private float resetTimer = 0.0f;

//private float[] engineForceValues;


private int currentGear;
private float currentEnginePower = 0.0f;

private float handbrakeXDragFactor = 0.1f;
private float initialDragMultiplierX = 10.0f;
private float handbrakeTime = 0.0f;
private float handbrakeTimer = 1.0f;

private Skidmarks skidmarks = null;
private ParticleEmitter skidSmoke = null;
float[] skidmarkTime;

private SoundController sound = null;
//private float accelerationTimer = 0.0f;

private bool  canSteer;
private bool  canDrive;
	
private bool IsDrift;
//private float MaxSpeed;
	
private float DVAngle;
//private bool IsSlowTurn;

private float MassMultiForceFactor;
public float MaxGearAcc = 25.0f;
private bool IsAirTime;
private float CarSpeed;
private float MinDragVel;
private bool IsCanBrake = true;
private bool IsMaxSpeed = false;
private bool IsCanSteer = true;
private float EngineForceFactor;
private bool IsEngineStart;
private bool IsReverse;
//public bool queryUserInput = false;
public int NextNode = 0;
public int NowNode = 0;
public int NodeWeights = 0;
public int round = 0;
private BackgroundSoundController backgroundSoundController;
private Timer timer;
private Vector3 oldPosition;
public float coefficient = 10000;
public bool IsWaiting = true;
private float engineBonus = 0.0f;
//public bool HoldTest;
//	public float HoldTime;
//	private float NowHoldTime;
private static string YShakeIt = "YShakeIt";
private static string RotateXShakeIt = "RotateXShakeIt";
private static string XShakeIt = "XShakeIt";
	private static string StopXShakeIt = "StopXShakeIt";
private static string RotateZShakeIt = "RotateZShakeIt";
	private SmoothFollow MySmoothFollow;
	private float HitWallBrakeForce;
	private Transform MyTransform;
	private Camera MyCamera;
	
	//private float OriginalCameraHeight;
//private static string DarkOn = "DarkOn";
//private static string DarkOff = "DarkOff";
internal class Wheel
{
	public WheelCollider collider;
	public Transform wheelGraphic;
	public Transform tireGraphic;
	public bool  driveWheel = false;
	public bool  steerWheel = false;
	public int lastSkidmark = -1;
	public Vector3 lastEmitPosition = Vector3.zero;
	public float lastEmitTime = Time.time;
	public Vector3 wheelVelo = Vector3.zero;
	public Vector3 groundSpeed = Vector3.zero;
}

void  Awake (){	
	// Measuring 1 - 60
//	accelerationTimer = Time.time;
	EngineForceFactor = 1.0f;
	SetupRigidBody();	
	backgroundSoundController = FindObjectOfType(typeof(BackgroundSoundController)) as BackgroundSoundController;
			
	dragMultiplier = MyCarSettings.dragMultiplier;
	centerOfMass = MyCarSettings.centerOfMass;
	frontWheels = MyCarSettings.frontWheels;
	rearWheels = MyCarSettings.rearWheels;
	
	gearSpeeds = MyCarSettings.gearSpeeds;
	gearTime = MyCarSettings.gearTime;
	gearAcc = MyCarSettings.gearAcc;
	ForceFactor = MyCarSettings.ForceFactor;
	BrakeForceFactor = MyCarSettings.BrakeForceFactor;
	DriftXDrag = MyCarSettings.DriftXDrag;
	
	wheels = new Wheel[frontWheels.Length + rearWheels.Length];
	MassMultiForceFactor = rigidbody.mass * ForceFactor;
	sound = transform.GetComponent<SoundController>();
		
	timer = transform.GetComponent<Timer>();
		
	SetupWheelColliders();
	
	SetupCenterOfMass();
	
	topSpeed = Convert_Miles_Per_Hour_To_Meters_Per_Second(topSpeed);
	
	SetupGears();
	
	SetUpSkidmarks();
	
	initialDragMultiplierX = dragMultiplier.x;
	dragMultiplier.x = initialDragMultiplierX * handbrakeXDragFactor;
	IsEngineStart = false;
		
		
//		if(HoldTest){
//			Wait(true);
//		}else{
//			Wait(false);
//		}
	
		
		//StartEngine();
		//SetBackgroundMusic(true);
}

IEnumerator Start(){
	StartEngine();
	SetBackgroundMusic(true);
	oldPosition = transform.position;
	while(!Camera.main.transform.GetComponent<SmoothFollow>().target)
	{
		yield return null;
	}
		MyCamera = Camera.main;
	IsPlayer = (MyCamera.transform.GetComponent<SmoothFollow>().target == this.transform);
	BrakeLight.gameObject.SetActiveRecursively(false);
	NowTurnRatio = TurnRatio;
	MySmoothFollow = Camera.main.GetComponent<SmoothFollow>();
	
		MyTransform = transform;
	
		
		//OriginalCameraHeight = MySmoothFollow.height;
}
//void OnGUI () {
	/*GUI.color = Color.red;
	if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
	{
		if(GUI.Button(new Rect(Screen.width - 150 - 50,Screen.height - 100 - 50, 100, 100), "Brake") || GUI.Button(new Rect(100,Screen.height - 100 - 50, 100, 100), "Brake"))
		{
		}
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				//GUI.DrawTexture(new Rect(touch.position.x - touchTexture.width * 0.25f,Screen.height - touch.position.y - touchTexture.height * 0.25f, touchTexture.width / 2, touchTexture.height / 2), touchTexture);
			}
		}
	}
	//GUI.Label(new Rect(Screen.width / 2,10,100,100),"Throtle = "+ Vector3.Angle(rigidbody.velocity,transform.forward));
	//GUI.Label(new Rect(Screen.width / 2,300,100,100),"transform.eulerAngles = "+ DVAngle);
		
	if(IsDrift)
	{
		GUI.Label(new Rect(Screen.width / 2,30,100,100),"Drift!");
	}
		
	GUI.Label(new Rect(Screen.width / 2,50,100,100),"Gear = "+currentGear);
			GUI.Label(new Rect(Screen.width / 2,70,100,100),"Velocity = "+CarSpeed);*/
//		GUI.Label(new Rect(Screen.width / 2,300,100,100),"Steer = "+steer);
//}
public float YShakeItL = 0.005f;
	public float YShakeItS = 0.01f;
	private bool enabledYShake = false;
	private float shakeForce = 0;
	private bool pressBrake = false;
	private bool dontGoThrough = false;
void  Update (){	
		/*
		if(Camera.main.transform.GetComponent<SmoothFollow>().target != null){
		}
		*/
		CarSpeed = rigidbody.velocity.magnitude;//for optimize
		if(CarSpeed > 25)
		{
			ShowEffect(true);
		}
		else
		{
			ShowEffect(false);
		}
		if(CarSpeed > 20 && !dontGoThrough)
		{
			dontGoThrough = true;
			SendMessage("OpenDontGoThroughThings", dontGoThrough, SendMessageOptions.DontRequireReceiver);
		}
		if(CarSpeed <= 20 && dontGoThrough)
		{
			dontGoThrough = false;
			SendMessage("OpenDontGoThroughThings", dontGoThrough, SendMessageOptions.DontRequireReceiver);
		}
	Check_If_Car_Is_Flipped();
	
	UpdateWheelGraphics(relativeVelocity);
		
	AutoBalance();
	//CheckHandbrake();
	
	if(!IsAirTime && !IsDrift && !enabledYShake){

				enabledYShake = true;
			SendMessage(YShakeIt, SendMessageOptions.DontRequireReceiver);
	}
		else if(IsAirTime && enabledYShake)
		{
			enabledYShake = false;
			SendMessage("StopYShakeIt", SendMessageOptions.DontRequireReceiver);
		}
		if(!pressBrake)
		{
			Accelaration();
		}
	//UpdateGear(relativeVelocity);
}
	private string SShowEffect = "ShowEffect";
	private bool isShow = false;
	void ShowEffect (bool show) {
		if(show != isShow)
		{
			isShow = show;
			if(MySmoothFollow.target == transform.root)
			{
				MySmoothFollow.SendMessage(SShowEffect, show, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	
public float RotateXShakeItLevel = 0.3f;
Vector3 relativeVelocity;
void  FixedUpdate (){	
	// The rigidbody velocity is always given in world space, but in order to work in local space of the car model we need to transform it first.
	relativeVelocity = MyTransform.InverseTransformDirection(rigidbody.velocity);
	//CarSpeed = rigidbody.velocity.magnitude;//for optimize
	DVAngle = Vector3.Angle(rigidbody.velocity,transform.TransformDirection( Vector3.forward));
		
	//GetInput(relativeVelocity);
	bool beforeIsAirTime = IsAirTime;
	CheckAirTime();
//	if(IsAirTime){
//		BroadcastMessage("StopYShakeIt");
//	}
	if(!IsAirTime && beforeIsAirTime && IsPlayer){
		MyCamera.SendMessage(RotateXShakeIt , RotateXShakeItLevel, SendMessageOptions.DontRequireReceiver);
		if(sound)
			sound.PlayJumpCollisionSound(true);
	}
		CheckHandbrake();
	AntiRollBar();
	//CheckSlowTurn( relativeVelocity);
	CalculateState();	
	
	UpdateFriction(relativeVelocity);
	
	UpdateDrag(relativeVelocity);
	
	CalculateEnginePower(relativeVelocity);
	
	ApplyThrottle(canDrive, relativeVelocity);
	SteerAutoCenter();
	ApplySteering(canSteer, relativeVelocity);
		
	CheckDrift(relativeVelocity);
	UpdateGear(relativeVelocity);
//	HitWallFreeze();
	
}
//void LateUpdate(){
//		HitWallFreezeTime -= Time.deltaTime;
//		if(HitWallFreezeTime < 0.0f){
//			rigidbody.constraints = RigidbodyConstraints.None;
//		}
//	}
	
	private void AntiRollBar () {
		//WheelHit hit = new WheelHit();
	    float travelL = 1.0f;
	    float travelR = 1.0f;
	
	    bool groundedL = wheels[0].collider.GetGroundHit(out wh);
	
	    if (groundedL)
		{
	        travelL = (-wheels[0].collider.transform.InverseTransformPoint(wh.point).y - wheels[0].collider.radius) / wheels[0].collider.suspensionDistance;
		}
	
	    bool groundedR = wheels[1].collider.GetGroundHit(out wh);
	
	    if (groundedR)
		{
	        travelR = (-wheels[1].collider.transform.InverseTransformPoint(wh.point).y - wheels[1].collider.radius) / wheels[1].collider.suspensionDistance;
		}
	
	    float antiRollForce = (travelL - travelR) * coefficient;
	
	    if (groundedL)
		{
	        rigidbody.AddForceAtPosition(wheels[0].collider.transform.up * -antiRollForce, wheels[0].collider.transform.position); 
		}
	
	    if (groundedR)
		{
	        rigidbody.AddForceAtPosition(wheels[1].collider.transform.up * antiRollForce, wheels[1].collider.transform.position); 
		}
	}
private float DriftTime;
private void CheckDrift(Vector3 relativeVelocity){
	//DVAngle = Vector3.Angle(rigidbody.velocity,transform.forward);
	if(relativeVelocity.sqrMagnitude < 625.0f && !IsDrift || 
			relativeVelocity.sqrMagnitude < 10.0f && IsDrift){
		IsDrift = false;
			if(enabledXShake)
			{
				enabledXShake = false;
				SendMessage(StopXShakeIt, SendMessageOptions.DontRequireReceiver);
			}
			if(IsPlayer){
				MySmoothFollow.height = MySmoothFollow.GetOriginalHeight();
			}
			
		//BrakeLight.gameObject.SetActiveRecursively(false);
		//print("relativeVelocity.sqrMagnitude < 100.0f IsDrift = false");
	}else{
		if( HaveTheSameSign(relativeVelocity.z, throttle) && throttle < 0.0f && !IsDrift)
		{
			IsDrift = false;
				if(enabledXShake)
				{
					enabledXShake = false;
					SendMessage(StopXShakeIt, SendMessageOptions.DontRequireReceiver);
				}
				//BrakeLight.gameObject.SetActiveRecursively(false);
				//print("HaveTheSameSign(relativeVelocity.z, throttle) && throttle < 0.0f) IsDrift = false");
		}else{
			if(IsDrift && (Time.time - handbrakeTime) > 0.5f){
				if(DVAngle > 5.0f && DVAngle < 175.0f){
					//IsDrift = true;
					//IsSlowTurn = true;
						/*
						bool isleft = false;
						if(steerDir < 0.0f){
							isleft = true;
						}
						*/
						BrakeLight.gameObject.SetActiveRecursively(true);
				}else{
					IsDrift = false;
					if(enabledXShake)
					{
						enabledXShake = false;
						SendMessage(StopXShakeIt, SendMessageOptions.DontRequireReceiver);
					}
						if(IsPlayer){
							MySmoothFollow.height = MySmoothFollow.GetOriginalHeight();
						}
						//BrakeLight.gameObject.SetActiveRecursively(false);
						//print("DVAngle > 5.0f && DVAngle < 175.0f IsDrift = false");
				}
			}
		}
	}
	
	if(IsDrift && IsPlayer){
		DriftTime += Time.deltaTime;
		//if(DriftTime <= 0.3f){
			MySmoothFollow.rotationDamping = 2.0f;
		//}else{
		//	MySmoothFollow.rotationDamping = 3.0f;
		//}
	}else if(!IsDrift && IsPlayer){
		//DriftTime = 0.0f;
			MySmoothFollow.rotationDamping = 4.0f;
	}
	
}

public bool CheckAirTime(){
	IsAirTime = true;
	foreach(Wheel w in wheels)
	{
		
		WheelCollider wheel = w.collider;
		//WheelHit wh = new WheelHit();
		
		if(wheel.GetGroundHit(out wh))
		{
			IsAirTime = false;
				break;
		}
	}
	return IsAirTime;
}
public void ResetNowNode (){
	NowNode= 0 ;
}
private string OpenShadow = "OpenShadow";
	private bool onFall = false;
private void AutoBalance () {
	RaycastHit hit;
	if(Time.timeScale > 0 && Physics.Raycast(MyTransform.position, -Vector3.up, out hit, 20.0f, 1 << 8))
	{
		Quaternion targetRotation = Quaternion.FromToRotation (transform.up, hit.normal) * MyTransform.rotation;
			targetRotation = Quaternion.RotateTowards(MyTransform.rotation, targetRotation, Time.deltaTime * 25.0f);
//			Debug.Log(targetRotation);
		MyTransform.rotation = targetRotation;
			BroadcastMessage(OpenShadow, hit.point, SendMessageOptions.DontRequireReceiver);
			onFall = false;
	}
		else
		{
			BroadcastMessage(OpenShadow, Vector3.zero, SendMessageOptions.DontRequireReceiver);
			onFall = true;
		}
	rigidbody.angularVelocity = new Vector3(0.0f, rigidbody.angularVelocity.y * 0.5f, 0.0f);
//		rigidbody.angularVelocity.Set(0.0f, rigidbody.angularVelocity.y * 0.5f, 0.0f);
}
	
/**************************************************/
/* Functions called from Start()                  */
/**************************************************/
void  SetupRigidBody (){
	if(rigidbody)
		Destroy(rigidbody);
	
	//setup rigidbody	
	gameObject.AddComponent<Rigidbody>();
	rigidbody.mass = MyCarSettings.mass;
	rigidbody.drag = drag;
		if(GetComponent<CarProperty>().carID == 2)
		{
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}
//	rigidbody.angularDrag =  5;//Mathf.Infinity;
	//rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
	//rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
		
	//rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
}
void  SetupWheelColliders (){
	SetupWheelFrictionCurve();
		
	int wheelCount = 0;
	
	foreach(Transform t in frontWheels)
	{
		wheels[wheelCount] = SetupWheel(t, true);
		wheelCount++;
	}
	
	foreach(Transform t in rearWheels)
	{
		wheels[wheelCount] = SetupWheel(t, false);
		wheelCount++;
	}
}

void  SetupWheelFrictionCurve (){
	wfc = new WheelFrictionCurve();
	wfc.extremumSlip = 1;
	wfc.extremumValue = 50;
	wfc.asymptoteSlip = 2;
	wfc.asymptoteValue = 25;
	wfc.stiffness = 1;
}

Wheel  SetupWheel ( Transform wheelTransform ,   bool isFrontWheel  ){
	 GameObject go = new GameObject(wheelTransform.name + " Collider");
	go.transform.position = wheelTransform.position;
	go.transform.parent = transform;
	go.transform.rotation = wheelTransform.rotation;
	go.layer = LayerMask.NameToLayer("Wheel");
		
	WheelCollider wc = go.AddComponent(typeof(WheelCollider)) as WheelCollider;
	wc.suspensionDistance = suspensionRange;
	JointSpring js = wc.suspensionSpring;
	
	if (isFrontWheel)
		js.spring = suspensionSpringFront;
	else
		js.spring = suspensionSpringRear;
		
	js.damper = suspensionDamper;
	wc.suspensionSpring = js;
		
	Wheel wheel = new Wheel(); 
	wheel.collider = wc;
	wc.sidewaysFriction = wfc;
	wheel.wheelGraphic = wheelTransform;
	//wheel.tireGraphic = wheelTransform.GetComponentsInChildren<Transform>()[1];
	wheel.tireGraphic = wheelTransform;
	
	wheelRadius = wheel.tireGraphic.renderer.bounds.size.y / 2;	
	wheel.collider.radius = wheelRadius;
	
	if (isFrontWheel)
	{
		wheel.steerWheel = true;
		
		go = new GameObject(wheelTransform.name + " Steer Column");
		go.transform.position = wheelTransform.position;
		go.transform.rotation = wheelTransform.rotation;
		go.transform.parent = transform;
		wheelTransform.parent = go.transform;
	}
	else
	{
		wheel.driveWheel = true;
		go = new GameObject(wheelTransform.name + " drive Column");
		go.transform.position = wheelTransform.position;
		go.transform.rotation = wheelTransform.rotation;
		go.transform.parent = transform;
		wheelTransform.parent = go.transform;
	}
		
		
	return wheel;
}

void  SetupCenterOfMass (){
	if(centerOfMass != null)
		rigidbody.centerOfMass = centerOfMass.localPosition;
}
void  SetupGears (){
	numberOfGears = gearSpeeds.Length;
	//engineForceValues = new float[numberOfGears];
		gearAcc  = new float[numberOfGears];
	//gearSpeeds = new float[numberOfGears];
	/*
	float tempTopSpeed = topSpeed;
	
	for(int i= 0; i < numberOfGears; i++)
	{
		if(i > 0)
			gearSpeeds[i] = tempTopSpeed / 4 + gearSpeeds[i-1];
		else
			gearSpeeds[i] = tempTopSpeed / 4;
		print("gearSpeeds[i] = "+gearSpeeds[i]);
		tempTopSpeed -= tempTopSpeed / 4;
	}
	
	float engineFactor = topSpeed / gearSpeeds[gearSpeeds.Length - 1];
	print("engineFactor = "+engineFactor);
	*/
	MaxGearAcc = MaxGearAcc / 4.0f;
	//MinDragVel = MaxGearAcc - 10.0f;
		MinDragVel = gearSpeeds[0] / 4.0f;
	for(int i= 0; i < numberOfGears; i++)
	{
		gearSpeeds[i] = gearSpeeds[i] / 4.0f;
	}
	//float engineFactor = 1.5f;
	for(int i = 0; i < numberOfGears; i++)
	{
		//float maxLinearDrag = gearSpeeds[i] * gearSpeeds[i];// * dragMultiplier.z;
		//engineForceValues[i] = maxLinearDrag * engineFactor;
		if(i == 0){
				gearAcc[i] = gearSpeeds[i] / gearTime[i];
		}else{
				gearAcc[i] = (gearSpeeds[i] - gearSpeeds[i - 1]) / gearTime[i];
		}
		if(gearAcc[i] > MaxGearAcc){
				MaxGearAcc = gearAcc[i];
		}
	}
	topSpeed = gearSpeeds[numberOfGears - 1];
	topSpeedSqr = gearSpeeds[numberOfGears - 1] * gearSpeeds[numberOfGears - 1];
	//BrakeForce = engineForceValues[0] * rigidbody.mass * BrakeForceFactor;
	
		BrakeForce = gearAcc[0] * MassMultiForceFactor * BrakeForceFactor;
		HitWallBrakeForce = gearAcc[numberOfGears - 1]  * MassMultiForceFactor / 10.0f;
	//MaxSpeed = gearSpeeds[gearSpeeds.Length - 1] * gearSpeeds[gearSpeeds.Length - 1];
}

void  SetUpSkidmarks (){
//	if(FindObjectOfType(typeof(Skidmarks)))
//	{
//		skidmarks = (Skidmarks)FindObjectOfType(typeof(Skidmarks));
//		//skidSmoke = skidmarks.GetComponentInChildren<ParticleEmitter>();
//		skidSmoke =	GameObject.Find("WheelSmoke").GetComponentInChildren<ParticleEmitter>();
//	}
//	else
//		Debug.Log("No skidmarks object found. Skidmarks will not be drawn");
		
	skidmarks = FindObjectOfType(typeof(Skidmarks)) as Skidmarks;
	skidSmoke =	transform.Find("WheelSmoke").GetComponent<ParticleEmitter>();
	skidmarkTime = new float[wheels.Length];
	for(int i = 0 ; i < skidmarkTime.Length ; i++){
		skidmarkTime[i] = 0.0f;
	}
}

/**************************************************/
/* Functions called from Update()                 */
/**************************************************/
//float steerfactor = 0.1f;
//void  GetInput (Vector3 relativeVelocity){
//
//	/*if(Input.GetKey("space")){
//		throttle -= 0.5f;
//		if(throttle < -1.0f){
//			throttle = -1.0f;
//			
//		}
//	}else{
//		throttle += 0.5f;
//		if(throttle > 1.0f){
//			throttle = 1.0f;
//		}
//	}
//	if(Input.GetAxis("Horizontal") >0.0f){
//			steer += steerfactor;
//	}else if(Input.GetAxis("Horizontal") <0.0f){
//			steerfactor = -steerfactor;
//			steer += steerfactor;
//	}else{
//			if(!IsDrift){
//				steer = Mathf.MoveTowards(steer,0.0f,steerfactor * 2.0f);
//			}
//	}*/
//	//add new code
//	/*if(queryUserInput)
//	{
//		if(Input.GetKey(KeyCode.Escape)){
//				Application.Quit();
//		}
//		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
//		{
//			steerfactor = 0.2f;
//			bool isbrake = false;
//			bool isturn = false;
//			foreach (Touch touch in Input.touches) {
//				if(touch.position.y < 150 && touch.position.y > 50 && ((touch.position.x > 100 && touch.position.x < 200) || (touch.position.x > Screen.width - 150 - 50 && touch.position.x < Screen.width - 100)))
//				{
//					//brake
//					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//					{
//						isbrake = true;
//						isturn = true;
//					}
//				}else{
//					if(touch.position.x > Screen.width * 0.5f)
//					{	
//						//turn right
//						if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//						{
//							steer += steerfactor;
//								isturn = true;
//						}
//					}else if(touch.position.x < Screen.width * 0.5f)
//					{
//						//turn left
//						if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//						{
//							//steerfactor = -steerfactor;
//							steer -= steerfactor;
//								isturn = true;
//						}
//					}
//				}
//			}
//				if(!IsCanBrake){
//					isbrake = false;
//				}
//				if(!isturn){
//					if(!IsDrift){
//							steer = Mathf.MoveTowards(steer,0.0f,steerfactor * 2.0f);
//						}
//				}
//			if(isbrake){
//				throttle -= 0.5f;
//				if(throttle < -1.0f){
//					throttle = -1.0f;
//				}
//			}else{
//				throttle += 0.5f;
//				if(throttle > 1.0f){
//					throttle = 1.0f;
//				}
//			}
//		}
//		else
//		{
//			if(Input.GetKey("space") && IsCanBrake){
//				throttle -= 0.5f;
//				if(throttle < -1.0f){
//					throttle = -1.0f;
//					
//				}
//			}else{
//				throttle += 0.5f;
//				if(throttle > 1.0f){
//					throttle = 1.0f;
//				}
//			}
//			if(Input.GetAxis("Horizontal") >0.0f){
//				steer += steerfactor;
//			}else if(Input.GetAxis("Horizontal") <0.0f){
//				steer -= steerfactor;
//			}else{
//				if(!IsDrift){
//					steer = Mathf.MoveTowards(steer,0.0f,steerfactor * 2.0f);
//				}
//			}
//		}
//	}
//	//add new code
//	steer = Mathf.Clamp(steer , -1.0f , 1.0f);
//		
//	CheckHandbrake(relativeVelocity);*/
//}
private float steerDir;
//private float diff = 0.0f;
private Detonator fx;
	private bool enabledXShake = false;
public void  CheckHandbrake (){
	//if(throttle <= -0.5f && Mathf.Abs(steer) >= 0.6f && relativeVelocity.z > 5.0f)
	if(pressBrake && Mathf.Abs(steer) >= 0.9f && CarSpeed > MinDragVel)
	{
		if(!handbrake)
		{
			handbrake = true;
			IsDrift = true;
				if(!enabledXShake)
				{
					enabledXShake = true;
					SendMessage(XShakeIt, SendMessageOptions.DontRequireReceiver);
				}
			steerDir = steer;
			handbrakeTime = Time.time;
			dragMultiplier.x = initialDragMultiplierX * handbrakeXDragFactor;
//			diff = initialDragMultiplierX - dragMultiplier.x;
				
			MyTransform.RotateAround((frontWheels[frontWheels.Length - 2].position + frontWheels[frontWheels.Length - 1].position) * 0.5f,//transform.position + transform.right * turnRadius * steer, 
						transform.up, 
						5.0f * steer);
			if(fx == null)
			{
				fx = Instantiate(DriftFx,MyTransform.position,Quaternion.identity) as Detonator;
					fx.destroyTime = 0f;
				fx.Followed = transform;
			}
			//fx.parent = transform;
//				fx.transform.position = MyTransform.position;
				fx.Explode();
				if(IsPlayer){
					MySmoothFollow.height =	MySmoothFollow.GetOriginalHeight() * 1.5f;
				}
		}
	}
	else
	{
		if(handbrake)
		{
			handbrake = false;
			StartCoroutine(StopHandbraking(Mathf.Min(5.0f, Time.time - handbrakeTime)));
		}
		// Get the x value of the dragMultiplier back to its initial value in the specified time.
		//dragMultiplier.x = Mathf.Clamp(dragMultiplier.x + diff * (Time.deltaTime / Mathf.Min(5, Time.deltaTime - handbrakeTime)), diff * (Time.deltaTime / Mathf.Min(5, Time.time - handbrakeTime)), initialDragMultiplierX);
			
		//dragMultiplier.x = initialDragMultiplierX;
	}
}

IEnumerator  StopHandbraking ( float seconds  ){
//		if(handbrakeTimer > 0)
//		{
//			yield break;
//		}
	float diff = initialDragMultiplierX - dragMultiplier.x;
	handbrakeTimer = 1;
	// Get the x value of the dragMultiplier back to its initial value in the specified time.
	while(dragMultiplier.x < initialDragMultiplierX && !handbrake)
	{
		dragMultiplier.x += diff * (Time.deltaTime / seconds);
		handbrakeTimer -= Time.deltaTime / seconds;
		yield return null;
	}
	
	dragMultiplier.x = initialDragMultiplierX;
	handbrakeTimer = 0.0f;
}
	
void Check_If_Car_Is_Flipped (){
	if(onFall || (MyTransform.localEulerAngles.z > 80.0f && MyTransform.localEulerAngles.z < 280.0f) || (MyTransform.localEulerAngles.x > 60.0f && MyTransform.localEulerAngles.x < 300.0f) || (!IsWaiting && Vector3.Distance(oldPosition, MyTransform.position) < 0.3f))
	{
		resetTimer += Time.deltaTime;
	}
	else
	{
		resetTimer = 0.0f;
		oldPosition = transform.position;
	}
	if(resetTimer > resetTime)
		StartCoroutine(FlipCar());
}
	
	private bool isFlip = false;
	public IEnumerator FlipCar (){
		if(isFlip)
		{
			yield break;
		}
		isFlip = true;
		MyTransform.rotation = Quaternion.LookRotation(transform.forward);
		Vector3 flipPosition;
		Vector2 randomPosition = Random.insideUnitCircle * 5.0f;
		flipPosition = PathNodeManager.SP.getNode(NowNode).position;
		Vector3 finalPosition = flipPosition + new Vector3(randomPosition.x, 3.0f, randomPosition.y);
		while(Physics.CheckSphere(finalPosition, 1.5f, ~((1 << 20) | (1 << 8))))
		{
			randomPosition = Random.insideUnitCircle * 5.0f;
			finalPosition = flipPosition + new Vector3(randomPosition.x, 3.0f, randomPosition.y);
//			Debug.Log(finalPosition);
			yield return null;
		}
//		while(Physics.Linecast(flipPosition, finalPosition))
//		{
//			randomPosition = Random.insideUnitCircle * 5;
//			finalPosition = flipPosition + new Vector3(randomPosition.x, 3.5f, randomPosition.y);
//			yield return null;
//		}
		RaycastHit hit;
		Physics.Raycast(finalPosition, -Vector3.up, out hit, 10.0f, 1 << 8);
//		if(Physics.Raycast(transform.position, -transform.up, out hit, 5.0f, 1 << 8))
//		{
//			Quaternion targetRotation = Quaternion.FromToRotation (transform.up, hit.normal) * transform.rotation;
//			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime);
//		}
		finalPosition.y = hit.point.y + wheels[0].collider.bounds.size.y - wheels[0].collider.transform.localPosition.y;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		MyTransform.position = finalPosition;
//		transform.rotation = Quaternion.Euler(0, PathNodeManager.SP.getNode(NowNode).rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
		MyTransform.rotation = Quaternion.FromToRotation (MyTransform.up, hit.normal) * MyTransform.rotation;
		MyTransform.rotation = Quaternion.Euler(MyTransform.rotation.eulerAngles.x, PathNodeManager.SP.getNode(NowNode).rotation.eulerAngles.y, MyTransform.rotation.eulerAngles.z);
		resetTimer = 0.0f;
		currentEnginePower = 0.0f;
		
		CarWait(true);
		gameObject.layer = 1;
		float waitTime = 2.0f;
		while(waitTime > 0.0f)
		{
			rigidbody.velocity = Vector3.zero;
//			rigidbody.angularVelocity = Vector3.zero;
			waitTime -= Time.deltaTime;
			yield return null;
		}
		CarWait(false);
		gameObject.layer = 12;
		isFlip = false;
	}

int wheelCount;
WheelHit wh = new WheelHit();//for optimize
Vector3 staticVel;//for optimize
float dt;
float handbrakeSkidding;
int skidGroundSpeed;
float emission;
float lastParticleCount;
float currentParticleCount;
int noOfParticles;
int lastParticle;
float particleTime;
Vector3 ea;
	Vector3 forParticle = Vector3.up;
void  UpdateWheelGraphics ( Vector3 relativeVelocity  ){
	wheelCount = -1;
		/*
	WheelHit wh = new WheelHit();//for optimize
	Vector3 staticVel;//for optimize
	float dt;
	float handbrakeSkidding;
	int skidGroundSpeed;
	float emission;
	float lastParticleCount;
	float currentParticleCount;
	int noOfParticles;
	int lastParticle;
	float particleTime;
	Vector3 ea;
	*/
	foreach(Wheel w in wheels)
	{
		wheelCount++;
		WheelCollider wheel = w.collider;
		//WheelHit wh = new WheelHit();//for optimize
		// First we get the velocity at the point where the wheel meets the ground, if the wheel is touching the ground
		if(wheel.GetGroundHit(out wh))
		{
			//w.wheelGraphic.localPosition = -wheel.transform.up * (wheelRadius + wheel.transform.InverseTransformPoint(wh.point).y);
				
			w.wheelVelo = rigidbody.GetPointVelocity(wh.point);
//			w.groundSpeed = w.wheelGraphic.InverseTransformDirection(w.wheelVelo);
			w.groundSpeed = w.wheelGraphic.parent.InverseTransformDirection(w.wheelVelo);
			/*
			if(!w.steerWheel)
			{
				if(CarSpeed > 20.0f){
					skidSmoke.Emit(	wh.point,
					(w.wheelVelo * 0.05f), 
					skidSmoke.minSize, 
					skidSmoke.minEnergy, 
					Color.white);
				}else if(CarSpeed > 1.0f){
					skidSmoke.Emit(	wh.point + new Vector3(Random.Range(-0.2f, 0.2f),Random.Range(0.2f, 0.3f),Random.Range(-0.2f, 0.2f)), 
					(w.wheelVelo * 0.05f), 
					Random.Range(skidSmoke.minSize, skidSmoke.maxSize), 
					skidSmoke.maxEnergy, 
					Color.white);
				}
			}
			*/
			//w.groundSpeed = w.tireGraphic.InverseTransformDirection(w.wheelVelo);
			// Code to handle skidmark drawing. Not covered in the tutorial
			if(skidmarks)
			{
					
				if(skidmarkTime[wheelCount] < 0.02f && w.lastSkidmark != -1)
				{
					skidmarkTime[wheelCount] += Time.deltaTime;
				}
				else
				{
					dt = skidmarkTime[wheelCount] == 0.0f ? Time.deltaTime : skidmarkTime[wheelCount];
					skidmarkTime[wheelCount] = 0.0f;

					handbrakeSkidding = handbrake && w.driveWheel ? w.wheelVelo.magnitude * 0.3f : 0;
					skidGroundSpeed= (int)Mathf.Abs(w.groundSpeed.x) - 15;
					//if(skidGroundSpeed > 0 || handbrakeSkidding > 0)
					if(IsDrift)
					{
						staticVel = MyTransform.TransformDirection(skidSmoke.localVelocity) + skidSmoke.worldVelocity;
						if(w.lastSkidmark != -1)
						{
							emission = UnityEngine.Random.Range(skidSmoke.minEmission, skidSmoke.maxEmission);
							lastParticleCount = w.lastEmitTime * emission;
							currentParticleCount = Time.time * emission;
							noOfParticles = Mathf.CeilToInt(currentParticleCount) - Mathf.CeilToInt(lastParticleCount);
							lastParticle = Mathf.CeilToInt(lastParticleCount);
							
							for(int i= 0; i <= noOfParticles; i++)
							{
								particleTime = Mathf.InverseLerp(lastParticleCount, currentParticleCount, lastParticle + i);
								//skidSmoke.Emit(	Vector3.Lerp(w.lastEmitPosition, wh.point, particleTime) + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), staticVel + (w.wheelVelo * 0.05f), Random.Range(skidSmoke.minSize, skidSmoke.maxSize) * Mathf.Clamp(skidGroundSpeed * 0.1f,0.5f,1), Random.Range(skidSmoke.minEnergy, skidSmoke.maxEnergy), Color.white);
								forParticle.Set(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
								skidSmoke.Emit(	Vector3.Lerp(w.lastEmitPosition, wh.point, particleTime) + forParticle, staticVel + (w.wheelVelo * 0.05f), Random.Range(skidSmoke.minSize, skidSmoke.maxSize) * Mathf.Clamp(skidGroundSpeed * 0.1f,0.5f,1), Random.Range(skidSmoke.minEnergy, skidSmoke.maxEnergy), Color.white);	
							}
						}
						else
						{
							//skidSmoke.Emit(	wh.point + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)), staticVel + (w.wheelVelo * 0.05f), Random.Range(skidSmoke.minSize, skidSmoke.maxSize) * Mathf.Clamp(skidGroundSpeed * 0.1f,0.5f,1), Random.Range(skidSmoke.minEnergy, skidSmoke.maxEnergy), Color.white);
							forParticle.Set(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));	
							skidSmoke.Emit(	wh.point + forParticle, staticVel + (w.wheelVelo * 0.05f), Random.Range(skidSmoke.minSize, skidSmoke.maxSize) * Mathf.Clamp(skidGroundSpeed * 0.1f,0.5f,1), Random.Range(skidSmoke.minEnergy, skidSmoke.maxEnergy), Color.white);
						}
					
						w.lastEmitPosition = wh.point;
						w.lastEmitTime = Time.time;
					
						w.lastSkidmark = skidmarks.AddSkidMark(wh.point + rigidbody.velocity * dt, wh.normal, (skidGroundSpeed * 0.1f + handbrakeSkidding) * Mathf.Clamp01(wh.force / wheel.suspensionSpring.spring), w.lastSkidmark);
						sound.Skid(true, Mathf.Clamp01(DVAngle * DVAngle * 0.000025f));
					}
					else
					{
						w.lastSkidmark = -1;
						sound.Skid(false, 0);
					}
				}
			}
		}
		else
		{
			// If the wheel is not touching the ground we set the position of the wheel graphics to
			// the wheel's transform position + the range of the suspension.
			// don't change wheelGraphic.position by Vincent
//			w.wheelGraphic.position = wheel.transform.position + (-wheel.transform.up * suspensionRange);
			if(w.steerWheel)
				w.wheelVelo *= 0.9f;
			else
				w.wheelVelo *= 0.9f * (1 - throttle);
			
			if(skidmarks)
			{
				w.lastSkidmark = -1;
				sound.Skid(false, 0);
			}
		}
		// If the wheel is a steer wheel we apply two rotations:
		// *Rotation around the Steer Column (visualizes the steer direction)
		// *Rotation that visualizes the speed
		if(w.steerWheel)
		{
			ea = w.wheelGraphic.parent.localEulerAngles;
			ea.y = steer * maximumTurn;
			w.wheelGraphic.parent.localEulerAngles = ea;
//			Vector3 ea = w.wheelGraphic.localEulerAngles;
//			ea.y = steer * maximumTurn;
//			w.wheelGraphic.localEulerAngles = ea;
				
			//w.tireGraphic.Rotate(Vector3.right * (w.groundSpeed.z / wheelRadius) * Time.deltaTime * Mathf.Rad2Deg);
			w.tireGraphic.Rotate(Vector3.right * (w.groundSpeed.z * 10.0f / wheelRadius) * Time.deltaTime * Mathf.Rad2Deg);
		}
		else if(!handbrake && w.driveWheel)
		{
			// If the wheel is a drive wheel it only gets the rotation that visualizes speed.
			// If we are hand braking we don't rotate it.
			//w.tireGraphic.Rotate(Vector3.right * (w.groundSpeed.z / wheelRadius) * Time.deltaTime * Mathf.Rad2Deg);
			w.tireGraphic.Rotate(Vector3.right * (w.groundSpeed.z * 10.0f / wheelRadius) * Time.deltaTime * Mathf.Rad2Deg);	
		}
	}
}

void  UpdateGear ( Vector3 relativeVelocity  ){
	currentGear = 0;
		if(IsDrift){
			for(int i= 0; i < numberOfGears - 1; i++)
			{
				if(relativeVelocity.z > gearSpeeds[i])		
					currentGear = i + 1;
			}
		}else{
			for(int i= 0; i < numberOfGears - 1; i++)
			{
				//if(relativeVelocity.z > gearSpeeds[i])		
				if(relativeVelocity.sqrMagnitude > gearSpeeds[i] * gearSpeeds[i])
					currentGear = i + 1;
			}
		}
}

/**************************************************/
/* Functions called from FixedUpdate()            */
/**************************************************/

void  UpdateDrag ( Vector3 relativeVelocity  ){
	Vector3 relativeDrag = new Vector3(	-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), 
												-relativeVelocity.y * Mathf.Abs(relativeVelocity.y) * Mathf.Abs(relativeVelocity.y), 
												-relativeVelocity.z * Mathf.Abs(relativeVelocity.z) );
	if(relativeVelocity.y < 0.0f){
		relativeDrag.y = relativeVelocity.y;
	}
	//if(IsAirTime){
			//dragMultiplier.y = dragMultiplier.y * 2.0f;
	//}
	Vector3 drag= Vector3.Scale(dragMultiplier, relativeDrag);
		/*
		print("relativeVelocity.x = "+(-relativeVelocity.x * Mathf.Abs(relativeVelocity.x)));
		print("dragMultiplier = "+dragMultiplier);
		print("drag = "+drag);
		*/
	
		
	/*if(initialDragMultiplierX > dragMultiplier.x) // Handbrake code
	{			
		drag.x /= (relativeVelocity.magnitude / (topSpeed / ( 1 + 2 * handbrakeXDragFactor ) ) );
		drag.z *= (1 + Mathf.Abs(Vector3.Dot(rigidbody.velocity.normalized, transform.forward)));
			drag += rigidbody.velocity * Mathf.Clamp01(rigidbody.velocity.magnitude / topSpeed);
			
	}
	else // No handbrake
	{
		//drag.x *= topSpeed / relativeVelocity.magnitude;
		//drag.x *=  topSpeed / relativeVelocity.x;
		//drag.x /= (relativeVelocity.magnitude / (topSpeed / ( 1 + 2 * handbrakeXDragFactor ) ) );
	}*/
	
	if(Mathf.Abs(relativeVelocity.x) < 5 && !handbrake)
		drag.x = -relativeVelocity.x * dragMultiplier.x;
		
		if(IsReverse)
		{
			drag.x = drag.x * 50.0f;
		}
		if(IsDrift){
			//drag.x = drag.x * (1.5f - throttle);
				drag.x = drag.x * DriftXDrag;//DriftFriction;
		}else{
			if(!IsMaxSpeed){
				drag.z += Mathf.Abs(drag.x);
			}
		}
	if(drag.sqrMagnitude > 1.0f && drag.magnitude != Mathf.Infinity){
		rigidbody.AddForce(MyTransform.TransformDirection(drag) * rigidbody.mass * Time.deltaTime);
	}
}

void  UpdateFriction ( Vector3 relativeVelocity  ){
	float sqrVel = relativeVelocity.x * relativeVelocity.x;
	
	// Add extra sideways friction based on the car's turning velocity to avoid slipping
	wfc.extremumValue = Mathf.Clamp(300.0f - sqrVel, 0.0f, 300.0f);
	wfc.asymptoteValue = Mathf.Clamp(150.0f - (sqrVel / 2.0f), 0.0f, 150.0f);
	float w1 = Mathf.Clamp(150.0f - (sqrVel / 2.0f), 0.0f, 150.0f);
		
	//float w2 = Mathf.Clamp(100 - (sqrVel / 2), 0, 100);
		float w2 = 0.0f;
	foreach(Wheel w in wheels)
	{
		if(w.steerWheel){
			wfc.asymptoteValue = w1;
				
		}else{
				if(IsDrift){
					wfc.asymptoteValue = w2;
				}else{
					wfc.asymptoteValue = w1;
				}
		}
		w.collider.sidewaysFriction = wfc;
		w.collider.forwardFriction = wfc;
	}
}

void  CalculateEnginePower ( Vector3 relativeVelocity  ){
	/*if(throttle == 0)
	{
		currentEnginePower -= Time.deltaTime * 200;
	}
	else if( HaveTheSameSign(relativeVelocity.z, throttle) )
	{
		float normPower = (currentEnginePower / engineForceValues[engineForceValues.Length - 1]) * 2;
		currentEnginePower += Time.deltaTime * 200 * EvaluateNormPower(normPower);
	}
	else
	{
		currentEnginePower -= Time.deltaTime * 300;
	}
	
	if(currentGear == 0)
		currentEnginePower = Mathf.Clamp(currentEnginePower, 0, engineForceValues[0]);
	else
		currentEnginePower = Mathf.Clamp(currentEnginePower, engineForceValues[currentGear - 1], engineForceValues[currentGear]);*/
		if(IsDrift){
			if(currentGear == 0){
				currentEnginePower = MaxGearAcc * MassMultiForceFactor ;
			
			}else{
				currentEnginePower =  gearAcc[currentGear] * MassMultiForceFactor;
			}
			
		}else{
			currentEnginePower =  gearAcc[currentGear] * MassMultiForceFactor;
		}
		currentEnginePower = currentEnginePower * EngineForceFactor + engineBonus;
		if( relativeVelocity.sqrMagnitude >= topSpeedSqr * EngineForceFactor + engineBonus){
			IsMaxSpeed = true;
			currentEnginePower = 0.0f;
		}
		else
		{
			IsMaxSpeed = false;
		}
		
}

void  CalculateState (){
	canDrive = false;
	canSteer = true;
	//WheelHit wheelHit = new WheelHit();
		
	foreach(Wheel w in wheels)
	{
		if(w.collider.GetGroundHit(out wh))
		{
			if(w.driveWheel && Vector3.Angle(wh.normal, Vector3.up) < 45)
			{
				canDrive = true;
			}
		}
	}
	if(!IsAirTime)
	{
		canSteer = true;
	}
}
private float BeforeThrottle;
void  ApplyThrottle ( bool canDrive ,    Vector3 relativeVelocity  ){
	if(BeforeThrottle > throttle || throttle <= -0.01f){
			BrakeLight.gameObject.SetActiveRecursively(true);
		}else{
			BrakeLight.gameObject.SetActiveRecursively(false);
		}
	if(canDrive && !IsWaiting)
	{
		float throttleForce = 0.0f;
		float brakeForce = 0.0f;
		
		if (HaveTheSameSign(relativeVelocity.z, throttle))
		{
			//car going forward or going backward
			if (!handbrake){
				//if not just drifting
				//throttleForce = Mathf.Sign(throttle) * currentEnginePower * rigidbody.mass;
				throttleForce = Mathf.Sign(throttle) * currentEnginePower;
			}
			
			if((throttle < 0.0f && rigidbody.velocity.sqrMagnitude > 100)){
					//this prevent car reverse speed too fast
				throttleForce = 0.0f;
			}
			
			if(throttle < 0.0f){
				if(!IsDrift && DVAngle <= 90.0f){
					sound.Reverse(true);
				}
				IsReverse = true;
				//BrakeLight.gameObject.SetActiveRecursively(true);
			}else{
				sound.Reverse(false);
				IsReverse = false;
					//BrakeLight.gameObject.SetActiveRecursively(false);
			}
		}
		else
		{
		//brakeForce = Mathf.Sign(throttle) * engineForceValues[0] * rigidbody.mass * BrakeForceFactor;
			if(throttle > 0.0f){
				//throttleForce = Mathf.Sign(throttle) * currentEnginePower * rigidbody.mass;
				throttleForce = Mathf.Sign(throttle) * currentEnginePower;
					rigidbody.velocity = Vector3.zero;
			}else{
				brakeForce = Mathf.Sign(throttle) * BrakeForce;
			}
		}
		/*
		if(rigidbody.velocity.sqrMagnitude > MaxSpeed * EngineForceFactor + engineBonus){
				throttleForce = 0.0f;
		}
		*/
			
		if(IsDrift){
			brakeForce = 0;
		}
		
			
			if(IsReverse)
			{
				throttleForce = throttleForce * 0.3f;
				//BrakeLight.gameObject.SetActiveRecursively(true);
			}else{
				//BrakeLight.gameObject.SetActiveRecursively(false);
			}
			
			//print("transform.forward = "+Vector3.Dot(rigidbody.velocity , transform.forward));
		rigidbody.AddForce(transform.forward * Time.deltaTime * (throttleForce + brakeForce));
	}
		BeforeThrottle = throttle;
}
bool CheckSameSign(float a, float b){
	if(a > 0.0f){
		if(b < 0.0f){
			return false;
		}else{
			return true;
		}
	}else{
		if(b < 0.0f){
			return true;
		}else{
			return false;
		}
	}
}
	public float TurnRatio = 1.0f;
	private float NowTurnRatio;
	private Vector3 steerPoint;
	private float rotateAngle;
	float turnRadius;
	//float minMaxTurn = EvaluateSpeedToTurn(rigidbody.velocity.magnitude);
	float minMaxTurn ;
	float turnSpeed;
//private Vector3 reverseSteerPoint = Vector3.zero;
//public Transform Test;
void  ApplySteering ( bool canSteer ,    Vector3 relativeVelocity  ){
	
	if(canSteer)
	{
		turnRadius = 3.0f / Mathf.Sin((90.0f - (steer * 30.0f)) * Mathf.Deg2Rad);
		//float minMaxTurn = EvaluateSpeedToTurn(rigidbody.velocity.magnitude);
		minMaxTurn = EvaluateSpeedToTurn(CarSpeed);
		turnSpeed = Mathf.Clamp(relativeVelocity.z / turnRadius, -minMaxTurn / 10, minMaxTurn / 10);
		steerPoint = (frontWheels[frontWheels.Length - 2].position + frontWheels[frontWheels.Length - 1].position) * 0.5f;
		if(IsDrift && !CheckSameSign(steerDir,steer)){
				//turnSpeed = Mathf.Clamp(DVAngle / 10.0f, 0.0f ,2.0f);
				turnSpeed = Mathf.Clamp(90.0f / DVAngle ,1.0f,1.5f);
		}
		rotateAngle = turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer;
//		transform.RotateAround(	transform.TransformPoint( (	frontWheels[0].localPosition + frontWheels[1].localPosition) * 0.5f),//transform.position + transform.right * turnRadius * steer, 
//								transform.up, 
//								turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
		if(IsDrift){
			/*if(DVAngle >= 170.0f){
			}else{
			transform.RotateAround((rearWheels[rearWheels.Length - 2].position + rearWheels[rearWheels.Length - 1].position) * 0.5f,//transform.position + transform.right * turnRadius * steer, 
						transform.up, 
						turnSpeed * Mathf.Rad2Deg * Time.deltaTime * steer);
			*/
			MyTransform.RotateAround(steerPoint,//transform.position + transform.right * turnRadius * steer, 
						transform.up, 
						rotateAngle);
			//}
		}else{
				
			//if(DVAngle <= 40.0f || DVAngle >= 160.0f){
				if(!IsReverse)
				{
					turnSpeed *= NowTurnRatio;
//						if(reverseSteerPoint == Vector3.zero && steer != 0)
//						{
//							reverseSteerPoint = transform.position + (transform.right * steer * 5f);
//						}
//					bool canturn = true;
					Vector3 dir = Vector3.Cross(rigidbody.velocity,MyTransform.TransformDirection( Vector3.forward));
					
					if(/*(DVAngle <= 40.0f || DVAngle >= 160.0f)*/(DVAngle <= 45.0f || DVAngle >= 315.0f) || !HaveTheSameSign(dir.y,steer)){
						/*transform.RotateAround(	(	frontWheels[frontWheels.Length - 2].position + frontWheels[frontWheels.Length - 1].position) * 0.5f,//transform.position + transform.right * turnRadius * steer, 
								transform.up, 
								(turnSpeed  * 2 * Mathf.Rad2Deg ) * Time.deltaTime * steer);*/
						/*
						Vector3 temp = (frontWheels[frontWheels.Length - 2].position + frontWheels[frontWheels.Length - 1].position) * 0.5f;
						Vector3 temp2 = (rearWheels[rearWheels.Length - 1].position + frontWheels[frontWheels.Length - 1].position) * 0.5f;
						temp.Set(temp.x,temp.y,temp2.z);
						*/
						MyTransform.RotateAround(steerPoint,//transform.position + transform.right * turnRadius * steer, 
								transform.up, 
								rotateAngle * 2.0f);
					}
//						transform.RotateAround(reverseSteerPoint,//transform.position + transform.right * turnRadius * steer, 
//								transform.up, 
//								(turnSpeed * Mathf.Rad2Deg ) * Time.deltaTime * steer);
//						transform.RotateAround((transform.position + (transform.right * steer * 0.5f)), 
//													transform.up, 
//													CarSpeed * Mathf.Clamp01(1 - CarSpeed / topSpeed) * -Mathf.Sign(steer) * Time.deltaTime * 30);
//						transform.Rotate(transform.up * Time.deltaTime * steer * (turnSpeed  * 30 * Mathf.Rad2Deg ), Space.World);
				}
				else
				{
//					reverseSteerPoint = Vector3.zero;
					MyTransform.RotateAround(steerPoint,//transform.position + transform.right * turnRadius * steer, 
							transform.up, 
							rotateAngle);
				}
			//}
		}
			
		//transform.Rotate(new Vector3(0.0f , DVAngle , 0.0f) , Space.Self);
		
		
		if(initialDragMultiplierX > dragMultiplier.x) // Handbrake
		{
			float rotationDirection = Mathf.Sign(steer); // rotationDirection is -1 or 1 by default, depending on steering
			if(steer == 0)
			{
				if(rigidbody.angularVelocity.y < 1.0f) // If we are not steering and we are handbraking and not rotating fast, we apply a random rotationDirection
					rotationDirection = Random.Range(-1.0f, 1.0f);
				else
					rotationDirection = rigidbody.angularVelocity.y; // If we are rotating fast we are applying that rotation to the car
			}
			// -- Finally we apply this rotation around a point between the cars front wheels.
			/*transform.RotateAround( transform.TransformPoint( (	frontWheels[0].localPosition + frontWheels[1].localPosition) * 0.5f), 
																transform.up, 
																rigidbody.velocity.magnitude * Mathf.Clamp01(1 - rigidbody.velocity.magnitude / topSpeed) * rotationDirection * Time.deltaTime * 6);
				*/
//			transform.RotateAround( transform.TransformPoint( (	frontWheels[0].localPosition + frontWheels[1].localPosition) * 0.5f), 
//															transform.up, 
//															CarSpeed * Mathf.Clamp01(1 - CarSpeed / topSpeed) * rotationDirection * Time.deltaTime * 6);
			if(DVAngle >= 150.0f){
			}else{
				if(IsReverse)
				{
					MyTransform.RotateAround(steerPoint, 
									transform.up, 
									CarSpeed * Mathf.Clamp01(1 - CarSpeed / topSpeed) * rotationDirection * Time.deltaTime * 6);
				}
				else
				{
					MyTransform.RotateAround(steerPoint, 
									transform.up, 
									CarSpeed * Mathf.Clamp01(1 - CarSpeed / topSpeed) * rotationDirection * Time.deltaTime * 6);
				}
			}
		}
	}
}
public float GetTopSpeed(){
	return topSpeed;
}
public int GetGear(){
	return currentGear;
}
public float GetThrottle(){
	return throttle;
}
public float GetEngineLinearFactor(){
	if(currentGear == 0){
		return Mathf.Lerp(0, gearSpeeds[1], relativeVelocity.z / gearSpeeds[1]) / gearSpeeds[1];
	}else{
		float temp = gearSpeeds[currentGear] - gearSpeeds[currentGear - 1];
		
		return (Mathf.Lerp(gearSpeeds[currentGear - 1], gearSpeeds[currentGear],
		    (relativeVelocity.z - gearSpeeds[currentGear - 1])  / temp) - gearSpeeds[currentGear - 1]) / temp;
	}
	
}
public float GetCarSpeed(){
	return CarSpeed;
}
public bool IsCarDrift(){
	return IsDrift;
}
//for Vincent
public void SetSteer (float s) {
	if(IsCanSteer)
	{
		steer = s;
		steer = Mathf.Clamp(steer , -1.0f , 1.0f);
	}
}	
public void SetThrottle (float t) {
	
	throttle = t;
	throttle = Mathf.Clamp(throttle , -1.0f , 1.0f);
}
public void SetBackgroundMusic(bool enable)
{
	
		if(backgroundSoundController)
			backgroundSoundController.PlayBackgroundMusic(enable);
	
}
public void SetReverseSound(bool enable)
{
	
		if(sound)
			sound.Reverse(enable);
	
}
public void SetDragX(float percent){
	dragMultiplier.x = MyCarSettings.dragMultiplier.x * percent;
}
public void SetCanBrake(bool iscanbrake){
	IsCanBrake = iscanbrake;
}
public bool IsCarCanBrake(){
	return IsCanBrake;
}
public bool IsCarMaxSpeed(){
	return IsMaxSpeed;
}
public void IsCarCanSteer(bool iscansteer){
	IsCanSteer = iscansteer;
}
public bool GetIsCanSteer () {
	return IsCanSteer;
}	
	
public void SetEngineForceFactor(float percent){
	EngineForceFactor = percent;
}
public void SetMaxVel(float speed){
	rigidbody.velocity = speed * rigidbody.velocity.normalized;
	topSpeedSqr = speed * speed;
}
public void RestoreMaxVel(){
	topSpeedSqr = gearSpeeds[numberOfGears - 1] * gearSpeeds[numberOfGears - 1];
}
public int GetNextNode(){
	return NextNode;
}
public void StartEngine(){
	if(!IsEngineStart){
		if(sound)
			sound.EngineStart(true);
		IsEngineStart = true;
		//SetEnableUserInput(true);
	}
}
	
//public void SetEnableUserInput(bool isCan) {
//	queryUserInput = isCan;
//}

public void Wait(bool iswaiting){
	if(timer)
		timer.SetWaiting(iswaiting);
	IsWaiting = iswaiting;
		enabled = !iswaiting;
}
	
public void CarWait(bool isWaiting){
	IsWaiting = isWaiting;
}

public bool IsWait () {
	return IsWaiting;
}

public void SetDrift () {
	IsDrift = true;
}
	
public float GetSteer()
{
	return steer;
}
public void SetRound(int tempround)
{
	round = tempround;
}
public int GetRound()
{
	return round;
}
public bool IsCarReverse(){
	return IsReverse;
}
//private void HitWallFreeze(){
//	if(IsHitWallFreeze){
//		NowTurnRatio = 1.0f;
//		if(sound)
//			sound.PlayCollisionSound(true);
//		if(IsPlayer)
//			Camera.main.SendMessage(RotateZShakeIt, SendMessageOptions.DontRequireReceiver);
//		
//		HitWallFreezeTime = 0.2f;
//		rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
//		if(relativeVelocity.z > 0.0f){
//				rigidbody.AddRelativeForce(Vector3.back  * HitWallBrakeForce);
//			}
//				
//		/*
//		float tempk = 2.0f * rigidbody.mass * gearAcc[currentGear]  / collisionInfo.contacts.Length;
//		foreach (ContactPoint contact in collisionInfo.contacts) {
//			rigidbody.AddForce(contact.normal.normalized  * tempk);
//			if(!IsReverse){
//				rigidbody.AddRelativeForce(Vector3.back  * tempk );
//			}
//		}
//		*/
//	}else{
//		NowTurnRatio = TurnRatio;
//	}
//}
//private float HitWallFreezeTime;
//private bool IsHitWallFreeze;
bool startLine = false;
//void  OnCollisionEnter (Collision collisionInfo) {
//	if(collisionInfo.transform.tag == "sidebump"){
//		IsHitWallFreeze = true;
//	}
//}
//void  OnCollisionExit (Collision collisionInfo) {
//	if(collisionInfo.transform.tag == "sidebump"){
//		
//		IsHitWallFreeze = false;
//	}
//}
//void  OnTriggerEnter (Collider other) {
//	if(other.transform.tag.Equals(MaterialCollider)){
//			BroadcastMessage(MyChangeColor,other.transform.GetComponent<MaterialInfo>().Info);
//			
//	}
//}
//void  OnTriggerExit (Collider other) {
//		if(other.transform.tag.Equals(MaterialCollider)){
//			BroadcastMessage(ResetColor);
//	}
//}
public void SetStartLine (bool setStart) {
	startLine = setStart;
//	if(!setStart)
//	{
		NodeWeights = 0;
//	}
}
public bool GetStartLine () {
	return startLine;
}
public void SetNowNode (int index){
		if(!networkView.isMine && networkView.enabled)
		{
			return;
		}
//	int tempNode = index;
//	if(firstNode == -1)
//	{
//		NowNode = tempNode;
//		firstNode = tempNode;
//	}
//	if(NowNode > tempNode)
//	{
//		NodeWeights -= 1;
//	}
//	else if(NowNode < tempNode)
//	{
//		NodeWeights += 1;
//	}
//	NowNode = tempNode;
//	if(firstNode == NowNode && NodeWeights > PathNodeManager.SP.GetNodeLength() * 0.5f)
//	{
//		round += 1;
//		Mathf.Clamp(round,0.0f,10.0f);
//		NodeWeights = 0;
//	}
//	NextNode = NowNode + 1;
//	if(NextNode >= PathNodeManager.SP.GetNodeLength()){
//		NextNode = 0;
//	}
	int tempNode = index;
	if(startLine)
	{
//		if(NowNode != 0 || tempNode != PathNodeManager.SP.GetNodeLength() - 1)
//		{
			NodeWeights += tempNode - NowNode;
//			Debug.Log(NodeWeights);
//		}
//		else
//		{
//			startLine = false;
//		}
		NowNode = tempNode;
		NextNode = NowNode + 1;
		if(NextNode >= PathNodeManager.SP.GetNodeLength()){
			NextNode = 0;
		}
	}
		
}
public int GetWeights () {
	return NodeWeights;
}
/**************************************************/
/*               Utility Functions                */
/**************************************************/

float Convert_Miles_Per_Hour_To_Meters_Per_Second ( float value  ){
	return value * 0.44704f;
}

float Convert_Meters_Per_Second_To_Miles_Per_Hour ( float value  ){
	return value * 2.23693629f;	
}

bool HaveTheSameSign ( float first ,   float second  ){
	 if (Mathf.Sign(first) == Mathf.Sign(second))
		return true;
	else
		return false;
}

float  EvaluateSpeedToTurn ( float speed  ){
	if(speed > topSpeed / 2)
		return minimumTurn;
	
	float speedIndex = 1 - (speed / (topSpeed / 2));
	return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
}

float  EvaluateNormPower ( float normPower  ){
	if(normPower < 1)
		return 10 - normPower * 9;
	else
		return 1.9f - normPower * 0.9f;
}

float  GetGearState (){
	Vector3 relativeVelocity = MyTransform.InverseTransformDirection(rigidbody.velocity);
	float lowLimit = (currentGear == 0 ? 0 : gearSpeeds[currentGear-1]);
	return (relativeVelocity.z - lowLimit) / (gearSpeeds[currentGear - (int)lowLimit]) * (1 - currentGear * 0.1f) + currentGear * 0.1f;
}

	public void EngineBonus (int rank) {
		switch (rank)
		{
			case 1:
				engineBonus = 0.0f;
				break;
			case 2:
				engineBonus = 250.0f;
				break;
			case 3:
				engineBonus = 750.0f;
				break;
			case 4:
				engineBonus = 1500.0f;
				break;
		}
	}
	
	public bool CheckWrongWay () {
		if(MyTransform == null){
			 MyTransform = transform;
		}
		if(Quaternion.Angle(MyTransform.rotation, PathNodeManager.SP.getNode(NowNode).rotation) > 120)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public void Accelaration () {
		throttle += 0.5f;
		throttle = Mathf.Clamp(throttle , -1.0f , 1.0f);
	}
	public void Brake(){
		if(!IsCarCanBrake())
			return;
		pressBrake = true;
		throttle -= 0.5f;
		throttle = Mathf.Clamp(throttle , -1.0f , 1.0f);
		
	}
	
	public void UnBrake () {
		pressBrake = false;
	}
	private bool IsSteerRight = false;
	private bool IsSteerLeft = false;
	private bool IsSArrowPress = false;
	/*
	public void Steer(float steerfactor){
		if(steerfactor > 0.0f){
			IsSteerRight = true;
		}else{
			IsSteerLeft = true;
		}
		steer += steerfactor;
	}
	*/
	public void SetPressSteer (float s) {
		if(IsCanSteer)
		{
			steer += s;
			steer = Mathf.Clamp(steer , -1.0f , 1.0f);
		}
	}	
	void SetSteerRight(bool isstr){
		IsSteerRight = isstr;
	}
	void SetSteerLeft(bool isstr){
		IsSteerLeft = isstr;
	}
	
	
	public void SetArrowPress(bool isstr){
		IsSArrowPress = isstr;
	}
	public void SteerAutoCenter(){
		
		if(!IsDrift && !IsSteerRight && !IsSteerLeft && !IsSArrowPress){
			//SetSteer(Mathf.MoveTowards(steer,0.0f,0.2f));
			steer = Mathf.MoveTowards(steer,0.0f,0.2f);
			//steer = 0.0f;
		}
		
	}
	
}
