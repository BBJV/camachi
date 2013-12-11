using UnityEngine;
using System.Collections;

public class FadeInFadeOut : MonoBehaviour {
	
	private UISprite sprite;
	private UILabel label;
	void Start () {
		sprite = GetComponent<UISprite>();
		label = GetComponent<UILabel>();
	}
	
	IEnumerator FadeIn (float fadeTime) {
		float time = 0;
		while(time < fadeTime)
		{
			if(sprite)
				sprite.alpha = Mathf.Lerp(0.0f, 1.0f, time / fadeTime);
			if(label)
				label.alpha = Mathf.Lerp(0.0f, 1.0f, time / fadeTime);
			time += Time.deltaTime;
			yield return null;
		}
		if(sprite)
			sprite.alpha = 1.0f;
		if(label)
			label.alpha = 1.0f;
	}
	
	IEnumerator FadeOut (float fadeTime) {
		float time = fadeTime;
		while(time > 0)
		{
			if(sprite)
				sprite.alpha = Mathf.Lerp(0.0f, 1.0f, time / fadeTime);
			if(label)
				label.alpha = Mathf.Lerp(0.0f, 1.0f, time / fadeTime);
			time -= Time.deltaTime;
			yield return null;
		}
		if(sprite)
			sprite.alpha = 0.0f;
		if(label)
			label.alpha = 0.0f;
	}
}
