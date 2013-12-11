using UnityEngine;
using System.Collections;
//public enum RankManagerState{
//	INIT,
//	START
//};

public class RankManager : MonoBehaviour
{
	public class CarState {
//		public DriftCar carBody;
		public CarB carBBody;
		public CarProperty property;
//		
//		public CarState (CarB carb) {
//			carBBody = carb;
//		}
		
		public int nowNode {
			get {
//				if(carBody)
//				{
//					if(carBody.GetWeights() <= 0)
//						return 0;
//					return carBody.NowNode;
//				}
//				else
//				{
				if(carBBody.GetWeights() <= 0)
					return 0;
				return carBBody.NowNode;
//				}
			}
		}
		public int round {
			get {
//				if(carBody)
//					return carBody.round;
//				else
				return carBBody.round;
			}
		}
		
		public CarState (CarB car) {
			carBBody = car;
			property = car.GetComponent<CarProperty>();
			name = ((Definition.eCarID)property.carID).ToString();
		}
		public float distance = 0.0f;
		public int rank = 0;
		public string name = "";
	}
//	public int round;
//	private RankManagerState MyRankManagerState;
//	public Texture2D[] carsIcon;
//	public GUITexture maxPlayer;
//	public GUITexture[] playerRanks;
	public UISprite[] playerRanks;
//	private UserInterfaceControl uiController;
	private CarState[] _cars;
	private CarB[] cars;
	// Use this for initialization
	void Start ()
	{
//		//for optimize
//		while(cars == null)
//		{
//			cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//			yield return null;
//		}
//		_cars = new CarState[cars.Length];
//		for(int i = 0 ; i < cars.Length ; i++){
//			_cars[i] = new CarState();
//		}
		SetRanksCar();
	}

	// Update is called once per frame
	void Update ()
	{
//		if(_cars == null || _cars.Length == 0)
//		{
////			GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
//			CarB[] cars = FindObjectsOfType(typeof(CarB)) as CarB[];
//			if(cars.Length > 0)
//			{
//				_cars = new CarState[cars.Length];
//				int i = 0;
//				foreach(CarB car in cars)
//				{
//					_cars[i] = new CarState();
//					_cars[i].carBody = car.GetComponent<DriftCar>();
//					_cars[i].carBBody = car;
//					i++;
//				}
//			}
//		}
//		else
//			SortRank();
		
		if(_cars != null)
		{
			Sort(_cars);
//			SortRank();
			int rank = 1;
			foreach(CarState car in _cars)
			{
				car.rank = rank;
				car.carBBody.EngineBonus(rank);
				playerRanks[rank - 1].spriteName = car.name;
				rank++;
			}
			if(rank != 5)
			{
				for(; rank < 5; rank++)
				{
					playerRanks[rank - 1].enabled = false;
				}
			}
		}
	}
	
	public void SetRanksCar () {
		CarB[] cars = FindObjectsOfType(typeof(CarB)) as CarB[];//for optimize
		if(cars.Length > 0)
		{
			_cars = new CarState[cars.Length];//for optimize
			int i = 0;
			foreach(CarB car in cars)
			{
				_cars[i] = new CarState(car);
//				_cars[i].carBody = car.GetComponent<DriftCar>();
//				_cars[i].carBBody = car;
//				_cars[i].property = car.GetComponent<CarProperty>();
//				_cars[i].name = ((Definition.eCarID)car[i].property.carID).ToString();
				i++;
			}
			foreach(UISprite sprite in playerRanks)
			{
				if(sprite)
				{
					sprite.enabled = true;
				}
			}
		}
	}
	
	public void Sort(CarState[] cars)
    {
        Sort(cars, 0, cars.Length - 1);
    }

    private void Sort(CarState[] cars, int left, int right)
    {
        if (left < right)
        {
			try
			{
	            CarState middleCar = cars[(left + right) / 2];
	            int i = left - 1;
	            int j = right + 1;
			
	            while (true)
	            {
	                while (CalculateRank(cars[++i], middleCar)) ;
	
	                while (CalculateRank(middleCar, cars[--j])) ;
	
	                if (i >= j)
	                    break;
	
	                Swap(cars, i, j);
	            }
	
	            Sort(cars, left, i - 1);
	            Sort(cars, j + 1, right);
			}
			catch
			{
				SetRanksCar();
			}
        }
    }
	
