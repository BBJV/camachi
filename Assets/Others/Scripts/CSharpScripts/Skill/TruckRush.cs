using UnityEngine;
using System.Collections;

public class TruckRush : Skill {
	public GameObject[] speedUpEffects;
	public SkillSettingPercent[] skillSetting;
//	public float speedUpPercent = 20.0f;
//	public float effectTime = 1.0f;
	private bool rush = false;
	/*audio*/
	public AudioClip RushReleaseSound = null;
	public float RushReleaseAudioVolume = 1.3f;
	private AudioSource RushReleaseAudio = null;
	public float RushReleasePitch = 1.0f;
	
	public AudioClip RushHornSound = null;
	public float RushHornAudioVolume = 1.3f;
	private AudioSource RushHornAudio = null;
	public float RushHornPitch = 1.0f;
	
	
	
	public override void Use (CarProperty car, int level) {
		if(!RushReleaseAudio)
		RushReleaseAudio = gameObject.AddComponent<AudioSource>();
		RushReleaseAudio.loop = false;
		RushReleaseAudio.clip = RushReleaseSound;
		RushReleaseAudio.volume = RushReleaseAudioVolume;
		RushReleaseAudio.pitch = RushReleasePitch;
		RushReleaseAudio.Play();
		
		if(!RushHornAudio)
		RushHornAudio = gameObject.AddComponent<AudioSource>();
		RushHornAudio.loop = true;
		RushHornAudio.clip = RushHornSound;
		RushHornAudio.volume = RushHornAudioVolume;
		RushHornAudio.pitch = RushHornPitch;
		//RushHornAudio.Play();
		base.Use(car, level);
		StartCoroutine(CarRush(car));
	}
	
	IEnumerator CarRush(CarProperty car) {
		if(rush)
			yield break;
		skillUsing = true;
		rush = true;
		car.AddSkillTime();
		foreach(GameObject effect in speedUpEffects)
		{
			effect.active = true;
		}
		car.SlowDown(false);
		car.RPMUp(skillSetting[skillLevel - 1].effectPercent);
		car.TorqueUp(skillSetting[skillLevel - 1].effectPercent, Color.white);
		car.CanBrake(false);
		RushHornAudio.Play();
		/* show effect
		while(){} 
		*/
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		car.SlowDown(true);
		car.RPMUp(0);
		car.TorqueUp(0, Color.white);
		car.CanBrake(true);
		foreach(GameObject effect in speedUpEffects)
		{
			effect.active = false;
		}
		rush = false;
		skillUsing = false;
		RushHornAudio.Stop();
	}
	
	void OnCollisionEnter(Collision collision) {
		if(rush && collision.transform.root.tag == "car" && collision.transform.root != transform.root)
		{
			rigidbody.AddForce(transform.forward * collision.impactForceSum.magnitude * rigidbody.mass * 50);
		}
    }
}
