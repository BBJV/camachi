using UnityEngine;
using System.Collections;

public class CarProperty : MonoBehaviour {
	public float _energy = 0.0f;
	public int carID = 0;
	public int ownerGUPID = 0;
	public string playerName;
	public float energyRate = 0.1f;
	public Skill[] skills;
//	public Skill levelOne;
//	public Skill levelTwo;
//	public Skill levelThree;
	public Texture2D[][] SkillIcon;
	public ParticleEmitter StateBoard;
	
	private bool _canSlowDown = true;
	private DriftCar car;
	private CarB carB;
	private Vector3 defaultScale;
	private float oriGrip;
	private bool skillProtect = false;
	private bool isStick = false;
	private bool unStick = false;
	public Transform[] rearWheels;
	private StickData[] stickWheels;
	private int skillTimes = 0;
	private int barrierTimes = 0;
	private SmoothFollow SMCamera;
//	private TvCMatchRoom tvcMatchRoom;
	private bool isSpeedLimit = false;
	public bool isAI = false;
	public BoxCollider oriCollider;
	public BoxCollider triggerCollider;
	private CarAI carAI;
	private RankManager rank;
	public UILabel rankText;
	public bool showRank = false;
//	public GUITexture takePicture;
//	public GUITexture shutterPicture;
//	public GUITexture limitPicture;
//	public AudioClip snapShotAudio;
//	public Helicopter helicopterPrefab;
//	public GUITexture oilClickPrefab;
//	public AudioClip oilAudio;
//	public AudioClip oilClickAudio;
	private Vector2 resolutionScale = new Vector2(Screen.width / 1024.0f, Screen.height / 768.0f);
	private float driftTime = 0.0f;
	private float useEnergy = 0.0f;
	private int beHitTime = 0;
	private SkillProbability[] skillProbabilitys;
	public Texture2D showSkillIcon;
	private Rect skillRect;
	public AudioClip slipAudio;
	public AudioClip slowAudio;
	
	class SkillProbability {
		public ArrayList probabilitys = new ArrayList();
		private int index = 0;
		
		public int GetSkill () {
			return (int)probabilitys[Random.Range(0, probabilitys.Count)];
		}
		
		public void AddProbability (int probability) {
			for(int i = 0; i < probability; i++)
			{
				if(i % 2 == 0)
				{
					probabilitys.Insert(Random.Range(0, probabilitys.Count - 1), index);
				}
				else
				{
					probabilitys.Add(index);
				}
			}
			index += 1;
		}
	}
	
	class StickData {
		public Transform wheel;
		public int isStick = 1;
		public GUITexture oilClick;
		
		public StickData (Transform wheelTransform) {
			wheel = wheelTransform;
		}
	};
	
