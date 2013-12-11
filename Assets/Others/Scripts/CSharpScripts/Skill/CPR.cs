using UnityEngine;
using System.Collections;

public class CPR : Skill {
	public Texture textureCPR;
	public SkillSettingPercent[] skillSetting;
	public AudioClip ambulanceSiren;
	public float ambulanceSirenVolume;
	public AudioClip clickSound;
	public float clickSoundVolume;
	public GameObject lightning;
	public GameObject redLight;
	private bool CPRnow = false;
	private CarProperty selfCar;
	private bool showButton = false;
	private SmoothFollow smfCamera;
	private Rect cprRect;
	private Rect clickRect;
	
	void Start () {
		Vector2 pixel = new Vector2(textureCPR.width * Screen.width / 1024.0f, textureCPR.height * Screen.height / 768.0f);
		cprRect = new Rect((Screen.width - pixel.x) * 0.5f, (Screen.height - pixel.y) * 0.5f, pixel.x, pixel.y);
		clickRect = cprRect;
		clickRect.y = Screen.height - clickRect.y - clickRect.height;
	}
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		if(!smfCamera)
			smfCamera = Camera.main.GetComponent<SmoothFollow>();
		selfCar = car;
		StartCoroutine(CPRSpeed());
	}
	
	IEnumerator CPRSpeed() {
		if(CPRnow)
			yield break;
		skillUsing = true;
		selfCar.AddSkillTime();
		float startTime = 0.0f;
		CPRnow = true;
		redLight.SetActiveRecursively(true);
		AudioSource sirenAudio = gameObject.AddComponent<AudioSource>();
		sirenAudio.clip = ambulanceSiren;
		sirenAudio.volume = ambulanceSirenVolume;
		sirenAudio.loop = true;
		sirenAudio.Play();
		while(startTime < skillSetting[skillLevel - 1].skillEffectTimes)
		{
			if(selfCar.UnMaxSpeed())
			{
				showButton = true;
			}
			else
			{
				showButton = false;
			}
			startTime += Time.deltaTime;
			yield return null;
		}
		sirenAudio.Stop();
		Destroy(sirenAudio);
		redLight.SetActiveRecursively(false);
		showButton = false;
		CPRnow = false;
		skillUsing = false;
	}
	
	private bool showLightning = false;
	IEnumerator LightningEffect () {
		if(showLightning)
			yield break;
		showLightning = true;
		lightning.SetActiveRecursively(true);
		float effectTime = 1.0f;
		while(effectTime > 0.0f)
		{
			lightning.transform.position = transform.position + Random.insideUnitSphere * 5;
			effectTime -= Time.deltaTime;
			yield return null;
		}
		lightning.SetActiveRecursively(false);
		showLightning = false;
	}
	
	private float delayTime = 0.0f;
	void OnGUI ()
	{
		if(delayTime <= 0.0f && showButton)
		{
			if(smfCamera.target == transform)
			{
				if(CustomGUI.Button(cprRect, textureCPR, new GUIStyle()))
				{
					AudioSource.PlayClipAtPoint(clickSound, transform.position, clickSoundVolume);
					StartCoroutine(LightningEffect());
					selfCar.SpeedUp(skillSetting[skillLevel - 1].effectPercent);
					delayTime = 0.5f;
				}
			}
		}
		delayTime = Mathf.Clamp(delayTime - Time.deltaTime, 0.0f, delayTime);
	}
}