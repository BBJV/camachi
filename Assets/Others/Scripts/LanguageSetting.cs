using UnityEngine;
using System.Collections;

public class LanguageSetting : MonoBehaviour {
	
	// Events and delegates
	public delegate void OnLanguageChangeEventHandler(UIAtlas currentAtlas);
	
	public static event OnLanguageChangeEventHandler OnLanguageChanged;
	
	[System.Serializable]
	public class Language {
		public string name;
		public UIAtlas atlas;
	}
	
	public Language[] languagelist;
	private int currentLanguage;
	private static LanguageSetting mInstance;
	
	void ChangeLanguage () {
		PlayerPrefs.SetString("Language", languagelist[currentLanguage].name);
		if(OnLanguageChanged != null)
		{
			OnLanguageChanged(languagelist[currentLanguage].atlas);
		}
	}
	
	void ChangeNextLanguage () {
		currentLanguage += 1;
		if(currentLanguage >= languagelist.Length)
		{
			currentLanguage = 0;
		}
		ChangeLanguage();
	}
	
	void Start () {
		mInstance = this;
		string _currentLanguage = PlayerPrefs.GetString("Language", "English");
		currentLanguage = 0;
		foreach(Language language in languagelist)
		{
			if(language.name == _currentLanguage)
			{
				break;
			}
			currentLanguage += 1;
		}
		ChangeLanguage();
	}
	
	public static void SetCurrentLanguage () {
		if(mInstance != null)
		{
			mInstance.SendMessage("ChangeLanguage", SendMessageOptions.DontRequireReceiver);
		}
	}
}
