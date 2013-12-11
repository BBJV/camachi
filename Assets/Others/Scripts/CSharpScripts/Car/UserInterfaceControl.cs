using UnityEngine;
using System.Collections;

public class UserInterfaceControl : MonoBehaviour {
//	public Texture2D Bg_CarPos;
//	public Texture2D Bg_Skill;
//	public Texture2D[] Btn_Brake;
//	public Texture2D lockIcon;
//	public Texture2D Tex_Yahoo;
	
//	private bool IsShowCarPos;
//	private bool IsShowSkillBg;
	private bool[] IsShowSkill;
//	private bool IsShowEnergy;
//	private bool IsShowLoop;
//	private bool IsShowRank;
//	private bool IsShowTime;
//	private bool IsShowBrake;
	/*
	public Texture2D Bg_EnergyBar;
	public Texture2D[] EnergyBar;
	*/
	public Texture2D[] LoopNum;
//	public Texture2D LoopSlash;
//	public GUITexture loopNumNow;
//	public GUITexture loopNumBase;
	/*
	public Texture2D[] TimeNum;
	public Texture2D TimeColon;
	public GUITexture timeNumSec10;
	public GUITexture timeNumSec;
	public GUITexture timeNumMin10;
	public GUITexture timeNumMin;
	*/
//	public GUITexture rankTexture;
	
	public float scale;
	/*
	public float[] EnergyBar1;
	public float[] EnergyBar2;
	public float[] EnergyBar3;
	*/
	private CarB carb;
//	private Timer timer;
	private float steerfactor;
//	private float autosteerfactor;
	private CarProperty property;
	private Rect Bg_CarPos_Co;
	/*
	private Rect Bg_Skill_Co;
	private Rect[] Skill_Co;
	*/
	private Rect[] Btn_Brake_Co;
	/*
	private Rect Bg_EnergyBar_Co;
	private Rect[] EnergyBar_Co;
	*/
	private Rect LoopNumNow_Co;
	private Rect LoopSlash_Co;
	private Rect LoopNumBase_Co;
	/*
	private Rect TimeNumMin10_Co;
	private Rect TimeNumMin_Co;
	private Rect TimeColon_Co;
	private Rect TimeNumSec10_Co;
	private Rect TimeNumSec_Co;
	*/
	private float Btn_Brake_Buttom_Co_Y;
	private float L_Btn_Brake_Buttom_Co_X;
	private float R_Btn_Brake_Buttom_Co_X;
	
	private float Pad_Brake_Buttom_Co_UP_Y;
	private float Pad_Brake_Buttom_Co_DOWN_Y;
	public int maxLoop = 3;
	public bool showGUI = false;
//	public GUISkin controlSkin;
//	private Rect rect = new Rect(11.0f, Screen.height - 109.0f, 237.0f, 69.0f);
//	private Rect brakeRect = new Rect(800.0f, 45.0f, 142.0f, 170.0f);
	//private GUITexture sliderTexture;
	private Vector2 guiTouchOffset;
	private Rect oriRect;
	private float minLimit;
	private float maxLimit;
//	public GUITexture backgroundTexture;
//	public GUITexture brakeTexture;
//	public GUITexture skillTexture;
	public SmoothFollow smCamera;
//	private Color sliderColor = new Color(0.5f, 0.5f, 0.5f, 0.25f);
//	private Color brakeColor = new Color(0.5f, 0.5f, 0.5f, 0.25f);
//	public GUITexture digits;
//	public GUITexture tenDigits;
//	public GUITexture hundredDigits;
//	public Texture[] digitTextures;
	private Rect skillRect;
	private bool startControl = false;
	public GUITexture wrongWayTexture;
	public GUITexture getSkillTexture;
	void Awake () {
//		IsShowCarPos = true;
//		IsShowSkillBg = true;
		IsShowSkill = new bool[3];
		for(int i = 0 ; i < 3 ; i++){
			IsShowSkill[i] = true;
		}
//		IsShowEnergy = true;
//		IsShowLoop = true;
//		IsShowRank = true;
//		IsShowBrake = true;
	}
	// Use this for initialization
	void Start () {
		//carb = GetComponent<CarB>();
//		smCamera.GetComponent<SmoothFollow>();
//		carb = smCamera.target.GetComponent<CarB>();
//		property = smCamera.target.GetComponent<CarProperty>();
//		timer = smCamera.target.GetComponent<Timer>();
		StartCoroutine(SettingTarget());
	}
	
//	private bool isSetting = false;
	private IEnumerator SettingTarget () {
		while(!smCamera.target)
		{
			yield return null;
		}
		carb = smCamera.target.GetComponent<CarB>();
		property = smCamera.target.GetComponent<CarProperty>();
//		timer = smCamera.target.GetComponent<Timer>();
//		isSetting = true;
		//sliderTexture = GetComponent<GUITexture>();
		//sliderTexture.color = sliderTextureliderColor;
//		backgroundTexture.color = sliderColor;
//		brakeTexture.color = brakeColor;
//		brakeRect = brakeTexture.pixelInset;
//		skillRect = skillTexture.pixelInset;
		//oriRect = sliderTexture.pixelInset;
		//guiTouchOffset.x = sliderTexture.pixelInset.width * 0.5f;
		//guiTouchOffset.y = sliderTexture.pixelInset.height * 0.5f;
//		minLimit = backgroundTexture.pixelInset.x + guiTouchOffset.x;
//		maxLimit = backgroundTexture.pixelInset.x + backgroundTexture.pixelInset.width - guiTouchOffset.x;
		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			steerfactor = 0.2f;
		}else{
			steerfactor = 0.5f;
		}
//		autosteerfactor = steerfactor * 2.0f;
//		CalculateCoordinate();
		//OnGUI type
//		skillRect = property.GetSkillRect();
//		skillRect.y = Screen.height - skillRect.y - skillRect.height;
		startControl = true;
//		StartCoroutine(StartControl());
	}
	
