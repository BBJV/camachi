using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {
	public Transform[] Weathers;
	public float TimeInterval;
	private float NowTime;
	private int NowWeather;
	private int BeforeWeather;
	private int SameWeatherCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		NowTime += Time.deltaTime;
		if(NowTime > TimeInterval){
			NowTime = 0;
			BeforeWeather = NowWeather;
			NowWeather = Random.Range(0,Weathers.Length);
			if(NowWeather == BeforeWeather){
				SameWeatherCount++;
				if(SameWeatherCount >= 3){
					while(NowWeather == BeforeWeather){
						NowWeather = Random.Range(0,Weathers.Length);
					}
					SameWeatherCount = 0;
				}
			}else{
				SameWeatherCount = 0;
			}
			if(NowWeather != BeforeWeather){
				Weathers[BeforeWeather].gameObject.SetActiveRecursively(false);
				Weathers[NowWeather].gameObject.SetActiveRecursively(true);
			}
		}
	}
	void OnEnable(){
		int templength = Weathers.Length;
		
		for(int i = 0 ; i < templength ; i++){
			Weathers[i].gameObject.SetActiveRecursively(false);
		}
		SameWeatherCount = 0;
		NowWeather = Random.Range(0,Weathers.Length);
		BeforeWeather = NowWeather;
		Weathers[NowWeather].gameObject.SetActiveRecursively(true);
		NowTime = 0.0f;
	}
}
