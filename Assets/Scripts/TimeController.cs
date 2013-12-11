using UnityEngine;
using System.Collections;
/*
public enum DayTime{
	Morning = 0,
	Day,
	Dusk,
	Night
}
*/
public class TimeController : MonoBehaviour {
	/*
	 |morning|day|dusk|night|
	 * */
	public float[] DayTimeCount;
	public Color[] SunColors;
	public int FirstState;
	public Transform Sun;
	public Transform Moon;
	public Transform SunStartPosition;
	public Transform SunRestPosition;
	public Transform SunRotateAxis;
	public float TransferTime = 2.0f;
	public Transform SkyDay;
	public Transform SkyNight;
	
	private Color[] ColorIndex;
	private int Morning = 0;
	private int Day = 1;
	private int Dusk = 2;
	private int Night = 3;
	
	
	private int NowState;
	private int NextState;
	private float NowTime;
	
	private float SunRotateAngle;
	private float MoonRotateAngle;

	// Use this for initialization
	void Start () {
		
		NowState = FirstState;
		NextState = FirstState + 1;
		if(NextState > Night){
			NextState = Morning;
		}
		RenderSettings.ambientLight = SunColors[NowState];
		ColorIndex = new Color[SunColors.Length];
		for(int i = 0 ; i < ColorIndex.Length - 1; i++){
			ColorIndex[i] = (SunColors[i + 1] - SunColors[i]) / DayTimeCount[i];
			//ColorIndex[i] = (SunColors[i + 1] - SunColors[i]) / TransferTime;
		}
		//ColorIndex[ColorIndex.Length - 1] = (SunColors[0] - SunColors[SunColors.Length - 1]) / DayTimeCount[DayTimeCount.Length - 1];
		ColorIndex[ColorIndex.Length - 1] = (SunColors[0] - SunColors[SunColors.Length - 1]) / TransferTime;
		ResetSunMoonPosition();
		SunRotateAngle = 200.0f / (DayTimeCount[Morning] + DayTimeCount[Day] + DayTimeCount[Dusk]);
		MoonRotateAngle = 200.0f / (DayTimeCount[Night]);
		//Moon.RotateAround(SunRotateAxis.position,-Vector3.forward,90.0f);
		//CenterAxis
	}
	void ResetSunMoonPosition(){
		if(NowState == Morning){
			Sun.position = SunStartPosition.position;
			Moon.position = SunRestPosition.position;
			SkyDay.gameObject.SetActiveRecursively(true);
			SkyNight.gameObject.SetActiveRecursively(false);
		}else if(NowState == Night){
			Moon.position = SunStartPosition.position;
			Sun.position = SunRestPosition.position;
			SkyDay.gameObject.SetActiveRecursively(false);
			SkyNight.gameObject.SetActiveRecursively(true);
		}
	}
	int tempState;
	// Update is called once per frame
	void Update () {
		NowTime += Time.deltaTime;
		if(NowState == Night){
			Moon.RotateAround(SunRotateAxis.position,-Vector3.forward,MoonRotateAngle * Time.deltaTime);
		}else {
			Sun.RotateAround(SunRotateAxis.position,-Vector3.forward,SunRotateAngle * Time.deltaTime);
		}
		/*
		RenderSettings.ambientLight = RenderSettings.ambientLight + ColorIndex[NowState]
				 * Time.deltaTime;
		if(NowTime >= DayTimeCount[NowState]){
			NowTime = 0.0f;
			NowState = NextState;
			RenderSettings.ambientLight = SunColors[NowState];
			NextState++;
			if(NextState > Night){
				NextState = Morning;
			}
			ResetSunMoonPosition();
		}
		*/
		if(NowTime >= DayTimeCount[NowState]){
			/*
			RenderSettings.ambientLight = RenderSettings.ambientLight + ColorIndex[NowState]
				  Time.deltaTime;
			*/
			/*RenderSettings.ambientLight = Vector4.Lerp(
				RenderSettings.ambientLight,
				SunColors[NextState],
				Time.deltaTime * 0.5f);*/
			/*
			if(Vector4.SqrMagnitude(RenderSettings.ambientLight - SunColors[NextState]) < 0.01f){
			*/
				NowTime = 0.0f;
				NowState = NextState;
				RenderSettings.ambientLight = SunColors[NowState];
				NextState++;
				if(NextState > Night){
					NextState = Morning;
				}
				ResetSunMoonPosition();
			//}
		}
		if(NowState == Morning || NowState == Dusk){
			RenderSettings.ambientLight = RenderSettings.ambientLight + ColorIndex[NowState]
				 * Time.deltaTime;
		}
	}
}
