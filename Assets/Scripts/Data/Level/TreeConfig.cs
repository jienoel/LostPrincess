using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TreeVO
{
	public Vector3 position;
	public Vector3 rotation;
	public Vector3 scale;
}

public class TreeConfig : ScriptableObject
{
	public List<TreeVO> trees = new List<TreeVO>();
}
