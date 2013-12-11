using UnityEngine;
using System.Collections;

public class TruckOil : Skill {
	public OilSpill oilPrefab;
	public Transform oilPosition;
	public SkillSettingLiveTime[] skillSetting;
//	public float percent = -50.0f;
//	public float effectTime = 1.0f;
//	public float liveTime = 30.0f;
	public int hit = 1;
	private bool oilNow = false;
	private CarProperty selfCar;
//	private OilSpill oilMark;
	private int lastOilMark = -1;
	
	/*audio*/
	public AudioClip OilSound;
	public float OilAudioVolume = 1.3f;
	private AudioSource OilAudio;
	public float OilPitch = 1.0f;
	public GameObject oilParticle;
	public override void Use (CarProperty car, int level) {
		if(!OilAudio)
			OilAudio = gameObject.AddComponent<AudioSource>();
		OilAudio.loop = true;
		OilAudio.clip = OilSound;
		OilAudio.volume = OilAudioVolume;
		OilAudio.pitch = OilPitch;

		base.Use(car, level);
		selfCar = car;
		StartCoroutine(Oil(car));
	}
	
//	void Start () {
//		oilMark = Instantiate(oilPrefab) as OilSpill;
//	}
	
	IEnumerator Oil(CarProperty car) {
		if(oilNow)
			yield break;
		skillUsing = true;
		lastOilMark=-1;
		float startTime = 0.0f;
		oilNow = true;
		animation["skill"].time = 0.0f;
		animation["skill"].speed = 1.0f;
		animation.CrossFade("skill");
		yield return StartCoroutine(ShowEffect());
		OilSpill oilMark = Instantiate(oilPrefab) as OilSpill;
		oilMark.liveTime = skillSetting[skillLevel - 1].effectLiveTime;
		car.SetUnStick(true);
		while(startTime < skillSetting[skillLevel - 1].skillEffectTimes)
		{
			if(oilMark != null && !selfCar.IsAirTime())
			{
				lastOilMark=oilMark.AddOilMark(oilPosition.position,transform.up,0.02f,lastOilMark,skillSetting[skillLevel - 1].effectPercent,hit, car);
				if(!OilAudio.isPlaying)
					OilAudio.Play();
			}
			else
			{
				lastOilMark=-1;
				OilAudio.Stop();
			}
			startTime += Time.deltaTime;
			yield return null;
		}
		oilParticle.active = false;
		animation["skill"].time = animation["skill"].length;
		animation["skill"].speed = -1.0f;
		animation.CrossFade("skill");
		car.SetUnStick(false);
		oilNow = false;
		skillUsing = false;
		OilAudio.Stop();
	}
	
	IEnumerator ShowEffect() {
		while(animation.IsPlaying("skill"))
		{
			yield return null;
		}
		oilParticle.active = true;
	}
}
