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

public class SoundController : MonoBehaviour {
private CarB car;

/*
public AudioClip D = null;
public float DVolume = 1.0f;
public AudioClip E = null;
public float EVolume = 1.0f;
public AudioClip F = null;
public float FVolume = 1.0f;
public AudioClip K = null;
public float KVolume = 1.0f;
public AudioClip L = null;
public float LVolume = 1.0f;

public AudioClip wind = null;
public float windVolume = 1.0f;
public AudioClip tunnelSound = null;
public float tunnelVolume = 1.0f;

public AudioClip crashLowSpeedSound = null;
public float crashLowVolume = 1.0f;
public AudioClip crashHighSpeedSound = null;
public float crashHighVolume = 1.0f;
*/
public AudioClip engineSound = null;
public float engineVolume;
	
public AudioClip skidSound = null;
public float SkdAudioVolume = 0.1f;
public float SkidPitch = 0.1f;

public AudioClip EngineStartSound = null;
public float EngineStartAudioVolume = 0.3f;
	public float EngineStartPitch = 1.0f;

	

//public AudioClip BackgroundMusic = null;
//public float BackgroundMusicVolume = 0.6f;
/*
private AudioSource DAudio = null;
private AudioSource EAudio = null;
private AudioSource FAudio = null;
private AudioSource KAudio = null;
private AudioSource LAudio = null;

private AudioSource tunnelAudio = null;
private AudioSource windAudio = null;
*/
private AudioSource engineAudio = null;
private AudioSource skidAudio = null;
private AudioSource EngineStartAudio = null;
private AudioSource carAudio = null;

//private AudioSource backgroundMusic = null;
public AudioClip ReverseSound = null;
public float ReverseAudioVolume = 0.3f;
private AudioSource ReverseAudio = null;
public float ReversePitch = 0.1f;
	
public AudioClip CollisionSound = null;
public float CollisionAudioVolume = 0.3f;
private AudioSource CollisionAudio = null;
public float CollisionPitch = 0.1f;
	
public AudioClip JumpCollisionSound = null;
public float JumpCollisionAudioVolume = 0.3f;
private AudioSource JumpCollisionAudio = null;
public float JumpCollisionPitch = 0.1f;
	
private float gearShiftTime = 0.1f;
private bool  shiftingGear = false;
private int gearShiftsStarted = 0;
private int crashesStarted = 0;
private float crashTime = 0.2f;
private int oneShotLimit = 8;
/*
private float idleFadeStartSpeed = 3.0f;
private float idleFadeStopSpeed = 10.0f;
private float idleFadeSpeedDiff = 7.0f;
private float speedFadeStartSpeed = 0.0f;
private float speedFadeStopSpeed = 8.0f;
private float speedFadeSpeedDiff = 8.0f;
*/
private int gear = 0;


/*
public float idlePitch = 0.7f;
public float startPitch = 0.85f;
public float lowPitch = 1.17f;
public float medPitch = 1.25f;
public float highPitchFirst = 1.65f;
public float highPitchSecond = 1.76f;
public float highPitchThird = 1.80f;
public float highPitchFourth = 1.86f;
*/
public float[] gearStartPitch;
public float[] gearEndPitch;
	
//private float shiftPitch = 1.44f;

private float prevPitchFactor = 0;
private bool IsEngineStart = false;
	
public float DriftEngineSoundVolumne;

// Create the needed AudioSources
void  Awake (){
	car = transform.GetComponent<CarB>();
	engineAudio = gameObject.AddComponent<AudioSource>();
	engineAudio.loop = true;
	engineAudio.playOnAwake = true;
	engineAudio.clip = engineSound;
	engineAudio.volume = 0.0f;
		
		
	ReverseAudio = gameObject.AddComponent<AudioSource>();
	ReverseAudio.loop = true;
	ReverseAudio.playOnAwake = false;
	ReverseAudio.clip = ReverseSound;
	ReverseAudio.volume = ReverseAudioVolume;
	ReverseAudio.pitch = ReversePitch;
		
	CollisionAudio = gameObject.AddComponent<AudioSource>();
	CollisionAudio.loop = false;
	CollisionAudio.playOnAwake = false;
	CollisionAudio.clip = CollisionSound;
	CollisionAudio.volume = CollisionAudioVolume;
	CollisionAudio.pitch = CollisionPitch;
		
	JumpCollisionAudio = gameObject.AddComponent<AudioSource>();
	JumpCollisionAudio.loop = false;
	JumpCollisionAudio.playOnAwake = false;
	JumpCollisionAudio.clip = JumpCollisionSound;
	JumpCollisionAudio.volume = JumpCollisionAudioVolume;
	JumpCollisionAudio.pitch = JumpCollisionPitch;
	
	skidAudio = gameObject.AddComponent<AudioSource>();
	skidAudio.loop = false;
	skidAudio.playOnAwake = false;
	skidAudio.clip = skidSound;
	skidAudio.volume = SkdAudioVolume;
	skidAudio.pitch = SkidPitch;
		
	EngineStartAudio = gameObject.AddComponent<AudioSource>();
	EngineStartAudio.loop = false;
	EngineStartAudio.playOnAwake = false;
	EngineStartAudio.clip = EngineStartSound;
	EngineStartAudio.volume = EngineStartAudioVolume;
	EngineStartAudio.pitch = EngineStartPitch;
	
	carAudio = gameObject.AddComponent<AudioSource>();
	carAudio.loop = false;
	carAudio.playOnAwake = false;
	carAudio.Stop();
	
	/*
	crashTime = Mathf.Max(crashLowSpeedSound.length, crashHighSpeedSound.length);
	soundsSet = false;
	
	idleFadeSpeedDiff = idleFadeStopSpeed - idleFadeStartSpeed;
	speedFadeSpeedDiff = speedFadeStopSpeed - speedFadeStartSpeed;
	*/
}
private float volume = 0.0f;
void  Update (){
	if(IsEngineStart){
		if(EngineStartAudio.isPlaying){
			return;
		}
		else if(!engineAudio.isPlaying)
		{
			engineAudio.Play();
		}
	}else{
		return;
	}
	engineAudio.volume = engineVolume;
//	float carSpeed = car.GetCarSpeed();//.rigidbody.velocity.magnitude;
//	float carSpeedFactor = Mathf.Clamp01(carSpeed / car.GetTopSpeed());
	
	//KAudio.volume = Mathf.Lerp(0, KVolume, carSpeedFactor);
	//windAudio.volume = Mathf.Lerp(0, windVolume, carSpeedFactor * 2);
	
	if(shiftingGear)
		return;
	
	//float pitchFactor = Sinerp(0, topGear, carSpeedFactor);
 	//int newGear = (int)pitchFactor;
	float pitchFactor = car.GetEngineLinearFactor();
	//int newGear = (int)pitchFactor;
		//print("pitchFactor = " +pitchFactor);
	int newGear = car.GetGear();
	
	pitchFactor -= (int)pitchFactor;
	float throttleFactor = pitchFactor;
	pitchFactor *= 0.3f;
	pitchFactor += throttleFactor * (0.7f) * Mathf.Clamp01(car.GetThrottle() * 2.0f);
	if(newGear != gear && !car.IsCarDrift())
	{
		if(newGear > gear)
		{
			
			GearShift(prevPitchFactor, pitchFactor, gear, true);
		}
		else
			GearShift(prevPitchFactor, pitchFactor, gear, false);
			
		gear = newGear;
	}
	else
	{
		float newPitch = 0;
			/*
		if(gear == 0)
		{
			//pitchFactor += idlePitch;
			newPitch = Mathf.Lerp(idlePitch, highPitchFirst, pitchFactor);
		}
		else if(gear == 1)
		{
				//pitchFactor += startPitch;
			newPitch = Mathf.Lerp(startPitch, highPitchSecond, pitchFactor);
		}	
		else if(gear == 2)
		{
				//pitchFactor += lowPitch;
			newPitch = Mathf.Lerp(lowPitch, highPitchThird, pitchFactor);
		}	
		else
		{
				//pitchFactor += medPitch;
			newPitch = Mathf.Lerp(medPitch, highPitchFourth, pitchFactor);
		}
		*/
			if(gear == gearStartPitch.Length - 1)
			{
				newPitch = Mathf.Lerp(engineAudio.pitch, gearEndPitch[gear], pitchFactor);
			}
			else
			{
		newPitch = Mathf.Lerp(gearStartPitch[gear], gearEndPitch[gear], pitchFactor);
			}
//			Debug.Log(newPitch);
			//print("pitchFactor = "+pitchFactor);
		//print("newPitch = "+newPitch);
		volume = newPitch;
		if(car.IsCarDrift()){
			//newPitch = gearEndPitch[1];
			volume = (DriftEngineSoundVolumne);
		}
			/*else{
				volume = newPitch;
		}
		*/
			volume *= engineVolume;
			
		SetPitch(newPitch);
		SetVolume(volume);
	}
	prevPitchFactor = pitchFactor;
}

float Coserp ( float start ,   float end ,   float value  ){
	return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));
}

