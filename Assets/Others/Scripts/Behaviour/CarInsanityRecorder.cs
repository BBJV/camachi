using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarInsanityRecorder : MonoBehaviour {
	
	private Dictionary<int, string> recordDic = new Dictionary<int, string>();
	
	public void AddRecord(int GUPID, string timeRecord) {
		if(!recordDic.ContainsKey(GUPID)) {
			recordDic.Add(GUPID, timeRecord);
		}
	}
	
	public int GetRecordCount() {
		return this.recordDic.Count;
	}
	
	public Dictionary<int, string> GetRecordDic() {
		return this.recordDic;
	}
}
