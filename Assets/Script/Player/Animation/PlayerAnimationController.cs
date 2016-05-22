using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerMovementController movementController = null;

    PlayerStateManager playerState = null;

    Animation myAnimation = null;

    [SerializeField]
    AnimationDatabase database = null;

    /// <summary>
    /// 前フレームのアニメーションを記憶する
    /// </summary>
    PlayerAnimationParam.ANIMATION_ID lastAnimationID = PlayerAnimationParam.ANIMATION_ID.Idling;

    /// <summary>
    /// 現在のアニメーションを意味する
    /// </summary>
    PlayerAnimationParam.ANIMATION_ID nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Idling;

    // Use this for initialization
    void Start()
    {
        movementController = transform.parent.GetComponent<PlayerMovementController>();
        playerState = transform.parent.GetComponent<PlayerStateManager>();
        myAnimation = GetComponent<Animation>();
        BeginWithIdling();
    }

    /// <summary>
    /// 初期状態をアイドルアニメ―ションで開始します
    /// </summary>
    void BeginWithIdling()
    {
        lastAnimationID = nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Idling;
        myAnimation.Play(database.animationList[(int)nowAnimationID].clip.name);
    }

    float animationCoolTime = 0;

    /// <summary>
    /// ノックバックさせる
    /// 0.5f秒くらいがちょうどいい
    /// </summary>
    /// <param name="knockBackSecond">ノックバックアニメーションを実行する時間</param>
    public void CallHitAnimation(float knockBackSecond)
    {
        if (animationCoolTime < 0f)
        {
            nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Hit;
            animationCoolTime = knockBackSecond;
        }
    }

    public void BeginAttack(int attackID,float attackMotionTime)
    {
        if (animationCoolTime > 0f)
        {
            return;
        }
        switch(attackID)
        {
            case 0:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Attack00;
                break;

            case 1:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Attack01;
                break;

            default:
                Debug.LogError("想定されていない攻撃を呼び出しています。");
                break;
                    
        }

        animationCoolTime = attackMotionTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playerState.IsAlive)
        {
            nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Dead;
        }

        UpdateState();


        UpdateAnimation();

    }

    void UpdateState()
    {
        if(animationCoolTime >= 0f)
        {
            animationCoolTime -= Time.deltaTime;
         
            return;
        }

        if (!playerState.IsAlive)
        {
            return;
        }

        switch (movementController.moveType)
        {
            case PlayerMovementController.MOVE_TYPE.IDOLING:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Idling;
                break;

            case PlayerMovementController.MOVE_TYPE.WALKING:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Walking;
                break;

            case PlayerMovementController.MOVE_TYPE.SPRINT:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Run;
                break;

            case PlayerMovementController.MOVE_TYPE.JUMPING:
                nowAnimationID = PlayerAnimationParam.ANIMATION_ID.Jump;
                break;

        }

    }

    /// <summary>
    /// 現在のアニメーションに更新する
    /// </summary>
    void UpdateAnimation()
    {

        if (lastAnimationID == nowAnimationID)
        {
            return;
        }

        AnimationData nextAnimation = database.animationList[(int)nowAnimationID];

        if (nextAnimation.isBlend)
        {
            myAnimation.CrossFade(nextAnimation.clip.name, nextAnimation.blendTime);
        }
        else
        {
            myAnimation.Play(nextAnimation.clip.name);
        }

        //for (int index = 0; index < animationArray.Length; ++index)
        //{
        //    if (animationArray[index].animationID == NowAnimationID)
        //    {

        //        if (animationArray[index].isBlend)
        //        {
        //            myAnimation.Blend(animationArray[index].animationName, 1.0f, animationArray[index].blendTime);
        //        }
        //        else
        //        {
        //            myAnimation.Play(animationArray[index].animationName);
        //        }


        //        intervalTime = animationArray[index].IntervalForNext;

        //        break;
        //    }

        //}

        lastAnimationID = nowAnimationID;
    }

}
