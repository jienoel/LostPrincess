using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Graphs;
using UnityEngine;
using Utils;


public class BounderTest : MonoBehaviour
{

	public Transform target;
	public bool isPrint = false;
	private SpriteRenderer spriteRenderer;
	public Transform[] borders;

	public Transform player;
	public Transform plane;
	private Bounds _bounds;
	public List<Vector3> interPoses = new List<Vector3>();
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		PrintPlaneBorder();
	}

	void Update () {
		if( isPrint )
		{
			PrintInfo();
		}
		
	}

	void LateUpdate()
	{
//		_bounds = GetComponent<BoxCollider>().bounds;
//		if( !_bounds.Contains( player.position ) )
//		{
//			Debug.Log( "超出可视范围了" );
//		}
	}

	void PrintInfo()
	{
		Debug.Log( "Sprite World Size:" + SpriteUtility.GetWorldSizeOfSprite( spriteRenderer ) +"  Screen Size:"+ SpriteUtility.GetScreenSizeOfSprite( spriteRenderer ) );
		Debug.Log( "Camera orthographicSize:"+Camera.main.orthographicSize +"  aspectSize: "+Camera.main.aspect + " PixelWidth:"+Camera.main.pixelWidth+"  PixelHeight:"+Camera.main.pixelHeight);
		float minX;
		float minY;
		float maxX;
		float maxY;
		GameUtility.CalculateOrthographicCameraBorderBySprite( Camera.main, spriteRenderer, out minX, out maxX, out minY, out maxY );
		Debug.Log( "minX:"+minX+"  MaxX:"+maxX+"  minY:"+minY+"  maxY:"+maxY );
	}


	void PrintPlaneBorder()
	{
		Vector3 planePos = plane.transform.position;
		Vector3 planeNorVector3 = plane.transform.up;
		Vector3 intersectPos;
		foreach( var border in borders )
		{
			Vector3 linePos = border.position;
			Vector3 lineDir = -border.transform.forward;
			if( MathUtility.GetPlaneLineIntersectPoint( linePos, lineDir, planePos, planeNorVector3, out intersectPos ) )
			{
//				GameObject.CreatePrimitive( PrimitiveType.Cube ).transform.position = intersectPos;
				interPoses.Add( plane.transform.InverseTransformPoint( intersectPos ) );
			}
		}
	}

	private void OnDrawGizmos()
	{
		if(interPoses.Count<4)
			return;
		Gizmos.DrawLine( interPoses[0] , interPoses[1] );
		Gizmos.DrawLine( interPoses[1], interPoses[3] );
		Gizmos.DrawLine( interPoses[3], interPoses[2] );
		Gizmos.DrawLine( interPoses[2], interPoses[0] );
	}
}
