using UnityEngine;
using System.Collections;

public class MarionetteAnimationController : MonoBehaviour {

    NavMeshAgent navMeshAgent = null;

    EnemyStatusManager enemyState = null;

    Animation myAnimation = null;

    [SerializeField]
    AnimationDatabase database = null;

    /// <summary>
    /// 前フレームのアニメーションを記憶する
    /// </summary>
    MarionetteAnimationParam.ANIMATION_ID lastAnimationID = MarionetteAnimationParam.ANIMATION_ID.Idling;

    /// <summary>
    /// 現在のアニメーションを意味する
    /// </summary>
    MarionetteAnimationParam.ANIMATION_ID nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Idling;

    float animationCoolTime = 0;

    // Use this for initialization
    void Start () {
        navMeshAgent = transform.parent.GetComponent<NavMeshAgent>();
        enemyState = transform.parent.GetComponent<EnemyStatusManager>();
        myAnimation = GetComponent<Animation>();
        BeginWithIdling();
    }

    /// <summary>
    /// 初期状態をアイドルアニメ―ションで開始します
    /// </summary>
    void BeginWithIdling()
    {
        lastAnimationID = nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Idling;

        myAnimation.Play(database.animationList[(int)nowAnimationID].clip.name);
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!enemyState.IsAlive)
        {
            nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Dead;
        }

        UpdateState();


        UpdateAnimation();
    }


    public void BeginAttack(int attackID, float attackMotionTime)
    {
        if (animationCoolTime > 0f)
        {
            return;
        }
        switch (attackID)
        {
            case 0:
                nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Attack00;
                break;

            case 1:
                nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Attack01;
                break;

            default:
                Debug.LogError("想定されていない攻撃を呼び出しています。");
                break;

        }

        animationCoolTime = attackMotionTime;

    }


    void UpdateState()
    {
        if (animationCoolTime >= 0f)
        {
            animationCoolTime -= Time.deltaTime;

            return;
        }

        if (!enemyState.IsAlive)
        {
            return;
        }

        if (navMeshAgent.stoppingDistance > Vector3.Distance(transform.position,navMeshAgent.destination))
        {
            nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Idling;
        }
        else
        {
            nowAnimationID = MarionetteAnimationParam.ANIMATION_ID.Run;
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