	void Start () {
		car = GetComponent<DriftCar>();
		carB = GetComponent<CarB>();
		carAI = GetComponent<CarAI>();
		SMCamera = Camera.main.GetComponent<SmoothFollow>();
//		if(networkView.enabled)
//		{
//			MatchArenaState mas = FindObjectOfType(typeof(MatchArenaState)) as MatchArenaState;
//			tvcMatchRoom = mas.GetTvCMatchRoom();
//		}
		if(!rank)
		{
			rank = FindObjectOfType(typeof(RankManager)) as RankManager;
		}
		defaultScale = transform.localScale;
		if(car)
			oriGrip = car.handlingTendency;
		if(rearWheels.Length > 0)
		{
			stickWheels = new StickData[rearWheels.Length];
			int i = 0;
			foreach(Transform wheel in rearWheels)
			{
				stickWheels[i] = new StickData(wheel);
				i++;
			}
		}
		
		/*SkillIcon[0] = levelTwo.GetIcon()[0];
		SkillIcon = levelTwo.GetIcon()[1];
		SkillIcon[2][0] = levelThree.GetIcon()[0];
		SkillIcon[2][1] = levelThree.GetIcon()[1];*/
//		while(true)
//		{
//			AddEnergy(energyRate);
//			yield return new WaitForSeconds(1);
//		}
		skillProbabilitys = new SkillProbability[4];
		for(int i = 0; i < 4; i++)
		{
			skillProbabilitys[i] = new SkillProbability();
//			skillProbabilitys[i].AddProbability(levelOne.Probability(i));
//			skillProbabilitys[i].AddProbability(levelTwo.Probability(i));
//			skillProbabilitys[i].AddProbability(levelThree.Probability(i));
			foreach(Skill skill in skills)
			{
				skillProbabilitys[i].AddProbability(skill.Probability(i));
			}
		}
//		skillRect = new Rect((Screen.width - (levelOne.SkillIcon.width * resolutionScale.x)) * 0.5f, levelOne.SkillIcon.height * resolutionScale.y, levelOne.SkillIcon.width * resolutionScale.x, levelOne.SkillIcon.height * resolutionScale.y);
		skillRect = new Rect((Screen.width - (skills[0].SkillIcon.width * resolutionScale.x)) * 0.5f, skills[0].SkillIcon.height * resolutionScale.y, skills[0].SkillIcon.width * resolutionScale.x, skills[0].SkillIcon.height * resolutionScale.y);
	}
	
	// Update is called once per frame
	void Update () {
//		_energy = 3.0f;
//		if(Input.GetKeyDown(KeyCode.Q) && _energy >= 1.0f && !levelOne.IsSkillUsing())
//		{
//			LevelOne();
//		}
//		if(Input.GetKeyDown(KeyCode.W) && _energy >= 2.0f && !levelTwo.IsSkillUsing())
//		{
//			LevelTwo();
//		}
//		if(Input.GetKeyDown(KeyCode.E) && _energy >= 3.0f && !levelThree.IsSkillUsing())
//		{
//			LevelThree();
//		}
		isAI = carAI.enabled;
//		if(carB && carB.IsCarDrift())
//		{
//			AddEnergy(energyRate * Time.deltaTime);
//		}
		if(showRank)
		{
			rankText.text = GetRank().ToString() + ((SMCamera.target == transform) ? "" : (":" + playerName));
		}
#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.P) && SMCamera.target == transform)
		{
			SetEnergy(4);
		}
#endif
	}
	
	public void AddEnergy (float energy) {
		
		_energy += energy;
		_energy = Mathf.Clamp(_energy , 0.0f , 3.0f);
		driftTime += Time.deltaTime;
	}
	
	public float CheckEnergy () {
		return _energy;
	}
	
	public void ReduceEnergy (float energy) {
		if(_energy > 0)
			_energy -= energy;
	}
	
	public void SetShowRank (bool show) {
		showRank = show;
	}
	
	public void SetEnergy (float energy) {
		_energy = energy;
//		if(_energy == 1.0f)
//		{
//			showSkillIcon = levelOne.SkillIcon;
//		}
//		else if(_energy == 2.0f)
//		{
//			showSkillIcon = levelTwo.SkillIcon;
//		}
//		else if(_energy == 3.0f)
//		{
//			showSkillIcon = levelThree.SkillIcon;
//		}
		showSkillIcon = skills[(int)_energy - 1].SkillIcon;
	}
//	public void ReduceHealthPoint (float damage)
//	{
//		healthPoint = Mathf.Clamp(healthPoint - damage, 0, maxHealthPoint);
//		if(healthPoint == 0)
//		{
//			BroadcastMessage("RepairCar", SendMessageOptions.DontRequireReceiver);
//		}
//	}
//	
//	public void RecoverHp (float percent)
//	{
//		healthPoint = Mathf.Clamp(healthPoint + (maxHealthPoint * percent / 100.0f), 0, maxHealthPoint);
//	}
//	
//	IEnumerator RepairCar () {
//		//play repair animation
//		yield return new WaitForSeconds(5);
//		BroadcastMessage("RepairCarFinish", SendMessageOptions.DontRequireReceiver);
//	}
//	
//	void RepairCarFinish () {
//		healthPoint = maxHealthPoint;
//	}
	
