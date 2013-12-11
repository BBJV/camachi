using UnityEngine;
using System.Collections;

public class RoundBat : MonoBehaviour {
	public AnimationClip clip;
	public void Round () {
		animation.CrossFade(clip.name);
	}
}
