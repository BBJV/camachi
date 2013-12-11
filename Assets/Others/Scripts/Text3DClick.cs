using UnityEngine;
using System.Collections;

public class Text3DClick : MonoBehaviour {
	
	public string textFieldName = "";
	public bool isDraw = false;
	public string text = "";
	public string strBaseText = "";
	protected string strEmpty = "";
	
	public float xStart = -1;
	public float yStart = -1;
	
	public TextMesh textMesh;
	public Text3DClick[] text3DsToCloseOnFocus;
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
			
//			if(text != strEmpty) {
//				textMesh.text = text;
//			}else{
//				textMesh.text = strBaseText;
//			}
		}
		
	}
#endif
	
	#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
	
		IEnumerator OnKeyboardInput () {
			keyboard = TouchScreenKeyboard.Open(text);
			while(!keyboard.done)
			{
				yield return null;
			}
			text = keyboard.text;
			if(text != strEmpty) {
				textMesh.text = text;
			}else{
				textMesh.text = strBaseText;
			}
			isDraw = false;
		}	
	#endif
	
	IEnumerator ShowText () {
		while(isDraw)
		{
			textMesh.text = text + "|";
			yield return new WaitForSeconds(0.1f);
			textMesh.text = text;
			yield return new WaitForSeconds(0.1f);
		}
		if(text != strEmpty) {
			textMesh.text = text;
		}else{
			textMesh.text = strBaseText;
		}
	}
	
	void Assign () {
		if(text != strEmpty) {
			textMesh.text = text;
		}else{
			textMesh.text = strBaseText;
		}
	}
}
