using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputter : MonoBehaviour
{
	void OnMouseDown()
	{
		//Debug.Log("OnMouseDown");
		Messenger.Broadcast(MessageName.MN_Mouse_Down, transform);
	}


}
