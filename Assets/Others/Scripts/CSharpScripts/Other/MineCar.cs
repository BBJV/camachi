using UnityEngine;
using System.Collections;

public class MineCar : MonoBehaviour {
	public enum MineType {
		Once,
		Loop,
		PingPong
	}
	public MinePoint[] points;
	public MineType type;
	private int index = 0;
//	private Vector3 startPosition;
	private bool stop = false;
	
	[System.Serializable]
	public class MinePoint {
		public Transform point;
		public float speed = 10.0f;
		public bool stopAndRotate = true;
		public float rotateSpeed = 5.0f;
	}
	
//	void Start () {
//		startPosition = transform.position;
//	}
	
	void Update () {
		if(points.Length > 0)
		{
			Vector3 diff = points[index].point.position - transform.position;
			if(!stop)
			{
	        	transform.position = Vector3.MoveTowards (transform.position, points[index].point.position, Time.deltaTime * points[index].speed);
			}
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(points[index].point.position - transform.position), Time.deltaTime * points[index].rotateSpeed);
			Debug.Log(transform.rotation.eulerAngles + "  look : " + Quaternion.LookRotation(points[index].point.position - transform.position).eulerAngles);
			if(Quaternion.Angle(transform.rotation, Quaternion.LookRotation(points[index].point.position - transform.position)) < 0.5f)
			{
				stop = false;
				Debug.LogWarning("rotate finish");
			}
	        if (diff.magnitude < 0.1f)
	        {
				if(points[index].stopAndRotate)
				{
					stop = true;
				}
				else
				{
					stop = false;
				}
	            transform.position = points[index].point.position;
				index++;
				if(index >= points.Length)
				{
					if(type == MineType.Loop)
					{
						index = 0;
					}
					else if(type == MineType.PingPong)
					{
						System.Array.Reverse(points);
						index = 0;
					}
				}
	            index = Mathf.Clamp(index, 0, points.Length - 1);
	        }
		}
	}
}
