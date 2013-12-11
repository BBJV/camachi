using UnityEngine;
using System.Collections;

public class SkillBox : MonoBehaviour {
	public float waitTime = 5.0f;
	private Quaternion rotateAngle;
	private bool scaleUp = false;
	
	void Start () {
		StartCoroutine(Rotate());
		rotateAngle = Random.rotation;
	}
	
	void OnTriggerEnter(Collider other) {
		CarProperty car = other.GetComponent<CarProperty>();
		if(car)
		{
			car.StartCoroutine(car.GetSkill());
			StartCoroutine(Wait());
		}
	}
	
	IEnumerator Wait () {
		collider.enabled = false;
		renderer.enabled = false;
		yield return new WaitForSeconds(waitTime);
		rotateAngle = Random.rotation;
		collider.enabled = true;
		renderer.enabled = true;
		StartCoroutine(Rotate());
	}
	
	IEnumerator Rotate () {
		while(enabled && collider.enabled)
		{
			transform.Rotate(rotateAngle.eulerAngles * Time.deltaTime);
			yield return null;
		}
	}
	
	void OnBecameInvisible () {
		enabled = false;
	}
	
	void OnBecameVisible () {
		enabled = true;
		StartCoroutine(Rotate());
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
