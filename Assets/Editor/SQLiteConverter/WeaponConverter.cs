using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class WeaponConverter : EditorWindow
{
    public class ConverterData
    {
        public int id;
        public string enumText;
        public string comment;
    }

    const string TITLE = "WeaponConverter";
    const string MENU_PATH = "Tools/SQLiteConverter/" + TITLE;
    const string WRITE_PATH = "Assets/Script/DB/WeaponParam.cs";
    const string TABLE_NAME = "WeaponTable";
    const string TABLE_TYPE_NAME = "WeaponTypeTable";

    Object dbAsset = null;
    
    [MenuItem(MENU_PATH)]
    static void Open()
    {
        EditorWindow window = GetWindow<WeaponConverter>();
        window.titleContent = new GUIContent(TITLE);
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("データベースファイル");
        dbAsset = EditorGUILayout.ObjectField(dbAsset, typeof(Object), false);

        EditorGUILayout.Space();

        if (GUILayout.Button("反映"))
        {
            DataTable weaponTable = GetDataTable(dbAsset.name, TABLE_NAME);
            DataTable weaponTypeTable = GetDataTable(dbAsset.name, TABLE_TYPE_NAME);

            if (weaponTable != null)
            {
                ConverterData[] weaponList = GetConverterList(weaponTable);
                ConverterData[] weaponTypeList = GetConverterList(weaponTypeTable);

                using (var sw = new System.IO.StreamWriter(WRITE_PATH))
                {
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("// Weapon データID");
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine("public enum WEAPON_TYPE");
                    sw.WriteLine("{");
                    
                    for (int i = 0; i < weaponTypeList.Length; i++)
                    {
                        ConverterData data = weaponTypeList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public enum WEAPON_ID");
                    sw.WriteLine("{");

                    for (int i = 0; i < weaponList.Length; i++)
                    {
                        ConverterData data = weaponList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public class WeaponParam");
                    sw.WriteLine("{");
                    sw.WriteLine("    public WEAPON_ID id;");
                    sw.WriteLine("    public WEAPON_TYPE type;");
                    sw.WriteLine("    public SKILL_ID skillID;");

                    sw.WriteLine("    public string name;");
                    sw.WriteLine("    public string explain;");
                    sw.WriteLine("    public string iconPath;");
                    sw.WriteLine("    public string materialPath;");

                    sw.WriteLine("    public int hp;");
                    sw.WriteLine("    public int attack;");
                    sw.WriteLine("    public int defence;");
                    sw.WriteLine("    public int attackSpeed;");
                    sw.WriteLine("    public int moveSpeed;");
                    sw.WriteLine("    public int stamina;");
                    sw.WriteLine("    public int durability;");
                    sw.WriteLine("    public int durabilityMax;");
                    
                    sw.WriteLine("}");
                }

                AssetDatabase.Refresh();

                Debug.Log("出力成功");
            }
        }
    }




    ConverterData[] GetConverterList(DataTable dataTable)
    {
        List<ConverterData> outputLust = new List<ConverterData>();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            ConverterData addData = new ConverterData();

            addData.id = data.GetInt("id");
            addData.enumText = data.GetString("enumID");
            addData.comment = data.GetString("name");

            outputLust.Add(addData);
        }

        return outputLust.ToArray();
    }


    DataTable GetDataTable(string dbName, string tableName)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(dbName + ".db");
        string selectQuery = "SELECT * FROM " + tableName;
        return sqlDB.ExecuteQuery(selectQuery);
    }

}
