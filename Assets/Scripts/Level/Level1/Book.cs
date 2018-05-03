using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Book : MonoBehaviour
{
	private ColliderDetector _colliderDetector;
	public int id;
	private void Start()
	{
		_colliderDetector = GetComponentInChildren<ColliderDetector>();
		_colliderDetector.onPlayerEnter += OnPlayerEnter;
	}

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		
		if (collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player OnCollisionEnter");
			OnPlayerEnter();
		}

	}

	private void OnDestroy()
	{
		if (_colliderDetector)
			_colliderDetector.onPlayerEnter -= OnPlayerEnter;
	}

	void OnPlayerEnter()
	{
		Messenger.Broadcast(MessageName.MN_Book_Click, id);
		DestroyBook();
	}

	void ShowText()
	{
		
	}

	void DestroyBook()
	{
		Destroy(gameObject);
	}
}
