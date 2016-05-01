using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : ManagerBase
{
    static List<EnemyInfo> enemyList = new List<EnemyInfo>();

    void Start()
    {
        InitManager(this, MANAGER_ID.ENEMY);
    }

    //  ------------------------------------------------
    //  公開用 関数
    //  ------------------------------------------------

    static public void Clear()
    {
        enemyList.Clear();
    }

    static public void Remove(ref EnemyInfo enemyinfo)
    {
        enemyList.Remove(enemyinfo);
    }

    static public void Register(ref EnemyInfo enemyinfo)
    {
        enemyinfo.Setup();
        enemyList.Add(enemyinfo);
    }

    static public EnemyInfo[] GetEnemyList()
    {
        return enemyList.ToArray();
    }

    public static EnemyInfo GetEnemy(ref GameObject target)
    {
        int count = enemyList.Count;
        for (int i = 0; i < count; i++)
        {
            EnemyInfo data = enemyList[i];
            if (data.gameObject == target)
            {
                return data;
            }
        }

        return null;
    }

}
