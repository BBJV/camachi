using UnityEngine;
using System.Collections;

public class GameCountDownTimer : MonoBehaviour {
	public Texture2D[] TimeNum;
	public Texture2D TimeColon;
	public float scale;
	public bool IsShowCountDown;
	private float NowTime;
	private Rect TimeNumMin10_Co;
	private Rect TimeNumMin_Co;
	private Rect TimeColon_Co;
	private Rect TimeNumSec10_Co;
	private Rect TimeNumSec_Co;
	// Use this for initialization
	void Start () {
		IsShowCountDown = false;
		TimeNumMin10_Co = new Rect();
		TimeNumMin_Co = new Rect();
		TimeColon_Co = new Rect();
		TimeNumSec10_Co = new Rect();
		TimeNumSec_Co = new Rect();
		//time
		//10 min
		TimeNumMin10_Co.width = TimeNum[0].width * scale;
		TimeNumMin10_Co.height = TimeNum[0].height * scale;
		TimeNumMin10_Co.x = (Screen.width - TimeNum[0].width * scale * 4 - TimeColon.width * scale
		                     - 10.0f * 4.0f) / 2.0f;
		TimeNumMin10_Co.y = 0.0f;
		//min
		TimeNumMin_Co.width = TimeNum[0].width * scale;
		TimeNumMin_Co.height = TimeNum[0].height * scale;
		TimeNumMin_Co.x = TimeNumMin10_Co.x + TimeNumMin_Co.width + 10.0f;
		TimeNumMin_Co.y = TimeNumMin10_Co.y;
		//:
		TimeColon_Co.width = TimeColon.width * scale;
		TimeColon_Co.height = TimeColon.height * scale;
		TimeColon_Co.x = TimeNumMin_Co.x + TimeNumMin_Co.width  + 10.0f;
		TimeColon_Co.y = TimeNumMin10_Co.y;
		//10 sec
		TimeNumSec10_Co.width = TimeNum[0].width * scale;
		TimeNumSec10_Co.height = TimeNum[0].height * scale;
		TimeNumSec10_Co.x = TimeColon_Co.x + TimeColon_Co.width + 10.0f;
		TimeNumSec10_Co.y = TimeNumMin_Co.y;
		//sec
		TimeNumSec_Co.width = TimeNum[0].width * scale;
		TimeNumSec_Co.height = TimeNum[0].height * scale;
		TimeNumSec_Co.x = TimeNumSec10_Co.x + TimeNumMin_Co.width + 10.0f;
		TimeNumSec_Co.y = TimeNumMin_Co.y;
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	
	void OnGUI() {
		if(!IsShowCountDown)
			return;
		int mins = (int)(NowTime / 60.0f);
		int secs = (int)(NowTime % 60.0f);
		
		GUI.DrawTexture(TimeNumMin10_Co,TimeNum[mins / 10]);
		GUI.DrawTexture(TimeNumMin_Co,TimeNum[mins % 10]);
		GUI.DrawTexture(TimeColon_Co,TimeColon);
		GUI.DrawTexture(TimeNumSec10_Co,TimeNum[secs / 10]);
		GUI.DrawTexture(TimeNumSec_Co,TimeNum[secs % 10]);
	}
	
	public void SetNowTime(float time){
		NowTime = time;
	}
	
	
}
