using UnityEngine;
using System.Collections;

public class CountDownBat : MonoBehaviour {
	private CarB[] cars;
	
	private bool isStartPlay = false;
	
	// Use this for initialization
//	void Start () {
//		cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//		StartCoroutine(Wait());
//	}
	
	IEnumerator Wait () {
//		foreach(CarB car in cars)
//		{
//			car.Wait(true);
//		}
		animation.Play("timeBoard1_1"); //fly in and wait
		
		while(!isStartPlay)
		{
			yield return null;
		}
		
		//yield return new WaitForSeconds(1.0f); // wait for play "GO"
		
		cars = FindObjectsOfType(typeof(CarB)) as CarB[];
		foreach(CarB car in cars)
		{
			car.Wait(false);
		}
		
		animation.CrossFade("timeBoard1_3"); //fly out
		
		while(animation.IsPlaying("timeBoard1_3"))
		{
			yield return null;
		}
		Destroy(gameObject);
	}
	
	public void SetStartFlyIn() {
		StartCoroutine(Wait());
	}
	
	public void SetStartFlyOut() {
		isStartPlay = true;
	}
}
