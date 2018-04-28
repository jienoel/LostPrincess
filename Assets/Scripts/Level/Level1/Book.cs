using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Book : MonoBehaviour
{
	private BookColliderDetector _bookColliderDetector;
	public int id;
	private void Start()
	{
		_bookColliderDetector = GetComponentInChildren<BookColliderDetector>();
		_bookColliderDetector.onPlayerEnter += OnPlayerEnter;
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
		if (_bookColliderDetector)
			_bookColliderDetector.onPlayerEnter -= OnPlayerEnter;
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
