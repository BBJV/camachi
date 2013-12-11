using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour
{
	public int Index = -1;
	public int weight = 1;
	public bool fork = false;
	public bool limit;
	public float maxSpeed;
	public float minSpeed;
	public bool corner = false;
	public Vector3 cornerPosition;
	public ArrayList inforArray = new ArrayList();
	public CarInformation[] informations;
	public bool record = false;
	public bool startLine = false;
	public GameObject[] openRoadColliders;
	public GameObject[] closeRoadColliders;
	private SmoothFollow smCamera;
	
	[System.Serializable]
	public class CarInformation {
		public Vector3 position;
		public Quaternion rotation;
		public float velo;
		public Vector3 veloDir;
	}
	
	public void SetIndex(int index){
		Index = index;
	}
	
	public void SetFork() {
		fork = true;
	}
	
	public int GetIndex(){
		return Index;
	}
	
	public int GetWeight () {
		return weight;
	}
	
	public bool IsLimit () {
		return limit;
	}
	
	public float MaxSpeed () {
		return maxSpeed * 0.28f;
	}
	
	public float MinSpeed () {
		return minSpeed * 0.28f;
	}
	
	public bool IsCorner () {
		return corner;
	}
	
	public Vector3 CornerPosition () {
		return cornerPosition;
	}
	
//	void OnTriggerStay(Collider other) {
//		if(!record)
//			return;
//		CarB car = other.GetComponent<CarB>();
////        if(car && (recordTime + 0.5f <= Time.time || recordTime == 0.0f))
//		if(car)
//		{
//			CarInformation carInformation = new CarInformation();
//			carInformation.position = car.transform.position;
//			carInformation.rotation = car.transform.rotation;
//			carInformation.velo = car.rigidbody.velocity.magnitude;
//			carInformation.veloDir = car.rigidbody.velocity.normalized;
////			Debug.Log(carInformation.veloDir);
//			inforArray.Add(carInformation);
//		}
//    }
//	
//	void OnTriggerExit(Collider other) {
//		if(!record)
//			return;
//        CarB car = other.GetComponent<CarB>();
//        if(car)
//		{
//			informations = new CarInformation[inforArray.Count];
//			int index = 0;
//			foreach(CarInformation info in inforArray)
//			{
//				informations[index] = info;
//				index++;
//			}
//			inforArray.Clear();
//		}
//    }
	
	IEnumerator OnTriggerEnter(Collider other) {
        CarB car = other.transform.root.GetComponent<CarB>();
		//OpenRoads(other.transform);
        if(car)
		{
			if(startLine)
			{
//				Debug.Log("startline");
				car.SendMessage("PassThroughStartLine", SendMessageOptions.DontRequireReceiver);
				if(!car.GetStartLine())
				{
					car.SetStartLine(true);
				}
				else if(car.GetNextNode() != Index)
				{
					car.SetNowNode(Index);
					car.SetStartLine(false);
					yield break;
				}
				if(car.GetWeights() == PathNodeManager.SP.GetNodeLength() - 1)
				{
					car.SetRound(car.GetRound() + 1);
					if(!smCamera)
					{
						smCamera = Camera.main.GetComponent<SmoothFollow>();
					}
					if(smCamera.target == car.transform)
					{
						if(car.GetRound() == 3)
						{
							if(!car.networkView.enabled)
							{
								smCamera.BroadcastMessage("ShowCup", car.GetComponent<CarProperty>().GetRank(), SendMessageOptions.DontRequireReceiver);
								if(!backMapScene)
								{
									StartCoroutine(BackToSelectMapScene());
								}
							}
							else
							{
								car.SendMessage("FinishRound", SendMessageOptions.DontRequireReceiver);
							}
						}
						else
						{
							smCamera.BroadcastMessage("Round", car.GetRound(), SendMessageOptions.DontRequireReceiver);
						}
					}
					else if(car.GetRound() == 3)
					{
						car.SendMessage("FinishRound", SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			else if(car.GetStartLine() && Index == PathNodeManager.SP.GetNodeLength() - 1 && car.GetWeights() != PathNodeManager.SP.GetNodeLength() - 2)
			{
				car.SetStartLine(false);
				yield break;
			}
//			Debug.Log("car.GetNextNode() : " + car.GetNextNode() + " Index : " + Index + " car.GetWeights() : " + car.GetWeights() + " car.GetStartLine() : " + car.GetStartLine());
			car.SetNowNode(Index);
		}
    }
	
	private bool backMapScene = false;
	IEnumerator BackToSelectMapScene () {
		backMapScene = true;
		yield return new WaitForSeconds(3);
		Application.LoadLevelAsync((int)Definition.eSceneID.MapScene);
	}
	
	void OpenRoads (Transform onRoadObject) {
		foreach(GameObject road in openRoadColliders)
		{
			road.SendMessage("Use", onRoadObject, SendMessageOptions.DontRequireReceiver);
		}
		
		foreach(GameObject road in closeRoadColliders)
		{
			road.SendMessage("UnUse", onRoadObject, SendMessageOptions.DontRequireReceiver);
		}
//		Hashtable args = new Hashtable();
//		args.Add("onRoadObject", onRoadObject);
//		args.Add("ignoreRoads", openRoadColliders);
//		if(openRoadColliders.Length > 0)
//		{
//			openRoadColliders[0].transform.root.BroadcastMessage("UnUse", args, SendMessageOptions.DontRequireReceiver);
//		}
	}
	
//	void OnDrawGizmos() {
//		foreach(CarInformation info in informations)
//		{
//			Debug.DrawRay(info.position, info.veloDir);
//		}
//    }
}

