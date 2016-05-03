using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDatabase : MonoBehaviour
{
    static List<WeaponParam> weaponList = new List<WeaponParam>();

    void Start()
    {
        weaponList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.WEAPON);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            WeaponParam addData = new WeaponParam();

            addData.id = (WEAPON_ID)data.GetInt("id");
            addData.type = (WEAPON_TYPE)data.GetInt("type");
            addData.name = data.GetString("name");
            addData.explain = data.GetString("explain");
            addData.iconPath = data.GetString("iconPath");
            addData.materialPath = data.GetString("materialPath");

            addData.hp = data.GetInt("hp");
            addData.attack = data.GetInt("attack");
            addData.defence = data.GetInt("defence");
            addData.moveSpeed = data.GetInt("moveSpeed");
            addData.attackSpeed = data.GetInt("attackSpeed");
            addData.stamina = data.GetInt("stamina");
            addData.hardness = data.GetInt("hardness");

            addData.hardnessMax = addData.hardness;

            UIWeaponIconData uiWeaponIconData = new UIWeaponIconData();
            uiWeaponIconData.id = addData.id;
            uiWeaponIconData.sprite = Resources.Load(addData.iconPath, typeof(Sprite)) as Sprite;

            UIManager.WeaponIconRegister(uiWeaponIconData);


            weaponList.Add(addData);
        }
    }

    // --------------------------------------------
    // 公開用関数
    // --------------------------------------------

    static public string GetWeaponTypeName(WEAPON_TYPE id)
    {
        switch (id)
        {
            case WEAPON_TYPE.SWORD:
                return "ソード";
            case WEAPON_TYPE.DAGGER:
                return "ダガー";
            case WEAPON_TYPE.SPEAR:
                return "スピア";
            case WEAPON_TYPE.ROD:
                return "クラブ";
        }

        return "";
    }

    static public WeaponParam GetWeapon(WEAPON_ID id)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponParam data = weaponList[i];
            if (data.id == id)
            {
                return data;
            }
        }

        return null;
    }

    static public WeaponParam[] GetWeaponTypeList(WEAPON_TYPE type)
    {
        List<WeaponParam> weaponTypeList = new List<WeaponParam>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponParam data = weaponList[i];
            if (data.type == type)
            {
                weaponTypeList.Add(data);
            }
        }

        return weaponTypeList.ToArray();
    }

    static public WeaponParam[] GetWeaponListByName(string name)
    {
        List<WeaponParam> weaponByNameList = new List<WeaponParam>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponParam data = weaponList[i];
            if (data.name == name)
            {
                weaponByNameList.Add(data);
            }
        }

        return weaponByNameList.ToArray();
    }

    // 武器のマテリアルを取得
    static public Material LoadWeaponMaterial(WEAPON_ID id, bool isInspectorEdit = false)
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
                    return Resources.Load(data.GetString("materialPath"), typeof(Material)) as Material;
                }
            }
#endif

        }
        else
        {
            WeaponParam param = GetWeapon(id);
            if (param == null) return null;

            return Resources.Load(param.materialPath, typeof(Material)) as Material;
        }

        return null;
    }

}
