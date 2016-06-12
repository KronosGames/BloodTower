using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneItemMapHierarchyData
{
	public StoneItemInfo[] stoneItemList = null;
	public int hierarchyCount = -1;
}

public class StoneItemManager : ManagerBase {

	List<StoneItemMapHierarchyData> hierarchyList = new List<StoneItemMapHierarchyData>();

	void Start()
	{
		InitManager(this, MANAGER_ID.STONE_ITEM);
	}

	public void Setup()
	{
		hierarchyList.Clear();
	}

	public void Register(ref StoneItemInfo[] itemList, int hierarchyCount)
	{
		StoneItemMapHierarchyData addData = new StoneItemMapHierarchyData();
		addData.stoneItemList = itemList;

		for (int i = 0; i < addData.stoneItemList.Length; i++)
		{
			addData.stoneItemList[i].Setup(hierarchyCount);
		}

		addData.hierarchyCount = hierarchyCount;
		hierarchyList.Add(addData);
	}

	// 石アイテム情報のリストを取得
	public StoneItemInfo[] GetStoneItemInfoList(int hierarchyCount)
	{
		return hierarchyList[hierarchyCount].stoneItemList;
	}
}
