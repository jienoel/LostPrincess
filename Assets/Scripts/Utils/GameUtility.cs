using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Utils
{
    public static class GameUtility {

	    //根据地图计算正交摄像机的边界，保证摄像机不超过地图边界
	    public static void CalculateOrthographicCameraBorderBySprite(Camera camera, SpriteRenderer spriteRenderer, out float minX, out  float maxX, out float minY, out float maxY)
	    {
		    float vertExtent = camera.orthographicSize;
		    float horzExtent = vertExtent * Screen.width / Screen.height;
		    Vector3 worldSize = SpriteUtility.GetWorldSizeOfSprite( spriteRenderer ); 
		    // Calculations assume map is position at the origin
		    minX = horzExtent - worldSize.x / 2.0f;
		    maxX = worldSize.x / 2.0f - horzExtent;
		    minY = vertExtent - worldSize.y / 2.0f;
		    maxY = worldSize.y / 2.0f - vertExtent;
	    }

	    public static void CalculateMoveBorderBySpriteInPlane(Transform[] mapBorders, Transform planeTransform , out  Vector3 minPos, out  Vector3 maxPos)
	    {
		    Vector3 planePos = planeTransform.position;
		    Vector3 planeNorVector3 = planeTransform.up;
		    Vector3 intersectPos;
		    minPos = Vector3.positiveInfinity;
		    maxPos = Vector3.negativeInfinity;
		    foreach( var border in mapBorders )
		    {
			    Vector3 linePos = border.position;
			    Vector3 lineDir = -border.transform.forward;
			    if( MathUtility.GetPlaneLineIntersectPoint( linePos, lineDir, planePos, planeNorVector3, out intersectPos ) )
			    {
				    intersectPos = planeTransform.InverseTransformPoint( intersectPos );
				    minPos = Vector3.Min( minPos,  intersectPos);
				    maxPos = Vector3.Max( maxPos, intersectPos );
			    }
		    }
	    }

    }
}