	public bool CheckSkillUsing () {
//		if(levelOne.IsSkillUsing() || levelTwo.IsSkillUsing() || levelThree.IsSkillUsing())
//		{
//			return true;
//		}
		foreach(Skill skill in skills)
		{
			if(skill.IsSkillUsing())
			{
				return true;
			}
		}
		
		return false;
	}
	
//	public void LevelOne () {
////		Debug.Log("levelOne unuse");
//		if(levelOne && _energy == 1.0f && !CheckSkillUsing() && !levelOne.skillLock)
//		{
////			Debug.Log("levelOne use");
//			if(networkView.enabled)
//			{
//				networkView.RPC("SendToGameServer_SkillOne", RPCMode.Server, ownerGUPID, levelOne.skillLevel);
////				networkView.RPC("ReceiveByClientPortal_SkillOne", RPCMode.All, ownerGUPID, 1.0f);
//			}
//			else
//			{
//				levelOne.Use(this, levelOne.skillLevel);
//			}
//			_energy -= 1.0f;
//			useEnergy += 1.0f;
//			showSkillIcon = null;
//		}
//	}
//	
//	public void LevelTwo () {
//		if(levelTwo && _energy == 2.0f && !CheckSkillUsing() && !levelTwo.skillLock)
//		{
////			Debug.LogError("use skill : " + ownerGUPID + " energy : " + _energy);
//			if(networkView.enabled)
//			{
//				networkView.RPC("SendToGameServer_SkillTwo", RPCMode.Server, ownerGUPID, levelTwo.skillLevel);
////				networkView.RPC("ReceiveByClientPortal_SkillTwo", RPCMode.All, ownerGUPID, 1.0f);
//			}
//			else
//			{
//				levelTwo.Use(this, levelTwo.skillLevel);
//			}
//			_energy -= 2.0f;
//			useEnergy += 2.0f;
//			showSkillIcon = null;
//		}
//	}
//	
//	public void LevelThree () {
//		if(levelThree && _energy == 3.0f && !CheckSkillUsing() && !levelThree.skillLock)
//		{
//			if(networkView.enabled)
//			{
//				networkView.RPC("SendToGameServer_SkillThree", RPCMode.Server, ownerGUPID, levelThree.skillLevel);
////				networkView.RPC("ReceiveByClientPortal_SkillThree", RPCMode.All, ownerGUPID, 1.0f);
//			}
//			else
//			{
//				levelThree.Use(this, levelThree.skillLevel);
//			}
//			_energy -= 3.0f;
//			useEnergy += 3.0f;
//			showSkillIcon = null;
//		}
//	}
	
	public void UseSkill () {
//		_energy = 0.0f;
//		LevelOne();
//		LevelTwo();
//		LevelThree();
		if(_energy != 0 && !CheckSkillUsing() && !skills[(int)_energy - 1].skillLock)
		{
			if(networkView.enabled)
			{
				networkView.RPC("SendToGameServer_Skill", RPCMode.Server, ownerGUPID, (int)_energy, skills[(int)_energy - 1].skillLevel);
			}
			else
			{
				skills[(int)_energy - 1].Use(this, skills[(int)_energy - 1].skillLevel);
			}
			BroadcastMessage("ShowState",(int)_energy - 1);
			useEnergy += _energy;
			_energy = 0.0f;
			showSkillIcon = null;
			
		}
	}
	
	public void SlowDown (bool canSlowDown) {
		_canSlowDown = canSlowDown;
		if(!_canSlowDown)
		{
			if(isSpeedLimit)
			{
				isSpeedLimit = false;
				carB.RestoreMaxVel();
			}
			UnSticky();
		}
	}
	
