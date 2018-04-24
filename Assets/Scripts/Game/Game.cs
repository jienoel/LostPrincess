using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public  class Game :MonoBehaviour
{
	public GameModel gameModel;
	public static Game Instance;
	private void Awake()
	{
		Instance = this;
		if( gameModel == null )
			gameModel = GetComponent<GameModel>();
	}
	
	public enum  OperateType
	{
		PlantTree,
		ClickToMove,
		PlantFire
	}

	public OperateType operateType;

	private void Update()
	{
		if (Input.GetKey(KeyCode.T))
		{
			operateType = OperateType.PlantTree;
		}else if (Input.GetKey(KeyCode.C))
		{
			operateType = OperateType.ClickToMove;
		}
		else if(Input.GetKey(KeyCode.F))
		{
			operateType = OperateType.PlantFire;
		}
	}
}
