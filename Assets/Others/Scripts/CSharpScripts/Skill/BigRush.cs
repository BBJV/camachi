using UnityEngine;
using System.Collections;

public class BigRush : Skill {
	public GameObject[] speedUpEffects;
	public SkillSettingPercent[] skillSetting;
//	public float speedUpPercent = 20.0f;
//	public float effectTime = 1.0f;
	public float changeSize = 1.5f;
	public BoxCollider boxCollider;
	private bool rush = false;
	/*audio*/
	public AudioClip BigRushTurboSound = null;
	public float BigRushTurboAudioVolume = 0.3f;
	private AudioSource BigRushTurboAudio = null;
	public float BigRushTurboPitch = 1.0f;
	
	public AudioClip BigRushGrowSound = null;
	public float BigRushGrowoAudioVolume = 0.3f;
	private AudioSource BigRushGrowAudio = null;
	public float BigRushGrowPitch = 1.0f;
	public Renderer[] rendererTC01;
	public Renderer[] rendererTC02;
	public Material[] materials;
	private Material[] tempMaterials;
	private Color[] colors;
	private SmoothFollow smoothFollow;
	
	public override void Use (CarProperty car, int level) {
		if(!smoothFollow)
			smoothFollow = Camera.main.GetComponent<SmoothFollow>();
		
		if(!BigRushTurboAudio)
			BigRushTurboAudio = gameObject.AddComponent<AudioSource>();
		BigRushTurboAudio.loop = true;
		BigRushTurboAudio.clip = BigRushTurboSound;
		BigRushTurboAudio.volume = BigRushTurboAudioVolume;
		BigRushTurboAudio.pitch = BigRushTurboPitch;
		BigRushTurboAudio.Play();
		
		if(!BigRushGrowAudio)
			BigRushGrowAudio = gameObject.AddComponent<AudioSource>();
		BigRushGrowAudio.loop = true;
		BigRushGrowAudio.clip = BigRushGrowSound;
		BigRushGrowAudio.volume = BigRushGrowoAudioVolume;
		BigRushGrowAudio.pitch = BigRushGrowPitch;
		BigRushGrowAudio.Play();
		base.Use(car, level);
		
		if(colors == null || colors.Length == 0)
		{
			colors = new Color[materials.Length];
			tempMaterials = new Material[materials.Length];
			int index = 0;
			foreach(Material material in materials)
			{
				tempMaterials[index] = new Material(material);
				colors[index] = tempMaterials[index].color;
				index++;
			}
//			materials = tempMaterials;
			index = 0;
			foreach(Material material in tempMaterials)
			{
//				materials[index] = material;
				if(index == 0)
				{
					foreach(Renderer render in rendererTC01)
					{
						render.materials[0] = material;
					}
				}
				else if(index == 1)
				{
					foreach(Renderer render in rendererTC02)
					{
						render.materials[0] = material;
					}
				}
				index++;
			}
		}
		StartCoroutine(BiggerRush(car));
	}
	
