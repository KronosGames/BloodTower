using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerMovementController movementController = null;

    PlayerStateManager playerState = null;

    Animation myAnimation = null;

    // アニメーションID、名前(string)、ブレンドするか、ブレンド時間をまとめたデータ構造を作るべきかも
    // 外部データで読み込む？
    enum AnimationID
    {
        Idling = 0,
        Walking,
        Run,
        Jump,
        Attack00,
        Attack01,
        Dead,
        Hit
    }

    [System.Serializable]
    class AnimationBlendData
    {
        public AnimationID animationID;
        public string animationName;
        public bool isBlend;
        public float blendTime = 1.0f;
    }

    [SerializeField]
    AnimationBlendData[] animationArray;


    /// <summary>
    /// 前フレームのアニメーションを記憶する
    /// </summary>
    AnimationID lastAnimationID = AnimationID.Idling;

    /// <summary>
    /// 現在のアニメーションを意味する
    /// </summary>
    AnimationID nowAnimationID = AnimationID.Idling;

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
        lastAnimationID = nowAnimationID = AnimationID.Idling;
        myAnimation.Play("Idling");
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
            nowAnimationID = AnimationID.Hit;
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
                nowAnimationID = AnimationID.Attack00;
                break;

            case 1:
                nowAnimationID = AnimationID.Attack01;
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
            nowAnimationID = AnimationID.Dead;
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
                nowAnimationID = AnimationID.Idling;
                break;

            case PlayerMovementController.MOVE_TYPE.WALKING:
                nowAnimationID = AnimationID.Walking;
                break;

            case PlayerMovementController.MOVE_TYPE.SPRINT:
                nowAnimationID = AnimationID.Run;
                break;

            case PlayerMovementController.MOVE_TYPE.JUMPING:
                nowAnimationID = AnimationID.Jump;
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

        AnimationBlendData nextAnimation = animationArray[(int)nowAnimationID];


        if (nextAnimation.isBlend)
        {
            myAnimation.CrossFade(nextAnimation.animationName, nextAnimation.blendTime);
        }
        else
        {
            myAnimation.Play(nextAnimation.animationName);
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
