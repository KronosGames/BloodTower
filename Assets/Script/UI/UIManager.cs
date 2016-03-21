using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;

// UIBase群を管理するクラス
// データベースの管理
public class UIManager : ManagerBase
{
    static List<UIBase> baseList = new List<UIBase>();

    // 登録する。
    static public void Register(UIBase uiBase)
    {
        baseList.Add(uiBase);
    }

    // UIBaseを取得
    static public UIBase GetUIBase(UI_TYPE_ID id)
    {
        for(int i = 0;i < baseList.Count;i++)
        {
            if (baseList[i].GetID() == id)
            {
                return baseList[i];
            }
        }

        return null;
    }

    // 削除する。
    static public void Remove(UIBase uiBase)
    {
        baseList.Remove(uiBase);
    }

    void OnDestroy()
    {
        baseList.Clear();
    }

    void Awake()
    {
        UIAnimation.Init();
    }

    void Start() 
    {
        InitManager(this, MANAGER_ID.UI);
	}

    void Update()
    {
        for (int i = 0; i < baseList.Count; i++)
        {
            baseList[i].UpdateUIBase();
        }

        UIAnimation.UpdateAnim();
	}
}
