using UnityEngine;
using System.Collections;

public class ChangeMap : MonoBehaviour {
	
	public int mapIndex;
	public int mapCount = 1;
	public Animation panel;
	private int selectedMap = 0;
	
	void TurnLeft () {
		bool turn = false;
		if(selectedMap == mapIndex)
		{
			animation.Play("minePit_1_L");
			turn = true;
			panel.Play("trackTitle_1_panel_back");
		}
		
		selectedMap -= 1;
		if(selectedMap < 0)
		{
			selectedMap = mapCount - 1;
		}
		
		if(selectedMap == mapIndex)
		{
			animation.Play("minePit_1_R");
			panel.Play("trackTitle_1_panel");
		}
		else if(!turn)
		{
			animation.Play("minePit_1_R_stay");
		}
	}
	
	void TurnRight () {
		bool turn = false;
		if(selectedMap == mapIndex)
		{
			animation.Play("minePit_1_R_back");
			turn = true;
			panel.Play("trackTitle_1_panel_back");
		}
		
		selectedMap += 1;
		if(selectedMap >= mapCount)
		{
			selectedMap = 0;
		}
		if(selectedMap == mapIndex)
		{
			animation.Play("minePit_1_L_back");
			panel.Play("trackTitle_1_panel");
		}
		else if(!turn)
		{
			animation.Play("minePit_1_L_stay");
		}
	}
}
