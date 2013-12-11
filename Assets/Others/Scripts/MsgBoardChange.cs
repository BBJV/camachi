using UnityEngine;
using System.Collections;

public class MsgBoardChange : MonoBehaviour {

	public MeshRenderer meshRenderer;
	
	void Awake() {
		if(meshRenderer) {
			meshRenderer = GetComponent<MeshRenderer>();
		}
	}
	
	public void ChangeMatetial(Texture t) {
		meshRenderer.material.mainTexture = t;
	}
	
}
