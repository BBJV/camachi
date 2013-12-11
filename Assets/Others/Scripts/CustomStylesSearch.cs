using UnityEngine;
using System.Collections;

public class CustomStylesSearch : MonoBehaviour {

	public GUIStyle GetCustomStyleByName(GUISkin skin, string styleName) {
		foreach(GUIStyle gs in skin.customStyles) {
			if(gs.name == styleName) {
				return gs;
			}
		}
		
		return null;
	}
}
