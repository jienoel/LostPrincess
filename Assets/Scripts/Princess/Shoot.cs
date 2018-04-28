using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public Action onMouseDown;
	void OnMouseDown()
	{
		if (onMouseDown != null)
			onMouseDown();
		Debug.Log("OnMouseDown");
	}

	private void OnDestroy()
	{
		onMouseDown = null;
	}
}
