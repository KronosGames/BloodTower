using UnityEngine;
using System.Collections;

public class BossEnemyManager : ManagerBase
{
    static BossEnemyInfo currentBossEnemy = null;
    static bool isNone = true;

	void Start ()
    {
        InitManager(this, MANAGER_ID.BOSS_ENEMY);
        currentBossEnemy = null;
        isNone = true;
    }

    //  --------------------------------------
    //  公開用関数
    //  --------------------------------------

    static public void Setup(ref BossEnemyInfo infoData)
    {
        currentBossEnemy = infoData;

        isNone = currentBossEnemy == null;

        // 存在しない
        if (isNone)
        {
            return;
        }

        currentBossEnemy.Setup();
        UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.BOSS_ENEMY_INFO);

    }

    static public void SetHp(int hp)
    {
        if (isNone) return;

        currentBossEnemy.GetParam().hp = hp;
    }

    static public BOSS_ENEMY_ID GetID()
    {
        if (isNone) return BOSS_ENEMY_ID.NULL;

        return currentBossEnemy.GetParam().id;
    }

    static public string GetName()
    {
        if (isNone) return "存在しない";

        return currentBossEnemy.GetParam().name;
    }

    static public int GetHp()
    {
        if (isNone) return 0;

        return currentBossEnemy.GetParam().hp;
    }

    static public int GetMaxHp()
    {
        if (isNone) return 0;

        return currentBossEnemy.GetParam().maxHp;
    }

    static public int GetAttackPower()
    {
        if (isNone) return 0;

        return currentBossEnemy.GetParam().attack;
    }

    static public int GetDefense()
    {
        if (isNone) return 0;

        return currentBossEnemy.GetParam().defense;
    }

    static public int GetMoveSpeed()
    {
        if (isNone) return 0;

        return currentBossEnemy.GetParam().moveSpeed;
    }

    // 死亡したかどうか
    static public bool IsDead()
    {
        return GetHp() <= 0;
    }
}
