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
    static public void Setup(ref WeaponInfo[] infoList)
    {
        weaponList = infoList;

        for (int i = 0; i < weaponList.Length; i++)
        {
            WeaponInfo info = weaponList[i];
            info.Setup();
        }
    }

    static public WeaponInfo GetWeapon(WEAPON_ID id)
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            WeaponInfo info = weaponList[i];
            WeaponParam param = info.GetParam();
            if (param.id == id)
            {
                return info;
            }
        }

        return null;
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
            WeaponParam param = info.GetParam();
            if (param.type == type)
            {
                infoList.Add(info);
            }
        }

        return infoList.ToArray();
    }

}
