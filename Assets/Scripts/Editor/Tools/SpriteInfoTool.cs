using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utils;

public class SpriteInfoTool  {


	[MenuItem( "Tools/打印SpriteRender的WorldSize" )]
	public static void PlantTreesInScree()
	{
		SpriteRenderer spriteRenderer = Selection.activeGameObject.GetComponentInChildren<SpriteRenderer>();
		if( spriteRenderer != null )
		{
			Debug.Log( "sprite:" + spriteRenderer.gameObject + " worldSize: " + SpriteUtility.GetWorldSizeOfSprite( spriteRenderer ) );
		}
		else
		{
			Debug.Log( "sprite is null!" );
		}
	}
}
