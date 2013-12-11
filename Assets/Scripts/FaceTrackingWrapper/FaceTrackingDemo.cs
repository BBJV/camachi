using UnityEngine;
using System.Collections;
using System.IO;

public class FaceTrackingDemo : MonoBehaviour {
		
	public GameObject pointPrefab;
	private GameObject[] pointObjs;
	private Vector3 origin;
	
	public string licensePath = @"License_10_12_2012.lic";
	public string modelPath = @"R15154 Profile StaticCam.imbin";
	public float failThreshold = 0.4f;
	
	public DynamicTexture canvasTex;
	private GameObject canvas;
	
	private FaceTrackingWrapper ftWrapper;

	// Use this for initialization
	void Start () {
		
		ftWrapper = FaceTrackingWrapper.Wrapper;
		
		// try start face tracking
		if(ftWrapper.StartFaceTracking(Application.streamingAssetsPath+@"/"+licensePath, 
			Application.streamingAssetsPath+@"/"+modelPath)) {
			ftWrapper.Driver.FailureThreshold = failThreshold;
		}
		
		canvas = canvasTex.gameObject;
		
		// initialize tracking point game objects
		pointObjs = new GameObject[64];
		for(int i = 0; i < 64; i++) {
			pointObjs[i] = Instantiate(pointPrefab) as GameObject;
		}
		
		origin = canvas.transform.position;
		origin.x -= canvas.transform.localScale.x/2;
		origin.y += canvas.transform.localScale.y/2;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// if not started, try start face tracking
		if(!ftWrapper.IsStarted) {
			if(ftWrapper.StartFaceTracking(Application.streamingAssetsPath+licensePath, Application.streamingAssetsPath+modelPath)) {
				// change the failureThreshold by access to LiveDriver
				ftWrapper.Driver.FailureThreshold = failThreshold;
			}
		}
		else {
			// if started, do update
			ftWrapper.UpdateFaceTracking();
			// if not calibrated, do calibration
			if(!ftWrapper.IsCalibrated) {
				if(!ftWrapper.IsCalibrating) {
					ftWrapper.Calibrate();
				}
			}
			else {
				// re-position tracking points
				LiveDriverWrapper.Point[] points = ftWrapper.TrackingPoints;
				for(int i = 0; i < points.Length; i++) {
					pointObjs[i].transform.position = GetPointPos(points[i].Value);
				}
				// output info
				// example of getting more data through LiveDriver
				Debug.Log("timestamp: " + ftWrapper.Driver.LatestOutput.TimeStamp);
				// example of getting common data through FaceTrackingWrapper
				Debug.Log("head position" + ftWrapper.HeadPosition);
				Debug.Log("head rotation" + ftWrapper.HeadRotation);
			}
			
			// apply texture from webcam
			canvasTex.ApplyTex(ftWrapper.WebcamImage);
			
			// hot key for calibration;
			if(Input.GetKeyUp(KeyCode.F12)) {
				ftWrapper.Calibrate();
			}
		}
		
	}
	
	void OnApplicationQuit() {
		// stop face tracking before quit
		ftWrapper.StopFaceTracking();
		Debug.Log("quit");
	}
	
	private Vector3 GetPointPos(Vector2 pos) {
		Vector3 result = origin;
		if(ftWrapper != null) {
			result.x += pos.x/ftWrapper.WebcamImageWidth*canvas.transform.localScale.x;
			result.y -= pos.y/ftWrapper.WebcamImageHeight*canvas.transform.localScale.y;
		}
		return result;
	}
}
