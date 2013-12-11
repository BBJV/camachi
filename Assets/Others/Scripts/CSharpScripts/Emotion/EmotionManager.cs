using UnityEngine;
using System.Collections;

public class EmotionManager : MonoBehaviour {

	[System.Serializable]
	public class EmotionSystem {
		public string name;
		public EmotionElement[] elements;
		public AnimationClip animation;
	}
	
	[System.Serializable]
	public class EmotionElement {
		public Texture texture;
		public Mesh mesh;
		public MeshRenderer targetTexture;
		public MeshFilter targetMesh;
	}
	
	public EmotionSystem[] emotions;
	
	public void PlayEmotion (string emotionName) {
		foreach(EmotionSystem emotion in emotions)
		{
			if(emotion.name == emotionName)
			{
				StartCoroutine(Play(emotion));
				return;
			}
		}
	}
	
	private IEnumerator Play (EmotionSystem emotion) {
		foreach(EmotionElement element in emotion.elements)
		{
			if(element.texture)
			{
				element.targetTexture.material.mainTexture = element.texture;
			}
			if(element.mesh)
			{
				element.targetMesh.mesh = element.mesh;
			}
		}
		
		if(emotion.animation)
		{
			animation.CrossFade(emotion.animation.name);
		}
		yield return null;
	}
}
