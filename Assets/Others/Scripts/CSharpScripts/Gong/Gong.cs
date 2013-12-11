using UnityEngine;
using System.Collections;

public class Gong : MonoBehaviour {
	public Ray ray ;
	public RaycastHit hit;
	public Transform hitTransform;
	public Transform gongTransform;
	public Transform stickTransform;
	
	public AudioClip hitSound = null;
	public float hitAudioVolume = 0.3f;
	private AudioSource hitAudio = null;
	// Use this for initialization
	void Start () {
		hitAudio = gameObject.AddComponent<AudioSource>();
		hitAudio.loop = false;
		hitAudio.playOnAwake = false;
		hitAudio.clip = hitSound;
		hitAudio.volume = hitAudioVolume;
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			foreach (Touch touch in Input.touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					ray = Camera.main.ScreenPointToRay(touch.position);  
					if (Physics.Raycast(ray, out hit,Mathf.Infinity))    
					{
						gongTransform.animation.Play();
						stickTransform.animation.Play();
						hitAudio.Play();
						break;
						//Application.LoadLevel("SelectGameMode");
					}
				}
			}
		}else{
			if(Input.GetMouseButtonDown(0)){
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
				if (Physics.Raycast(ray, out hit,Mathf.Infinity))    
				{
					gongTransform.animation.Play();
					stickTransform.animation.Play();
					hitAudio.Play();
					//Application.LoadLevel("SelectGameMode");
				}else{
					return;
				}
			}else{
				return;
			}
		}
	}
}
