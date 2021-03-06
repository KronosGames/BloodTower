﻿using UnityEngine;
using System.Collections;

public class EnemyStatusManager : MonoBehaviour {

    /// <summary>
    /// 生きているかどうか
    /// true...生きている　false...死んでいる
    /// </summary>
    public bool IsAlive { get; private set;}


    /// <summary>
    /// 状態異常にかかっているかどうか
    /// </summary>
    bool[] isInTheStatus = new bool[(int)ENEMY_STATUS.SENTINEL]; 

    /// <summary>
    /// 状態異常　蓄積値
    /// </summary>
    int[] statusAccumulation = new int[(int)ENEMY_STATUS.SENTINEL];

    /// <summary>
    /// 自信のパラメータ
    /// </summary>
    EnemyParam thisParam = null;

    public void Setup(EnemyInfo enemyInfo)
    {
        IsAlive = true;
        InitializeParams(ref enemyInfo);
        InitializeHealth();
    }
	
	void FixedUpdate ()
    {
        if (!IsAlive) return;

        CheckDead();

    }

    // 状態異常管理部 ///////////////////////////////////// 

    /// <summary>
    /// 各種パラメータ初期化
    /// </summary>
    void InitializeParams(ref EnemyInfo enemyInfo)
    {
        if(enemyInfo == null)
        {
            Debug.LogError("EnemyInfo is null!");
            return;
        }

        thisParam = enemyInfo.GetParam();

        InitializeStatus();
    }

    /// <summary>
    /// 状態の初期化
    /// </summary>
    void InitializeStatus()
    {
        for (int index = 0; index < (int)ENEMY_STATUS.SENTINEL; ++index)
        {
            isInTheStatus[index] = false;
            statusAccumulation[index] = 0;
        }
    }

    /// <summary>
    /// 状態異常蓄積値を加算する
    /// </summary>
    /// <param name="enemyStatus">状態異常の種類</param>
    /// <param name="addValue">加算する量</param>
    public void AddStatusAccumulation(ENEMY_STATUS enemyStatus,int addValue)
    {
        int statusID = (int)enemyStatus;
        // 状態異常にかからない場合は処理を続けない
        if (!thisParam.canInTheStatus[statusID])
        {
            return;
        }
        
        // すでに状態異常だった場合は処理を続けない
        if(isInTheStatus[statusID])
        {
            return;
        }

        // 加算
        statusAccumulation[statusID] += addValue;

        // 状態異常を管理し、クリップする
        if(thisParam.statusResistance[statusID] < statusAccumulation[statusID])
        {
            isInTheStatus[statusID] = true;
            statusAccumulation[statusID] = thisParam.statusResistance[statusID];
        }

        if(0 > statusAccumulation[statusID])
        {
            isInTheStatus[statusID] = false;
            statusAccumulation[statusID] = 0;
        }
        
    }

    /// <summary>
    /// HPを確認して、0以下の場合死んでいる状態にする
    /// </summary>
    void CheckDead()
    {
        if (IsHealthUnderZero())
        {
            IsAlive = false;
            InitializeStatus();
        }
    }

    // HP管理部 ///////////////////////////////////////////

    int maxHealth = int.MaxValue;

    public int NowHealth { get; set; }

    void InitializeHealth()
    {
        maxHealth = thisParam.maxHp;
        NowHealth = thisParam.hp;
    }


    /// <summary>
    /// HPに加算する
    /// </summary>
    /// <param name="addValue"></param>
    /// <returns>計算後のHP量</returns>
    public int AddHealth(int addValue)
    {
        NowHealth += addValue;


        return ClipHealth();

    }

    /// <summary>
    /// HPが0以下かどうか
    /// </summary>
    /// <returns>true...0以下 false...0より大きい</returns>
    bool IsHealthUnderZero()
    {

        if (NowHealth <= 0)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// HPを0から最大値の間に合わせる
    /// </summary>
    /// <returns>合わせた後の数値</returns>
    int ClipHealth()
    {
        NowHealth = Mathf.Min(NowHealth, maxHealth);
        NowHealth = Mathf.Max(NowHealth, 0);

        return NowHealth;
    }



}
