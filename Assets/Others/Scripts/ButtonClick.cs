using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
	
	public GameObject target;
	public bool includeChild = true;
	public string method;
	
	void OnClick () {
		if(target)
		{
			if(includeChild)
			{
				target.BroadcastMessage(method, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				target.SendMessage(method, SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		{
			if(includeChild)
			{
				BroadcastMessage(method, SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				SendMessage(method, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
