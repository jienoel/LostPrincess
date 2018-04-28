using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;

public class Tree : MonoBehaviour
{

	public SpriteRenderer treeTex;
	
	// Use this for initialization
	void Start () {
		Messenger.AddListener(MessageName.MN_BoreFire_Change, OnBoneFireChange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDestroy()
	{
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
}
