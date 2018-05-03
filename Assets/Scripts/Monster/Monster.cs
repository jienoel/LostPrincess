using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public ColliderDetector colliderDetector;

	public MonsterItem monsterItem;
	// Use this for initialization
	void Start ()
	{
		colliderDetector.onEnter += OnCollider;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy()
	{
		colliderDetector.onEnter -= OnCollider;
	}

	public void OnHitByBullet()
	{
		Destroy(gameObject);
		if (monsterItem.dropType == DropType.Diary)
		{
			Book book = GameObject.Instantiate(Game.Instance.gameModel.bookPrefab,transform.parent);
			book.transform.localPosition = transform.localPosition;
			book.transform.localRotation = transform.localRotation;
			book.id = monsterItem.dropID;
		}
	}

	void OnCollider(GameObject gameObject)
	{
		if (gameObject.CompareTag("Player"))
		{
			
		}
		else if (gameObject.CompareTag("Monster"))
		{
		}else if (gameObject.CompareTag("Bullet"))
		{
			OnHitByBullet();
		}
	}
}
