using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour {
	public string missionName;
	public VictoryCondition[] victoryConditions;
	public CarAI player;
	private float raceTime = 0.0f;
	private bool showVictory = false;
	private RankManager rank;
	private bool checkFailGame = false;
	public enum MissionCondition {FinishLap, TimeRace, Win, UseSkill, Attak, Position, Destroy, Barrier};
	
	[System.Serializable]
	public class VictoryCondition {
		public MissionCondition condition;
		public float times;
		public GameObject[] targets;
		public bool reached = false;
	}
	
	void OnGUI () {
		if(checkFailGame && !showVictory)
		{
			GUI.Label(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f - 200, 100, 100), "Fail!!!");
		}
		else if(showVictory)
		{
			GUI.Label(new Rect(Screen.width * 0.5f - 50, Screen.height * 0.5f - 200, 100, 100), "Victory!!!");
		}
	}
	
	void Update () {
		int count = 0;
		foreach(VictoryCondition victor in victoryConditions)
		{
			switch (victor.condition)
			{
				case MissionCondition.FinishLap:
					if(IsFinishLap(victor))
					{
						checkFailGame = true;
						victor.reached = true;
						count++;
					}
					break;
				case MissionCondition.TimeRace:
					if(IsTimeRace(victor))
					{
						victor.reached = true;
						count++;
					}
					else
					{
						victor.reached = false;
						checkFailGame = true;
					}
					break;
				case MissionCondition.Win:
					if(IsWin(victor))
					{
						victor.reached = true;
						count++;
					}
					else
					{
						victor.reached = false;
					}
					break;
				case MissionCondition.UseSkill:
					if(IsUseSkill(victor))
					{
						victor.reached = true;
						count++;
					}
					break;
				case MissionCondition.Attak:
					if(IsAttak(victor))
					{
						victor.reached = true;
						count++;
					}
					break;
				case MissionCondition.Position:
					if(IsPosition(victor))
					{
						victor.reached = true;
						count++;
					}
					else
					{
						victor.reached = false;
					}
					break;
				case MissionCondition.Destroy:
					if(IsDestroy(victor))
					{
						victor.reached = true;
						count++;
					}
					break;
				case MissionCondition.Barrier:
					if(IsBarrier(victor))
					{
						victor.reached = false;
						checkFailGame = true;
					}
					else
					{
						victor.reached = true;
						count++;
					}
					break;
			}
		}
		if(count == victoryConditions.Length)
			showVictory = true;
		if(showVictory && !player.enabled)
		{
			player.enabled = true;
			((UserInterfaceControl)FindObjectOfType(typeof(UserInterfaceControl))).enabled = false;
		}
		if(checkFailGame && !showVictory)
		{
			if(player.GetComponent<DriftCar>())
				player.GetComponent<DriftCar>().enabled = false;
			if(player.GetComponent<CarB>())
			{
				player.GetComponent<CarB>().enabled = false;
				((UserInterfaceControl)FindObjectOfType(typeof(UserInterfaceControl))).enabled = false;
			}
		}
		raceTime += Time.deltaTime;
	}
	
	private DriftCar playersLap;
	private CarB playersLapB;
	bool IsFinishLap (VictoryCondition victor) {
		if(!playersLap && !playersLapB)
		{
			playersLap = victor.targets[0].GetComponent<DriftCar>();
			playersLapB = victor.targets[0].GetComponent<CarB>();
		}
		if(playersLap)
			return (playersLap.round == victor.times);
		else
			return (playersLapB.round == victor.times);
	}
	
	bool IsTimeRace (VictoryCondition victor) {
		if(raceTime <= victor.times)
			return true;
		else
			return false;
	}
	
	bool IsWin (VictoryCondition victor) {
		if(!rank)
		{
			rank = FindObjectOfType(typeof(RankManager)) as RankManager;
		}
		return (rank.GetRank(victor.targets[0].transform) == 1);
	}
	
	private CarProperty playersCar;
	bool IsUseSkill (VictoryCondition victor) {
		if(playersCar == null)
		{
			playersCar = victor.targets[0].GetComponent<CarProperty>();
		}
		return (playersCar.GetSkillTime() >= victor.times);
	}
	
	private CarProperty[] cars;
	bool IsAttak (VictoryCondition victor) {
		if(cars == null)
		{
			cars = new CarProperty[victor.targets.Length];
			int i = 0;
			foreach(GameObject target in victor.targets)
			{
				cars[i] = target.GetComponent<CarProperty>();
				i++;
			}
		}
		int count = 0;
		foreach(CarProperty car in cars)
		{
			count += car.GetSkillTime();
		}
		return (count >= victor.times);
	}
	
	private PositionMission[] positions;
	bool IsPosition (VictoryCondition victor) {
		int count = 0;
		if(positions == null)
		{
			positions = new PositionMission[victor.targets.Length];
			int i = 0;
			foreach(GameObject target in victor.targets)
			{
				positions[i] = target.AddComponent<PositionMission>();
				i++;
			}
		}
		foreach(PositionMission target in positions)
		{
			if(target.inNode)
				count++;
		}
		return (count == positions.Length);
	}
	
	bool IsDestroy (VictoryCondition victor) {
		int count = 0;
		foreach(GameObject target in victor.targets)
		{
			if(target)
				count++;
		}
		if(count == 0)
			return true;
		else
			return false;
	}
	
	bool IsBarrier (VictoryCondition victor) {
		if(playersCar == null)
		{
			playersCar = victor.targets[0].GetComponent<CarProperty>();
		}
		return (playersCar.GetBarrierTime() >= victor.times);
	}
}
