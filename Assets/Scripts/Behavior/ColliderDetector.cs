using System;
using UnityEngine;

public class ColliderDetector : MonoBehaviour
{
	public Action onPlayerEnter;
	public Action<GameObject> onEnterExceptPlayer;
	public Action<GameObject> onEnter;
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
		else if(onEnterExceptPlayer!=null)
		{
			onEnterExceptPlayer(collision.gameObject);
		}

		if (onEnter != null)
		{
			onEnter(collision.gameObject);
		}
	}
}
