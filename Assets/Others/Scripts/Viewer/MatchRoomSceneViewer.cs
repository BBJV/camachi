using UnityEngine;
using System.Collections;

public class MatchRoomSceneViewer : MonoBehaviour {
	
	public GameObject carPlane;
	public GameObject leftButton;
	public GameObject rightButton;
	public GameObject BGCover;
	public GameObject carDownBoard;
	
	public GameObject talentIcon;
	
	void Awake() {
		MatchController.OnAfterBoardDrawn += OnAfterBoardDrawn;
		MatchController.OnCarSelected += OnCarSelected;
		MatchController.OnAllPlayerSelectCar += OnAllPlayerSelectCar;
		MatchController.OnLeaveFromMatchRoom += OnLeaveFromMatchRoom;
		
		
	}
	
	void OnDisable() {
		MatchController.OnAfterBoardDrawn -= OnAfterBoardDrawn;
		MatchController.OnCarSelected -= OnCarSelected;
		MatchController.OnAllPlayerSelectCar -= OnAllPlayerSelectCar;
		MatchController.OnLeaveFromMatchRoom -= OnLeaveFromMatchRoom;
	}
	
	void OnAllPlayerSelectCar() {
		carPlane.SetActiveRecursively(false);
		BGCover.SetActiveRecursively(false);
		carDownBoard.SetActiveRecursively(false);
		
		talentIcon.SetActiveRecursively(false);
	}
	
	void OnLeaveFromMatchRoom() {
		carPlane.SetActiveRecursively(false);
		BGCover.SetActiveRecursively(false);
		carDownBoard.SetActiveRecursively(false);
		leftButton.SetActiveRecursively(false);
		rightButton.SetActiveRecursively(false);
		
		talentIcon.SetActiveRecursively(false);
	}
	
	void OnAfterBoardDrawn() {
		leftButton.SetActiveRecursively(true);
		rightButton.SetActiveRecursively(true);
		
		BGCover.SetActiveRecursively(true);
		carDownBoard.SetActiveRecursively(true);
		
		talentIcon.SetActiveRecursively(true);
	}
	
	void OnCarSelected() {
		leftButton.SetActiveRecursively(false);
		rightButton.SetActiveRecursively(false);
	}
	
	public void EnterMatchRoomSceneSeeting() {
		
	}
	
	public void LeaveMatchErrorSceneSetting() {
		
	}
	
	public void AbdicateErrorSceneSetting() {
		
	}
	
	public void LoadCarErrorSceneSetting() {
		
	}
	
	public void SelectCarErrorSceneSetting() {
		
	}
	
	public void StartMatchErrorSceneSetting() {
		
	}
}
