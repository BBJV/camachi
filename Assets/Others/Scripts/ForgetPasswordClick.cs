using UnityEngine;
using System.Collections;

public class ForgetPasswordClick : MonoBehaviour {

	public LoginSceneViewer loginSceneViewer;
	public GameObject loginPanel;
	public GameObject loginGear;
	public GameObject loginSetting;
		
	IEnumerator OnClick() {
		loginPanel.animation.Play("loginBoard_2");
		loginGear.animation.Play();
		loginSetting.animation[loginSetting.animation.clip.name].speed = -1;
		loginSetting.animation[loginSetting.animation.clip.name].time = loginSetting.animation[loginSetting.animation.clip.name].length;
		loginSetting.animation.Play();
		audio.Play();
		while(loginPanel.animation.isPlaying || loginGear.animation.isPlaying || loginSetting.animation.isPlaying)
		{
			yield return null;
		}
		loginSetting.animation[loginSetting.animation.clip.name].speed = 1;
		loginSetting.animation[loginSetting.animation.clip.name].time = 0;
		
		loginSceneViewer.ForgetPasswordSceneSetting();
	
	}
}
