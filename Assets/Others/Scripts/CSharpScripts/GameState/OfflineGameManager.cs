using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class OfflineGameManager : MonoBehaviour {
	public Transform[] carPrefabs;
	// Use this for initialization
	void Start () {
		Transform playerCar = null;
		int totalplayer = 4;//PlayerPrefs.GetInt("PlayerNum");
		
		print("totalplayer = "+totalplayer);
		GameObject[] startPositionArray =  GameObject.FindGameObjectsWithTag("StartPosition");
		int startPosition = UnityEngine.Random.Range(1,startPositionArray.Length);
		print("first startPosition = "+startPosition);
		int pcplayercount = totalplayer - 1;
		int initcount = 0;
		foreach(GameObject position in startPositionArray ) {
			
			if(totalplayer < initcount){
				break;
			}
//			Debug.Log("start : " + (Convert.ToInt32(position.name) == startPosition));
			if( Convert.ToInt32(position.name) == startPosition) {
				foreach(Transform cp in carPrefabs) {
					
					if(cp.name.Equals( PlayerPrefs.GetString("WhichCar"))) {
						print("init car = "+PlayerPrefs.GetString("WhichCar"));
						playerCar = cp;
					}
				}
				//print("startPosition = "+startPosition);
				Transform tf = Instantiate(playerCar, position.transform.position, position.transform.rotation) as Transform;
				tf.GetComponent<CarAI>().enabled = false;
				tf.GetComponent<CarB>().Wait(false);
				tf.Find("SkillEffect").gameObject.SetActiveRecursively(false);
				tf.GetComponent<NetworkView>().enabled = false;
				tf.GetComponent<NetworkRigidbody>().enabled = false;
				tf.GetComponent<CarNetworkInit>().enabled = false;
				Camera.main.GetComponent<SmoothFollow>().target = tf;
				initcount++;
		//tf.GetComponent<CarProperty>().ownerGUPID = driver.GetLoginedGameUserPlayerID();
			}else {
				//int startPosition = UnityEngine.Random.Range(0,startPositionArray.Length - 1);
				if(pcplayercount <= 0){
					continue;
				}
				pcplayercount--;
				initcount++;
				int randomaicar = UnityEngine.Random.Range(0,carPrefabs.Length);
				//print("carPrefabs.Length = "+carPrefabs.Length);
				//print("randomaicar = "+randomaicar);
				if(randomaicar > (carPrefabs.Length - 1)){
					randomaicar = carPrefabs.Length - 1;
				}
				int count = 0;
				foreach(Transform cp in carPrefabs) {
					if(count == randomaicar){
						playerCar = cp;
						break;
					}
					count++;
				}
				Transform tf = Instantiate(playerCar, position.transform.position, position.transform.rotation) as Transform;
				tf.GetComponent<CarAI>().enabled = true;
				tf.Find("SkillEffect").gameObject.SetActiveRecursively(false);
				tf.Find("HUD").gameObject.SetActiveRecursively(false);
				tf.GetComponent<NetworkView>().enabled = false;
				tf.GetComponent<NetworkRigidbody>().enabled = false;
				tf.GetComponent<CarNetworkInit>().enabled = false;
			}
			
		}
		RankManager rankManager = FindObjectOfType(typeof(RankManager)) as RankManager;
		rankManager.SetRanksCar();
	}
}
