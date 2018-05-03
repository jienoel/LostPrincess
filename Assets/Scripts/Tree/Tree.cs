using System.Collections;
         using System.Collections.Generic;
         using Battle;
         using UnityEngine;
using Utils;

public class Tree : MonoBehaviour
{
	public enum EStatus
	{
		normal,
		onfiring,
		dead
	}

	public EStatus status
	{
		get { return _status; }
		set
		{
			if (_status != value)
			{
				_status = value;
				OnStatusChange();
			}
		}
	}
	private EStatus _status = EStatus.normal;
	public ColliderDetector colliderDetector;
	public SpriteRenderer treeTex;

	public SpriteRenderer deadTreeTex;

	public GameObject fire;

	public float fireTime = 1;
	// Use this for initialization
	void Start()
	{
		Messenger.AddListener(MessageName.MN_BoreFire_Change, OnBoneFireChange);
		colliderDetector.onEnterExceptPlayer += OnHitByAnyoneExceptPlayer;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnDestroy()
	{
		colliderDetector.onEnterExceptPlayer -= OnHitByAnyoneExceptPlayer;
		Messenger.RemoveListener(MessageName.MN_BoreFire_Change, OnBoneFireChange);
	}

	void OnBoneFireChange()
	{
		bool isVisible = FOWSystem.instance.IsVisible(treeTex.transform.position);
		if (isVisible)
		{
			treeTex.sortingOrder = 30;
		}
	}

	public void OnHitByAnyoneExceptPlayer(GameObject gameObject)
	{

		if (gameObject.CompareTag("Monster"))
		{
			
		}
		else if (gameObject.CompareTag("Bullet"))
		{
			OnFire();
		}

	}

	void OnStatusChange()
	{
		if (status == EStatus.dead)
		{
			treeTex.gameObject.SetActiveSelf(false);
			fire.SetActiveSelf(false);
			deadTreeTex.gameObject.SetActiveSelf(true);
		}else if (status == EStatus.normal)
		{
			treeTex.gameObject.SetActiveSelf(true);
			fire.SetActiveSelf(false);
			deadTreeTex.gameObject.SetActiveSelf(false);
		}else if (status == EStatus.onfiring)
		{
			fire.SetActiveSelf(true);
			deadTreeTex.gameObject.SetActiveSelf(false);
			treeTex.gameObject.SetActiveSelf(true);
		}
	}

	public void OnFire()
	{
		Debug.Log("tree on fire!");
		if (status == EStatus.normal)
		{
			status = EStatus.onfiring;
			StartCoroutine(Firing(fireTime));
		}
	}

	IEnumerator Firing(float fireTime)
	{
		yield return  new WaitForSeconds(fireTime);
		status = EStatus.dead;
	}
}
