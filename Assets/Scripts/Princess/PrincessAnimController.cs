using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PrincessAnimController:MonoBehaviour
{
    public const string Speed = "speed";
    public const string Die = "die";
    public const string Hurt = "hurt";
    public const string Attack = "attack";
    #region  Inspector

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string attackAnimation;

    [SpineAnimation]
    public string waitAnimation;

    [SpineAnimation]
    public string hurtAnimation;

    [SpineAnimation]
    public string dieAnimation;

    #endregion

    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private TrackEntry currAnimation;
    void Start()
    {
        skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    public void Flip(bool right)
    {
        skeleton.flipX = right;
    }

    public void SetRun()
    {
        
    }

    public void SetWait()
    {
        
    }

    public void SetDie()
    {
        
    }

    public void SetAttack()
    {
        
    }

    public void SetHurt()
    {
        
    }


    void SetAnimaiton( string animation , bool loop )
    {
        if( currAnimation == null  || currAnimation.IsComplete)
        {
            currAnimation = spineAnimationState.SetAnimation( 0, animation, loop );
        }
        else
        {
           
        }
    }


}
