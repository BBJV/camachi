using UnityEngine;
using System.Collections;

public class newfindcolliderparticles : MonoBehaviour {
	public float burstEnergy = 10.0f;
	public Transform explosionObject;
	
	public int MaxParticleCount;
	public Transform[] explosionTransform;
	public Transform tempPosition;
	
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		myTransform = transform;
		explosionTransform = new Transform[MaxParticleCount];
		
		for(int i = 0; i < MaxParticleCount; i++ ){
		print("i = " + i);
			explosionTransform[i] = (Transform)Instantiate(explosionObject, 
			    		tempPosition.position,  
			    		Quaternion.identity );
			explosionTransform[i].parent = myTransform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