float Sinerp ( float start ,   float end ,   float value  ){
    return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
}

void  SetPitch ( float pitch  ){
//		Debug.Log("set:" + pitch);
		engineAudio.pitch = pitch;
//		Debug.Log(engineAudio.pitch);
	/*DAudio.pitch = pitch;
	EAudio.pitch = pitch;
	FAudio.pitch = pitch;
	LAudio.pitch = pitch;
	tunnelAudio.pitch = pitch;
	*/
}

void  SetVolume ( float volume  ){
		engineAudio.volume = volume;
	/*float pitchFactor = Mathf.Lerp(0, 1, (pitch - startPitch) / (highPitchSecond - startPitch));
	DAudio.volume = Mathf.Lerp(0, DVolume, pitchFactor);
	float fVolume = Mathf.Lerp(FVolume * 0.80f, FVolume, pitchFactor);
	FAudio.volume = fVolume * 0.7f + fVolume * 0.3f * Mathf.Clamp01(car.GetThrottle());
	float eVolume = Mathf.Lerp(EVolume * 0.89f, EVolume, pitchFactor);
	EAudio.volume = eVolume * 0.8f + eVolume * 0.2f * Mathf.Clamp01(car.GetThrottle());
*/
}

IEnumerator  GearShift ( float oldPitchFactor ,   float newPitchFactor ,   int gear ,   bool shiftUp  ){
	 shiftingGear = true;
	
	float timer = 0;
	float pitchFactor	= 0;
	float newPitch = 0;
	
	if(shiftUp)
	{
		while(timer < gearShiftTime)
		{
			pitchFactor = Mathf.Lerp(oldPitchFactor, 0, timer / gearShiftTime);
				/*
			if(gear == 0)
				newPitch = Mathf.Lerp(lowPitch, highPitchFirst, pitchFactor);
			else
				newPitch = Mathf.Lerp(lowPitch, highPitchSecond, pitchFactor);
				*/
			newPitch = Mathf.Lerp(gearStartPitch[gear], gearEndPitch[gear], pitchFactor);
			SetPitch(newPitch);
			SetVolume(newPitch * engineVolume);
			timer += Time.deltaTime;
			yield return null;
		}
	}
	else
	{
		while(timer < gearShiftTime)
		{
			pitchFactor = Mathf.Lerp(0, 1, timer / gearShiftTime);
			//newPitch = Mathf.Lerp(lowPitch, shiftPitch, pitchFactor);
			newPitch = Mathf.Lerp(gearStartPitch[gear], gearEndPitch[gear], pitchFactor);
			SetPitch(newPitch);
			SetVolume(newPitch * engineVolume);
			timer += Time.deltaTime;
			yield return null;
		}
	}
		
	shiftingGear = false;
}