//	private float steerSlide = 0.0f;
//	void OnGUI () {
//		/*GUI.color = Color.red;
//		GUI.Label(new Rect(Screen.width / 2,300,300,100),"position = "+temp);
//
//		GUI.Label(new Rect(Screen.width / 2,350,300,100),"Btn_Brake_Co[1] = "+Btn_Brake_Co[1]);
//		GUI.Label(new Rect(Screen.width / 2,400,300,100),"Pad_Brake_Buttom_Co_UP_Y = "+Pad_Brake_Buttom_Co_UP_Y);
//		GUI.Label(new Rect(Screen.width / 2,450,300,100),"Pad_Brake_Buttom_Co_DOWN_Y = "+Pad_Brake_Buttom_Co_DOWN_Y);*/
//		/*if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
//		{
//			if(GUI.Button(new Rect(Screen.width - 150 - 50,Screen.height - 100 - 50, 100, 100), "Brake") || GUI.Button(new Rect(100,Screen.height - 100 - 50, 100, 100), "Brake"))
//			{
//			}
//			foreach (Touch touch in Input.touches) {
//				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//				{
//					//GUI.DrawTexture(new Rect(touch.position.x - touchTexture.width * 0.25f,Screen.height - touch.position.y - touchTexture.height * 0.25f, touchTexture.width / 2, touchTexture.height / 2), touchTexture);
//				}
//			}
//		}*/
//		
//		//GUI.Label(new Rect(Screen.width / 2,10,100,100),"Input.mousePosition = "+ Input.mousePosition);
//		/*GUI.Label(new Rect(Screen.width / 2,10,100,100),"Throtle = "+ Vector3.Angle(rigidbody.velocity,transform.forward));
//		GUI.Label(new Rect(Screen.width / 2,10,100,100),"transform.eulerAngles = "+ DVAngle);
//			
//		if(IsDrift)
//		{
//			GUI.Label(new Rect(Screen.width / 2,30,100,100),"Drift!");
//		}
//			
//		GUI.Label(new Rect(Screen.width / 2,50,100,100),"Gear = "+currentGear);
//				GUI.Label(new Rect(Screen.width / 2,70,100,100),"Velocity = "+CarSpeed);*/
//		
//		/*
//		if(property.CheckEnergy() >= 1.0f && GUI.Button(new Rect(Screen.width - 125 - 25, Screen.height - 200 - 25, 50, 50), "skill1"))
//		{
//			property.LevelOne();
//		}
//		if(property.CheckEnergy() >= 2.0f && GUI.Button(new Rect(Screen.width - 75 - 25, Screen.height - 200 - 25, 50, 50), "skill2"))
//		{
//			property.LevelTwo();
//		}
//		if(property.CheckEnergy() >= 3.0f && GUI.Button(new Rect(Screen.width - 25 - 25, Screen.height - 200 - 25, 50, 50), "skill3"))
//		{
//			property.LevelThree();
//		}
//		*/
//		if(showGUI && isSetting)
//		{
//			if(IsShowCarPos){
//				GUI.DrawTexture(Bg_CarPos_Co,Bg_CarPos);
//			}
//			
//			if(IsShowSkillBg){
//				GUI.DrawTexture(Bg_Skill_Co,Bg_Skill);
//			}
//			
//			
//			for(int i = 0 ; i < 3 ; i++){
//				if(IsShowSkill[i]){
//					bool skillLock = property.CheckSkillLock(i);
//					if(!skillLock && property.CheckEnergy() >= (i + 1.0f))
//					{
//						GUI.DrawTexture(Skill_Co[i],property.GetSkillIcon(i)[1]);
//					}else{
//						GUI.DrawTexture(Skill_Co[i],property.GetSkillIcon(i)[0]);
//					}
//					if(skillLock)
//					{
//						GUI.DrawTexture(Skill_Co[i], lockIcon);
//					}
//					
//				}
//			}
//			int energy = (int)(property._energy * 10.0f);
//			if(IsShowEnergy){
//				GUI.DrawTexture(Bg_EnergyBar_Co,Bg_EnergyBar);
//				for(int i = 1 ; i <= 30 ; i++){
//					if(energy < i){
//						break;
//					}
//					GUI.DrawTexture(EnergyBar_Co[i - 1] , EnergyBar[i - 1]);
//				}
//			}
//			
//			if(IsShowBrake){
//				GUI.DrawTexture(Btn_Brake_Co[0],Btn_Brake[0]);
//				GUI.DrawTexture(Btn_Brake_Co[1],Btn_Brake[0]);
//			}
//			
//			if(IsShowLoop){
////				GUI.DrawTexture(LoopNumNow_Co,LoopNum[carb.GetRound() + 1]);
////				GUI.DrawTexture(LoopSlash_Co,LoopSlash);
////				GUI.DrawTexture(LoopNumBase_Co,LoopNum[maxLoop]);
//				loopNumNow.texture = LoopNum[carb.GetRound() + 1];
//				loopNumBase.texture = LoopNum[maxLoop];
//			}
//			if(IsShowTime){
////				GUI.DrawTexture(TimeNumMin10_Co,TimeNum[timer.GetMin() / 10]);
////				GUI.DrawTexture(TimeNumMin_Co,TimeNum[timer.GetMin() % 10]);
////				GUI.DrawTexture(TimeColon_Co,TimeColon);
////				GUI.DrawTexture(TimeNumSec10_Co,TimeNum[timer.GetSec() / 10]);
////				GUI.DrawTexture(TimeNumSec_Co,TimeNum[timer.GetSec() % 10]);
//				timeNumMin10.texture = TimeNum[timer.GetMin() / 10];
//				timeNumMin.texture = TimeNum[timer.GetMin() % 10];
//				timeNumSec10.texture = TimeNum[timer.GetSec() / 10];
//				timeNumSec.texture = TimeNum[timer.GetSec() % 10];
//			}
//		}
////		GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * 0.3f, 100,100), "last : " + lastFingerId + " touch : " + touchPosition.ToString());
//	}	
//	void OnGUI(){
//		if(carb.CheckAirTime())
//		GUI.DrawTexture(new Rect((Screen.width - Tex_Yahoo.width) / 2.0f
//		                         ,(Screen.height - Tex_Yahoo.height) / 2.0f,
//		                         Tex_Yahoo.width,
//		                         Tex_Yahoo.height),Tex_Yahoo);
//		
//	}
	private Vector2 temp;