	private float weakenSlowSkillPercent = 0.0f;
	public void SpeedUp (float percent) {
		percent = Mathf.Clamp(percent, -100.0f, percent);
		if(percent < 0.0f)
		{
			percent = percent * (1.0f - weakenSlowSkillPercent);
		}
		if(skillProtect && percent < 0.0f)
		{
			return;
		}
		rigidbody.velocity = new Vector3(rigidbody.velocity.x * (1.0f + (percent * 0.01f)), rigidbody.velocity.y, rigidbody.velocity.z * (1.0f + (percent * 0.01f)));
	}
	
	public void SetWeakenSlowSkillPercent (float percent) {
		weakenSlowSkillPercent = percent;
	}
	
	public void RPMUp (float percent) {
		if(car)
			car.SetMaxRPM(1.0f + (percent * 0.01f));
	}
	
	
	private bool speedUp = false;
	public void TorqueUp (float percent, Color skillColor) {
//		if(car)
//			car.SetMaxTorque(1.0f + (percent * 0.01f));
		if(carB)
		{
			if(percent < 0.0f)
			{
				percent = percent * (1.0f - weakenSlowSkillPercent);
			}
			if((skillProtect && percent <= 0.0f) || (speedUp && percent < 0.0f))
			{
				return;
			}
			if(percent > 0)
			{
				speedUp = true;
				slowness = false;
			}
			if(percent == 0)
			{
				speedUp = false;
				slowness = false;
			}
			carB.SetEngineForceFactor(1.0f + (percent * 0.01f));
			ChangeColor(skillColor);
		}
	}
	
	private bool slowness = false;
	private float slowEffectTime = 0;
	IEnumerator Slowness (float effectTime) {
		slowEffectTime = effectTime;
		if(skillProtect || slowness || speedUp)
		{
			yield break;
		}
		BroadcastMessage("ShowState",4);
		slowness = true;
		AudioSource slowSource = gameObject.AddComponent<AudioSource>();
		slowSource.clip = slowAudio;
		slowSource.loop = true;
		slowSource.Play();
		while(!skillProtect && !speedUp && slowness && slowEffectTime > 0)
		{
			slowEffectTime -= Time.deltaTime;
			yield return null;
		}
		if(!speedUp)
		{
			TorqueUp(0.0f, Color.white);
		}
		Destroy(slowSource);
	}
	
	void ChangeColor (Color skillColor) {
		BroadcastMessage("SkillColor", skillColor, SendMessageOptions.DontRequireReceiver);
	}
	
	public void CanBrake (bool canBrake) {
		if(skillProtect)
			return;
		if(car)
			car.SetCanBrake(canBrake);
		if(carB)
			carB.SetCanBrake(canBrake);
	}
	
	public int GetRank () {
		if(!rank)
		{
			rank = FindObjectOfType(typeof(RankManager)) as RankManager;
		}
		return rank.GetRank(transform);
	}
	
	public void ChangeSize (float percent) {
//		float locationY = transform.localPosition.y;
		float oldScaleY = transform.localScale.y;
		transform.localScale = defaultScale * percent;
		if(percent > 1)
			transform.localPosition += new Vector3(0,(transform.localScale.y - oldScaleY) * 0.5f,0);
	}
	
	private bool isSlip = false;
	private float weakenSkillTime = 0.0f;
	public IEnumerator SetSlip (Hashtable args) {
		if(skillProtect || isSlip)
			yield break;
		isSlip = true;
		AudioSource slipSource = gameObject.AddComponent<AudioSource>();
		slipSource.clip = slipAudio;
		slipSource.loop = true;
		slipSource.Play();
		float second = (float)args["second"];
		float speed = (float)args["speed"];
		second = Mathf.Clamp(second - weakenSkillTime, 0.0f, second);
		beHitTime += 1;
		float startTime = 0.0f;
		BroadcastMessage("ShowState",5);
		while(startTime < second)
		{
			transform.Rotate(Vector3.up * Mathf.Clamp(speed, 1, speed));
			carB.SetDrift();
			startTime += Time.deltaTime;
			yield return null;
		}
		Destroy(slipSource);
		isSlip = false;
	}
	
