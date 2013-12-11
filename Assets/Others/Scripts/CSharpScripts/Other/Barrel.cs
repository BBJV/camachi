using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {
	public BarrelFire fireParticle;
	private bool isBoom = false;
	void OnCollisionEnter(Collision collision) {
        if(collision.impactForceSum.magnitude > 30)
		{
			StartCoroutine(Boom());
		}
    }
	
	IEnumerator Boom () {
		if(isBoom)
		{
			yield break;
		}
		isBoom = true;
		yield return new WaitForSeconds(1.0f);
		BarrelFire fireObject = Instantiate(fireParticle, transform.position, Quaternion.identity) as BarrelFire;
		fireObject.StartCoroutine(fireObject.FireTime(3.0f));
		Destroy(gameObject);
	}
}
