using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WEAPON_TYPE
{
    NONE = -1,  //< 未定
    ROD,        //< 棒
    SWORD,      //< 剣
    DAGGER,     //< 短剣
    SPEAR,      //< 槍
}

public enum WEAPON_ID
{
    NONE = -1,  //< 未定

    //< 棒
    ROD = 0,


    //< 剣
    SWORD = 20,


    //< 短剣
    DAGGER = 40,


    //< 槍
    SPEAR = 60,    
    
     
}


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
    }

    // 現在の階層の武器一覧が取得できる。
    static public WeaponInfo[] GetWeaponList()
    {
        return weaponList;
    }

    // IDからTypeに変換します。
    static public WEAPON_TYPE GetWeaponType(WEAPON_ID id)
    {
        WeaponTable weapon = WeaponDatabase.GetWeapon((int)id);

        if (weapon.id >= 0 && weapon.id <= 19)
        {
            return WEAPON_TYPE.ROD;
        }
        if (weapon.id >= 20 && weapon.id <= 39)
        {
            return WEAPON_TYPE.SWORD;
        }
        if (weapon.id >= 40 && weapon.id <= 59)
        {
            return WEAPON_TYPE.DAGGER;
        }
        if (weapon.id >= 60 && weapon.id <= 79)
        {
            return WEAPON_TYPE.SPEAR;
        }

        return WEAPON_TYPE.NONE;
    }

    // 武器のマテリアルを取得
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
            WeaponTable weapon = WeaponDatabase.GetWeapon((int)id);
            if (weapon == null) return null;

            return Resources.Load(weapon.materialPath, typeof(Material)) as Material;
        }

        return null;
    }

    // 武器の画像を取得
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
            WeaponTable weapon = WeaponDatabase.GetWeapon((int)id);
            if (weapon == null) return null;

            return Resources.Load(weapon.iconPath, typeof(Sprite)) as Sprite;
        }

        return null;
    }
}
