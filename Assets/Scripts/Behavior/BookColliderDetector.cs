using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookColliderDetector : MonoBehaviour
{

	public Action onPlayerEnter;
	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		
		if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player OnCollisionEnter");
			if (onPlayerEnter != null)
				onPlayerEnter();
		}

	}
}
