using UnityEngine;
using System.Collections;

public class ReduceEnergy : Skill {
	public float time;
	public float percent;
	public float radius;
	public override void Use (CarProperty car, int level) {
		base.Use(car, level);
		StartCoroutine(ReduceEnergySkill(car));
	}
	
	IEnumerator ReduceEnergySkill (CarProperty car) {
//		Debug.Log("Use");
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
						dc.ApplyDamageEnergy(percent);
				}
			}
			yield return new WaitForSeconds(1);
			_time -= 1;
		}
	}
}
