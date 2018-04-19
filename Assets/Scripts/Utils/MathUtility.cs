using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class MathUtility  {

	public enum  PlaneLineContainmentType
	{
		//平行
		Parallel,
		//相交
		Intersect,
		//包含
		Contain,
	}
	public static PlaneLineContainmentType GetPlaneLineContainmentType(Vector3 linePos, Vector3 lineDir, Vector3 planePos, Vector3 planeNorVector3)
	{
		float denominator = Vector3.Dot( lineDir, planeNorVector3 ) ;
		float molecule = Vector3.Dot( planePos - linePos, planeNorVector3 );
		//直线与平面平行
		if( denominator == 0 )
		{
			if( molecule == 0 )
			{
				//直线在平面上
				return PlaneLineContainmentType.Contain;
			}
			else
			{
				return PlaneLineContainmentType.Parallel;
			}
		}
		else
		{
			return PlaneLineContainmentType.Intersect;
		}
	}

	public static bool GetPlaneLineIntersectPoint(Vector3 linePos, Vector3 lineDir, Vector3 planePos, Vector3 planeNorVector3 , out  Vector3 intersectPoint)
	{
		StringBuilder builder = new StringBuilder();
		intersectPoint = Vector3.zero;
		float denominator = Vector3.Dot( lineDir, planeNorVector3 ) ;
		Vector3 diff = planePos - linePos;
		float molecule = Vector3.Dot(diff, planeNorVector3 );
		builder.AppendFormat( "linepos:{0}, lineDir:{1}, planePos:{2}, planeNorVector:{3} , denominator:{4}, molecule:{5}, planePos - linePos :{6}",
		                      linePos, lineDir, planePos, planeNorVector3 , denominator, molecule, planePos - linePos);

		if( denominator != 0 )
		{
			float d = molecule / denominator;
			intersectPoint = (d * lineDir )+ linePos;
			builder.Append( "有交点:" + d + "  pos:" + intersectPoint );
			Debug.Log( builder.ToString() );
			return true;
		}
		Debug.Log( builder.ToString() );
		return false;
	}
}
