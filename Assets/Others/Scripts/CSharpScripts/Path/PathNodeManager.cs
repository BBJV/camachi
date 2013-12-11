using UnityEngine;
using System.Collections;

public class PathNodeManager : MonoBehaviour
{
	public static PathNodeManager SP;
	public Node[] Nodes;
	public Node[] forkNodes;
//	private Vector3[] Sections;
//	private int SectionCount;
	// Use this for initialization
	void Awake ()
	{
		SP = this;
//		SectionCount = Nodes.Length - 1;
//		Sections = new Vector3[SectionCount];
//		for(int i = 0 ; i < SectionCount ; i++){
//			//Sections[i] = new Vector3(Nodes[i].position.x );
//		}
		for(int i = 0 ; i < Nodes.Length ; i++){
			//Sections[i] = new Vector3(Nodes[i].position.x );
			Nodes[i].SetIndex(i);
		}
		for(int i = 0 ; i < forkNodes.Length ; i++){
			//Sections[i] = new Vector3(Nodes[i].position.x );
			forkNodes[i].SetIndex(i + Nodes.Length);
			forkNodes[i].SetFork();
		}
	}
	
	public Transform getNode(int index){
		if(index < Nodes.Length)
			return Nodes[index].transform;
		else
			return GetForkNode(index);
	}
	
	public Node GetNode (int index) {
		if(index < Nodes.Length)
			return Nodes[index];
		else
			return GetForkNode(index, false);
	}
	
	public Transform GetForkNode(int index) {
		return forkNodes[index - Nodes.Length].transform;
	}
	
	public Node GetForkNode(int index, bool getTransform) {
		return forkNodes[index - Nodes.Length];
	}
	
	public int GetNodeLength(){
		return Nodes.Length;
	}
	
	public bool IsLimitNode(int index) {
		return Nodes[index].IsLimit();
	}
	
	public float MaxSpeedNode(int index) {
		return Nodes[index].MaxSpeed();
	}
	
	public float MinSpeedNode(int index) {
		return Nodes[index].MinSpeed();
	}
	
	public bool IsCornerNode(int index) {
		return Nodes[index].IsCorner();
	}
	
	public Vector3 GetCorner(int index) {
		return Nodes[index].CornerPosition();
	}
	
	public int GetWeight(int index) {
		return Nodes[index].weight;
	}
}

