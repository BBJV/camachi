using UnityEngine;
using System.Collections;

public class NGUIPasswordText3DClick : NGUIText3DClick {

	private string encryptedStr = "";
	private string strStar = "*";

	
	#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
		private TouchScreenKeyboard keyboard;
	#endif
	
	void OnClick() {
		if(isDraw) {
			isDraw = false;
		}else{
			foreach(NGUIText3DClick t in text3DsToCloseOnFocus) {
				t.isDraw = false;
			}
			isDraw = true;
#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
			StartCoroutine(OnKeyboardInput());
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
			StartCoroutine(ShowText());
#endif
		}
	}
	
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
	void OnGUI() {
				
		if(isDraw) {
			GUI.SetNextControlName(textFieldName);
			text = GUI.TextField(new Rect(xStart, yStart, 100, 30), text, maxLength);
			GUI.FocusControl(textFieldName);
//			encryptedStr = "";
//			foreach(char c in text) {
//				encryptedStr = encryptedStr + strStar;
//			}
			encryptedStr = Encrypted(text);
//			if(text != strEmpty) {
//				labelText.text = encryptedStr;
//			}else{
//				labelText.text = strBaseText;
//			}
		}
		
	}
#endif
	
	string Encrypted (string text) {
		string encrypted = "";
		foreach(char c in text) {
			encrypted = encrypted + strStar;
		}
		return encrypted;
	}
	
	#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
	
		IEnumerator OnKeyboardInput () {
			keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default, false);
			while(!keyboard.done && isDraw)
			{
				yield return null;
			}
			text = keyboard.text;
			if(text != strEmpty) {
				labelText.text = Encrypted(text);
			}else{
				labelText.text = strBaseText;
			}
			isDraw = false;
		}	
	#endif
	
	
	
	IEnumerator ShowText () {
		while(isDraw)
		{
			labelText.text = encryptedStr + "|";
			yield return new WaitForSeconds(0.1f);
			labelText.text = encryptedStr;
			yield return new WaitForSeconds(0.1f);
		}
		if(text != strEmpty) {
			labelText.text = Encrypted(text);
		}else{
			labelText.text = strBaseText;
		}
	}
	
	void Assign () {
		if(text != strEmpty) {
			labelText.text = Encrypted(text);
		}else{
			labelText.text = strBaseText;
		}
	}
}
