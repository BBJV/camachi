using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
	
	public Definition.eSceneID loadScene;
	
	// Use this for initialization
	void Start () {
		Application.LoadLevel(loadScene.ToString());
	}
	
	
}
