using UnityEngine;
using System.Collections;

public class PressSteer : MonoBehaviour {
	public Transform HitTransform;
	public Transform CarPosition;
	public Transform[] TranslateXRange;
	//public float TranslateXRange;
	public Vector3 TranslateIndex;
	public int ActiveLayer;
	public Engine MyEngine;
	Ray ray;
	RaycastHit hit ;
	private bool IsPressed;
	private Vector3 BeforeVector;
	private Vector3 NowVector;
	private Vector3 TempVector;
	private float CrossVector;
	private Quaternion OriginalQuaternion;
	private Vector3 NowTranslate;
	private static string  Translate = "Translate";
	//private float NowX;
	//private bool IsTurnRight;
	private int TurnDir;//0:none , 1:right , -1:left
	private float BeforeAngle;
	private float NowAngle;
	private float TempAngle;
	private int MyActiveLayer;
	// Use this for initialization
	void Start () {
		NowTranslate = Vector3.up;
		MyActiveLayer = 1 << ActiveLayer;
		MyActiveLayer = ~MyActiveLayer;
		
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetMouseButton(0)) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast (ray, out hit,Mathf.Infinity,MyActiveLayer) && hit.transform == HitTransform) {
				NowVector = hit.point - HitTransform.position;//HitTransform.InverseTransformPoint(hit.point);
				
				if(!IsPressed){
					BeforeVector = NowVector;
				}
				TempAngle = Vector3.Angle(BeforeVector,NowVector);
				//print("TempAngle = " + TempAngle);
				if(TempAngle > 3.0f){
					if(HitTransform.InverseTransformDirection(Vector3.Cross(BeforeVector,NowVector)).z < 0.0f){
						//turn right
						//wait now angle to a larger number ,before vector dont change and wait now vector to a larger number
						HitTransform.RotateAroundLocal(-HitTransform.forward,Vector3.Angle(BeforeVector,NowVector) / 50.0f);
						//HitTransform.rotation = Quaternion.Euler(0.0f ,0.0f ,Vector3.Angle(BeforeVector,NowVector));
						TurnDir = 1;
						//if(HitTransform.TransformDirection(Vector3.Cross(Vector3.up,NowVector)).z > 0.0f){
						if(Vector3.Cross(HitTransform.TransformDirection(HitTransform.up),Vector3.up).z < 0.0f){
							TurnDir = -1;
						}
					}else{
						HitTransform.RotateAroundLocal(HitTransform.forward,Vector3.Angle(BeforeVector,NowVector) / 50.0f);
						TurnDir = -1;
						//if(HitTransform.TransformDirection(Vector3.Cross(Vector3.up,NowVector)).z < 0.0f){
						if(Vector3.Cross(HitTransform.TransformDirection(HitTransform.up),Vector3.up).z > 0.0f){
							TurnDir = 1;
						}
					}
					BeforeVector = NowVector;
				}
				IsPressed = true;
	        }else{
				IsPressed = false;
			}
		}else 
		if(Input.GetMouseButtonUp(0)){
			IsPressed = false;
		}
		
#else
		/*
		foreach (Touch touch in Input.touches) {
	        if (touch.phase == TouchPhase.Began) {
	            ray = Camera.main.ScreenPointToRay(touch.position);
	            if (Physics.Raycast (ray, out hit) && hit.transform == HitTransform) {
					GameState.ChangeGameState(NextStateTransform);
		        }
	        }
	    }
		*/
#endif
		if(!IsPressed){
			HitTransform.rotation = 
				Quaternion.RotateTowards(HitTransform.rotation,OriginalQuaternion,Time.deltaTime * 150.0f);
			TurnDir = 0;
		}
	}
	private float NowTranslateX;
	void FixedUpdate(){
		if(TurnDir == 1){
			NowTranslateX = 0.1f * MyEngine.Velocity / MyEngine.MaxVelocity;
			//CarPosition.position.x += NowTranslateX;
			if((CarPosition.position.x + NowTranslateX) >= TranslateXRange[1].position.x){
				//NowX = TranslateXRange;
				NowTranslate.Set(0.0f,0.0f,0.0f);
			}else{
			
				NowTranslate.Set(NowTranslateX,0.0f,0.0f);
			}
		}else if(TurnDir == -1){
			NowTranslateX = 0.1f * MyEngine.Velocity / MyEngine.MaxVelocity;
			//NowX -= NowTranslateX;
			//if(NowX <= -TranslateXRange){
			if((CarPosition.position.x + NowTranslateX) <= TranslateXRange[0].position.x){
				//NowX = -TranslateXRange;
				NowTranslate.Set(0.0f,0.0f,0.0f);
			}else{
				NowTranslate.Set(-NowTranslateX,0.0f,0.0f);
			}
		}else{
			NowTranslate.Set(0.0f,0.0f,0.0f);
		}
		BroadcastMessage(Translate,NowTranslate);
	}
	void OnEnable(){
		IsPressed = false;
		OriginalQuaternion = HitTransform.rotation;
		TurnDir = 0;
	}
}
