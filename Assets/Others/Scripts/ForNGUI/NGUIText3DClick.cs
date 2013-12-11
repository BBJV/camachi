using UnityEngine;
using System.Collections;

public class NGUIText3DClick : MonoBehaviour {

	public string textFieldName = "";
	public bool isDraw = false;
	public int maxLength = 20;
	public string text = "";
	public string strBaseText = "";
	protected string strEmpty = "";
	
	public float xStart = -1;
	public float yStart = -1;
	
	public UILabel labelText;
	public NGUIText3DClick[] text3DsToCloseOnFocus;
	
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
			
//			if(text != strEmpty) {
//				labelText.text = text;
//			}else{
//				labelText.text = strBaseText;
//			}
		}
		
	}
#endif
	
	#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
		IEnumerator OnKeyboardInput () {
			keyboard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.Default, false);
			while(!keyboard.done && isDraw)
			{
				yield return null;
			}
			text = keyboard.text;
			if(text != strEmpty) {
				labelText.text = text;
			}else{
				labelText.text = strBaseText;
			}
			isDraw = false;
		}
	#endif
	
	
	
	IEnumerator ShowText () {
		while(isDraw)
		{
			labelText.text = text + "|";
			yield return new WaitForSeconds(0.1f);
			labelText.text = text;
			yield return new WaitForSeconds(0.1f);
		}
		if(text != strEmpty) {
			labelText.text = text;
		}else{
			labelText.text = strBaseText;
		}
	}
	
	void Assign () {
		if(text != strEmpty) {
			labelText.text = text;
		}else{
			labelText.text = strBaseText;
		}
	}
}
