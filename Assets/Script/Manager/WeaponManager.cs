using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : ManagerBase
{
    static WeaponInfo[] weaponList = null;

    void Start()
    {
        InitManager(this, MANAGER_ID.WEAPON);
    }

    //  -----------------------------------------
    //  公開用関数
    //  -----------------------------------------
    
    // 初期化
    static public void Setup(WeaponInfo[] infoList)
    {
        weaponList = infoList;

        for (int i = 0; i < weaponList.Length; i++)
        {
            WeaponInfo info = weaponList[i];
            WeaponParam table = WeaponDatabase.GetWeapon(info.weaponID);
            info.weaponType = (WEAPON_TYPE)table.type;
        }
    }

    // 現在の階層の武器一覧が取得できる。
    static public WeaponInfo[] GetWeaponList()
    {
        return weaponList;
    }

    // 配置された武器の種類リスト
    static public WeaponInfo[] GetWeaponTypeList(WEAPON_TYPE type)
    {
        List<WeaponInfo> infoList = new List<WeaponInfo>();

        for (int i = 0; i < weaponList.Length; i++)
        {
            WeaponInfo info = weaponList[i];
            if (info.weaponType == type)
            {
                infoList.Add(info);
            }
        }

        return infoList.ToArray();
    }

    // 配置された武器のマテリアルを取得
    static public Material GetWeaponMaterial(WEAPON_ID id,bool isInspectorEdit = false)
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
                    return Resources.Load(data.GetString("materialPath"),typeof(Material)) as Material;
                }
            }

#endif

        }
        else
        {
            WeaponParam weapon = WeaponDatabase.GetWeapon(id);
            if (weapon == null) return null;

            return Resources.Load(weapon.materialPath, typeof(Material)) as Material;
        }

        return null;
    }

    // 配置された武器の画像を取得
    static public Sprite GetWeaponSprite(WEAPON_ID id, bool isInspectorEdit = false)
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
            WeaponParam weapon = WeaponDatabase.GetWeapon(id);
            if (weapon == null) return null;

            return Resources.Load(weapon.iconPath, typeof(Sprite)) as Sprite;
        }

        return null;
    }
}
