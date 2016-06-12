using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossEnemyMapHierarchyData
{
	public EnemyInfo enemy = null;
    public bool isNone = true;
	public int hierarchyCount = -1;
}

public class BossEnemyManager : ManagerBase
{
	List<BossEnemyMapHierarchyData> hierarchyList = new List<BossEnemyMapHierarchyData>();

	void Start ()
    {
        InitManager(this, MANAGER_ID.BOSS_ENEMY);
    }

	//  --------------------------------------
	//  公開用関数
	//  --------------------------------------

	public void Setup()
	{
		hierarchyList.Clear();

	}

	// 登録する。
	public void Register(ref EnemyInfo bossEnemy,int hierarchyCount)
    {
		BossEnemyMapHierarchyData addData = new BossEnemyMapHierarchyData();
		addData.isNone = bossEnemy == null;
		if (!addData.isNone)
		{
			addData.enemy = bossEnemy;
			addData.enemy.Setup(hierarchyCount);
		}
		addData.hierarchyCount = hierarchyCount;
		hierarchyList.Add(addData);
	}

	// 現在の階層の武器一覧が取得できる。
	public EnemyInfo GetBossEnemyInfo(int hierarchyCount)
	{
		return hierarchyList[hierarchyCount].enemy;
	}

	public EnemyParam GetParam(int hierarchyCount)
	{
		return GetBossEnemyInfo(hierarchyCount).GetParam();
	}

	public void Damage(int hierarchyCount,int damageValue)
    {
		EnemyInfo info = GetBossEnemyInfo(hierarchyCount);
		if (info == null) return;

        EnemyParam param = info.GetParam();
        param.hp -= damageValue;

        param.hp = System.Math.Max(param.hp, 0);
    }

    // 死亡したかどうか
    public bool IsDead(int hierarchyCount)
    {
        return GetParam(hierarchyCount).hp <= 0;
    }
}
