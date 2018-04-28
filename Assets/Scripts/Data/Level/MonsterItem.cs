using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropType
{
	None,
	Diary,
	Jewel
}


[System.Serializable]
public class MonsterItem
{
	public int id;
	public int bodyLength = 1;
	public int color = 1;
	public DropType dropType;
	public int dropID;
	public int Hp;
	public int DamageValue;
	public float moveRadius;
	public Vector3 spawnPos;
}
