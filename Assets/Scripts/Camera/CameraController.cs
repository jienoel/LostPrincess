using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CameraController : MonoBehaviour {

	public GameObject player;


	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	public SpriteRenderer mapRender;
	public Rect centerArea = new Rect(new Vector2(0.1f,0.1f), new Vector2(0.8f, 0.8f));
	private Vector3 centerAreaMin;
	private Vector3 centerAreaMax;
	void Start ()
	{
		GameUtility.CalculateOrthographicCameraBorderBySprite( Camera.main, mapRender, out minX, out maxX, out minY, out maxY );
		Debug.Log( "Player local pos:"+mapRender.transform.InverseTransformPoint( player.transform.position ) );
		CalculateCenterArea();
	}

	void CalculateCenterArea()
	{
		Vector2 screenLeftUpPos = new Vector2(centerArea.x * Screen.width, centerArea.y * Screen.height);
		Vector3 pos1 = Camera.main.ScreenToWorldPoint(screenLeftUpPos);
		
//		Debug.Log( "ScreenLeftPos:"+screenLeftUpPos+"  pos1:"+pos1 + " posRelativeCamera"+ mapRender.transform.InverseTransformPoint( pos1 ));
		Vector2 screenRightButtomPos = new Vector2(centerArea.xMax * Screen.width, centerArea.yMax * Screen.height);
		Vector3 pos2 = Camera.main.ScreenToWorldPoint( screenRightButtomPos );
//		Debug.Log( "ScreenRightButtomPos:"+screenRightButtomPos+" pos2:"+pos2 +" posRelativeCamera:"+ mapRender.transform.InverseTransformPoint( pos2 ));
		centerAreaMin = mapRender.transform.InverseTransformPoint( pos1);
		centerAreaMax = mapRender.transform.InverseTransformPoint( pos2 );
	}

	void LateUpdate ()
	{
		CheckPlayerPosChange();
	}

	void CheckPlayerPosChange()
	{
		Vector3 playerRelativeCamPos = mapRender.transform.InverseTransformPoint( player.transform.position );
		if( !IsPlayerOutOfCenterArea(playerRelativeCamPos) )
		{
			return;
		}
		Vector3 targetPos = playerRelativeCamPos;
		targetPos.z = transform.localPosition.z;
		MoveCamera(targetPos);
		CalculateCenterArea();
	}

	void MoveCamera(Vector3 targetPos)
	{
		transform.localPosition = Vector3.Lerp( transform.localPosition, targetPos, Time.deltaTime );
		var v3 = transform.localPosition;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.localPosition = v3;
	}

	bool IsPlayerOutOfCenterArea(Vector3 playerRelativeCamPos)
	{
		if( playerRelativeCamPos.x < centerAreaMin.x || playerRelativeCamPos.x > centerAreaMax.x || playerRelativeCamPos.y < centerAreaMin.y || playerRelativeCamPos.y > centerAreaMax.y)
		{
			return false;
		}
		return true;
	}
}
