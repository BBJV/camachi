using UnityEngine;
using System.Collections;

public class SummerCar : Skill {
	public GameObject[] summers;
	
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		Summer(car);
	}
	
	void Summer (CarProperty car) {
		foreach(GameObject summer in summers)
		{
			summer.SetActiveRecursively(true);
		}
	}
}
