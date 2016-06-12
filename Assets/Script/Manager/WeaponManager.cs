using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponMapHierarchyData
{
    public WeaponInfo[] weaponList = null;
	public int hierarchyCount = -1;
}
public class WeaponManager : ManagerBase
{
	List<WeaponMapHierarchyData> weaponHierarchyList = new List<WeaponMapHierarchyData>();

    void Start()
    {
        InitManager(this, MANAGER_ID.WEAPON);
    }

	//  -----------------------------------------
	//  公開用関数
	//  -----------------------------------------

	// 初期化
	public void Setup()
	{
		weaponHierarchyList.Clear();
	}

	public void Register(ref WeaponInfo[] weaponList,int hierarchyCount)
	{
		WeaponMapHierarchyData addData = new WeaponMapHierarchyData();
		addData.weaponList = weaponList;

		for (int i = 0; i < addData.weaponList.Length; i++)
		{
			addData.weaponList[i].Setup(hierarchyCount);
		}

		addData.hierarchyCount = hierarchyCount;
		weaponHierarchyList.Add(addData);
    }

    // 現在の階層の武器一覧が取得できる。
    public WeaponInfo[] GetWeaponList(int hierarchyCount)
    {
        return weaponHierarchyList[hierarchyCount].weaponList;
    }
}
