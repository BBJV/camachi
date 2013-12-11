using UnityEngine;
using System.Collections;

public class Defense : Skill {
	public float time;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		Protect(car);
	}
	
	IEnumerator Protect (CarProperty car) {
		float _time = time;
		while(_time > 0)
		{
			//car hp recover x percent for 1 second
			yield return new WaitForSeconds(1);
			_time -= 1;
		}
	}
}
