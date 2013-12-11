using UnityEngine;
using System.Collections;

public class Dodge : Skill {
	public SkillSettingPercent[] skillSetting;
	public GameObject effectAnimationObjects;
	public Animation mainAnimation;
	public GameObject effectObjects;
	public AudioClip ambulanceSiren;
	public float ambulanceSirenVolume;
	public AudioClip effectSound;
	public float effectSoundVolume;
	public GameObject rabbitObject;
	private bool showEffect = false;
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(CarDodge());
	}
	
	IEnumerator CarDodge () {
		float startTime = 0.0f;
		skillUsing = true;
		StartCoroutine(ShowEffectAnimation(true));
		AudioSource sirenAudio = gameObject.AddComponent<AudioSource>();
		sirenAudio.clip = ambulanceSiren;
		sirenAudio.volume = ambulanceSirenVolume;
		sirenAudio.loop = true;
		sirenAudio.Play();
		AudioSource effectAudio = gameObject.AddComponent<AudioSource>();
		effectAudio.clip = effectSound;
		effectAudio.volume = effectSoundVolume;
		effectAudio.loop = true;
		effectAudio.Play();
		while(startTime < skillSetting[skillLevel - 1].skillEffectTimes)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, skillSetting[skillLevel - 1].effectPercent);
			foreach(Collider target in targets)
			{
				if(target.transform.root.tag == "car" && 
				   (target.transform.root != transform.root))
				{
					CarProperty _car = target.transform.root.GetComponent<CarProperty>();
					_car.AddSkillTime();
					StartCoroutine(ShowEffect());
					if(Vector3.Dot(transform.right,target.transform.root.position - transform.position) > 0)
					{
						_car.DodgeSteer(1.0f);
					}
					else
					{
						_car.DodgeSteer(-1.0f);
					}
				}
			}
			startTime += Time.deltaTime;
			yield return null;
		}
		sirenAudio.Stop();
		Destroy(sirenAudio);
		effectAudio.Stop();
		Destroy(effectAudio);
		StartCoroutine(ShowEffectAnimation(false));
		skillUsing = false;
	}
	
	IEnumerator ShowEffectAnimation (bool show) {
		if(show)
		{
			if(!effectAnimationObjects.active)
			{
				effectAnimationObjects.SetActiveRecursively(true);
			}
			while(mainAnimation.isPlaying)
			{
				yield return null;
			}
			showEffect = true;
			effectObjects.SetActiveRecursively(true);
			yield return new WaitForSeconds(1.0f);
			effectObjects.SetActiveRecursively(false);
			showEffect = false;
			rabbitObject.SetActiveRecursively(true);
		}
		else
		{
			if(effectAnimationObjects.active)
			{
				effectAnimationObjects.SetActiveRecursively(false);
				rabbitObject.SetActiveRecursively(false);
			}
		}
	}
	
	IEnumerator ShowEffect () {
		if(showEffect)
			yield break;
		showEffect = true;
		effectObjects.SetActiveRecursively(true);
		yield return new WaitForSeconds(1.0f);
		effectObjects.SetActiveRecursively(false);
		showEffect = false;
	}
}
