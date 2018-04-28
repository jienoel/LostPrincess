using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiaryConfig",menuName = "Level/Diary", order = 30)]
public class DiaryConfig : ScriptableObject
{

	public DiaryItem[] items;
	
	public DiaryItem GetDiaryItem(int id)
	{
		if (items == null)
			return null;
		for (int i = 0; i < items.Length; i++)
		{
			DiaryItem item = items[i];
			if (item.id == id)
				return item;
		}

		return null;
	}
}
