using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase
{
    static EnemyInfo[] enemyList = null;

    void Start()
    {
        InitManager(this, MANAGER_ID.ENEMY);

        enemyList = null;
    }

    static EnemyInfo GetEnemy(ref GameObject target)
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            EnemyInfo data = enemyList[i];
            if (data.gameObject == target)
            {
                return data;
            }
        }

        return null;
    }

    //  ------------------------------------------------
    //  公開用 関数
    //  ------------------------------------------------

    static public void Setup(ref EnemyInfo[] infoList)
    {
        enemyList = infoList;

        for (int i = 0; i < enemyList.Length; i++)
        {
            EnemyInfo data = enemyList[i];
            data.Setup();
        }
    }

    static public void SetHp(ref GameObject target,int hp)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return;

        EnemyParam param = data.GetParam();
        param.hp = hp;
    }

    static public int GetHp(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return 0;

        EnemyParam param = data.GetParam();
        return param.hp;
    }

    static public int GetMaxHp(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return 0;

        EnemyParam param = data.GetParam();
        return param.maxHp;
    }

    static public int GetAttackPower(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return 0;

        EnemyParam param = data.GetParam();
        return param.attack;
    }

    static public int GetDefense(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return 0;

        EnemyParam param = data.GetParam();
        return param.defense;
    }

    static public int GetMoveSpeed(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return 0;

        EnemyParam param = data.GetParam();
        return param.moveSpeed;
    }

    static public string GetName(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return "NULL";

        EnemyParam param = data.GetParam();
        return param.name;
    }

    static public ENEMY_ID GetEnemyID(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return ENEMY_ID.NULL;

        EnemyParam param = data.GetParam();
        return param.id;
    }

    static public bool IsDead(ref GameObject target)
    {
        EnemyInfo data = GetEnemy(ref target);
        if (data == null) return true;

        EnemyParam param = data.GetParam();
        return param.hp <= 0;
    }
}
