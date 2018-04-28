using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig",menuName = "Level/LevelConfig", order = 30)]
public class LevelConfig : ScriptableObject
{
	public TreeConfig trees;
	public MonsterConfig monsters;
	public DiaryConfig Diaries;
	public InnerOSConfig innerOses;

	public InnerOSItem GetInnerOsItem(int id)
	{
		if (innerOses == null)
			return null;
		return innerOses.GetOsItem(id);
	}

	public DiaryItem GetDiaryItem(int id)
	{
		if (Diaries == null)
			return null;
		return Diaries.GetDiaryItem(id);
	}
}
