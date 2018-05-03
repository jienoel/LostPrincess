using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private void Start()
	{
		Destroy(gameObject, 5);
	}

	private void OnCollisionEnter(Collision collision) {
		if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Plane"))
		{
			gameObject.SetActive(false);
			Debug.Log("OnCollider :"+collision.gameObject.name);
		}
	}
}
