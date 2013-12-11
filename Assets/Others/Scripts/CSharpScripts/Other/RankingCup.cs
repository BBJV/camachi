using UnityEngine;
using System.Collections;

public class RankingCup : MonoBehaviour {
	public GameObject cupBG;
	public GameObject floral;
	public GameObject[] cups;
	
	void ShowCup (int num) {
		cupBG.SetActiveRecursively(true);
		floral.SetActiveRecursively(true);
		cups[num - 1].SetActiveRecursively(true);
	}
}
