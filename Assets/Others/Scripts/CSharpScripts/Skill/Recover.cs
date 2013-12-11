using UnityEngine;
using System.Collections;

public class Recover : Skill {
	public float time;
	public float percent;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		RecoverHp(car);
	}
	
	IEnumerator RecoverHp (CarProperty car) {
		float _time = time;
		while(_time > 0)
		{
			//car hp recover x percent for 1 second
//			car.RecoverHp(percent);
			yield return new WaitForSeconds(1);
			_time -= 1;
		}
	}
}