	public void SetWeakenSkillTime (float second) {
		weakenSkillTime = second;
	}
	
	public bool CheckSlip () {
		return isSlip;
	}
	
	public void UnCrash (bool setTrigger)
	{
//		BoxCollider bx = GetComponent<BoxCollider>();
		oriCollider.isTrigger = setTrigger;
	}
	
	public bool UnMaxSpeed ()
	{
		if(car)
			return car.CheckSpeed();
		if(carB)
			return !carB.IsCarMaxSpeed();
		return false;
	}
	
	private bool isDodge = false;
	public void DodgeSteer (float steer) {
		if(skillProtect || isDodge)
			return;
		isDodge = true;
		if(car)
			car.SetSteer(steer);
		if(carB)
			carB.SetSteer(steer);
		beHitTime += 1;
		StartCoroutine(LockSteer(1.0f));
	}
	
	public bool IsAirTime () {
		return carB.CheckAirTime();
	}
	
	IEnumerator LockSteer (float time) {
		SetCarSteer(false);
		yield return new WaitForSeconds(time);
		SetCarSteer(true);
		isDodge = false;
	}
	
	void SetCarSteer (bool isCanSteer) {
		if(car)
			car.SetCanSteer(isCanSteer);
		if(carB)
			carB.IsCarCanSteer(isCanSteer);
	}
	
	public void Protect (bool isProtect) {
		if(isProtect)
			gameObject.layer = LayerMask.NameToLayer("Protect");
		else
			gameObject.layer = LayerMask.NameToLayer("Car");
		skillProtect = isProtect;
	}
	
	public bool IsProtect () {
		return skillProtect;
	}
	
	public void ChangeGrip (float percent) {
		if(car)
			car.handlingTendency = Mathf.Clamp01(oriGrip * (1 + percent * 0.01f));
		if(carB)
			carB.SetDragX(1 + percent * 0.01f);
	}
	
	public void SetUnStick (bool setting) {
		unStick = setting;
	}
	
	public IEnumerator Sticky (float percent, int hit, Collider oilCollider, CarProperty owner) {
		if(isStick || skillProtect || !_canSlowDown || (unStick && (owner == this)))
			yield break;
		AddSkillTime();
		beHitTime += 1;
		isStick = true;
		foreach(StickData wheel in stickWheels)
		{
			wheel.isStick = hit;
			if(SMCamera.target == transform)
			{
				if(wheel.oilClick)
				{
					Destroy(wheel.oilClick.gameObject);
				}
//				wheel.oilClick = Instantiate(oilClickPrefab, Vector3.zero, Quaternion.identity) as GUITexture;
			}
		}
		
		if(car)
		{
			RPMUp(percent);
			TorqueUp(percent, Color.white);
			SpeedUp(percent);
		}
		
		if(carB)
		{
			TorqueUp(percent, Color.white);
			SpeedUp(percent);
		}
		AudioSource oilSound = gameObject.AddComponent<AudioSource>();
//		oilSound.clip = oilAudio;
//		oilSound.loop = true;
		while(isStick)
		{
			if(!oilCollider || !triggerCollider.bounds.Contains(oilCollider.ClosestPointOnBounds(triggerCollider.transform.position)))
			{
				UnSticky();
			}
			yield return null;
		}
		Destroy(oilSound);
		if(car)
		{
			RPMUp(0);
			TorqueUp(0, Color.white);
		}
		
		if(carB)
		{
			TorqueUp(0, Color.white);
		}
	}
	
	public void UnSticky () {
		isStick = false;
		foreach(StickData wheel in stickWheels)
		{
			if(wheel.oilClick)
			{
				Destroy(wheel.oilClick.gameObject);
			}
		}
	}
	
