using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpriteFlipCtrl : MonoBehaviour
{

	public SkeletonAnimation SkeletonAnim;

	public Transform forwardTransform;

	private Vector3 lastPos;

	[Range(0,0.5f)]
	public float minDis = 0.01f;
	[Range(0,0.5f)]
	public float minXOffset = 0.005f;

	// Use this for initialization
	void Start ()
	{
		lastPos = forwardTransform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		var pos = forwardTransform.position;
		Flip(lastPos, pos);

		lastPos = pos;
	}

	public void Flip(Vector3 lastPos, Vector3 pos)
	{
		var dis = pos - lastPos;
		dis.y = 0;
		Vector3 v = Vector3.Project(dis, forwardTransform.right);
		if (dis.magnitude > minDis && Mathf.Abs(v.x) > minXOffset )
		{
			
			TurnLeft(v.x<0);
		}
	}

	void TurnLeft( bool faceLeft )
	{
		if( SkeletonAnim.skeleton.flipX != faceLeft )
			SkeletonAnim.skeleton.flipX = faceLeft;
	}

	private void OnGUI()
	{
		Debug.DrawRay(forwardTransform.position, forwardTransform.right);
	}
}
