using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NearstWeaponData
{
    public WeaponInfo weaponInfo = null;
    public float distance = 0;
}

public static class BattleMapUtility
{
	// バトルマップの初期化
	static public void BattleSetup()
	{
		// 初期化順に配置していく
		MapManager map = ManagerBase.GetManager<MapManager>(MANAGER_ID.MAP);
		map.Setup();

		BattleManager battle = ManagerBase.GetManager<BattleManager>(MANAGER_ID.BATTLE);
		battle.Setup();

		StoneItemManager stoneItem = ManagerBase.GetManager<StoneItemManager>(MANAGER_ID.STONE_ITEM);
		stoneItem.Setup();

		WeaponManager weapon = ManagerBase.GetManager<WeaponManager>(MANAGER_ID.WEAPON);
		weapon.Setup();

		EnemyManager enemy = ManagerBase.GetManager<EnemyManager>(MANAGER_ID.ENEMY);
		enemy.Setup();

		BossEnemyManager boss = ManagerBase.GetManager<BossEnemyManager>(MANAGER_ID.BOSS_ENEMY);
		boss.Setup();
		
		// マップに配置されているオブジェクトをすべて設定する。
		int mapCount = BattleMapUtility.GetMapCount();
		for (int i = 0; i < mapCount; i++)
		{
			MapInfoData mapInfo = BattleMapUtility.GetMapInfo(i);

			EnemyInfo[] enemyList = mapInfo.trans.GetComponentsInChildren<EnemyInfo>();
			enemy.Register(ref enemyList, i);

			EnemyInfo bossEnemy = mapInfo.trans.GetComponentInChildren<EnemyInfo>();
			boss.Register(ref bossEnemy,i);

			WeaponInfo[] weaponList = mapInfo.trans.GetComponentsInChildren<WeaponInfo>();
			weapon.Register(ref weaponList, i);

			StoneItemInfo[] stoneItemList = mapInfo.trans.GetComponentsInChildren<StoneItemInfo>();
			stoneItem.Register(ref stoneItemList, i);
		}
	}

	#region common
	// 切り替えるシーンの種類を取得
	// 通知用に使います。
	static public BattleMapScene.CHANGE_SCENE_TYPE GetChangeSceneType()
	{
		BattleManager manager = ManagerBase.GetManager<BattleManager>(MANAGER_ID.BATTLE);
		return manager.GetChangeSceneType();
	}
	#endregion

	#region item

	static public StoneItemInfo[] GetStoneItemList()
	{
		StoneItemManager manager = ManagerBase.GetManager<StoneItemManager>(MANAGER_ID.STONE_ITEM);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		return manager.GetStoneItemInfoList(hierarchy);
	}



	#endregion

	#region map
	// マップ数を取得
	static public int GetMapCount()
	{
		MapManager manager = ManagerBase.GetManager<MapManager>(MANAGER_ID.MAP);
		return manager.GetMapCount();
	}

	// マップの情報を取得
	// 引数に階層を設定する。
	static public MapInfoData GetMapInfo(int count)
	{
		MapManager manager = ManagerBase.GetManager<MapManager>(MANAGER_ID.MAP);
		return manager.GetMapInfo(count);
	}

	// 現在マップの情報を取得
	static public MapInfoData GetCurrentMapInfo()
	{
		BattleManager manager = ManagerBase.GetManager<BattleManager>(MANAGER_ID.BATTLE);
		return manager.GetCurrentMapData();
	}
	#endregion

	#region weapon

	static public WeaponInfo[] GetWeaponList()
	{
		WeaponManager manager = ManagerBase.GetManager<WeaponManager>(MANAGER_ID.WEAPON);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		return manager.GetWeaponList(hierarchy);
	}

	// 一番近い武器を取得
	static public NearstWeaponData GetNearstWeapon(ref Transform trans)
    {
        float distance = float.MaxValue;
        NearstWeaponData nearstWeaponData = new NearstWeaponData();

        WeaponInfo[] infoList = GetWeaponList();
        for (int i = 0; i < infoList.Length; i++)
        {
            WeaponInfo infoData = infoList[i];
            if (infoData == null) continue;

            float tempDist = Vector3.Distance(infoData.transform.position, trans.position);
            if (distance > tempDist)
            {
                distance = tempDist;

                nearstWeaponData.distance = distance;
                nearstWeaponData.weaponInfo = infoData;
            }
        }

        return nearstWeaponData;
    }
	#endregion

	#region enemy

	// ボスの情報を取得
	static public EnemyInfo GetBossEnemyInfo()
	{
		BossEnemyManager boss = ManagerBase.GetManager<BossEnemyManager>(MANAGER_ID.BOSS_ENEMY);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		return boss.GetBossEnemyInfo(hierarchy);
	}

	//　ボスが死んでいるかどうか
	static public bool IsBossEnemyDead()
	{
		BossEnemyManager boss = ManagerBase.GetManager<BossEnemyManager>(MANAGER_ID.BOSS_ENEMY);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		return boss.IsDead(hierarchy);
	}

	// ボスにダメージを与える
	static public void BossEnemyDamage(int damageValue)
	{
		BossEnemyManager boss = ManagerBase.GetManager<BossEnemyManager>(MANAGER_ID.BOSS_ENEMY);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		boss.Damage(hierarchy, damageValue);

		int hp = GetBossEnemyInfo().GetParam().hp;
		int maxHp = GetBossEnemyInfo().GetParam().maxHp;
		float rate = (float)hp / (float)maxHp;
		UIEvent.SetBossEnemyHpGauge(rate);
	}

	static public EnemyInfo[] GetEnemyList()
	{
		EnemyManager enemy = ManagerBase.GetManager<EnemyManager>(MANAGER_ID.ENEMY);
		int hierarchy = GetCurrentMapInfo().hierarchy;
		return enemy.GetEnemyList(hierarchy);
	}

	// 一番近い敵Transformを取得
	static public Transform GetNearstEnemy(ref Transform trans)
    {
        float distance = float.MaxValue;
        Transform nearsetEnnemyTrans = null;

        EnemyInfo[] enemyInfoList = GetEnemyList();
        for (int i = 0; i < enemyInfoList.Length; i++)
        {
            Transform enemyTrans = enemyInfoList[i].transform;
            if (enemyTrans == null) continue;

            float tempDist = Vector3.Distance(trans.position, enemyTrans.position);
            if (distance > tempDist)
            {
                distance = tempDist;
                nearsetEnnemyTrans = enemyTrans;
            }
        }

        return nearsetEnnemyTrans;
    }
	#endregion
}
