using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	
	static BackgroundMusic mInst;
	public int liveScene;
	public Texture creator;
	public float showCreatorDelay;
	private Rect creatorPosition;
	// Use this for initialization
	void Awake () {
		if (mInst == null) 
		{ 
			mInst = this;
		}
		creatorPosition = new Rect(Screen.width - creator.width, Screen.height - creator.height, creator.width, creator.height);
	}
	
	void OnDestroy () { 
		if (mInst == this) 
			mInst = null; 
	}
	
	void OnLevelWasLoaded (int level)
	{
		if(GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
		{
			if(mInst != this)
			{
				if(mInst != null && mInst.liveScene == level)
				{
					Destroy(gameObject);
				}
				else if(liveScene == level && mInst != null)
				{
					Destroy(mInst.gameObject);
					mInst = this;
				}
				else
				{
					Destroy(gameObject);
				}
			}
		}
	}
	
	void OnGUI () {
		GUI.depth = -5;
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
