using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {
	public float force = 500.0f;
	public Transform LightEffect;
	private float NowTime;
	AudioSource SpeedFx;
	private bool IsWorking;
	private bool scaleUp = false;
	void Start(){
		SpeedFx = transform.GetComponent<AudioSource>();
		NowTime = 0.0f;
		IsWorking = true;
	}
	void Update(){
		NowTime -= Time.deltaTime;
		if(NowTime < 0.0f){
			LightEffect.gameObject.SetActiveRecursively(true);
			IsWorking = true;
		}
	}
	void OnTriggerEnter(Collider other) {
		CarB car = other.transform.root.GetComponent<CarB>();
		if(car && IsWorking)
		{
			car.rigidbody.AddForce(car.transform.forward * car.rigidbody.mass * force);
			SpeedFx.Play();
			LightEffect.gameObject.SetActiveRecursively(false);
			NowTime = 1.5f;
			IsWorking = false;
			other.transform.root.BroadcastMessage("ShowState",3);
		}
	}
	
	void OnBecameInvisible () {
		enabled = false;
	}
	
	void OnBecameVisible () {
		enabled = true;
	}
	
	public void ScaleUp () {
		if(scaleUp)
		{
			return;
		}
		transform.localScale = transform.localScale * 2;
		scaleUp = true;
	}
}
