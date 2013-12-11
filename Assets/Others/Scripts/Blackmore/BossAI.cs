using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {
	public Transform[] Nodes;
	public Transform StartNode;
	public Transform HeadPoint;
	private int NowToNode;
	private Transform MyTransform;
	private string WallCollider = "wallcollider";
	// Use this for initialization
	void Start () {
		MyTransform = transform;
		for(int i = 0 ; i < Nodes.Length ; i++){
			if(StartNode.Equals(Nodes[i])){
				NowToNode = i;
				print("NowToNode = " + NowToNode);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(IsPassToNode())
			NowToNode++;
		if(NowToNode >= Nodes.Length){
			NowToNode = 0;
		}
		Steer();
		/*
		print("DetectLeftToWall() = " +DetectLeftToWall());
		print("DetectRightToWall() = " +DetectRightToWall());
		if(DetectLeftToWall() > DetectRightToWall()){
			BroadcastMessage("SetPressSteer",-1.0f);
			print("turn left = " + (DetectLeftToWall() - DetectRightToWall()));
		}
		else{
			BroadcastMessage("SetPressSteer",1.0f);
			print("turn right = " + (DetectLeftToWall() - DetectRightToWall()));
		}
		*/
	}
	
	
	void Steer(){
		Vector3 hopedir = Nodes[NowToNode].position - MyTransform.position;
		hopedir = MyTransform.InverseTransformDirection(hopedir);
		hopedir.Set(hopedir.x , 0.0f, hopedir.z);
		float angle = Vector3.Angle(Vector3.forward , hopedir);
		float dir = Vector3.Cross(Vector3.forward , hopedir).y;
		print("angle = "+angle);
		if(angle > 1.0f){
			if(dir < 0.0f){
				//turn left
				BroadcastMessage("SetPressSteer",-1.0f);
			}else{
				//turn right
				BroadcastMessage("SetPressSteer", 1.0f);
			}
		}
		
		//print("Vector3.Cross(Vector3.forward , hopedir); = "+Vector3.Cross(Vector3.forward , hopedir));
	}
	bool IsPassToNode(){
		int beforenode = NowToNode - 1;
		if(beforenode < 0){
			beforenode = Nodes.Length - 1;
		}
		if(Vector3.SqrMagnitude(MyTransform.position - Nodes[beforenode].position) >= 
			Vector3.SqrMagnitude(Nodes[NowToNode].position - Nodes[beforenode].position)){
			return true;
		}
		return false;
	}
	float DetectLeftToWall(){
		RaycastHit hit;
		if(Physics.Raycast(HeadPoint.position, -HeadPoint.right, out hit)){
			if(hit.collider.transform.name.Equals(WallCollider)){
				return Vector3.SqrMagnitude(HeadPoint.position - hit.point);
			}
		}
		return -1.0f;
	}
	float DetectRightToWall(){
		RaycastHit hit;
		if(Physics.Raycast(HeadPoint.position, HeadPoint.right, out hit)){
			if(hit.collider.transform.name.Equals(WallCollider)){
				return Vector3.SqrMagnitude(HeadPoint.position - hit.point);
			}
		}
		return -1.0f;
	}
}
