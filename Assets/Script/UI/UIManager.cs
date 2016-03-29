using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;

public class UIWeaponIconData
{
    public WEAPON_ID id;
    public Sprite sprite;
}

// UIBase群を管理するクラス
// データベースの管理
public class UIManager : ManagerBase
{
    static List<UIBase> baseList = new List<UIBase>();
    static List<UIWeaponIconData> weaponIconList = new List<UIWeaponIconData>();

    void OnDestroy()
    {
        weaponIconList.Clear();
        baseList.Clear();
    }

    void Awake()
    {
        weaponIconList.Clear();
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

    //  -----------------------------------------
    //  公開用関数
    //  -----------------------------------------

    // 登録する。
    static public void Register(UIBase uiBase)
    {
        baseList.Add(uiBase);
    }

    // UIBaseを取得
    static public UIBase GetUIBase(UI_TYPE_ID id)
    {
        for (int i = 0; i < baseList.Count; i++)
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

    static public void WeaponIconRegister(UIWeaponIconData data)
    {
        weaponIconList.Add(data);
    }

    static public Sprite GetWeaponIcon(WEAPON_ID id,bool isInspectorEdit = false)
    {
        if (isInspectorEdit)
        {
#if UNITY_EDITOR
            DataTable dataTable = DatabaseManager.DebugReqLoad(DB_ID.WEAPON, "WeaponList", "WeaponTable");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow data = dataTable.Rows[i];

                int dataID = data.GetInt("id");
                if (dataID == (int)id)
                {
                    return Resources.Load(data.GetString("iconPath"), typeof(Sprite)) as Sprite;
                }
            }
#endif

        }
        else
        {
            for (int i = 0; i < weaponIconList.Count; i++)
            {
                UIWeaponIconData iconData = weaponIconList[i];
                if (iconData.id == id)
                {
                    return iconData.sprite;
                }
            }
        }

        return null;
    }
}