	public void ChangeDirection (Vector3 direction) {
		if(skillProtect)
			return;
		rigidbody.AddForce(direction * rigidbody.mass * 10);
	}
	
	public bool CheckSpeedLimit () {
		return isSpeedLimit;
	}
	
	public void SetSpeedLimit (float speed, float time) {
		if(networkView.enabled)
		{
			networkView.RPC("SendToGameServer_SetMaxVel", RPCMode.Server, ownerGUPID, speed, time);
		}
		else
		{
			SetMaxVel(speed, time);
		}
	}
	
	public void SetMaxVel (float speed, float time) {
		if(skillProtect || !_canSlowDown || isSpeedLimit)
			return;
		AddSkillTime();
		beHitTime += 1;
		isSpeedLimit = true;
		if(SMCamera.target == transform)
		{
//			AudioSource.PlayClipAtPoint(snapShotAudio, transform.position);
		}
		carB.SetMaxVel(speed);
		StartCoroutine(TakePicture());
		StartCoroutine(RestoreMaxVel(time));
	}
	
	IEnumerator TakePicture () {
		if(SMCamera.target == transform)
		{
//			GUITexture shutter = Instantiate(shutterPicture, Vector3.zero, Quaternion.identity) as GUITexture;
//			shutter.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
//			GUITexture tp = Instantiate(takePicture, Vector3.zero, Quaternion.identity) as GUITexture;
			Rect tempPixel = new Rect(0, 0, Screen.width, Screen.height);
//			tp.pixelInset = tempPixel;
			yield return new WaitForSeconds(0.1f);
//			Destroy(tp);
		}
	}
	
	IEnumerator RestoreMaxVel(float time){
		yield return new WaitForSeconds(time);
		isSpeedLimit = false;
		carB.RestoreMaxVel();
	}
	
	public void Spikes (float percent) {
		if(skillProtect)
			return;
		StartCoroutine(SpikesEffect(percent));
	}
	
	IEnumerator SpikesEffect (float percent) {
		CanBrake(false);
		SetCarSteer(false);
		carB.CarWait(true);
		beHitTime += 1;
		while(rigidbody.velocity.magnitude > 1f)
		{
			SpeedUp(percent * Time.deltaTime);
			percent += percent * Time.deltaTime;
			yield return null;
		}
		carB.CarWait(false);
		CanBrake(true);
		SetCarSteer(true);
	}
	
	public Rect GetSkillRect () {
		return skillRect;
	}
	