public void  Skid ( bool play ,    float volumeFactor  ){
	if(!skidAudio)
		return;
	if(play)
	{
			if(skidAudio.isPlaying){
				return;
			}
		//skidAudio.volume = Mathf.Clamp01(volumeFactor + 0.0f);
			skidAudio.volume = SkdAudioVolume + volumeFactor;
			skidAudio.Play();
	}
	else
		skidAudio.volume = 0.0f;
}


public void  EngineStart ( bool play){
	if(!EngineStartAudio)
		return;
	if(play)
	{
		//skidAudio.volume = Mathf.Clamp01(volumeFactor + 0.0f);
			EngineStartAudio.volume = EngineStartAudioVolume;
			EngineStartAudio.Play();
			IsEngineStart = true;
	}
	else
		EngineStartAudio.volume = 0.0f;
}

public void Reverse ( bool play){
	if(!ReverseAudio)
		return;
		
		if(play)
		{
			if(ReverseAudio.isPlaying){
				return;
			}
			ReverseAudio.volume = ReverseAudioVolume;
			ReverseAudio.Play();
		}
		else
			ReverseAudio.Stop();
			//ReverseAudio.volume = 0.0f;
		
}
	
public void PlayCollisionSound ( bool play){
	if(!CollisionAudio)
		return;
	
	if(play)
	{
		if(CollisionAudio.isPlaying){
			return;
		}
		CollisionAudio.volume = CollisionAudioVolume;
		CollisionAudio.Play();
	}
	else
		CollisionAudio.Stop();
		//ReverseAudio.volume = 0.0f;
		
}
	
