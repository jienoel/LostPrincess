using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[ExecuteInEditMode]
public class PlantElement : MonoBehaviour
{
	public LayerMask cullingMask;
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
		if(Game.Instance.operateType == Game.OperateType.PlantTree && Input.GetButtonDown( "Fire1" ) )
		{

			PlantTreeAtPosition(Input.mousePosition);
		}
		else if(Game.Instance.operateType == Game.OperateType.PlantFire && Input.GetButtonDown( "Fire1" ) )
		{

			PlantBoneFire(Input.mousePosition);
		}
	}

/*	void OnGUI()
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

	}*/

		void PlantTreeAtPosition(Vector3 mousePos)
	{
		Ray ray = Camera.main.ScreenPointToRay( mousePos );
		RaycastHit hit;
		int layerMask = LayerMask.NameToLayer("Plane") << 8 ;
		Debug.Log(layerMask.ToString()+"  "+cullingMask.value.ToString());
		if( Physics.Raycast( ray, out hit, 1000 , cullingMask.value) )
		{
		//	GameObject tree = GameObject.CreatePrimitive( PrimitiveType.Cube );
			Debug.Log( "Set Tree " +hit.point.ToString()+"  "+hit.collider.name);
		    Tree tree = GameObject.Instantiate( game.gameModel.treePrefab );
			tree.transform.parent = game.gameModel.treeParent.transform;
			tree.transform.position = hit.point;
			var localPos = tree.transform.localPosition;
			localPos.y = 0;
			tree.transform.localPosition = localPos;
			tree.transform.localRotation = Quaternion.identity;
		}
		
	}

	 void PlantBoneFire(Vector3 mousePos)
	{
		//Debug.Log( "Set BoneFire " +mousePos.ToString());
		Ray ray = Camera.main.ScreenPointToRay( mousePos );
		RaycastHit hit;
		if( Physics.Raycast( ray, out hit, 1000 ) )
		{
			//	GameObject tree = GameObject.CreatePrimitive( PrimitiveType.Cube );
			Debug.Log("Put fire: "+hit.collider.name);
			BoneFire fire = GameObject.Instantiate( game.gameModel.boneFirePrefab );
			fire.transform.parent = game.gameModel.boneFireParent.transform;
			fire.transform.position = hit.point;
			var localPos = fire.transform.localPosition;
			localPos.y = 0;
			fire.transform.localPosition = localPos;
			fire.transform.localRotation = Quaternion.identity;
			game.ChangeFog();
		}
	}
}
