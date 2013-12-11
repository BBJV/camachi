using UnityEngine;
using System.Collections;

public class LoadingBackground : MonoBehaviour {
	[System.Serializable]
	public class PlayerInfo {
		public UISprite sprite;
		public UILabel label;
	}
	
	private PlayGameController controller;
	public UIAtlas BGAtlas;
	public UISprite sprite;
	public PlayerInfo[] playerInfos;
	// Use this for initialization
	void Start () {
		
		controller = FindObjectOfType(typeof(PlayGameController)) as PlayGameController;
		if(sprite.atlas.GetSprite(((Definition.eSceneID)controller.matchRoom.matchMap).ToString()) == null)
		{
			sprite.atlas = BGAtlas;
		}
		sprite.spriteName = ((Definition.eSceneID)controller.matchRoom.matchMap).ToString();
		
		int index = 0;
		foreach(GameUserPlayer player in controller.matchRoom.room.playersInRoom) {
			playerInfos[index].label.text = player.playerName;
			playerInfos[index].sprite.spriteName = ((Definition.eCarID)player.GetComponent<CarInsanityPlayer>().selectedCar.carID).ToString();
			index += 1;
		}
	}
}
