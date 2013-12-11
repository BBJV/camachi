using UnityEngine;
using System.Collections;
public enum MyState {
	State_Begin,
	State_Run,
	State_End
}
public class GameState : MonoBehaviour {
	//the game state
	public static Transform NowGameState = null;
	// the begin,run,end transform
	private Transform BeginTransform;
	private Transform RunTransform;
	private Transform EndTransform;
	//the state gameobject name
	private static string Begin = "Begin";
	private static string Run = "Run";
	private static string End = "End";
	//the first game state
	public Transform FirstGameState;
	private Transform MyTransform;
	private GameObject MyGameObject;
	//the state inside the game state
	private MyState State;
	private static string Reset = "Reset";
	// Use this for initialization
	void Start () {
		MyTransform = transform;
		MyGameObject = MyTransform.gameObject;
		BeginTransform = MyTransform.FindChild(Begin);
		RunTransform = MyTransform.FindChild(Run);
		EndTransform = MyTransform.FindChild(End);
		SetChild(false);
		if(FirstGameState == MyTransform){
			ChangeGameState(FirstGameState);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static void ChangeGameState(Transform temp){
		if(NowGameState){
			NowGameState.BroadcastMessage(Reset,SendMessageOptions.DontRequireReceiver);
			NowGameState.GetComponent<GameState>().SetChild(false);
		}
		
		NowGameState = temp;
		NowGameState.GetComponent<GameState>().ChangeState(MyState.State_Begin);
	}
	
	public void ChangeState(MyState state){
		//disable all state game object
		MyTransform.BroadcastMessage(Reset,SendMessageOptions.DontRequireReceiver);
		SetChild(false);
		State = state;
		switch(State){
			case MyState.State_Begin:
				BeginTransform.gameObject.SetActive(true);
				break;
			case MyState.State_Run:
				RunTransform.gameObject.SetActive(true);
				break;
			case MyState.State_End:
				EndTransform.gameObject.SetActive(true);
				break;
		}
	}
	
	public void SetChild(bool temp){
		int childcount = MyTransform.childCount;
		for(int i = 0 ; i < childcount ; i++){
			MyTransform.GetChild(i).gameObject.SetActive(temp);
		}
	}
}
