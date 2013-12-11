using UnityEngine;
using System.Collections;

public class SpeedLineEffect : MonoBehaviour {
	public GameObject effect;
	void ShowEffect (bool show) {
		effect.SetActiveRecursively(show);
	}
}
