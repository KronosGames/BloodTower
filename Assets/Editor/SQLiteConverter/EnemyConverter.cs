using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class EnemyConverter : EditorWindow {

    public class ConverterData
    {
        public int id;
        public string enumText;
        public string comment;
    }

    const string TITLE = "EnemyConverter";
    const string MENU_PATH = "Tools/SQLiteConverter/" + TITLE;
    const string WRITE_PATH = "Assets/Script/DB/EnemyParam.cs";
    const string TABLE_NAME = "EnemyTable";

    Object dbAsset = null;

    [MenuItem(MENU_PATH)]
    static void Open()
    {
        EditorWindow window = GetWindow<EnemyConverter>();
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
                    sw.WriteLine("// Enemy データID");
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine("");

                    sw.WriteLine("public enum ENEMY_ID");
                    sw.WriteLine("{");

                    for (int i = 0; i < dataList.Length; i++)
                    {
                        ConverterData data = dataList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public enum ENEMY_STATUS");
                    sw.WriteLine("{");
                    sw.WriteLine("    Burn = 0,   //< やけど、燃焼");
                    sw.WriteLine("    Poison = 1,   //< 毒");
                    sw.WriteLine("    Frozen = 2,   //< 凍結");
                    sw.WriteLine("    BloodLoss = 3,   //< 出血");
                    sw.WriteLine("    SENTINEL = 4,   //< 番兵");
                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public class EnemyParam");
                    sw.WriteLine("{");
                    sw.WriteLine("    public ENEMY_ID id;");
                    sw.WriteLine("    public string name;");
                    sw.WriteLine("    public int hp;");
                    sw.WriteLine("    public int maxHp;");
                    sw.WriteLine("    public int attack;");
                    sw.WriteLine("    public int defense;");
                    sw.WriteLine("    public int moveSpeed;");
                    sw.WriteLine("    public bool[] canInTheStatus = new bool[(int)ENEMY_STATUS.SENTINEL];   //< 状態異常にかかるかどうか");
                    sw.WriteLine("    public int[] statusResistance = new int[(int)ENEMY_STATUS.SENTINEL];   //< 状態異常耐性値(この値を超えると状態異常になる)");
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
