using UnityEngine;
using System.Collections;

public class Dash : Skill {

	public float startForce = 500.0f;
	public float speedUpPercent = 150.0f;
	public float effectTime = 10.0f;
	public Color skillColor = Color.red;
	public AudioClip speedUpAudio;
	private AudioSource speedUpSource;
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(DashCar(car));
	}
	
	IEnumerator DashCar(CarProperty car) {
		skillUsing = true;
		speedUpSource = gameObject.AddComponent<AudioSource>();
		speedUpSource.loop = false;
		speedUpSource.clip = speedUpAudio;
		speedUpSource.Play();
		car.rigidbody.AddForce(car.transform.forward * car.rigidbody.mass * startForce);
		car.TorqueUp(speedUpPercent, skillColor);
		yield return new WaitForSeconds(effectTime);
		car.TorqueUp(0.0f, Color.white);
		Destroy(speedUpSource);
		skillUsing = false;
	}
}
