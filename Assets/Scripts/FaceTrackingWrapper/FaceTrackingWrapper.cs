
/****************************************
 * 
 *   Face Tracking Wrapper
 *   
 *   developped by Xun Zhang (lxjk001@gmail.com)
 *   2012.10.13
 * 
 * **************************************/


using UnityEngine;
using System.Collections;
using System.IO;
using LiveDriverWrapper;

public sealed class FaceTrackingWrapper{
	
	static private FaceTrackingWrapper wrapper = new FaceTrackingWrapper();

	private WebcamAccessor webcam;
	
	private LiveDriver liveDriver;
	private Image image;
	
	private bool isStarted = false;
	
	
	static public FaceTrackingWrapper Wrapper {
		get {
			return wrapper;
		}
	}
	
	public LiveDriver Driver {
		get {
			return liveDriver;
		}
	}
	
	public Color32[] WebcamImage {
		get {
			if(isStarted) {
				return webcam.WebcamPixels32;
			}
			return null;
		}
	}
	
	public int WebcamImageWidth {
		get {
			if(isStarted) {
				return webcam.Width;
			}
			return 0;
		}
	}
	
	public int WebcamImageHeight {
		get {
			if(isStarted) {
				return webcam.Height;
			}
			return 0;
		}
	}
	
	public Point[] TrackingPoints {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.Points.ToArray();
			}
			return null;
		}
	}
	
	public AnimationControl[] AnimationControls {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.AnimationControls.ToArray();
			}
			return null;
		}
	}
	
	public Vector3 HeadPosition {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.HeadPosition;
			}
			return Vector3.zero;
		}
	}
	
	public Vector3 HeadRotation {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.HeadRotation;
			}
			return Vector3.zero;
		}
	}
	
	public Vector3 CharacterHeadPosition {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.CharacterHeadPosition;
			}
			return Vector3.zero;
		}
	}
	
	public Vector3 CharacterHeadRotation {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.CharacterHeadRotation;
			}
			return Vector3.zero;
		}
	}
	public Vector3 CameraPosition {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.CameraPosition;
			}
			return Vector3.zero;
		}
	}
	
	public Vector3 CameraRotation {
		get {
			if(isStarted) {
				return liveDriver.LatestOutput.CameraRotation;
			}
			return Vector3.zero;
		}
	}
	
	public bool IsStarted {
		get {
			return isStarted;
		}
	}
	
	public bool IsCalibrating {
		get {
			if(isStarted) {
				return liveDriver.IsCalibrating;
			}
			return false;
		}
	}
	
	public bool IsCalibrated {
		get {
			if(isStarted) {
				return liveDriver.IsCalibrated;
			}
			return false;
		}
	}
	
	
	
	
	public bool StartFaceTracking(string licensePath, string modelPath) {
		return StartLiveDriver(licensePath, modelPath) && webcam.StartWebcam();
	}
		
	public void Calibrate() {
		if(isStarted) {
			liveDriver.Calibrate();
		}
	}
	
	public void UpdateFaceTracking() {
		if(isStarted) {
			byte[] data = GetByteArrayFromColor32Array(webcam.WebcamPixels32);
			image.SetFromRawBuffer(data,
				640,
				480,
				24,
				false);
			liveDriver.AddImage(image);
		}
	}
	
	public void StopFaceTracking() {
		liveDriver.Stop();
		liveDriver.DestoryImage(image);
		LiveDriverGlobals.DestoryLiveDriver(liveDriver);
		webcam.StopWebcam();
		isStarted = false;
	}
	
	
	
	private FaceTrackingWrapper() {
		webcam = new WebcamAccessor();
		liveDriver = null;
		image = null;
	}
	
	private bool StartLiveDriver(string licensePath, string modelPath) {
		string license = File.ReadAllText(licensePath);
		liveDriver = LiveDriverGlobals.CreateLiveDriver(license);
		if(liveDriver == null) {
			return false;
		}
		liveDriver.CurrentTrackingModel = modelPath;
		if(!liveDriver.Start()) {
			return false;
		}
		image = liveDriver.CreateImage();
		isStarted = true;
		Calibrate();
		return true;
	}
	
	private byte[] GetByteArrayFromColor32Array(Color32[] colorArray) {
		byte[] result = new byte[colorArray.Length*3];
		for(int i = 0; i < colorArray.Length; ++i) {
			int idx = i*3;
			result[idx + 0] = colorArray[i].b;
			result[idx + 1] = colorArray[i].g;
			result[idx + 2] = colorArray[i].r;
		}
		return result;
	}
}
