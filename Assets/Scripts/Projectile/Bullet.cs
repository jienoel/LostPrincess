using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private void Start()
	{
		Destroy(gameObject, 5);
	}

	private void OnCollisionEnter(Collision collision) {
		if (!collision.gameObject.CompareTag("Player"))
		{
			gameObject.SetActive(false);
			Debug.Log("OnCollider :"+collision.gameObject.name);
			Monster monster = collision.gameObject.GetComponent<Monster>();
			if (monster!=null)
			{
				monster.OnHitByBullet();
			}
		}
	}
}
