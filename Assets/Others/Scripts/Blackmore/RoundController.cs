using UnityEngine;
using System.Collections;

public class RoundController : MonoBehaviour {
	private UILabel RankLabel;
	private Transform PlayerCar;
	private CarB carB;
	private int NowLoopNum;
	private string[] Rank = {"0/3","1/3","2/3","3/3","Goal"};
	// Use this for initialization
	IEnumerator Start () {
		SmoothFollow smCamera = Camera.main.GetComponent<SmoothFollow>();
		while(!smCamera.target)
		{
			yield return null;
		}
		RankLabel = transform.GetComponent<UILabel>();
		PlayerCar = smCamera.target;
		carB = PlayerCar.GetComponent<CarB>();
		NowLoopNum = carB.GetRound();
	}
	
	// Update is called once per frame
	void Update () {
		if(!PlayerCar)
			return;
		if(NowLoopNum != carB.GetRound()){
			NowLoopNum = carB.GetRound();
//			Debug.Log(NowLoopNum);
			RankLabel.text = Rank[NowLoopNum + 1];
		}
	}
}
