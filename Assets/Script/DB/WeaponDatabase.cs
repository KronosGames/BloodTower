using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponTable
{
    public int id;          //< ID
    public int type;        //< 種類
    public string name;     //< 名前
    public string explain;  //< 説明
    public string iconPath;     //< アイコンパス
    public string materialPath;     //< マテリアルパス
}

public class WeaponDatabase : MonoBehaviour
{
    static List<WeaponTable> weaponList = new List<WeaponTable>();

    void Start()
    {
        weaponList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.WEAPON);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            WeaponTable addData = new WeaponTable();

            addData.id = data.GetInt("id");
            addData.type = data.GetInt("type");
            addData.name = data.GetString("name");
            addData.explain = data.GetString("explain");
            addData.iconPath = data.GetString("iconPath");
            addData.materialPath = data.GetString("materialPath");

            weaponList.Add(addData);
        }
    }

    // --------------------------------------------
    // 公開用関数
    // --------------------------------------------

    static public WeaponTable GetWeapon(int id)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponTable data = weaponList[i];
            if (data.id == id)
            {
                return data;
            }
        }

        return null;
    }

    static public WeaponTable[] GetWeaponTypeList(int type)
    {
        List<WeaponTable> weaponTypeList = new List<WeaponTable>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponTable data = weaponList[i];
            if (data.type == type)
            {
                weaponTypeList.Add(data);
            }
        }

        return weaponTypeList.ToArray();
    }

    static public WeaponTable[] GetWeaponListByName(string name)
    {
        List<WeaponTable> weaponList = new List<WeaponTable>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            WeaponTable data = weaponList[i];
            if (data.name == name)
            {
                weaponList.Add(data);
            }
        }

        return weaponList.ToArray();
    }

}
