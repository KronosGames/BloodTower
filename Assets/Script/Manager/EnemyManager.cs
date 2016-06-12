using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMapHierarchyData
{
	public EnemyInfo[] enemyList = null;
	public bool isNone = true;
	public int hierarchyCount = -1;
}
public class EnemyManager : ManagerBase
{
    List<EnemyMapHierarchyData> hierarchyList = new List<EnemyMapHierarchyData>();

    void Start()
    {
        InitManager(this, MANAGER_ID.ENEMY);
    }

	//  ------------------------------------------------
	//  公開用 関数
	//  ------------------------------------------------

	public void Setup()
	{
		hierarchyList.Clear();
	}

	public void Register(ref EnemyInfo[] enemyList, int hierarchyCount)
	{
		EnemyMapHierarchyData addData = new EnemyMapHierarchyData();
		addData.enemyList = enemyList;

		for (int i = 0; i < addData.enemyList.Length; i++)
		{
			addData.enemyList[i].Setup(hierarchyCount);
		}

		addData.hierarchyCount = hierarchyCount;
		hierarchyList.Add(addData);
	}

	public EnemyInfo[] GetEnemyList(int hierarchyCount)
    {
        return hierarchyList[hierarchyCount].enemyList;
    }

}
