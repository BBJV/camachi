using UnityEngine;
using System.Collections;

public class SkillButtonController : MonoBehaviour {

	public Transform PressEffect;
	public Transform SkillBar;
	//private CarProperty property;
	private Transform PlayerCar;
	private bool IsWaiting;
	// Use this for initialization
	IEnumerator Start () {
		SmoothFollow smCamera = Camera.main.GetComponent<SmoothFollow>();
		while(!smCamera.target)
		{
			yield return null;
		}
		PlayerCar = smCamera.target;
		//property = PlayerCar.GetComponent<CarProperty>();
		IsWaiting = false;
		if(PressEffect)
			PressEffect.gameObject.SetActiveRecursively(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(IsWaiting && !SkillBar.animation.isPlaying)
		{
			SkillBar.gameObject.SetActiveRecursively(false);
			IsWaiting = false;
		}
	}
	
	void OnPress(bool ispressed){
		if(!PlayerCar)
			return;
		if(ispressed){
			if(PressEffect)
				PressEffect.gameObject.SetActiveRecursively(true);
		}else{
			if(SkillBar.gameObject.active && !SkillBar.animation.isPlaying){
				SkillBar.animation[SkillBar.animation.name].normalizedTime = 1.0f;
				SkillBar.animation[SkillBar.animation.name].speed = -2;
				SkillBar.animation.Play();
				//property.UseSkill();
				PlayerCar.BroadcastMessage("UseSkill");
				IsWaiting = true;
			}
		}
	}
}
