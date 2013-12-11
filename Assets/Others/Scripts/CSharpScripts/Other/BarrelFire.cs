using UnityEngine;
using System.Collections;

public class BarrelFire : MonoBehaviour {
	private ParticleEmitter[] particles;
	void Start () {
		particles = GetComponentsInChildren<ParticleEmitter>();
	}
	
	public IEnumerator FireTime (float time) {
		yield return new WaitForSeconds(time);
		bool stillFire = true;
		
		while(stillFire)
		{
			bool canDestroy = true;
			foreach(ParticleEmitter emit in particles)
			{
				emit.maxEmission = Mathf.Clamp(emit.maxEmission - Time.deltaTime, 0, emit.maxEmission);
				if(emit.maxEmission > 0)
				{
					canDestroy = false;
				}
			}
			
			if(canDestroy)
			{
				stillFire = false;
			}
			yield return null;
		}
		Destroy(gameObject);
	}
}
