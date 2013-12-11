using UnityEngine;
using System.Collections;

public class CollisionParticle : MonoBehaviour {
	public GameObject particle;
	public float particleTime;
	
//	void OnCollisionEnter(Collision collision) {
//		if(collision.transform.tag != "Road")
//		{
//			foreach(ContactPoint contact in collision.contacts)
//			{
//				StartCoroutine(ParticleLive(contact.point));
//			}
////			Debug.LogError("hit : " + collision.transform.name + " position : " + collision.transform.position + " parent : " + collision.transform.parent);
//		}
//    }
//	
//	IEnumerator ParticleLive (Vector3 point) {
//		GameObject p = Instantiate(particle, point, Quaternion.identity) as GameObject;
////		p.transform.parent = this.transform;
//		yield return new WaitForSeconds(particleTime);
//		Destroy(p);
//	}
}