	private bool CalculateRank(CarState car1, CarState car2) {
		if(car1.round > car2.round || (car1.round == car2.round && (car1.nowNode > car2.nowNode || (car1.nowNode == car2.nowNode && CalculateDistance(car1, car2, car1.nowNode)))))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
    private void Swap(CarState[] cars, int i, int j)
    {
        CarState car = cars[i];
        cars[i] = cars[j];
        cars[j] = car;
    }
	
	void SortRank ()
	{
		int i;
		int j;
		CarState tempCar;
		try
		{
			for(i = 1;i < _cars.Length;i++)
			{
				tempCar = _cars[i];
				for(j = i; j > 0;j--)
				{
					if(tempCar.round == _cars[j - 1].round)
					{
						if(tempCar.nowNode > _cars[j - 1].nowNode || (tempCar.nowNode == _cars[j - 1].nowNode && CalculateDistance(tempCar,_cars[j - 1], tempCar.nowNode)))
						{
							_cars[j] = _cars[j - 1];
						}
					}
					else if(tempCar.round > _cars[j - 1].round)
					{
						_cars[j] = _cars[j - 1];
					}
				}
				_cars[j] = tempCar;
			}
		}
		catch
		{
			SetRanksCar();
		}

	}
	
	bool CalculateDistance (CarState car1, CarState car2, int node)
	{
		float distance1;
		Vector3 nodePosition = PathNodeManager.SP.getNode(node).position;
//		if(car1.carBody)
//			distance1 = Vector3.Distance(car1.carBody.transform.position, nodePosition);
//		else
		distance1 = Vector3.Distance(car1.carBBody.transform.position, nodePosition);
		float distance2;
//		if(car2.carBody)
//			distance2 = Vector3.Distance(car2.carBody.transform.position, nodePosition);
//		else
		distance2 = Vector3.Distance(car2.carBBody.transform.position, nodePosition);
		car1.distance = distance1;
		car2.distance = distance2;
		if(distance1 < distance2)
			return true;
		else
			return false;
	}
	
	public int GetRank (Transform target) {
		if(_cars == null)
		{
//			Debug.LogWarning("Can't find any GameObject with tag car");
			return -1;
		}
		foreach(CarState car in _cars)
		{
//			if(car.carBody)
//			{
//				if(target == car.carBody.transform)
//				{
//					return car.rank;
//				}
//			}
//			else if(car.carBBody)
//			{
			if(target == car.carBBody.transform)
			{
				return car.rank;
			}
//			}
		}
		return -1;
	}
	
//	public CarProperty[] GetAllRank () {
//		CarProperty[] allCar = new CarProperty[_cars.Length];
//		SortRank();
//		int index = 0;
//		foreach(CarState car in _cars)
//		{
//			allCar[index] = car.property;
//			index++;
//		}
//		return allCar;
//	}
	
	//private TvCMatchRoom tvcRoom;
	
//	void OnGUI ()
//	{
////		if(tvcRoom == null)
////		{
////			MatchRoomState matchRoom = FindObjectOfType(typeof(MatchRoomState)) as MatchRoomState;
////			tvcRoom = matchRoom.GetTvCMatchRoom();
////		}
//		try
//		{
//			if(_cars != null)
//			{
//				int maxPlayer = _cars.Length;
////				Debug.Log(maxPlayer);
//				Vector2 scale = new Vector2(Screen.width / 1024.0f, Screen.height / 768.0f);
//				float tempx = 0 + ((78.0f - carsIcon[0].width) * scale.x) * 0.5f;
//				float tempoffset = 25 * scale.y;
//				GUI.depth = -5;
//				for(int i = 0 ; i < maxPlayer ; i++){
//					Rect CarPos_Co = new Rect();
//					CarPos_Co.width = carsIcon[0].width;// * scale.x;
//					CarPos_Co.height = carsIcon[0].height;// * scale.y;
//					
//					CarPos_Co.x = tempx;
//					CarPos_Co.y = (Screen.height - 369.0f * scale.y) / 2.0f + tempoffset * (i + 1) + CarPos_Co.height * (i);
//	//				Debug.Log(i + " : " + tempoffset);
//					GUI.DrawTexture(CarPos_Co, carsIcon[_cars[i].property.carID - 1]);
//				}
//			}
//		}
//		catch
//		{
//			SetRanksCar();
//		}
//	}
}

