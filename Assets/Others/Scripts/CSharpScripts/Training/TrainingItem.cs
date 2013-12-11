using UnityEngine;
using System.Collections;

public class TrainingItem  : MonoBehaviour {
	public State NextState = State.RunItem;
	public Transform StartPoint;
	public Transform MyGameCountDownTimer;
	public virtual void StartAction(){}
	
	public virtual void UpdateAction(){}
	
	public virtual void Start(){}
	
	public virtual void OnGUI(){}
	
	public virtual bool IsEnd(){ return false;}
	
	public virtual void Update(){}
	
	public virtual int GetBackStep(){return 2;}
	
	public virtual State GetNextState(){ return NextState;}
}
