using UnityEngine;
using System.Collections;

/// <summary>
/// 傀儡エネミーの攻撃手段を決める
/// </summary>
public class MarionetteAttackBrain : MonoBehaviour {

    /// <summary>
    /// 攻撃目標
    /// </summary>
    public Transform Target = null;

    /// <summary>
    /// 自分のナビメッシュエージェント
    /// </summary>
    NavMeshAgent navMeshAgent = null;

    [SerializeField]
    Transform weapon;

    AggressionController aggressionController = null;

    [SerializeField]
    MarionetteAnimationController marionetteAnimationController = null;

    WeaponInfo weaponInfo = null;

    enum AttackID
    {
        Normal01 = 0,
        Normal02,
        Normal03,
        Sentinel
    }

    // Use this for initialization
    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aggressionController = weapon.GetComponent<AggressionController>();
        weaponInfo = weapon.GetComponent<WeaponInfo>();

        

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	    if(null == Target)
        {
            return;
        }

        if(null == aggressionController.MyWeaponParam)
        {
            aggressionController.MyWeaponParam = weaponInfo.GetParam();
        }

        if (navMeshAgent.stoppingDistance > Vector3.Distance(Target.position,transform.position))
        {
            int mask = CheckInsideAttackArea();

            // 通常攻撃01       
            if ((mask & (1 << (int)AttackID.Normal01)) != 0)
            {
                Debug.Log("通常攻撃01の実行");
                aggressionController.ActivateWeapon(1.0f,transform.tag);
                marionetteAnimationController.BeginAttack(0, 1.0f);
            }

            // 通常攻撃02
            if ((mask & (1 << (int)AttackID.Normal02)) != 0)
            {
                Debug.Log("通常攻撃02の実行");
                aggressionController.ActivateWeapon(1.5f, transform.tag);
                marionetteAnimationController.BeginAttack(1, 1.5f);
            }

            // 通常攻撃03
            if ((mask & (1 << (int)AttackID.Normal03)) != 0)
            {
                Debug.Log("通常攻撃03の実行");
            }


        }

    }

    int CheckInsideAttackArea()
    {
        int attackListBitMask = 0;

        // 攻撃範囲に敵がいるかを確認する
        // AttackID.Normal01
        if (CollisionChecker2D.IsInside(Target.position, new CollisionChecker2D.CircleAreaData(transform.position, 5f)))
        {
            attackListBitMask |= 1 << (int)AttackID.Normal01;
        }

        // AttackID.Normal02
        if (CollisionChecker2D.IsInside(Target.position, new CollisionChecker2D.FanAreaData(transform.position, 0f, 10f, 45f - transform.eulerAngles.y, 90f)))
        {
            attackListBitMask |= 1 << (int)AttackID.Normal02;
        }

        // AttackID.Normal03
        if (CollisionChecker2D.IsInside(Target.position, new CollisionChecker2D.FanAreaData(transform.position, 7.5f, 18.5f, 75f - transform.eulerAngles.y, 40f)))
        {
            attackListBitMask |= 1 << (int)AttackID.Normal03;
        }

        return attackListBitMask;
    }

}

