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
    // 一番近い武器を取得
    static public NearstWeaponData GetNearstWeapon(ref Transform trans)
    {
        float distance = float.MaxValue;
        NearstWeaponData nearstWeaponData = new NearstWeaponData();

        WeaponInfo[] infoList = WeaponManager.GetWeaponList();
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

    // 一番近い敵Transformを取得
    static public Transform GetNearstEnemy(ref Transform trans)
    {
        float distance = float.MaxValue;
        Transform nearsetEnnemyTrans = null;

        EnemyInfo[] enemyInfoList = EnemyManager.GetEnemyList();
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
}
