using UnityEngine;
using System.Collections;

public class PasswordText3DClick : Text3DClick {
	
	private string encryptedStr = "";
	private string strStar = "*";
	#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
		private TouchScreenKeyboard keyboard;
	#endif
	
	void OnClick() {
		if(isDraw) {
			isDraw = false;
		}else{
			foreach(Text3DClick t in text3DsToCloseOnFocus) {
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
			text = GUI.TextField(new Rect(xStart, yStart, 100, 30), text, 20);
			GUI.FocusControl(textFieldName);
//			encryptedStr = "";
//			foreach(char c in text) {
//				encryptedStr = encryptedStr + strStar;
//			}
			encryptedStr = Encrypted(text);
//			if(text != strEmpty) {
//				textMesh.text = encryptedStr;
//			}else{
//				textMesh.text = strBaseText;
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
			keyboard = TouchScreenKeyboard.Open(text);
			while(!keyboard.done)
			{
				yield return null;
			}
			text = keyboard.text;
			if(text != strEmpty) {
				textMesh.text = Encrypted(text);
			}else{
				textMesh.text = strBaseText;
			}
			isDraw = false;
		}	
	#endif
	
	IEnumerator ShowText () {
		while(isDraw)
		{
			textMesh.text = encryptedStr + "|";
			yield return new WaitForSeconds(0.1f);
			textMesh.text = encryptedStr;
			yield return new WaitForSeconds(0.1f);
		}
		if(text != strEmpty) {
			textMesh.text = Encrypted(text);
		}else{
			textMesh.text = strBaseText;
		}
	}
	
	void Assign () {
		if(text != strEmpty) {
			textMesh.text = Encrypted(text);
		}else{
			textMesh.text = strBaseText;
		}
	}
}
