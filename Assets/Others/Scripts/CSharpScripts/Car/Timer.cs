using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
//	private ArrayList timeRace = new ArrayList();
	private bool timeCount = false;
	private float TotalTime;
	
//	void OnGUI () {
		/*int i = 1;
		foreach(float time in timeRace)
		{
//			if(time >= 5.0f)
//				Debug.LogError(time);
			GUI.Label(new Rect( 200, 25 * i, 200, 50),"time" + i + " count = " + time);
			i++;
		}*/
//	}
	
	void Update () {
//		if(timeRace.Count <= 0){
//			return;
//			}
		if(timeCount)
		{
			
//			timeRace[timeRace.Count - 1] = (float)timeRace[timeRace.Count - 1] + Time.deltaTime;
			TotalTime += Time.deltaTime;
		}
	}
	
	public void SetWaiting(bool wait){
//		Debug.Log("TotalTime:" +TotalTime+", wait:"+wait.ToString());
		timeCount = !wait;
	}
	
//	int nodeNum = -1;
	
//	void OnTriggerEnter(Collider other){
//		Node collNode = other.transform.GetComponent<Node>();
//		if(!collNode)
//			return;
//		if(nodeNum == -1)
//		{
//			nodeNum = collNode.GetIndex();
//			//timeCount = true;
//			timeRace.Add(0.0f);
//		}
//		else if(nodeNum == collNode.GetIndex())
//		{
//			timeRace.Add(0.0f);
//		}
//	}
	
	public int GetMin()
	{
//		if(timeRace.Count <= 0){
//			return 0;
//		}else{
			int min = (int)(TotalTime / 60.0f);
			if(min > 99){
				min = 99;
			}
			return min;
//		}
	}
	
	public int GetSec()
	{
//		if(timeRace.Count <= 0){
//			return 0;
//		}else{
			return (int)(TotalTime % 60.0f);
//		}
	}
}