	private TurnOnOffTriangle turnOnOff;
	public IEnumerator Terminal (float effectTime, float reactionTime, AudioClip policeSirenAudio) {
		if(skillProtect)
		{
			yield break;
		}
		if(SMCamera.target == transform)
		{
//			GUITexture limit = Instantiate(limitPicture, Vector3.zero, Quaternion.identity) as GUITexture;
//			limit.pixelInset = new Rect((Screen.width - limit.pixelInset.width) * 0.5f * resolutionScale.x, (Screen.height - limit.pixelInset.height) * 0.5f * resolutionScale.y, limit.pixelInset.width * resolutionScale.x, limit.pixelInset.height * resolutionScale.y);
		}
		yield return new WaitForSeconds(reactionTime);
		if(rigidbody.velocity.magnitude < 25.0f)
		{
			yield break;
		}
		if(!turnOnOff)
		{
			turnOnOff = GetComponent<TurnOnOffTriangle>();
		}
		turnOnOff.TurnOnTriangles();
		if(SMCamera.target == transform)
		{
//			Helicopter helicopter = Instantiate(helicopterPrefab, new Vector3(transform.position.x + 20, transform.position.y + 25, transform.position.z + 20), Quaternion.identity) as Helicopter;
//			helicopter.target = transform;
			AudioSource sirenAudio = gameObject.AddComponent<AudioSource>();
			sirenAudio.clip = policeSirenAudio;
			sirenAudio.loop = true;
			SMCamera.camera.enabled = false;
			carB.CarWait(true);
			SpeedUp(-100.0f);
			while(effectTime > 0 && !skillProtect)
			{
				effectTime -= Time.deltaTime;
				yield return null;
			}
//			Destroy(helicopter.gameObject);
			Destroy(sirenAudio);
			SMCamera.camera.enabled = true;
			beHitTime += 1;
		}
		else
		{
			carB.CarWait(true);
			SpeedUp(-100.0f);
			while(effectTime > 0 && !skillProtect)
			{
				effectTime -= Time.deltaTime;
				yield return null;
			}
		}
		carB.CarWait(false);
		turnOnOff.TurnOffTriangles();
	}
	
//	private int showSkillIndex = 0;
//	private float showSkillIndexTime = 0.0f;
//	void OnGUI () {
//		if(SMCamera.target == transform)
//		{
//			if(getSkillBox)
//			{
//				if(Time.time - showSkillIndexTime >= 0.1f)
//				{
//					showSkillIndex = Random.Range(0, skills.Length);
//					showSkillIndexTime = Time.time;
//				}
//				GUI.DrawTexture(skillRect, skills[showSkillIndex].SkillIcon);
////				switch(showSkillIndex)
////				{
////					case 0:
////						GUI.DrawTexture(skillRect, levelOne.SkillIcon);
////						break;
////					case 1:
////						GUI.DrawTexture(skillRect, levelTwo.SkillIcon);
////						break;
////					case 2:
////						GUI.DrawTexture(skillRect, levelThree.SkillIcon);
////						break;
////				}
//				if(Time.time - getSkillBoxTime >= 2.0f && !CheckSkillUsing())
//				{
//					getSkillBox = false;
//				}
//			}
//			if(showSkillIcon != null)
//			{
//				GUI.DrawTexture(skillRect, showSkillIcon);
//			}
//			bool stillStick = false;
//			if(isStick)
//			{
//				Vector2 pixel = new Vector2(oilClickPrefab.pixelInset.width * resolutionScale.x, oilClickPrefab.pixelInset.height * resolutionScale.y);
//				foreach(StickData wheel in stickWheels)
//				{
//					if(wheel.isStick > 0)
//					{
//						Vector3 screenPos = Camera.main.WorldToScreenPoint(wheel.wheel.position);
//						wheel.oilClick.pixelInset = new Rect(screenPos.x - pixel.x * 0.5f, screenPos.y - pixel.y * 0.5f, pixel.x, pixel.y);
//						if(CustomGUI.Button(new Rect(screenPos.x - pixel.x * 0.5f,Screen.height -  screenPos.y - pixel.y * 0.5f, pixel.x, pixel.y), "", new GUIStyle()) || isAI)
//						{
//							wheel.isStick -= 1;
//							AudioSource.PlayClipAtPoint(oilClickAudio, transform.position);
//						}
//						if(wheel.isStick > 0)
//						{
//							stillStick = true;
//						}
//					}
//					else
//					{
//						if(wheel.oilClick)
//						{
//							Destroy(wheel.oilClick.gameObject);
//						}
//					}
//				}
//				isStick = stillStick;
//				if(!isStick)
//				{
//					UnSticky();
//				}
//			}
//		}
//	}
	
	public void AddSkillTime () {
		skillTimes += 1;
	}
	
	public int GetSkillTime () {
		return skillTimes;
	}
	
	public void AddBarrierTime () {
		barrierTimes += 1;
	}
	
	public int GetBarrierTime () {
		return barrierTimes;
	}
	
	public Texture2D GetSkillIcon(int level){
		return skills[level].SkillIcon;
//		if(level == 0 && levelOne){
//			return levelOne.SkillIcon;
//		}else if(level == 1 && levelTwo){
//			return levelTwo.SkillIcon;
//		}else{
//			return levelThree.SkillIcon;
//		}
	}
	
