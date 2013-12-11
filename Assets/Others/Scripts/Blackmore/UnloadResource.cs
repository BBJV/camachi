using UnityEngine;
using System.Collections;

public class UnloadResource : MonoBehaviour {
	public string objectName;
	public int requireMem = 512;
	public GameObject[] destroyObjects;
	void Awake(){
		Resources.UnloadUnusedAssets();
		//Application.targetFrameRate = 30;
		if(SystemInfo.systemMemorySize > requireMem && objectName != "")
		{
			Instantiate(Resources.Load(objectName, typeof(GameObject)));
		}
	}
	
//	void Update () {
//		if (Time.frameCount % 30 == 0)
//		 {
//		    System.GC.Collect();
//		 }
//	}
	
	void DestroyObjects () {
		foreach(GameObject obj in destroyObjects)
		{
			Destroy(obj);
		}
	}
}
