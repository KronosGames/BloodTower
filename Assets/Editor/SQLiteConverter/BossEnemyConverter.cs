using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class BossEnemyConverter : EditorWindow {

    public class ConverterData
    {
        public int id;
        public string enumText;
        public string comment;
    }

    const string TITLE = "BossEnemyConverter";
    const string MENU_PATH = "Tools/SQLiteConverter/" + TITLE;
    const string WRITE_PATH = "Assets/Script/DB/BossEnemyParam.cs";
    const string TABLE_NAME = "BossEnemyTable";

    Object dbAsset = null;

    [MenuItem(MENU_PATH)]
    static void Open()
    {
        EditorWindow window = GetWindow<BossEnemyConverter>();
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
            DataTable table = GetDataTable(dbAsset.name, TABLE_NAME);

            if (table != null)
            {
                ConverterData[] dataList = GetConverterList(table);

                using (var sw = new System.IO.StreamWriter(WRITE_PATH))
                {
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("// BossEnemy データID");
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine("");

                    sw.WriteLine("public enum BOSS_ENEMY_ID");
                    sw.WriteLine("{");

                    for (int i = 0; i < dataList.Length; i++)
                    {
                        ConverterData data = dataList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public class BossEnemyParam");
                    sw.WriteLine("{");
                    sw.WriteLine("    public BOSS_ENEMY_ID id;");
                    sw.WriteLine("    public string name;");
                    sw.WriteLine("    public int hp;");
                    sw.WriteLine("    public int maxHp;");
                    sw.WriteLine("    public int attack;");
                    sw.WriteLine("    public int defense;");
                    sw.WriteLine("    public int moveSpeed;");
                    
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
