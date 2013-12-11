using UnityEngine;
using System.Collections;

public class TurnOnOffTriangle : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
//	
//	}
	public GameObject[] triangles;
	
	public void TurnOnTriangles () {
		foreach(GameObject triangle in triangles)
		{
			triangle.SetActiveRecursively(true);
		}
	}
	
	public void TurnOffTriangles () {
		foreach(GameObject triangle in triangles)
		{
			triangle.SetActiveRecursively(false);
		}
	}
}
