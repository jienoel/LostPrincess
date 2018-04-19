using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public  class Game :MonoBehaviour
{
	public GameModel gameModel;
	private void Awake()
	{
		if( gameModel == null )
			gameModel = GetComponent<GameModel>();
	}
}