	IEnumerator BiggerRush(CarProperty car) {
		if(rush)
			yield break;
		rush = true;
		skillUsing = true;
		animation["skill"].time = 0.0f;
		animation["skill"].speed = 1.0f;
		animation.CrossFade("skill");
		StartCoroutine(ShowEffect());
		StartCoroutine(ChangeColor(false));
		car.AddSkillTime();
		car.SlowDown(false);
		car.RPMUp(skillSetting[skillLevel - 1].effectPercent);
		car.TorqueUp(skillSetting[skillLevel - 1].effectPercent, Color.white);
		car.UnCrash(true);
		boxCollider.isTrigger = false;
//		boxCollider.transform.localPosition = Vector3.zero;
		float size = 1.0f;
		while(size < changeSize)
		{
			size += 0.1f;
			car.ChangeSize(size);
			if(smoothFollow.target == transform.root)
				smoothFollow.SendMessage("SetTarget", transform, SendMessageOptions.DontRequireReceiver);
			//smoothFollow.height = smoothFollow.height + 0.3f;
			yield return new WaitForSeconds(0.1f);
		}
		BigRushGrowAudio.Stop();
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		car.SlowDown(true);
		car.RPMUp(0);
		car.TorqueUp(0, Color.white);
		while(size > 1.0f)
		{
			size -= 0.1f;
			car.ChangeSize(size);
			yield return new WaitForSeconds(0.1f);
		}
		foreach(GameObject effect in speedUpEffects)
		{
			effect.active = false;
		}
		animation["skill"].time = animation["skill"].length;
		animation["skill"].speed = -1.0f;
		animation.CrossFade("skill");
		StartCoroutine(ChangeColor(true));
		car.UnCrash(false);
		boxCollider.isTrigger = true;
//		boxCollider.transform.localPosition = new Vector3(0, -0.5f, 0);
		BigRushTurboAudio.Stop();
		rush = false;
		skillUsing = false;
		if(smoothFollow.target == transform.root)
		{
			smoothFollow.SendMessage("SetTarget", transform, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	IEnumerator ShowEffect() {
		while(animation.IsPlaying("skill"))
		{
			yield return null;
		}
		foreach(GameObject effect in speedUpEffects)
		{
			effect.active = true;
		}
	}
	
	IEnumerator ChangeColor (bool reset) {
		if(reset)
		{
			Color targetColor = new Color(0.784f,0.784f,0.784f);
			while(rendererTC01[0].materials[0].color != targetColor)
			{
				foreach(Renderer render in rendererTC01)
				{
//					material.color = new Color(Mathf.Clamp(material.color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(material.color.g + Time.deltaTime, 0.435f, 0.784f), Mathf.Clamp(material.color.b + Time.deltaTime, 0.0f, 0.784f));
					render.materials[0].color = new Color(Mathf.Clamp(render.materials[0].color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(render.materials[0].color.g + Time.deltaTime, 0.435f, 0.784f), Mathf.Clamp(render.materials[0].color.b + Time.deltaTime, 0.0f, 0.784f));
				}
				foreach(Renderer render in rendererTC02)
				{
//					material.color = new Color(Mathf.Clamp(material.color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(material.color.g + Time.deltaTime, 0.435f, 0.784f), Mathf.Clamp(material.color.b + Time.deltaTime, 0.0f, 0.784f));
					render.materials[0].color = new Color(Mathf.Clamp(render.materials[0].color.r - Time.deltaTime, 0.784f, 1.0f), Mathf.Clamp(render.materials[0].color.g + Time.deltaTime, 0.435f, 0.784f), Mathf.Clamp(render.materials[0].color.b + Time.deltaTime, 0.0f, 0.784f));
				}
				yield return null;
			}
		}
		else
		{
			Color targetColor = new Color(1.0f,0.435f,0.0f);
			while(rendererTC01[0].materials[0].color != targetColor)
			{
				foreach(Renderer render in rendererTC01)
				{
//					Debug.Log(material.name);
//					material.color = new Color(Mathf.Clamp01(material.color.r + Time.deltaTime), Mathf.Clamp(material.color.g - Time.deltaTime, 0.435f, 1.0f), Mathf.Clamp01(material.color.b - Time.deltaTime));
					render.materials[0].color = new Color(Mathf.Clamp01(render.materials[0].color.r + Time.deltaTime), Mathf.Clamp(render.materials[0].color.g - Time.deltaTime, 0.435f, 1.0f), Mathf.Clamp01(render.materials[0].color.b - Time.deltaTime));
				}
				foreach(Renderer render in rendererTC02)
				{
					render.materials[0].color = new Color(Mathf.Clamp01(render.materials[0].color.r + Time.deltaTime), Mathf.Clamp(render.materials[0].color.g - Time.deltaTime, 0.435f, 1.0f), Mathf.Clamp01(render.materials[0].color.b - Time.deltaTime));
				}
				yield return null;
			}
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if(rush)
		{
			CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
			if(collisionCar && !other.isTrigger)
			{
//				StartCoroutine(collisionCar.SetSlip(1.0f,rigidbody.velocity.magnitude * 0.5f));
				Hashtable args = new Hashtable();
				args.Add("second", 1.0f);
				args.Add("speed", rigidbody.velocity.magnitude * 0.5f);
				collisionCar.SendMessage("SetSlip", args, SendMessageOptions.DontRequireReceiver);
			}
		}
    }
}
