using UnityEngine;
using System.Collections;

public class SkillBarController : MonoBehaviour {
	public Transform SkillBar;
	private CarProperty property;
	private Transform PlayerCar;
	

	// Use this for initialization
	IEnumerator Start () {
		SmoothFollow smCamera = Camera.main.GetComponent<SmoothFollow>();
		while(!smCamera.target)
		{
			yield return null;
		}
		PlayerCar = smCamera.target;
		property = PlayerCar.GetComponent<CarProperty>();
	}
	// Update is called once per frame
	void Update () {
		if(PlayerCar){
			if(property.showSkillIcon && !SkillBar.gameObject.active){
				SkillBar.gameObject.SetActiveRecursively(true);
				SkillBar.animation[SkillBar.animation.name].normalizedTime = 0.0f;
					SkillBar.animation[SkillBar.animation.name].speed = 1;
					SkillBar.animation.Play();
			}
		}
	}
}
