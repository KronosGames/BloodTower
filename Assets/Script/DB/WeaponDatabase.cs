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

            weaponList.Add(addData);
        }
    }

    // --------------------------------------------
    // 公開用関数
    // --------------------------------------------

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
        List<WeaponParam> weaponList = new List<WeaponParam>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponParam data = weaponList[i];
            if (data.name == name)
            {
                weaponList.Add(data);
            }
        }

        return weaponList.ToArray();
    }

}
