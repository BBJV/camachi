using UnityEngine;
using System.Collections;

public class ChangeNumber : MonoBehaviour {
	private int index = -1;
	public GameObject[] numbers;
	public AudioClip boomSound;
	public float boomVolume;
	public AudioClip[] humanSounds;
	public float[] humanVolumes;
	public float[] delayTimes;
//	public GUISkin skin;
	// Update is called once per frame
	
	void Update () {
		int temp = index;
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
////			guiText.text = Input.GetTouch(0).position.x.ToString();
////			guiText.text = guiText.fontSize.ToString();
			if(Input.GetTouch(0).position.x > Screen.width * 0.5f )
			{
//			if(Input.GetKeyDown(KeyCode.C))
//			{
				index++;
//				guiText.fontSize++;
			}
			else// if(Input.GetKeyDown(KeyCode.V))
			{
//				guiText.fontSize--;
				index--;
			}
		}
		index = Mathf.Clamp(index, 0, numbers.Length - 1);
		if(temp != index)
		{
			foreach(GameObject number in numbers)
			{
				number.SetActiveRecursively(false);
			}
			numbers[index].SetActiveRecursively(true);
			AudioSource.PlayClipAtPoint(boomSound, numbers[index].transform.position, boomVolume);
			StartCoroutine(DelaySound());
		}
//		guiText.text = index.ToString();
	}
	
	IEnumerator DelaySound () {
		yield return new WaitForSeconds(delayTimes[index]);
		AudioSource.PlayClipAtPoint(humanSounds[index], numbers[index].transform.position, humanVolumes[index]);
	}
	
//	void OnGUI () {
////		GUILayout.MaxWidth = Screen.width;
////		GUILayout.MaxHeight = Screen.height;
//		GUI.skin = skin;
//		GUILayout.Label(index.ToString(), GUILayout.MaxWidth(Screen.width), GUILayout.MaxHeight(Screen.height));
//	}
}
