using UnityEngine;
using System.Collections;

public class InterpNodes : MonoBehaviour {
	public Transform Nodes;
	private Transform[] OriginalNodes;
	private Transform[] NewNodes;
	// Use this for initialization
	void Start () {
		OriginalNodes = new Transform[Nodes.GetChildCount()];
		for(int i = 0 ; i < Nodes.GetChildCount() ; i++){
			OriginalNodes[i] = Nodes.GetChild(i);
		}
		NewNodes = new Transform[Nodes.GetChildCount() * 4];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
