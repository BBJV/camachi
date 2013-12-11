using UnityEngine;
using System.Collections;

public class IdleManager : MonoBehaviour {
	public Transform[] IdleActions;
	public float TimeInterval;
	private int[] ActionTable;
	private float NowTime;
	private int NowAction;
	private bool IsActionFinish;
	private static string Play = "Play";
	// Use this for initialization
	void Start () {
		ActionTable = new int[30];
		int actionlength = IdleActions.Length;
		for(int i = 0 ; i < 30 ; i ++){
			ActionTable[i] = Random.Range(0,actionlength);
		}
		IsActionFinish = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsActionFinish) return;
		NowTime += Time.deltaTime;
		if(NowTime >= TimeInterval){
			NowTime = 0;
			NowAction++;
			if(NowAction >= ActionTable.Length){
				NowAction = 0;
			}
			//IdleActions[ActionTable[NowAction]].gameObject.SetActiveRecursively(true);
			IdleActions[ActionTable[NowAction]].BroadcastMessage(Play);
			IsActionFinish = false;
		}
	}
	void OnEnable(){
		NowTime = 0.0f;
		NowAction = 0;
		IsActionFinish = true;
		/*
		for(int i = 0 ; i < IdleActions.Length - 1 ; i ++){
			IdleActions[i].gameObject.SetActiveRecursively(false);
		}
		*/
	}
	void ActionFinish(){
		IsActionFinish = true;
	}
}
