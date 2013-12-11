using UnityEngine;
using System.Collections;

public class SpeedEffect : MonoBehaviour {
	
	public float showSpeed;
	public GameObject[] effectObjects;
	public AnimationClip speedAnimate;
	private bool isShow = false;
	private SmoothFollow smCamera;
	private bool effectTime = false;
	private static string SShowEffect = "ShowEffect";
	void Start () {
		if(speedAnimate)
		{
			animation[speedAnimate.name].wrapMode = WrapMode.Once;
		}
		smCamera = Camera.main.GetComponent<SmoothFollow>();
		showSpeed *= showSpeed;
	}
	
	void Update () {
		if(rigidbody.velocity.sqrMagnitude > showSpeed)
		{
			if(isShow)
			{
				return;
			}
			if(speedAnimate)
			{
				animation[speedAnimate.name].time = 0.0f;
				animation[speedAnimate.name].speed = 1.0f;
				animation.CrossFade(speedAnimate.name);
			}
			isShow = true;
			StartCoroutine(ShowEffect(true));
		}
		else
		{
			if(!isShow)
			{
				return;
			}
			isShow = false;
			StartCoroutine(ShowEffect(false));
			if(speedAnimate)
			{
				animation[speedAnimate.name].time = animation[speedAnimate.name].length;
				animation[speedAnimate.name].speed = -1.0f;
				animation.CrossFade(speedAnimate.name);
			}
		}
	}
	
	IEnumerator ShowEffect (bool show) {
		if(effectTime)
		{
			yield break;
		}
		effectTime = true;
		while(speedAnimate && animation.IsPlaying(speedAnimate.name))
		{
			yield return null;
		}
		foreach(GameObject effect in effectObjects)
		{
			if(show == isShow)
			{
				effect.SetActiveRecursively(show);
			}
		}
		if(show == isShow)
		{
			if(smCamera.target == transform.root)
			{
				smCamera.SendMessage(SShowEffect, show, SendMessageOptions.DontRequireReceiver);
			}
		}
		effectTime = false;
	}
}
