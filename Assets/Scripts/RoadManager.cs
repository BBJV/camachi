using UnityEngine;
using System.Collections;

public class RoadManager : MonoBehaviour {
	public Transform Car;
	public Engine MyEngine;
	public Transform[] Tiles;
	public Transform[] NowRoad;
	/*
	public float Velocity;
	public float Acceleration = 50.0f;
	public float MaxVelocity = 30.0f;
	*/
	private Transform[] BeforeRoad;
	private float RunDistance;
	//private bool IsStartEngine;
	
	// Use this for initialization
	void Start () {
		//Tiles.position = new Vector3(NowRoad.position.x,NowRoad.position.y,NowRoad.position.z + NowRoad.GetComponent<Tile>().Size.z);
			//NowRoad.GetComponent<Tile>().Size
		
		BeforeRoad = new Transform[NowRoad.Length];
		//IsStartEngine = false;
		//Velocity = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//if(!IsStartEngine) return;
		
		RunDistance = MyEngine.Velocity * Time.deltaTime;
		for(int i = 0 ; i < NowRoad.Length ; i++){
			NowRoad[i].Translate(0.0f,0.0f,-RunDistance);
		}
		if(Car.position.z > NowRoad[2].position.z){
			SetRoadArray();
		}
	}
	
	//attach new tile and rearrange road array
	void SetRoadArray(){
		for(int i = 0 ; i < NowRoad.Length ; i++){
			BeforeRoad[i] = NowRoad[i];
		}
		NowRoad[0] = BeforeRoad[BeforeRoad.Length - 1];
		for(int i = 1 ; i < NowRoad.Length ; i++){
			NowRoad[i] =  BeforeRoad[i - 1];
		}
		NowRoad[0].position = new Vector3(NowRoad[1].position.x,NowRoad[1].position.y,
			NowRoad[1].position.z + NowRoad[1].GetComponent<Tile>().Size.z);
	}

}
