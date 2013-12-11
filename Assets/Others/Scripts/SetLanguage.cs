using UnityEngine;
using System.Collections;

public class SetLanguage : MonoBehaviour {
	public string spriteName;
	public UISprite sprite;
	// Use this for initialization
	void OnEnable () {
		LanguageSetting.OnLanguageChanged += ChangeAtlas;
		LanguageSetting.SetCurrentLanguage();
	}
	
	// Update is called once per frame
	void OnDisable () {
		LanguageSetting.OnLanguageChanged -= ChangeAtlas;
	}
	
	void ChangeAtlas (UIAtlas currentAtlas)
	{
		if(spriteName != "")
			sprite.spriteName = spriteName;
		sprite.atlas = currentAtlas;
		sprite.MakePixelPerfect();
	}
}
