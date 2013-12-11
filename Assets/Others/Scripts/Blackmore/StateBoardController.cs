using UnityEngine;
using System.Collections;

public class StateBoardController : MonoBehaviour {
	public Transform StateBoard;
	private ParticleEmitter StateEmiiter;
	private ParticleRenderer StateRenderer;
	
	//0:block
	//1:invin
	//2:tire
	//3:speed up
	//4:speed down
	//5:hit
	public Material[] English;
	public Material[] SC;
	public Material[] TC;
	private Material[][] All;
	private float NowTime;
	// Use this for initialization
	void Start () {
		StateEmiiter = StateBoard.GetComponent<ParticleEmitter>();
		StateEmiiter.emit = false;
		StateRenderer = StateBoard.GetComponent<ParticleRenderer>();
		All = new Material[3 ][ ];
		
			All[0] = English;
		All[1] = SC;
		All[2] = TC;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(StateEmiiter.emit){
			NowTime -= Time.deltaTime;
			if(NowTime < 0.0f){
				NowTime = 1.0f;
				StateEmiiter.emit = false;
			}
		}
		
	}
	
	void ShowState(int type){
		string lan = PlayerPrefs.GetString("Language");
		int lanindex = 0;
		if(lan.Equals("Simplified Chinese")){
			lanindex = 1;
		}else if(lan.Equals("Traditional Chinese")){
			lanindex = 2;
		}
		StateRenderer.material = All[lanindex][type];
		StateEmiiter.Emit();
	}
}
