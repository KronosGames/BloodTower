using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WEAPON_ID
{
    NONE = -1,       //< 未定
    SWORD,      //< 剣
    ROD,        //< 棒
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
}
