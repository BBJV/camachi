using UnityEngine;
using System.Collections;

public class WhirlObstacle : MonoBehaviour {
	[System.Serializable]
	public class Model {
		public GameObject modelObject;
		public Vector3 position;
	}
	public CarProperty selfCar;
	public float effectPercent;
	public float liveTime;
	public AudioClip roadBlockAudio;
	protected float activeTime;
	public Model[] models;
	
	IEnumerator Start () {
		int random = Random.Range(0, models.Length);
		GameObject child = Instantiate(models[random].modelObject, transform.position, models[random].modelObject.transform.rotation) as GameObject;
		child.transform.parent = transform;
		child.transform.localPosition = models[random].position;
		activeTime = Time.time;
		yield return new WaitForSeconds(liveTime);
		if(gameObject)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider other) {
		CarProperty collisionCar = other.transform.root.GetComponent<CarProperty>();
		if(collisionCar == selfCar && Time.time - activeTime <= 3.0f)
		{
			return;
		}
		if(collisionCar && !other.isTrigger)
		{
//			collisionCar.StartCoroutine(collisionCar.SetSlip(1.0f,collisionCar.rigidbody.velocity.magnitude * 0.5f));
			Hashtable args = new Hashtable();
			args.Add("second", 1.0f);
			args.Add("speed", collisionCar.rigidbody.velocity.magnitude * 0.5f);
			collisionCar.SendMessage("SetSlip", args, SendMessageOptions.DontRequireReceiver);
			collisionCar.HitBySkill(1.0f);
			if(roadBlockAudio)
				AudioSource.PlayClipAtPoint(roadBlockAudio, collisionCar.transform.position);
			Destroy(gameObject);
		}
    }
}
