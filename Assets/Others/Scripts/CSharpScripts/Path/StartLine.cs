using UnityEngine;
using System.Collections;

public class StartLine : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
        CarB car = other.GetComponent<CarB>();
        if(car)
		{
//			Debug.Log("car.GetWeights() : " + car.GetWeights() + " PathNodeManager.SP.GetNodeLength() : " + PathNodeManager.SP.GetNodeLength());
			if(car.GetWeights() == PathNodeManager.SP.GetNodeLength() - 1)
			{
				car.SetRound(car.GetRound() + 1);
			}
			else if(car.GetStartLine() && car.GetWeights() == 0)
			{
				car.SetStartLine(false);
			}
			else
			{
				car.SetStartLine(true);
			}
		}
    }
}
