using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MeshSave : Editor {

	[MenuItem( "Tools/Save Circle Mesh" )]
	static void SaveCircleMesh()
	{
		string path =	EditorUtility.SaveFilePanel("Mesh", Application.dataPath, "circle","asset");
		if(string.IsNullOrEmpty(path))
			return;
		path = FileUtil.GetProjectRelativePath(path);
		Mesh mesh = FogTest.GenerateCircleMesh();
		AssetDatabase.CreateAsset( mesh, path);
		AssetDatabase.SaveAssets();
	}
}