public void PlayJumpCollisionSound ( bool play){
	if(!JumpCollisionAudio)
		return;
	
	if(play)
	{
		if(JumpCollisionAudio.isPlaying){
			return;
		}
		JumpCollisionAudio.volume = JumpCollisionAudioVolume;
		JumpCollisionAudio.Play();
	}
	else
		JumpCollisionAudio.Stop();
		//ReverseAudio.volume = 0.0f;
		
}

// Checks if the max amount of crash sounds has been started, and
// if the max amount of total one shot sounds has been started.
public IEnumerator  Crash ( float volumeFactor  ){
	if(crashesStarted > 3 || OneShotLimitReached())
		yield break;
	if(volumeFactor > 0.9f)
		//carAudio.PlayOneShot(crashHighSpeedSound, Mathf.Clamp01((0.5f + volumeFactor * 0.5f) * crashHighVolume));
	//carAudio.PlayOneShot(crashLowSpeedSound, Mathf.Clamp01(volumeFactor * crashLowVolume));
	crashesStarted++;
	
	yield return new WaitForSeconds(crashTime);
	
	crashesStarted--;
}
public void  CrashInto (float volumeFactor){
		
		//carAudio.PlayOneShot(crashHighSpeedSound, volumeFactor);
		//print("TestCrash");
}

// A function for testing if the maximum amount of OneShot AudioClips
// has been started.
bool  OneShotLimitReached (){
	return (crashesStarted + gearShiftsStarted) > oneShotLimit;
}

//void  OnTriggerEnter ( Collider coll  ){
//	SoundToggler st = coll.transform.GetComponent<SoundToggler>();
//	if(st)
//		ControlSound(true, st.fadeTime);
//}
//
//void  OnTriggerExit ( Collider coll  ){
//	SoundToggler st = coll.transform.GetComponent<SoundToggler>();
//	if(st)
//		ControlSound(false, st.fadeTime);
//}

//public IEnumerator  ControlSound ( bool play ,    float fadeTime  ){
//	float timer = 0;
//	/*if(play && !tunnelAudio.isPlaying)
//	{
//		tunnelAudio.volume = 0;
//		tunnelAudio.Play();
//		while(timer < fadeTime)
//		{
//			tunnelAudio.volume = Mathf.Lerp(0, tunnelVolume, timer / fadeTime);
//			timer += Time.deltaTime;
//			yield return 0;
//		}
//	}
//	else
//	 if(!play && tunnelAudio.isPlaying)
//	{
//		while(timer < fadeTime)
//		{
//			tunnelAudio.volume = Mathf.Lerp(0, tunnelVolume, timer / fadeTime);
//			timer += Time.deltaTime;
//			yield return 0;
//		}
//		tunnelAudio.Stop();
//	}
//	*/
//		yield return 0;
//}
}
