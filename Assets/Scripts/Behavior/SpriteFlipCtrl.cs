using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class SpriteFlipCtrl : MonoBehaviour
{

	public SkeletonAnimation SkeletonAnim;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		TurnLeft( SkeletonAnim.transform.forward.x >=0 );
	}

	void TurnLeft( bool faceLeft )
	{
		if( SkeletonAnim.skeleton.flipX != faceLeft )
			SkeletonAnim.skeleton.flipX = faceLeft;
	}
}
