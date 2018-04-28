using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

	public DiaryView diaryView;

	public Game game;
	// Use this for initialization
	void Start () {
		Messenger.AddListener<int>(MessageName.MN_Book_Click, OnBookClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBookClick(int bookId)
	{
		DiaryItem item = game.gameModel.levelConfig.GetDiaryItem(bookId);
		if (item == null)
		{
			Debug.LogError("没找到对应的日记页，请检查配置");
			return;
		}
		Messenger.Broadcast(MessageName.MN_Diary_Read, true);
		diaryView.ShowDialog(item);
	}

	private void OnDestroy()
	{
		Messenger.RemoveListener<int>(MessageName.MN_Book_Click, OnBookClick);
	}
}