//	private int lastFingerId = -1;
	// Update is called once per frame
				/*
	private IEnumerator StartControl () {
//		if(Input.GetKeyDown(KeyCode.Q))
//		{
//			property.LevelOne();
//		}
//		if(Input.GetKeyDown(KeyCode.W))
//		{
//			property.LevelTwo();
//		}
//		if(Input.GetKeyDown(KeyCode.E))
//		{
//			property.LevelThree();
//		}
		
		while(true)
		{
			loopNumNow.texture = LoopNum[Mathf.Clamp(carb.GetRound() + 1, 0, LoopNum.Length - 1)];
			loopNumBase.texture = LoopNum[Mathf.Clamp(maxLoop, 0, LoopNum.Length - 1)];
			
			timeNumMin10.texture = LoopNum[Mathf.Clamp(timer.GetMin() / 10, 0, LoopNum.Length - 1)];
			timeNumMin.texture = LoopNum[timer.GetMin() % 10];
			timeNumSec10.texture = LoopNum[timer.GetSec() / 10];
			timeNumSec.texture = LoopNum[timer.GetSec() % 10];
			
			if(property.GetRank() >= 0)
			{
				rankTexture.texture = LoopNum[property.GetRank()];
			}
			
			if(Input.GetKey(KeyCode.Escape)){
					Application.Quit();
			}

			if(Input.GetKeyDown(KeyCode.Space))
			{
				property.UseSkill();
			}
			if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
			{
				bool isbrake = false;
				bool isturn = false;
				brakeColor.a = 0.25f;
				brakeTexture.color = brakeColor;
		        foreach (Touch touch in Input.touches) {
					Vector2 touchPos = touch.position - guiTouchOffset;
	//				touchPos.x -= sliderTexture.pixelInset.x;
		            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						if(backgroundTexture.pixelInset.Contains(touch.position) && lastFingerId == -1)
						{
							lastFingerId = touch.fingerId;
						}
						if(brakeRect.Contains(touch.position))
						{
							isbrake = true;
							brakeColor.a = 0.5f;
							brakeTexture.color = brakeColor;
						}
						if(skillRect.Contains(touch.position))
						{
							property.UseSkill();
						}
					}
					else
					{
						if(lastFingerId == touch.fingerId)
						{
							steerSlide = 0.0f;
							sliderTexture.pixelInset = oriRect;
							sliderColor.a = 0.25f;
							sliderTexture.color = sliderColor;
							backgroundTexture.color = sliderColor;
							lastFingerId = -1;
						}
					}
					
					if ( lastFingerId == touch.fingerId )
					{
						// Change the location of the joystick graphic to match where the touch is
						sliderTexture.pixelInset =  new Rect(Mathf.Clamp(touchPos.x, backgroundTexture.pixelInset.x, backgroundTexture.pixelInset.x + backgroundTexture.pixelInset.width - guiTouchOffset.x * 2.0f), sliderTexture.pixelInset.y, sliderTexture.pixelInset.width, sliderTexture.pixelInset.height);
						steerSlide = (Mathf.Clamp01((touch.position.x - minLimit) / (maxLimit - minLimit)) - 0.5f) * 2;
						sliderColor.a = 0.5f;
						sliderTexture.color = sliderColor;
						backgroundTexture.color = sliderColor;
					}
		        }
	//				foreach (Touch touch in Input.touches) {
	//					temp = touch.position;
	//					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
	//					{
	//						if(brakeRect.Contains(new Vector2(touch.position.x, touch.position.y)))
	//						{
	//							isbrake = true;
	//						}
	//					}
	//					if(touch.position.y <= Pad_Skill_Co_Up_Y[0] && touch.position.y >= Pad_Skill_Co_Down_Y[Pad_Skill_Co_Down_Y.Length - 1] 
	//				  	  	&& touch.position.x >= Skill_Co[0].x){
	//						for(int i = 0 ; i < Skill_Co.Length ; i++){
	//							if((touch.position.y <= Pad_Skill_Co_Up_Y[i] 
	//						    && touch.position.y >= Pad_Skill_Co_Down_Y[i]) 
	//				  	  	   && touch.position.x >= Skill_Co[0].x
	//						   && property.CheckEnergy() >= (i + 1.0f)){
	//								if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
	//								{
	//									if(i == 0){
	//										property.LevelOne();
	//									}else if(i == 1){
	//										property.LevelTwo();
	//									}else{
	//										property.LevelThree();
	//									}
	//								}
	//							}
	//						}
	//					}
						//else{
	//					if(touch.position.x > Screen.width * 0.5f)
	//					{	
	//						//turn right
	//						if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
	//						{
	//							if((touch.position.x >= Btn_Brake_Co[1].x && touch.position.x <= R_Btn_Brake_Buttom_Co_X)
	//						   		&& (touch.position.y <= Pad_Brake_Buttom_Co_UP_Y && 
	//						       touch.position.y >= Pad_Brake_Buttom_Co_DOWN_Y)){
	//								isbrake = true;
	//							}else{
	//								//steer += steerfactor;
	//								carb.SetSteer(carb.GetSteer() + steerfactor);
	//								isturn = true;
	//							}
	//						}
	//					}
	//					if(touch.position.x < Screen.width * 0.5f)
	//					{
	//						//turn left
	//						if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
	//						{
	//							if((touch.position.x > Btn_Brake_Co[0].x && touch.position.x < L_Btn_Brake_Buttom_Co_X)
	//						   && (touch.position.y <= Pad_Brake_Buttom_Co_UP_Y && 
	//						       touch.position.y >= Pad_Brake_Buttom_Co_DOWN_Y)){
	//								
	//								isbrake = true;
	//							}else{
	//							//steerfactor = -steerfactor;
	//							//steer -= steerfactor;
	//								carb.SetSteer(carb.GetSteer() - steerfactor);
	//								isturn = true;
	//							}
	//						}
	//					}
						//}
	//				}
				if(!carb.IsCarCanBrake()){
					isbrake = false;
				}
	//				if(!isturn){
	//					if(!carb.IsCarDrift()){
	//						//steer = Mathf.MoveTowards(steer,0.0f,steerfactor * 2.0f);
	//						carb.SetSteer(Mathf.MoveTowards(carb.GetSteer(),0.0f,autosteerfactor));
	//					}
	//				}
				carb.SetSteer(steerSlide);
				if(isbrake){
					carb.SetThrottle(carb.GetThrottle() - 0.5f); //throttle -= 0.5f;
					
				}else{
					carb.SetThrottle(carb.GetThrottle() + 0.5f); //throttle += 0.5f;
				}
			}
			else
			{
				if(Input.GetAxis("Vertical") < 0 && carb.IsCarCanBrake()){
					carb.SetThrottle(carb.GetThrottle() - 0.5f); //throttle -= 0.5f;
				}else{
					carb.SetThrottle(carb.GetThrottle() + 0.5f); //throttle += 0.5f;
				}
				if(Input.GetAxis("Horizontal") >0.0f){
					//steer += steerfactor;
					carb.SetSteer(carb.GetSteer() + steerfactor);
				}else if(Input.GetAxis("Horizontal") <0.0f){
					//steer -= steerfactor;
					carb.SetSteer(carb.GetSteer() - steerfactor);
				}else{
					if(!carb.IsCarDrift()){
						carb.SetSteer(Mathf.MoveTowards(carb.GetSteer(),0.0f,autosteerfactor));
					}
				}
//				for(int i = 0 ; i < Skill_Co.Length ; i++){
//					if(Skill_Co[i].Contains(new Vector2(Input.mousePosition.x
//				        ,Screen.height - Input.mousePosition.y)) && property.CheckEnergy() >= (i + 1.0f)){
//						if(i == 0){
//							property.LevelOne();
//						}else if(i == 1){
//							property.LevelTwo();
//						}else{
//							property.LevelThree();
//						}
//					}
//				}
			}
			
			//add new code
			
//				Speedometer();
			//CheckHandbrake(relativeVelocity);
	//		carb.CheckHandbrake();
			
			yield return null;
		}
	}
	*/
	private int showSkillIndex = 0;
	private float showSkillIndexTime = 0.0f;
	void Update () {
		if(startControl)
		{
			/*
			if(loopNumNow)
			{
				loopNumNow.texture = LoopNum[Mathf.Clamp(carb.GetRound() + 1, 0, maxLoop)];
			}
			if(loopNumBase)
			{
				loopNumBase.texture = LoopNum[Mathf.Clamp(maxLoop, 0, LoopNum.Length - 1)];
			}
			
			if(timeNumMin10)
			{
				timeNumMin10.texture = LoopNum[Mathf.Clamp(timer.GetMin() / 10, 0, LoopNum.Length - 1)];
			}
			if(timeNumMin)
			{
				timeNumMin.texture = LoopNum[timer.GetMin() % 10];
			}
			if(timeNumSec10)
			{
				timeNumSec10.texture = LoopNum[timer.GetSec() / 10];
			}
			if(timeNumSec)
			{
				timeNumSec.texture = LoopNum[timer.GetSec() % 10];
			}
			*/
//			if(property.GetRank() >= 0)
//			{
//				if(rankTexture)
//				{
//					rankTexture.texture = LoopNum[property.GetRank()];
//				}
//			}
#if UNITY_EDITOR
			if(Input.GetKeyDown(KeyCode.Space))
			{
				property.UseSkill();
			}
#endif
			if(Input.GetKey(KeyCode.Escape)){
					Application.Quit();
			}
			
//			if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
//			{
//				bool isbrake = false;
//				bool isturn = false;
//				brakeColor.a = 0.25f;
//				brakeTexture.color = brakeColor;
//		        foreach (Touch touch in Input.touches) {
//					Vector2 touchPos = touch.position - guiTouchOffset;
//	//				touchPos.x -= sliderTexture.pixelInset.x;
//		            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
//					{
//						if(backgroundTexture.pixelInset.Contains(touch.position) && lastFingerId == -1)
//						{
//							lastFingerId = touch.fingerId;
//						}
//						if(brakeRect.Contains(touch.position))
//						{
//							isbrake = true;
//							brakeColor.a = 0.5f;
//							brakeTexture.color = brakeColor;
//						}
//						if(skillRect.Contains(touch.position))
//						{
//							property.UseSkill();
//						}
//					}
//					else
//					{
//						if(lastFingerId == touch.fingerId)
//						{
//							steerSlide = 0.0f;
//							sliderTexture.pixelInset = oriRect;
//							sliderColor.a = 0.25f;
//							sliderTexture.color = sliderColor;
//							backgroundTexture.color = sliderColor;
//							lastFingerId = -1;
//						}
//					}
//					
//					if ( lastFingerId == touch.fingerId )
//					{
//						// Change the location of the joystick graphic to match where the touch is
//						sliderTexture.pixelInset =  new Rect(Mathf.Clamp(touchPos.x, backgroundTexture.pixelInset.x, backgroundTexture.pixelInset.x + backgroundTexture.pixelInset.width - guiTouchOffset.x * 2.0f), sliderTexture.pixelInset.y, sliderTexture.pixelInset.width, sliderTexture.pixelInset.height);
//						steerSlide = (Mathf.Clamp01((touch.position.x - minLimit) / (maxLimit - minLimit)) - 0.5f) * 4;
//						sliderColor.a = 0.5f;
//						sliderTexture.color = sliderColor;
//						backgroundTexture.color = sliderColor;
//					}
//		        }
//
//				if(!carb.IsCarCanBrake()){
//					isbrake = false;
//				}
//
//				carb.SetSteer(steerSlide);
//				if(isbrake){
//					carb.SetThrottle(carb.GetThrottle() - 0.5f); //throttle -= 0.5f;
//					
//				}else{
//					carb.SetThrottle(carb.GetThrottle() + 0.5f); //throttle += 0.5f;
//				}
//			}
//			else
//			{
//				/*
//				if(Input.GetAxis("Vertical") < 0 && carb.IsCarCanBrake()){
//					carb.SetThrottle(carb.GetThrottle() - 0.5f); //throttle -= 0.5f;
//				}else{
//					carb.SetThrottle(carb.GetThrottle() + 0.5f); //throttle += 0.5f;
//				}
//				*/
			
				if(Input.GetAxis("Vertical") < 0 && carb.IsCarCanBrake()){
					carb.SetThrottle(carb.GetThrottle() - 0.5f); //throttle -= 0.5f;
				}
				if(Input.GetAxis("Horizontal") >0.0f){
					//steer += steerfactor;
					carb.SetSteer(carb.GetSteer() + steerfactor);
					//carb.Steer(steerfactor);
					//carb.SetSteer(steerfactor);
					carb.SetArrowPress(true);
				}else if(Input.GetAxis("Horizontal") <0.0f){
					//steer -= steerfactor;
					carb.SetSteer(carb.GetSteer() - steerfactor);
					//carb.Steer(-steerfactor);
					//carb.SetSteer( -steerfactor);
					carb.SetArrowPress(true);
				}else{
					carb.SetArrowPress(false);
					//if(!carb.IsCarDrift()){
						
						//carb.SetSteer(Mathf.MoveTowards(carb.GetSteer(),0.0f,autosteerfactor));
						
						//carb.SteerAutoCenter();
					//}
					
				}
//				
//			}
			
			if(carb.CheckWrongWay())
			{
				if(!wrongWayTexture.enabled)
				{
					wrongWayTexture.enabled = true;
					StartCoroutine(Twinkling());
				}
			}
			else
			{
				wrongWayTexture.enabled = false;
			}
			
			if(property.getSkillBox)
			{
				if(Time.time - showSkillIndexTime >= 0.1f)
				{
					showSkillIndex = Random.Range(0, property.skills.Length);
					showSkillIndexTime = Time.time;
				}
				getSkillTexture.texture = property.skills[showSkillIndex].SkillIcon;
				if(Time.time - property.getSkillBoxTime >= 2.0f && !property.CheckSkillUsing())
				{
					property.getSkillBox = false;
				}
			}
			else
			{
				getSkillTexture.texture = property.showSkillIcon;
			}
		}
	}
	
	IEnumerator Twinkling () {
		while(wrongWayTexture.enabled)
		{
			wrongWayTexture.color = new Color(wrongWayTexture.color.r, wrongWayTexture.color.g, wrongWayTexture.color.b, 1);
			yield return new WaitForSeconds(0.5f);
			if(!wrongWayTexture.enabled)
			{
				yield break;
			}
			wrongWayTexture.color = new Color(wrongWayTexture.color.r, wrongWayTexture.color.g, wrongWayTexture.color.b, 0);
			yield return new WaitForSeconds(0.5f);
		}
	}
	
