using UnityEngine;
using System.Collections;

public class OnTriggerEnterha : MonoBehaviour {
	
//	public GameObject groundCube;
	
	
//	public GameObject trangle;
	
	public float effectPercent;
	public float liveTime;
	public AudioClip roadBlockAudio;
	public CarProperty selfCar;
	protected float activeTime;
//	public Transform screenGarbage;
	
	// DriftCar dc = new DriftCar();
	
//	public int level;
	
	//Level 1~Level 5
//	public float speedDownPercentL1 = -100.0f;
//	public float speedDownPercentL2 = -50.0f;
//	public float speedDownPercentL3 = -50.0f;
//	public float speedDownPercentL4 = -50.0f;
//	public float speedDownPercentL5 = -50.0f;
	
	IEnumerator Start () {
		activeTime = Time.time;
		yield return new WaitForSeconds(liveTime);
		if(gameObject)
			Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {    
		CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
		if(collisionCar == selfCar && Time.time - activeTime <= 3.0f)
		{
			return;
		}
//		Debug.Log(collisionCar.name);
		if(collisionCar)
		{
			collisionCar.Spikes(effectPercent);
			AudioSource.PlayClipAtPoint(roadBlockAudio, collisionCar.transform.position);
			Destroy(gameObject);
		}
    }
}
