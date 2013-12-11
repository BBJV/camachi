using UnityEngine;
using System.Collections;

public class Invincible : Skill {
	
	public BoxCollider boxCollider;
	public SkillSettingPercent[] skillSetting;
	public Color skillColor = Color.yellow;
	public float changeSize = 1.5f;
	public AudioClip turboAudio;
	private AudioSource turboSource;
	public AudioClip growAudio;
	private AudioSource growSource;
	private SmoothFollow smoothFollow;
	private string methodName = "SetTarget";
	
	public override void Use (CarProperty car, int level) {
		if(!smoothFollow)
			smoothFollow = Camera.main.GetComponent<SmoothFollow>();
		base.Use(car, level);
		StartCoroutine(InvincibleCar(car));
	}
	
	IEnumerator InvincibleCar (CarProperty car) {
		if(skillUsing)
			yield break;
		skillUsing = true;
		InvincibleTrigger invincibleTrigger = gameObject.AddComponent<InvincibleTrigger>();
		car.Protect(true);
		boxCollider.isTrigger = false;
		car.SlowDown(false);
		if(turboAudio)
		{
			turboSource = gameObject.AddComponent<AudioSource>();
			turboSource.loop = true;
			turboSource.clip = turboAudio;
		}
		if(growAudio)
		{
			growSource = gameObject.AddComponent<AudioSource>();
			growSource.loop = true;
			growSource.clip = growAudio;
			growSource.Play();
		}
		car.RPMUp(skillSetting[skillLevel - 1].effectPercent);
		car.TorqueUp(skillSetting[skillLevel - 1].effectPercent, skillColor);
		car.UnCrash(true);
		float size = 1.0f;
		while(size < changeSize)
		{
			size += 0.1f;
			car.ChangeSize(size);
			if(smoothFollow.target == transform.root)
				smoothFollow.SendMessage(methodName, transform, SendMessageOptions.DontRequireReceiver);
			//smoothFollow.height = smoothFollow.height + 0.3f;
			yield return new WaitForSeconds(0.1f);
		}
		growSource.Stop();
		turboSource.Play();
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		turboSource.Stop();
		growSource.Play();
		while(size > 1.0f)
		{
			size -= 0.1f;
			car.ChangeSize(size);
			growSource.Play();
			if(smoothFollow.target == transform.root)
				smoothFollow.SendMessage(methodName, transform, SendMessageOptions.DontRequireReceiver);
			yield return new WaitForSeconds(0.1f);
		}
		growSource.Stop();
		car.Protect(false);
		boxCollider.isTrigger = true;
		car.SlowDown(true);
		car.RPMUp(0);
		car.TorqueUp(0, Color.white);
		car.UnCrash(false);
		Destroy(turboSource);
		Destroy(growSource);
		Destroy(invincibleTrigger);
		skillUsing = false;
	}
}