//	private void Speedometer () {
//		float speed = carb.GetCarSpeed() * 8.0f;
//		digits.texture = digitTextures[(int)(speed % 10)];
//		tenDigits.texture = digitTextures[(int)((speed % 100) * 0.1f)];
//		hundredDigits.texture = digitTextures[(int)((speed % 1000) * 0.01f)];
//	}
	
	public float[] Loop_Co;
	private float[] Pad_Skill_Co_Up_Y;
	private float[] Pad_Skill_Co_Down_Y;
	void CalculateCoordinate()
	{
//		Bg_CarPos_Co = new Rect();
		/*
		Bg_Skill_Co = new Rect();
		Skill_Co = new Rect[3];
		*/
//		Btn_Brake_Co = new Rect[2];
		/*
		Bg_EnergyBar_Co = new Rect();
		EnergyBar_Co = new Rect[30];
		*/
//		LoopNumNow_Co = new Rect();
//		LoopSlash_Co = new Rect();
//		LoopNumBase_Co = new Rect();
		/*
		TimeNumMin10_Co = new Rect();
		TimeNumMin_Co = new Rect();
		TimeColon_Co = new Rect();
		TimeNumSec10_Co = new Rect();
		TimeNumSec_Co = new Rect();
		*/
//		Pad_Skill_Co_Up_Y = new float[3];
//		Pad_Skill_Co_Down_Y = new float[3];
//		
//		Bg_CarPos_Co.width = Bg_CarPos.width * scale;
//		Bg_CarPos_Co.height = Bg_CarPos.height * scale;
//		Bg_CarPos_Co.x = 0.0f;
//		Bg_CarPos_Co.y = (Screen.height - Bg_CarPos.height * scale) / 2.0f;
		/*
		Bg_Skill_Co.width = Bg_Skill.width * scale;
		Bg_Skill_Co.height = Bg_Skill.height * scale;
		Bg_Skill_Co.x = Screen.width - Bg_Skill.width * scale;
		Bg_Skill_Co.y = (Screen.height - Bg_Skill.height * scale) / 2.0f;
		
		float tempx = Bg_Skill_Co.x + (Bg_Skill_Co.width - property.GetSkillIcon(0)[0].width * scale) / 2.0f;
		float tempoffset = (Bg_Skill_Co.height - property.GetSkillIcon(0)[0].height * scale * 3.0f) / 4.0f;
		
		for(int i = 0 ; i < 3 ; i++){
			Skill_Co[i].width = property.GetSkillIcon(0)[0].width * scale;
			Skill_Co[i].height = property.GetSkillIcon(0)[0].height * scale;
			
			Skill_Co[i].x = tempx;
			Skill_Co[i].y = Bg_Skill_Co.y + tempoffset * (i + 1) + Skill_Co[i].height * (i);
			
			Pad_Skill_Co_Up_Y[i] = Screen.height - Skill_Co[i].y;
			Pad_Skill_Co_Down_Y[i] = Pad_Skill_Co_Up_Y[i] - Skill_Co[i].height;
			//print("Pad_Skill_Co_Up_Y "+ i+" = "+ Pad_Skill_Co_Up_Y[i]);
			//print("Pad_Skill_Co_Down_Y "+ i+" = "+ Pad_Skill_Co_Down_Y[i]);
		}
		 */
//		Btn_Brake_Co[0] = new Rect();
//		Btn_Brake_Co[1] = new Rect();
//		
//		Btn_Brake_Co[0].width = Btn_Brake[0].width * scale;
//		Btn_Brake_Co[0].height = Btn_Brake[0].height * scale;
//		Btn_Brake_Co[0].x = Btn_Brake[0].width * scale;
//		Btn_Brake_Co[0].y = Screen.height - (Btn_Brake[0].height + 50.0f) * scale;
//		
//		
//		Btn_Brake_Co[1].width = Btn_Brake_Co[0].width;
//		Btn_Brake_Co[1].height = Btn_Brake_Co[0].height;
//		Btn_Brake_Co[1].x = Screen.width - Btn_Brake[0].width * 2.0f * scale;
//		Btn_Brake_Co[1].y = Screen.height - (Btn_Brake[0].height + 50.0f) * scale;
//		
//		Pad_Brake_Buttom_Co_UP_Y = (Btn_Brake[0].height + 50.0f) * scale;
//		Pad_Brake_Buttom_Co_DOWN_Y = 50.0f * scale;
		
		/*print("Pad_Brake_Buttom_Co_UP_Y = "+Pad_Brake_Buttom_Co_UP_Y);
		print("Pad_Brake_Buttom_Co_DOWN_Y = "+Pad_Brake_Buttom_Co_DOWN_Y);
		print("Btn_Brake_Co[0].x = "+ Btn_Brake_Co[0].x);
		print("Btn_Brake_Co[1].x = "+ Btn_Brake_Co[1].x);*/
		
			
//		Btn_Brake_Buttom_Co_Y = Btn_Brake_Co[0].y + Btn_Brake_Co[0].height;
//		L_Btn_Brake_Buttom_Co_X = Btn_Brake_Co[0].x + Btn_Brake_Co[0].width;
//		R_Btn_Brake_Buttom_Co_X = Btn_Brake_Co[1].x + Btn_Brake_Co[1].width;
		//print("R_Btn_Brake_Buttom_Co_X = "+ R_Btn_Brake_Buttom_Co_X);
		//print("L_Btn_Brake_Buttom_Co_X = "+ L_Btn_Brake_Buttom_Co_X);
		/*
		Bg_EnergyBar_Co.width = Bg_EnergyBar.width * scale;
		Bg_EnergyBar_Co.height = Bg_EnergyBar.height * scale;
		Bg_EnergyBar_Co.x = (Screen.width - Bg_EnergyBar.width * scale) / 2.0f;
		Bg_EnergyBar_Co.y = 0.0f;
		//time
		//10 min
		TimeNumMin10_Co.width = TimeNum[0].width * scale;
		TimeNumMin10_Co.height = TimeNum[0].height * scale;
		TimeNumMin10_Co.x = (Screen.width - TimeNum[0].width * scale * 4 - TimeColon.width * scale
		                     - 10.0f * 4.0f) / 2.0f;
		TimeNumMin10_Co.y = 0.0f;
		//min
		TimeNumMin_Co.width = TimeNum[0].width * scale;
		TimeNumMin_Co.height = TimeNum[0].height * scale;
		TimeNumMin_Co.x = TimeNumMin10_Co.x + TimeNumMin_Co.width + 10.0f;
		TimeNumMin_Co.y = TimeNumMin10_Co.y;
		//:
		TimeColon_Co.width = TimeColon.width * scale;
		TimeColon_Co.height = TimeColon.height * scale;
		TimeColon_Co.x = TimeNumMin_Co.x + TimeNumMin_Co.width  + 10.0f;
		TimeColon_Co.y = TimeNumMin10_Co.y;
		//10 sec
		TimeNumSec10_Co.width = TimeNum[0].width * scale;
		TimeNumSec10_Co.height = TimeNum[0].height * scale;
		TimeNumSec10_Co.x = TimeColon_Co.x + TimeColon_Co.width + 10.0f;
		TimeNumSec10_Co.y = TimeNumMin_Co.y;
		//sec
		TimeNumSec_Co.width = TimeNum[0].width * scale;
		TimeNumSec_Co.height = TimeNum[0].height * scale;
		TimeNumSec_Co.x = TimeNumSec10_Co.x + TimeNumMin_Co.width + 10.0f;
		TimeNumSec_Co.y = TimeNumMin_Co.y;
		*/
		//public Texture2D[] EnergyBar;
		//loop	
//		LoopNumNow_Co.width = LoopNum[0].width * scale;
//		LoopNumNow_Co.height = LoopNum[0].height * scale;
//		LoopSlash_Co.width = LoopSlash.width * scale;
//		LoopSlash_Co.height = LoopSlash.height * scale;
//		
//		LoopNumNow_Co.x = (Screen.width - LoopNumNow_Co.width * 2.0f - LoopSlash_Co.width - 10.0f * 2.0f);//LoopNumNow_Co.width;
//		LoopNumNow_Co.y = Loop_Co[1];//200;//LoopNumNow_Co.height;
//		
//		
//		LoopSlash_Co.x = LoopNumNow_Co.x + LoopNumNow_Co.width + 10.0f * scale;
//		LoopSlash_Co.y = LoopNumNow_Co.y;
//		
//		LoopNumBase_Co.width = LoopNumNow_Co.width;
//		LoopNumBase_Co.height = LoopNumNow_Co.height;
//		LoopNumBase_Co.x = LoopSlash_Co.x + LoopSlash_Co.width + 10.0f * scale;
//		LoopNumBase_Co.y = LoopNumNow_Co.y;
		/*
		EnergyBar1[0] = EnergyBar1[0] * scale + Bg_EnergyBar_Co.x;
		EnergyBar1[1] = EnergyBar1[1] * scale + Bg_EnergyBar_Co.y;
		EnergyBar2[0] = EnergyBar2[0] * scale + Bg_EnergyBar_Co.x;
		EnergyBar2[1] = EnergyBar2[1] * scale + Bg_EnergyBar_Co.y;
		EnergyBar3[0] = EnergyBar3[0] * scale + Bg_EnergyBar_Co.x;
		EnergyBar3[1] = EnergyBar3[1] * scale + Bg_EnergyBar_Co.y;
		float width = EnergyBar[0].width * scale;
		float height = EnergyBar[0].height * scale;
		for(int i = 0 ; i < 30 ; i++){
			if(i < 10){
				EnergyBar_Co[i] = new Rect(EnergyBar1[0] + width * i ,EnergyBar1[1],width,height);
			}else if(i < 20){
				EnergyBar_Co[i] = new Rect(EnergyBar2[0] + width * (i - 10) ,EnergyBar2[1],width,height);
			}else{
				EnergyBar_Co[i] = new Rect(EnergyBar3[0] + width * (i - 20) ,EnergyBar3[1],width,height);
			}
		}
		*/
		
	}
//	public void SetShowCarPos(bool isshow)
//	{
////		IsShowCarPos = isshow;
//	}
//	public void SetShowSkill(int index,bool isshow)
//	{
//		IsShowSkill[index] = isshow;
//	}
//	public void SetShowEnergy(bool isshow)
//	{
////		IsShowEnergy = isshow;
//	}
//	public void SetShowLoop(bool isshow)
//	{
////		IsShowLoop = isshow;
//	}
//	public void SetShowRank(bool isshow)
//	{
////		IsShowRank = isshow;
//	}
//	public void SetShowTime(bool isshow)
//	{
////		IsShowTime = isshow;
//	}
//	public void SetShowBrake(bool isshow)
//	{
////		IsShowBrake = isshow;
//	}
//	public void SetShowSkillBg(bool isshow)
//	{
////		IsShowSkillBg = isshow;
//	}
}