	public bool CheckSkillLock (int level) {
		return skills[level].skillLock;
//		if(level == 0 && levelOne){
//			return levelOne.skillLock;
//		}else if(level == 1 && levelTwo){
//			return levelTwo.skillLock;
//		}else{
//			return levelThree.skillLock;
//		}
	}
	
	public float GetDriftTime () {
		return driftTime;
	}
	
	public float GetUseEnergy () {
		return useEnergy;
	}
	
	public int GetBeHitTime () {
		return beHitTime;
	}
	
	public bool getSkillBox = false;
	public float getSkillBoxTime = 0.0f;
	public IEnumerator GetSkill () {
		if(showSkillIcon == null)
		{
			getSkillBox = true;
			getSkillBoxTime = Time.time;
			int selfRank = GetRank();
			while(Time.time - getSkillBoxTime < 2.0f || CheckSkillUsing())
			{
				yield return null;
			}
//			yield return new WaitForSeconds(1.0f);
			int level = skillProbabilitys[selfRank - 1].GetSkill();
			if(level >= 0)
			{
				SetEnergy((float)level + 1);
			}
			else
			{
				SetEnergy(1.0f);
			}
//			if(level == 0)
//			{
//				SetEnergy(1.0f);
//			}
//			else if(level == 1)
//			{
//				SetEnergy(2.0f);
//			}
//			else if(level == 2)
//			{
//				SetEnergy(3.0f);
//			}
//			else
//			{
//				SetEnergy((float)Random.Range(1, 4));
//			}
		}
	}
	
	public bool canAbsorbSkill = false;
	public void HitBySkill (float skill) {
		if(canAbsorbSkill)
		{
			if(_energy == 0.0f)
			{
				if(Random.Range(0, 2) == 1)
				{
					SetEnergy(skill);
				}
			}
		}
	}
	
	void OnDestroy() {
        rank.SetRanksCar();
    }
	
	[RPC]
	public void SendToGameServer_SkillOne(int GUPID, int skillOne) {
		
	}
	
	[RPC]
	public void SendToGameServer_SkillTwo(int GUPID, int skillTwo) {
		
	}
	
	[RPC]
	public void SendToGameServer_SkillThree(int GUPID, int skillThree) {
		
	}
	
	[RPC]
	public void SendToGameServer_Skill(int GUPID, int skillIndex, int skillLevel) {
		
	}
	
	[RPC]
	public void SendToGameServer_SetMaxVel(int GUPID, float speed, float time) {
		
	}
	
	
//	[RPC]
//	public void ReceiveByClientPortal_SkillOne(int GUPID, int skillOne) {
//		if(ownerGUPID == GUPID)
//		{
//			levelOne.Use(this, skillOne);
//		}
//	}
//	
//	[RPC]
//	public void ReceiveByClientPortal_SkillTwo(int GUPID, int skillTwo) {
//		if(ownerGUPID == GUPID)
//		{
////			Debug.LogError("Receive skill : " + ownerGUPID);
//			levelTwo.Use(this, skillTwo);
//		}
//	}
//	
//	[RPC]
//	public void ReceiveByClientPortal_SkillThree(int GUPID, int skillThree) {
//		if(ownerGUPID == GUPID)
//		{
//			levelThree.Use(this, skillThree);
//		}
//	}
	
	[RPC]
	public void ReceiveByClientPortal_Skill(int GUPID, int skillIndex, int skillLevel) {
		if(ownerGUPID == GUPID)
		{
			skills[skillIndex - 1].Use(this, skillLevel);
		}
	}
	
	[RPC]
	public void ReceiveByClientPortal_SetMaxVel(int GUPID, float speed, float time) {
		if(ownerGUPID == GUPID)
		{
			SetMaxVel (speed, time);
		}
	}
 }
