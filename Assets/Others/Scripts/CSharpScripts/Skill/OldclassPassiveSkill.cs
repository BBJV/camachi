using UnityEngine;
using System.Collections;

public class OldclassPassiveSkill : MonoBehaviour {

	// Use this for initialization
	public void PassiveSkillInit () {
		SpeedUp[] speedUps = GameObject.FindObjectsOfType(typeof(SpeedUp)) as SpeedUp[];
		foreach(SpeedUp speedUp in speedUps)
		{
			speedUp.ScaleUp();
		}
		SkillBox[] skillBoxs = GameObject.FindObjectsOfType(typeof(SkillBox)) as SkillBox[];
		foreach(SkillBox skillBox in skillBoxs)
		{
			skillBox.ScaleUp();
		}
	}
}
