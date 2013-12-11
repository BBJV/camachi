using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {
	public AudioClip MyMusic = null;
	public float Volume = 1.0f;
	public bool IsLoop = true;
	public bool IsPlayOnAwake = false;
	public bool IsFxSound = false;
	private Transform MyTransform;
	private AudioSource MyAudio = null;
	private bool IsPlayed;
	private static string SoundFinish = "SoundFinish";
	// Use this for initialization
	void Start () {
		MyTransform = transform;
		MyAudio = gameObject.AddComponent<AudioSource>();
		MyAudio.loop = IsLoop;
		MyAudio.playOnAwake = IsPlayOnAwake;
		MyAudio.clip = MyMusic;
	
		MyAudio.volume = Volume;
		IsPlayed = false;
	}
	void Update(){
		if(!IsLoop && IsPlayed && !MyAudio.isPlaying){
			MyTransform.parent.BroadcastMessage(SoundFinish,SendMessageOptions.DontRequireReceiver);
			IsPlayed = false;
		}
	}
	void SoundOn(){
		if(!MyAudio.isPlaying){
			MyAudio.Play();
			IsPlayed = true;
		}else if(IsFxSound){
			//it is fx sound
			MyAudio.Play();
		}
		//AudioHorn.volume = 1.0f;
	}
	void SoundOff(){
		MyAudio.Stop();
		//AudioHorn.volume = 0.0f;
	}
}
