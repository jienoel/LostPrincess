using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TreeItem
{
	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;
}

[CreateAssetMenu(fileName = "TreeConfig",menuName = "Level/TreeConfig", order = 30)]
public class TreeConfig : ScriptableObject
{
	public List<TreeItem> trees = new List<TreeItem>();
}
