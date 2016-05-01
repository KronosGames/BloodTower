using UnityEngine;
using System.Collections;

public class BossEnemyManager : ManagerBase
{
    static EnemyInfo currentBossEnemy = null;
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

    static public void Register(ref EnemyInfo infoData)
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

    static public void Damage(int damageValue)
    {
        if (isNone) return;

        EnemyParam param = currentBossEnemy.GetParam();
        param.hp -= damageValue;

        param.hp = System.Math.Max(param.hp, 0);
    }

    static public EnemyParam GetParam()
    {
        if (isNone) return null;

        return currentBossEnemy.GetParam();
    }
    
    // 死亡したかどうか
    static public bool IsDead()
    {
        return GetParam().hp <= 0;
    }
}
