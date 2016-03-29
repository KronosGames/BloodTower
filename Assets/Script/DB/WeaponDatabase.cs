using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponDatabase : MonoBehaviour
{
    static List<WeaponParam> weaponList = new List<WeaponParam>();
    static Sprite noneIconSprite = null;    //< 存在しない時のアイコン

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

            addData.iconSprite = Resources.Load(addData.iconPath, typeof(Sprite)) as Sprite;
            addData.material = Resources.Load(addData.iconPath, typeof(Material)) as Material;

            weaponList.Add(addData);
        }

        noneIconSprite = Resources.Load("Weapon/noneIcon", typeof(Sprite)) as Sprite;
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

    // 配置された武器のマテリアルを取得
    static public Material GetWeaponMaterial(WEAPON_ID id, bool isInspectorEdit = false)
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
            WeaponParam weapon = GetWeapon(id);
            if (weapon == null) return null;

            return weapon.material;
        }

        return null;
    }

    // 配置された武器の画像を取得
    static public Sprite GetWeaponIconSprite(WEAPON_ID id, bool isInspectorEdit = false)
    {
        if (id == WEAPON_ID.NULL)
        {
            return noneIconSprite;
        }

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
            WeaponParam weapon = GetWeapon(id);
            if (weapon == null) return null;

            return weapon.iconSprite;
        }

        return null;
    }
}
