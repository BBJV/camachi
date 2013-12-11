using UnityEngine;
using System.Collections;

public class TalkManager : MonoBehaviour {
	public Transform[] Talks;
	public float TimeInterval;
	private int[] TalkTable;
	private float NowTime;
	private int NowAction;
	// Use this for initialization
	void Start () {
		TalkTable = new int[30];
		int talklength = Talks.Length - 1;
		for(int i = 0 ; i < 30 ; i ++){
			TalkTable[i] = Random.Range(0,talklength);
		}
	}
	
	// Update is called once per frame
	void Update () {
		NowTime += Time.deltaTime;
		if(NowTime >= TimeInterval){
			NowTime = 0;
			NowAction++;
			if(NowAction >= TalkTable.Length){
				NowAction = 0;
			}
			Talks[TalkTable[NowAction]].BroadcastMessage("SoundOn");
			//IdleActions[NowAction].gameObject.SetActiveRecursively(true);
		}
	}
	void OnEnable(){
		NowTime = 0.0f;
		NowAction = 0;
		/*
		for(int i = 0 ; i < IdleActions.Length - 1 ; i ++){
			IdleActions[i].gameObject.SetActiveRecursively(false);
		}
		*/
	}
}
