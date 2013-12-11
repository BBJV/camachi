using UnityEngine;
using System.Collections;

public class Protect : Skill {
	public SkillSettingTime[] skillSetting;
	public AudioClip ambulanceSiren;
	public float ambulanceSirenVolume;
	public AudioClip effectSound;
	public float effectSoundVolume;
	public Renderer[] renderers;
	public Material ambulanceMaterial;
	public GameObject effectAnimationObjects;
	public GameObject effectObject;
	public Animation mainAnimation;
	public GameObject particleObject;
	private Material tempMaterial;
	private Color color = Color.black;
	private bool inProtect = false;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		if(color == Color.black)
		{
			tempMaterial = new Material(ambulanceMaterial);
			color = tempMaterial.color;
			foreach(Renderer render in renderers)
			{
				render.materials[0] = tempMaterial;
			}
		}
		StartCoroutine(ProtectCar(car));
	}
	
	IEnumerator ProtectCar (CarProperty car) {
		if(inProtect)
			yield break;
		skillUsing = true;
		inProtect = true;
//		Behaviour halo = GetComponent("Halo") as Behaviour;
//		halo.enabled = true;
		StartCoroutine(ShowEffectAnimation(true));
		animation["skill"].time = 0.0f;
		animation["skill"].speed = 1.0f;
		animation.CrossFade("skill");
		
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
		car.Protect(true);
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		StartCoroutine(ShowEffectAnimation(false));
		car.Protect(false);
		animation["skill"].time = animation["skill"].length;
		animation["skill"].speed = -1.0f;
		animation.CrossFade("skill");
//		StartCoroutine(ChangeColor(true));
		sirenAudio.Stop();
		Destroy(sirenAudio);
		effectAudio.Stop();
		Destroy(effectAudio);
//		halo.enabled = false;
		inProtect = false;
		skillUsing = false;
	}
	
	IEnumerator ChangeColor (bool reset) {
		if(reset)
		{
			Color targetColor = new Color(0.784f,0.784f,0.784f);
			while(renderers[0].materials[0].color != targetColor)
			{
				foreach(Renderer render in renderers)
				{
//					material.color = new Color(Mathf.Clamp(material.color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(material.color.g + Time.deltaTime, 0.435f, 0.784f), Mathf.Clamp(material.color.b + Time.deltaTime, 0.0f, 0.784f));
					render.materials[0].color = new Color(Mathf.Clamp(render.materials[0].color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(render.materials[0].color.g - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(render.materials[0].color.b + Time.deltaTime, 0.0f, 0.784f));
				}
				yield return null;
			}
		}
		else
		{
			Color targetColor = new Color(1.0f,0.9137f,0.0589f);
			while(renderers[0].materials[0].color != targetColor)
			{
				foreach(Renderer render in renderers)
				{
//					Debug.Log(material.name);
//					material.color = new Color(Mathf.Clamp01(material.color.r + Time.deltaTime), Mathf.Clamp(material.color.g - Time.deltaTime, 0.435f, 1.0f), Mathf.Clamp01(material.color.b - Time.deltaTime));
					render.materials[0].color = new Color(Mathf.Clamp01(render.materials[0].color.r + Time.deltaTime), Mathf.Clamp(render.materials[0].color.g + Time.deltaTime, render.materials[0].color.g, 0.9137f), Mathf.Clamp(render.materials[0].color.b - Time.deltaTime, 0.0589f, render.materials[0].color.b));
				}
				yield return null;
			}
		}
	}
	
	IEnumerator ShowEffectAnimation (bool show) {
		if(show)
		{
			if(!effectAnimationObjects.active)
			{
				effectAnimationObjects.SetActiveRecursively(true);
				effectObject.SetActiveRecursively(true);
			}
			while(mainAnimation.isPlaying)
			{
				yield return null;
			}
			particleObject.SetActiveRecursively(true);
			yield return new WaitForSeconds(0.5f);
			particleObject.SetActiveRecursively(false);
			StartCoroutine(ChangeColor(false));
//			showEffect = true;
//			effectObjects.SetActiveRecursively(true);
//			yield return new WaitForSeconds(1.0f);
//			effectObjects.SetActiveRecursively(false);
//			showEffect = false;
		}
		else
		{
			if(effectAnimationObjects.active)
			{
				effectAnimationObjects.SetActiveRecursively(false);
				effectObject.SetActiveRecursively(false);
				particleObject.SetActiveRecursively(false);
				StartCoroutine(ChangeColor(true));
			}
		}
	}
}
