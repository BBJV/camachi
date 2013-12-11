using UnityEngine;
using System.Collections;

public class GUISkinLanguageSetting : MonoBehaviour {

	// Events and delegates
	public delegate void OnGUISkinLanguageChangeEventHandler(Definition.eLanguage currentLanguage);
	
	public static event OnGUISkinLanguageChangeEventHandler OnGUISkinLanguageChanged;
	
	[System.Serializable]
	public class GUISkinLanguage {
		public string name;
		public Definition.eLanguage language;
	}
	
	public GUISkinLanguage[] skinLanguageList;
	private int currentLanguage;

	void ChangeLanguage () {
		PlayerPrefs.SetString("Language", skinLanguageList[currentLanguage].name);
		if(OnGUISkinLanguageChanged != null)
		{
			OnGUISkinLanguageChanged(skinLanguageList[currentLanguage].language);
		}
	}
	
	void ChangeNextLanguage () {
		currentLanguage += 1;
		if(currentLanguage >= skinLanguageList.Length)
		{
			currentLanguage = 0;
		}
		ChangeLanguage();
	}
	
	void Start () {

		string _currentLanguage = PlayerPrefs.GetString("Language", "English");
		currentLanguage = 0;
		foreach(GUISkinLanguage language in skinLanguageList)
		{
			if(language.name == _currentLanguage)
			{
				break;
			}
			currentLanguage += 1;
		}
		ChangeLanguage();
	}
}
