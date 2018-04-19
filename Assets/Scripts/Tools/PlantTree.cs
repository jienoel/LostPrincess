using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PlantTree : MonoBehaviour
{
	private static Game _game;
	private static Game game
	{
		get
		{
			if( _game == null )
			{
				_game = FindObjectOfType<Game>();
			}
			return _game;
		}
	}
		[MenuItem( "Tools/PlantTree %t" )]
	public static void PlantTreesInScree()
	{
		if( _game == null )
		{
			_game = FindObjectOfType<Game>();
		}
	}

	private void Update()
	{
		if( Input.GetButtonDown( "Fire1" ) )
		{

//			PlantTreeAtPosition();
		}
	}

	void OnGUI()
	{
		if(Application.isPlaying)
			return;
		Event e = Event.current;
		if( e.type == EventType.Layout || e.type == EventType.Repaint )
		{
			EditorUtility.SetDirty( this ); // this is important, if omitted, "Mouse down" will not be display
		}
		else if( e.type == EventType.MouseDown && e.button == 0 )
		{
			PlantTreeAtPosition( e.mousePosition );
		}

	}

	static	void PlantTreeAtPosition(Vector3 mousePos)
	{
		Debug.Log( "Set Tree " +mousePos);
		Ray ray = Camera.main.ScreenPointToRay( mousePos );
		RaycastHit hit;
		if( Physics.Raycast( ray, out hit, 1000 ) )
		{
			GameObject tree = GameObject.CreatePrimitive( PrimitiveType.Cube );
//			Tree tree = GameObject.Instantiate( game.gameModel.treePrefab );
//			tree.transform.parent = game.gameModel.treeParent.transform;
			tree.transform.position = hit.point;
		}
		
	}
}
