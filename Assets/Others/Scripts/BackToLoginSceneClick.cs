using UnityEngine;
using System.Collections;

public class BackToLoginSceneClick : MonoBehaviour {

	public LoginSceneViewer loginSceneViewer;
	public GameObject panel;
	public GameObject gear;
	public GameObject photoBoard;
	
	IEnumerator OnClick() {
		Network.Disconnect();
		if(panel)
		{
			panel.animation.Play("stuffBoard_2");
		}
		if(gear)
		{
			gear.animation.Play();
		}
		if(audio)
		{
			audio.Play();
		}
		if(photoBoard)
		{
			photoBoard.animation[photoBoard.animation.clip.name].speed = -1;
			photoBoard.animation[photoBoard.animation.clip.name].normalizedTime = 1;
			photoBoard.animation.Play();
		}
		while(panel && panel.animation.isPlaying)
		{
			yield return null;
		}
		if(photoBoard)
		{
			photoBoard.animation[photoBoard.animation.clip.name].speed = 1;
			photoBoard.animation[photoBoard.animation.clip.name].normalizedTime = 0;
		}
		loginSceneViewer.InitialSceneSetting();
	
	}
}
