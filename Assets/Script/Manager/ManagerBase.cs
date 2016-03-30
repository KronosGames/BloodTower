using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Managerを登録するIDを記述する。
public enum MANAGER_ID
{ 
    NULL,   //< 未定義
    UI,
    DATABASE,
    GAME_MAIN,
    BATTLE,
    EFFECT,
    INPUT,
    WEAPON,
    MAP,
    BOSS_ENEMY,
}

// 管理データ
public class ManagerData
{
    public bool isActive = false;
    public MANAGER_ID id = MANAGER_ID.NULL;
    public ManagerBase managerBase = null;
}

public class ManagerBase : MonoBehaviour 
{
    static List<ManagerData> managerList = new List<ManagerData>();

    protected void InitManager(ManagerBase managerBase,MANAGER_ID id)
    {
        ManagerData data = new ManagerData();
        data.id = id;
        data.managerBase = managerBase;
        data.isActive = gameObject.activeInHierarchy;
        managerList.Add(data);
    }

    void OnDestroy()
    {
        for (int i = 0; i < managerList.Count; i++)
        {
            if (managerList[i].managerBase == this)
            {
                managerList.RemoveAt(i);
            }
        }
    }

    static public T GetManager<T>(MANAGER_ID id) where T : ManagerBase
    {
        for (int i = 0; i < managerList.Count; i++)
        {
            if (managerList[i].id == id)
            {
                return managerList[i].managerBase as T;
            }
        }

        return null;
    }
}
