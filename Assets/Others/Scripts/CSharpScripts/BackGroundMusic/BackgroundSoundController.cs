/////////
// SoundController.js
//
// This script controls the sound for a car. It automatically creates the needed AudioSources, and ensures
// that only a certain number of sound are played at any time, by limiting the number of OneShot
// audio clip that can be played at any time. This is to ensure that it does not play more sounds than Unity
// can handle.
// The script handles the sound for the engine, both idle and running, gearshifts, skidding and crashing.
// PlayOneShot is used for the non-looping sounds are needed. A separate AudioSource is create for the OneShot
// AudioClips, since the should not be affected by the pitch-changes applied to other AudioSources.

using UnityEngine;
using System.Collections;

public class BackgroundSoundController : MonoBehaviour {

	public AudioClip BackgroundMusic = null;
	public float BackgroundMusicVolume = 0.6f;
	/*
	private AudioSource DAudio = null;
	private AudioSource EAudio = null;
	private AudioSource FAudio = null;
	private AudioSource KAudio = null;
	private AudioSource LAudio = null;
	
	private AudioSource tunnelAudio = null;
	private AudioSource windAudio = null;
	*/
	public AudioSource backgroundAudio = null;
//	private float time;
	public Texture creator;
	public float showCreatorDelay;
	private Rect creatorPosition;
//	private bool WaitPlay;
	
	// Create the needed AudioSources
	void  Awake (){
		creatorPosition = new Rect(Screen.width - creator.width, Screen.height - creator.height, creator.width, creator.height);
		backgroundAudio = gameObject.AddComponent<AudioSource>();
		backgroundAudio.loop = true;
		backgroundAudio.playOnAwake = true;
		backgroundAudio.clip = BackgroundMusic;
	//	backgroundMusic.maxVolume = BackgroundMusicVolume;
	//	backgroundMusic.minVolume = BackgroundMusicVolume;
		backgroundAudio.volume = 0.0f;
		backgroundAudio.Play();
//		time = 0.0f;
//		print("Awake = "+WaitPlay);
	}
	
//	void  Update (){
//		time += Time.deltaTime;
//		if(time > 5.0f){
//			backgroundAudio.pitch = 1.5f;
//		}
//		/*if(WaitPlay){
//			backgroundAudio.volume = BackgroundMusicVolume;
//		}*/
//	}
	
	float Coserp ( float start ,   float end ,   float value  ){
		return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
	}
	
	float Sinerp ( float start ,   float end ,   float value  ){
	    return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
	}
	
	void  SetPitch ( float pitch  ){
			
	}
	
	void  SetVolume ( float pitch  ){
		
	}
	
	public void  PlayBackgroundMusic ( bool play ){
		
		if(!backgroundAudio)
		{
			/*if(play){
				
				WaitPlay = true;
			}*/
			return;
		}
		//WaitPlay = false;
		if(play)
		{
			//skidAudio.volume = Mathf.Clamp01(volumeFactor + 0.0f);
				backgroundAudio.volume = BackgroundMusicVolume;
		}
		else
		{
			backgroundAudio.volume = 0.0f;
		}
	}
	
	
	void OnGUI () {
		GUI.DrawTexture(creatorPosition, creator);
	}
	
	IEnumerator Start () {
		yield return new WaitForSeconds(showCreatorDelay);
		while (creatorPosition.x > -creator.width)
		{
			creatorPosition.x -= Time.deltaTime * 200;
			yield return null;
		}
		enabled = false;
	}
}
