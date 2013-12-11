using UnityEngine;
using System.Collections;

public class OfflineDataTransmitter : MonoBehaviour {

	static public bool HasKey (string key) {
		return PlayerPrefs.HasKey(key);
	}
	
	public string GetString (string key) {
		return PlayerPrefs.GetString(key);
	}
	
	public int GetInt (string key) {
		return PlayerPrefs.GetInt(key);
	}
	
	public float GetFloat (string key) {
		return PlayerPrefs.GetFloat(key);
	}
	
	public void DeleteKey (string key) {
		PlayerPrefs.DeleteKey(key);
	}
	
	public void SetValue (string key,string value) {
		PlayerPrefs.SetString(key, value);
	}
	
	public void SetValue (string key,float value) {
		PlayerPrefs.SetFloat(key, value);
	}
	
	public void SetValue (string key,int value) {
		PlayerPrefs.SetInt(key, value);
	}
}
