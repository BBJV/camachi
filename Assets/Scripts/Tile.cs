using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	public Transform RoadMesh;
	public Vector3 Size;
	
	// Use this for initialization
	void Start () {
		Size = RoadMesh.GetComponent<MeshFilter>().mesh.bounds.size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
