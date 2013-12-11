using UnityEngine;
using System.Collections;

public class NGUITalentButton : MonoBehaviour {
	
	
	
	public NGUIProductSceneViewer nguiProductSceneViewer;
	
	void OnClick() {
		nguiProductSceneViewer.OpenTalentInfoBoard();
	}
}
