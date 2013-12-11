using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Mip : MonoBehaviour {
	public float sensitivity = 100;
    public float loudness = 0;
	private bool IsRecording;
	
	private float[] ListenData;
	
	private float[] RecordData;
	public Transform RecordAudioTransform;
	/*
	public TextMesh MaxF;
	public TextMesh MinF;
	public TextMesh MinF2;
	*/
	private AudioSource RecordAudio;
	private float RecordTime;
	private int StartRecordTimeSamples;
	private float StartRecordTime;
	private int MyFrequency = 44100;
	private int MinFrequency;
	private int MaxFrequency;
    void Start() {
		//MyFrequency = AudioSettings.outputSampleRate;//wrong way
		AudioSettings.SetDSPBufferSize(256 , 4);
        audio.clip = Microphone.Start(null, true, 30, MyFrequency);
		//audio.clip = Microphone.Start(null, true, 100, 100);
		//print ("audio.clip.samples = " + audio.clip.samples);
        audio.loop = true; // Set the AudioClip to loop
        //audio.mute = true; // Mute the sound, we don't want the player to hear it
        while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
        //audio.Play(); // Play the audio source!
		//print ("audio.clip.frequency = " + audio.clip.frequency);
		//Microphone.GetDeviceCaps(null,out MinFrequency,out MaxFrequency);
		//MaxF.text = "M" + MinFrequency;
		//print ("MinFrequency = " + MinFrequency);
		//print ("aMaxFrequency = " + MaxFrequency);
		int temp1;
		int temp2;
		AudioSettings.GetDSPBufferSize(out temp1 , out temp2);
		//MaxF.text = "" + temp1;
		//MinF.text = "" + temp2;
		//MinF.text = ""+MinFrequency;
		IsRecording = false;
		RecordTime = 0.0f;
		//ListenData = new float[audio.clip.samples];
		ListenData = new float[10];
		
		RecordAudio = RecordAudioTransform.GetComponent<AudioSource>();
		StartRecord();
		//print ("AudioSettings.outputSampleRate = " + AudioSettings.outputSampleRate);
		//print ("RecordAudio.clip = " + RecordAudio.clip.length);
    }
	//float beforePosition;
    void Update(){
       // loudness = GetAveragedVolume() * sensitivity;
		
		//print ("audio.time = " + audio.clip.samples * audio.clip.channels);
		//print ("audio.time = " + audio.time);
		
		//print ("Microphone.GetPosition = " + Microphone.GetPosition(null));
		//print ("GetPosition diff= " + (Microphone.GetPosition(null) - beforePosition) / Time.deltaTime);
		//print ("Microphone.GetPosition = " + Microphone.IsRecording(null));
		//beforePosition = Microphone.GetPosition(null);
		//MinF.text = ""+audio.timeSamples;
		//MinF2.text = ""+audio.time;
		//MinF2.text = "F"+Microphone.GetPosition(null);
		
		if(IsRecording){
			RecordTime += Time.deltaTime;
			
			//print ("Recording = " + audio.time);
			if(RecordTime > 5.0f){
				//print ("Recording Over Time");
				StopRecord();
			}
		}
		
    }

	public float GetAveragedVolume()
    { 
        float a = 0;
		//print ("audio.time = " + audio.time);
		//print ("audio.timeSamples = " + audio.timeSamples);
        audio.GetOutputData(ListenData,0);
        foreach(float s in ListenData)
        {
            a += Mathf.Abs(s);
        }
	
		//print ("a = " + a);
		//a about inside 50
        //return a/256;
		return a;
    }
	public float GetNowVolume()
    { 
        float a = 0;
		//audio.clip.GetData(ListenData,Microphone.GetPosition(null));
		//print ("audio.timeSamples = " + audio.timeSamples);
        audio.clip.GetData(ListenData,audio.timeSamples);
		//audio.GetOutputData(ListenData,0);
		foreach(float s in ListenData)
        {
            a += Mathf.Abs(s);
        }
		//print ("a = " + a);
		return a;
    }
	public void StartRecord(){
		if(IsRecording){
			return;
		}
		print ("Start Record");
		IsRecording = true;
		//StartRecordTimeSamples = Microphone.GetPosition(null);
		
		StartRecordTime = audio.time - 0.5f;
		if(StartRecordTime < 0.0f){
			StartRecordTime = audio.clip.length + StartRecordTime;
		}
		StartRecordTimeSamples = (int)(StartRecordTime * audio.clip.frequency);
		
		//audio.Stop();
		//audio.Play();
	}
	int TempLengthSamples;
	public void StopRecord(){
		if(!IsRecording){
			return;
		}
		
		print ("Stop Record");
		
		print ("RecordTime = " + RecordTime);
		
		//TempLengthSamples = (int)(MyFrequency * RecordTime);
		//RecordAudio.clip = AudioClip.Create("MySinoid", TempLengthSamples, 1, MyFrequency, false, false);
		//RecordData = new float[TempLengthSamples];
		
		TempLengthSamples = Microphone.GetPosition(null) - StartRecordTimeSamples;
		int fre = (int)(TempLengthSamples / RecordTime);
		RecordAudio.clip = AudioClip.Create("MySinoid", TempLengthSamples, 1, MyFrequency, false, false);
		RecordData = new float[TempLengthSamples];
		
		//RecordAudio.clip = AudioClip.Create("MySinoid", 5 * MyFrequency, 1, MyFrequency, false, false);
		//RecordData = new float[5 * MyFrequency];
		
		//RecordData = new float[TempLengthSamples];
		//audio.clip.GetData(RecordData,StartRecordTimeSamples);
		audio.clip.GetData(RecordData,0);
		Microphone.End(null);
		//print ("audio.clip.time = " + audio.time);
		RecordAudio.clip.SetData(RecordData,0);
		//MaxF.text = "M" + RecordAudio.clip.length;
		//MinF.text = "N"+AudioSettings.driverCaps;
		//MinF2.text = "F"+Microphone.GetPosition(null);
		//RecordAudio.Stop();
		
		RecordAudio.loop = false;
		RecordAudio.Play();
		
		IsRecording = false;
		RecordTime = 0.0f;
		
	}
}
