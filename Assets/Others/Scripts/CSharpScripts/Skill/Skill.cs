using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CarProperty))]
public class Skill : MonoBehaviour {
	public Texture2D SkillIcon ;
	protected bool skillUsing = false;
	public int skillLevel = 1;
	public bool skillLock = false;
	public int[] probability;
	
	[System.Serializable]
	public class SkillSettingTime {
		public float skillEffectTimes;
	}
	
	[System.Serializable]
	public class SkillSettingPercent : SkillSettingTime{
		public float effectPercent;
	}
	
	[System.Serializable]
	public class SkillSettingLiveTime : SkillSettingPercent {
		public float effectLiveTime;
	}
	
	[System.Serializable]
	public class SkillSettingRadius : SkillSettingTime {
		public float radius;
	}
	
	public virtual void Use (CarProperty car, int level) {
//		car.UseSkill();
		skillLevel = level;
	}
	
	public bool IsSkillUsing () {
		return skillUsing;
	}
	
	void Start () {
		skillLevel = PlayerPrefs.GetInt(this.GetType().Name, skillLevel);
	}
	
	void SetAIRandomLevel () {
		skillLevel = Random.Range(1, 6);
	}
	
	public int Probability (int rank) {
		return probability[rank];
	}
}
