using UnityEngine;
using System.Collections;

public class FollowNormal : MonoBehaviour {
	public Transform Follower;
//	private static string RoadColliderName = "roadcollider";
	// Use this for initialization
	void Start () {
		//transform.position = Follower.position;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = Follower.position;
		RaycastHit hit;
		if(Physics.Raycast(Follower.position, -Vector3.up, out hit, Mathf.Infinity,1<<8))
		{
			transform.renderer.enabled = true;
				Quaternion targetRotation = Quaternion.FromToRotation (Follower.up, hit.normal) * Follower.rotation;
				transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.fixedDeltaTime * 25);
				transform.position = hit.point + hit.normal.normalized * 0.5f;
			
			
		}else{
			transform.renderer.enabled = false;
		}
	}
}
