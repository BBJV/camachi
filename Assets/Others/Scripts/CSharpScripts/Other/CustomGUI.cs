using UnityEngine;
using System.Collections;

public class CustomGUI {
	
	/// <summary>
	///position: Rectangle on the screen to use for the button.  
	///text: Text to display on the button.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	
	public static bool Button (Rect position, string text) {
		GUI.Button(position, text);
		position.y = Screen.height - position.y - position.height;
		
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	/// <summary>
	/// Parameters
	///position: Rectangle on the screen to use for the button.  
	///image:	Texture to display on the button.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	public static bool Button (Rect position, Texture image) {
		GUI.Button(position, image);
		position.y = Screen.height - position.y - position.height;
		
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	/// <summary>
	/// Parameters
	///position: Rectangle on the screen to use for the button.  
	///content	: Text, image and tooltip for this button.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	public static bool Button (Rect position, GUIContent content) {
		GUI.Button(position, content);
		position.y = Screen.height - position.y - position.height;
		
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	/// <summary>
	/// Parameters
	///position: Rectangle on the screen to use for the button.  
	///text: Text to display on the button.  
	///style: The style to use. If left out, the button style from the current GUISkin is used.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	public static bool Button (Rect position, string text, GUIStyle style) {
		GUI.Button(position, text, style);
		position.y = Screen.height - position.y - position.height;
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	/// <summary>
	/// Parameters
	///position: Rectangle on the screen to use for the button.  
	///image:	Texture to display on the button.  
	///style: The style to use. If left out, the button style from the current GUISkin is used.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	public static bool Button (Rect position, Texture image, GUIStyle style) {
		GUI.Button(position, image, style);
		position.y = Screen.height - position.y - position.height;
		
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	/// <summary>
	/// Parameters
	///position: Rectangle on the screen to use for the button.  
	///content: Text, image and tooltip for this button.  
	///style: The style to use. If left out, the button style from the current GUISkin is used.  
	///Returns  
	///boolean - "true" when the users clicks the button
	///
	/// </summary>
	public static bool Button (Rect position, GUIContent content, GUIStyle style) {
		GUI.Button(position, content, style);
		position.y = Screen.height - position.y - position.height;
		
		if(Input.GetMouseButtonUp(0) && position.Contains(Input.mousePosition))
		{
			return true;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position))
				{
					return true;
				}
			}
		}
		
		return false;
	}
	
	private static int lastFingerId = -1;
	
	public static float HorizontalSlider (Rect position, float value, float leftValue, float rightValue) {
		float thumbOffset = GUI.skin.horizontalSliderThumb.normal.background.width * 0.5f;
		GUI.HorizontalSlider(position, value, leftValue, rightValue);
		position.y = Screen.height - position.y - position.height;
		float minLimit = position.x + thumbOffset;
		float maxLimit = position.x + position.width - thumbOffset;
		float diff = rightValue - leftValue;
		
		if(Input.GetMouseButton(0) && position.Contains(Input.mousePosition))
		{
			return Mathf.Clamp01((Input.mousePosition.x - minLimit) / (maxLimit - minLimit)) * diff + leftValue;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position) && lastFingerId == -1)
				{
					lastFingerId = touch.fingerId;
				}
			}
			else
			{
				if(lastFingerId == touch.fingerId)
				{
					lastFingerId = -1;
					return Mathf.Clamp(value, leftValue, rightValue);
				}
			}
			
			if (lastFingerId == touch.fingerId)
			{
				return Mathf.Clamp01((touch.position.x - minLimit) / (maxLimit - minLimit)) * diff + leftValue;
			}
        }
		return Mathf.Clamp(value, leftValue, rightValue);
	}
	
	public static float HorizontalSlider (Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb) {
		float thumbOffset = thumb.normal.background.width * 0.5f;
		GUI.HorizontalSlider(position, value, leftValue, rightValue, slider, thumb);
		position.y = Screen.height - position.y - position.height;
		float minLimit = position.x + thumbOffset;
		float maxLimit = position.x + position.width - thumbOffset;
		float diff = rightValue - leftValue;
		
		if(Input.GetMouseButton(0) && position.Contains(Input.mousePosition))
		{
			return Mathf.Clamp01((Input.mousePosition.x - minLimit) / (maxLimit - minLimit)) * diff + leftValue;
		}
		
		foreach (Touch touch in Input.touches) {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			{
				if(position.Contains(touch.position) && lastFingerId == -1)
				{
					lastFingerId = touch.fingerId;
				}
			}
			else
			{
				if(lastFingerId == touch.fingerId)
				{
					lastFingerId = -1;
					return Mathf.Clamp(value, leftValue, rightValue);
				}
			}
			
			if (lastFingerId == touch.fingerId)
			{
				return Mathf.Clamp01((touch.position.x - minLimit) / (maxLimit - minLimit)) * diff + leftValue;
			}
        }
		return Mathf.Clamp(value, leftValue, rightValue);
	}
}
