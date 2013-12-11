using UnityEngine;
using System.Collections;

public class Rainy : Skill {
	public SkillSettingPercent[] skillSetting;
	private CarProperty[] cars;
	
	void Start () {
		cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
	}
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(Rain());
	}
	
	IEnumerator Rain () {
		skillUsing = true;
		try
		{
			foreach(CarProperty car in cars)
			{
				if(car.transform.root != transform.root)
				{
					car.AddSkillTime();
					car.ChangeGrip(skillSetting[skillLevel - 1].effectPercent);
				}
			}
		}
		catch
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
		yield return new WaitForSeconds(skillSetting[skillLevel - 1].skillEffectTimes);
		try
		{
			foreach(CarProperty car in cars)
			{
				if(car.transform.root != transform.root)
				{
					car.ChangeGrip(0);
				}
			}
		}
		catch
		{
			cars = FindObjectsOfType(typeof(CarProperty)) as CarProperty[];
		}
		skillUsing = true;
	}
}
