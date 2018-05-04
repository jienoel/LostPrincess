using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Game))]
public class GameInspector : Editor
{
	private Game _game;
	private string path;
	private void OnEnable()
	{
		_game = (Game) target;
		path =Path.Combine( Application.dataPath, "Data/Level1");
	}

	private void OnDisable()
	{
		_game = null;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("ExportLevel"))
		{
			
		}

		if (GUILayout.Button("ImpoetLevel"))
		{
			
		}
	}

	T CreateWhenNull<T>(T input) where  T : ScriptableObject
	{
		if (input == null)
		{
			input = ScriptableObject.CreateInstance<T>();
			input.name = typeof(T).FullName + "_" + DateTime.Now.ToString();
		}

		return input;
	}

	public void ExportLevel(LevelConfig levelConfig)
	{
		levelConfig = CreateWhenNull(levelConfig);

	}

	public TreeConfig ExportTree(TreeConfig treeConfig)
	{

		treeConfig = CreateWhenNull(treeConfig);
		return treeConfig;
	}
	
	
	
}
