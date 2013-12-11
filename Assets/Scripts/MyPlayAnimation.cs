using UnityEngine;
using System.Collections;
public enum TrigType{
	Default,
	Click,
}
public class MyPlayAnimation : MonoBehaviour {
	public Transform MyTransform;
	public WrapMode MyWarpMode;
	public TrigType MyTrigType;
	public bool IsEnableClickOn = false;
	public bool IsEnableClickOff = false;
	//public bool IsCanBeStop = false;
	private Animation MyAnimation;
	private AnimationState MyAnimationState;
	private float MyAnimationStateTime;
	void OnEnable(){
		MyAnimation = MyTransform.GetComponent<Animation>();
		MyAnimation.wrapMode = MyWarpMode;
		MyAnimationState = MyAnimation[MyAnimation.animation.clip.name];
		MyAnimationStateTime = 0.0f;
	}
	void ClickedOn(){
		if(MyTrigType == TrigType.Click && IsEnableClickOn){
			/*
			if(MyAnimation.animation.isPlaying && IsCanBeStop){
				//print ("StopAnimatio = " + MyAnimation.animation.clip.name);
				StopAnimation();
			}else{
			*/
				//print ("PlayAnimation = " + MyAnimation.animation.clip.name);
			PlayAnimation();
			//}
		}
	}
	void ClickedOff(){
		if(MyTrigType == TrigType.Click && IsEnableClickOff){
			/*
			if(MyAnimation.animation.isPlaying && IsCanBeStop){
				//print ("StopAnimatio = " + MyAnimation.animation.clip.name);
				StopAnimation();
			}else{
			*/
				//print ("PlayAnimation = " + MyAnimation.animation.clip.name);
			StopAnimation();
			//}
		}else{
			PlayAnimation();
		}
	}
	/*
	void Update(){
		//print (MyAnimationState.length);
		//print (MyAnimationState.normalizedTime);
	}
	*/
	void PlayAnimation () {
		//print(MyAnimation.animation.clip.name);
		//MyAnimation.Rewind(MyAnimation.animation.name);
		MyAnimationState.time = MyAnimationStateTime;
		MyAnimation.Play(MyAnimation.animation.clip.name);
		
	}
	void StopAnimation(){
		
		
		//MyAnimation.animation.clip.AddEvent
		MyAnimationStateTime = MyAnimationState.time;
		MyAnimation.Stop(MyAnimation.animation.clip.name);
	}
}
