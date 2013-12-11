using UnityEngine;
using System.Collections;
public enum Language{
	Trad_Chinese = 0,
	Sim_Chinese,
	English,
	End
}
public class TrainingBoard : TrainingItem {
	
	public Texture2D PoliceBoard;
	public Texture2D SkipBoard;
	public Texture2D[] DemoBoard;
	public Texture2D[] ArrowBoard;
	public Texture2D[] ArrowEndBoard;
	public GameObject[] BoardObject;
	private MessageBoard[] Board;
	public int SelectLan;
	public float scale;
	public Transform car;
	private CarB carb;
	
	private Rect PoliceBoard_Co;
	private Rect MessageBoard_Co;
//	private Rect SkipBoard_Co;
	private Rect ArrowBoard_Co;
	private bool IsComplete = true;
	private int MessageIndex;
	
//	private UserInterfaceControl userinterfacecontrol;
	private int ArrowIndex;
	private float ArrowTime;
	
	private GameCountDownTimer CountDownTimer;
	public override void Start()
	{
		Board = new MessageBoard[BoardObject.Length];
		for(int i = 0 ; i < BoardObject.Length ; i++){
			Board[i] = BoardObject[i].GetComponent<MessageBoard>();
		}
		
		PoliceBoard_Co = new Rect((Screen.width - PoliceBoard.width * scale) / 2 ,
		              (Screen.height - PoliceBoard.height * scale) ,
		                          PoliceBoard.width * scale, PoliceBoard.height * scale);
		/*MessageBoard_Co = new Rect(PoliceBoard_Co.x + PoliceBoard_Co.width -  Board[0].GetBoard()[0].width  * scale,
		              (Screen.height - Board[0].GetBoard()[0].height * scale) ,
		                           Board[0].GetBoard()[0].width  * scale, Board[0].GetBoard()[0].height * scale);*/
		MessageBoard_Co = new Rect(PoliceBoard_Co.x + PoliceBoard_Co.width -  Board[0].GetBoard()[0].width  * scale,
		              (50 * scale) ,
		                           Board[0].GetBoard()[0].width  * scale, Board[0].GetBoard()[0].height * scale);
//		SkipBoard_Co = new Rect(PoliceBoard_Co.x + PoliceBoard_Co.width - SkipBoard.width * scale ,
//		              (Screen.height - SkipBoard.height * scale) ,SkipBoard.width  * scale,
//		                        SkipBoard.height * scale);
		
		ArrowBoard_Co = new Rect(PoliceBoard_Co.x + PoliceBoard_Co.width - ArrowBoard[0].width * scale ,
		              (Screen.height - ArrowBoard[0].height * scale) ,ArrowBoard[0].width  * scale,
		                        ArrowBoard[0].height * scale);
		
		
		/*
		AllMessageBoard = new Texture2D[(int)Language.End][];
		
		for(int i = 0 ; i < AllMessageBoard.Length ; i++){
			AllMessageBoard[i] = new Texture2D[MessageBoard.Length];
		}
		*/
		//Co = new Rect((Screen.width - Num[0].width) / 2 ,
		              //(Screen.height - Num[0].height) / 2 ,Num[0].width , Num[0].height);
	}
	public override void StartAction() {
		
		IsComplete = false;
		MessageIndex = 0;
		
		CountDownTimer = MyGameCountDownTimer.GetComponent<GameCountDownTimer>();
		CountDownTimer.IsShowCountDown = false;
		
//		userinterfacecontrol = FindObjectOfType(typeof(UserInterfaceControl)) as UserInterfaceControl;
//		userinterfacecontrol.SetShowCarPos(false);
//		userinterfacecontrol.SetShowEnergy(false);
//		userinterfacecontrol.SetShowLoop(false);
//		userinterfacecontrol.SetShowRank(false);
//		userinterfacecontrol.SetShowTime(false);
//		userinterfacecontrol.SetShowBrake(false);
//		userinterfacecontrol.SetShowSkillBg(false);
//		for(int i = 0 ; i < 3 ; i++){
//			userinterfacecontrol.SetShowSkill(i,false);
//		}
		
		GameObject exsitaicar = GameObject.FindWithTag("TrainingAICar");
		if(exsitaicar){
			Destroy(exsitaicar);
		}
	}
	
	// Update is called once per frame
	public override void UpdateAction() {
	
	}
	
	public override bool IsEnd(){
		return IsComplete;
	}
	
	public override void Update(){
		
		if(IsComplete){
			return;
		}
		if(carb == null){
			carb = car.GetComponent<CarB>();
			return;
		}
		
		carb.Wait(true);
		carb.SetThrottle(0.0f);
		carb.SetSteer(0.0f);
		car.transform.rigidbody.useGravity = false;
		car.transform.rigidbody.isKinematic = true;
		car.position = StartPoint.position;
		car.rotation = StartPoint.rotation;
		
		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			foreach (Touch touch in Input.touches) {
					if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					{
						MessageIndex++;
					}
			}
		}
		else
		{
			if(Input.GetMouseButtonDown(0)){
				MessageIndex++;
			}
		}
		//print("MessageIndex = "+MessageIndex);
		if(MessageIndex >= Board[SelectLan].GetBoard().Length){
			MessageIndex = Board[SelectLan].GetBoard().Length - 1;
			IsComplete = true;
			car.transform.rigidbody.isKinematic = false;
			car.transform.rigidbody.useGravity = true;
			
		}
	}
	
	public override void OnGUI () {
		if(!IsComplete){
			GUI.DrawTexture(PoliceBoard_Co , PoliceBoard);
			GUI.DrawTexture(MessageBoard_Co , Board[SelectLan].GetBoard()[MessageIndex]);
			if(MessageIndex <= DemoBoard.Length - 1 && DemoBoard[MessageIndex] != null){
				Rect tempco = new Rect((Screen.width - DemoBoard[MessageIndex].width * scale) / 2 ,
		              /*(Screen.height - DemoBoard[MessageIndex].height * scale)*/0.0f ,
		                          DemoBoard[MessageIndex].width * scale, DemoBoard[MessageIndex].height * scale);
				GUI.DrawTexture(tempco , DemoBoard[MessageIndex]);
			}
			//GUI.DrawTexture(SkipBoard_Co , SkipBoard);
			ArrowTime += Time.deltaTime;
			if(ArrowTime <= 0.2f){
				
			}else{
				ArrowTime = 0.0f;
				ArrowIndex++;
			}
			
			if(MessageIndex >= (Board[SelectLan].GetBoard().Length - 1)){
				if(ArrowIndex >= ArrowBoard.Length){
					ArrowIndex = 0;
				}
				GUI.DrawTexture(ArrowBoard_Co , ArrowEndBoard[ArrowIndex]);
				
			}else{
				if(ArrowIndex >= ArrowBoard.Length){
					ArrowIndex = 0;
				}
				GUI.DrawTexture(ArrowBoard_Co , ArrowBoard[ArrowIndex]);
			}
		}
	}
}
