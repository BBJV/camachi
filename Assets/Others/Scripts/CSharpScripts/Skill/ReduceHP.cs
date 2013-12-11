using UnityEngine;
using System.Collections;

public class ReduceHP : Skill {
	public float time;
	public float percent;
	public float radius;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(ReduceHPSkill(car));
	}
	
	IEnumerator ReduceHPSkill (CarProperty car) {
		float _time = time;
		while(_time > 0)
		{
			Collider[] targets = Physics.OverlapSphere(transform.position, radius);
			Transform targetTransform = null;
			foreach(Collider target in targets)
			{
				if(target.transform.root.tag == "car" && (target.transform.root != targetTransform) && 
				   (target.transform.root != transform) && 
				   (Vector3.Dot(transform.TransformDirection(Vector3.right),target.transform.root.position - transform.position) > 0))
				{
					targetTransform = target.transform.root;
//					Debug.Log(target.transform.root.name + " " + target.name);
					DamageController dc = target.transform.root.GetComponent<DamageController>();
					if(dc)
						dc.ApplyDamageHp(percent);
				}
			}
			yield return new WaitForSeconds(1);
			_time -= 1;
		}
	}
}
