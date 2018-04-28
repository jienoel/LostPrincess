using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;


public  class Game :MonoBehaviour
{
	public GameModel gameModel;
	public static Game Instance;
	//TODO @chenjie 测试代码，需删除
	public bool testChangeFog;
	public Camera mainCamera;
	public bool isReadDiary;
	
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

		if (testChangeFog && Application.isPlaying)
		{
			ChangeFog();
			//testChangeFog = false;
		}
	}

	void Start()
	{
		Instance = this;
		if( gameModel == null )
			gameModel = GetComponent<GameModel>();
		mainCamera = Camera.main;
		// fow系统启动
		FOWLogic.instance.Startup();
		Messenger.AddListener<bool>(MessageName.MN_Diary_Read, OnReadDiary);
	}

	 void OnDestroy()
	{
		//Messenger.RemoveListener<bool>(MessageName.MN_Diary_Read, OnReadDiary);
		Instance = null;
	}

	void OnReadDiary(bool isReading)
	{
		this.isReadDiary = isReading;
	}

	public void ChangeFog()
	{
		FOWLogic.instance.Update(0);
	}
}
