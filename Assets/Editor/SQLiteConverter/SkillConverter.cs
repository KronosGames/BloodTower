using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SkillConverter : EditorWindow
{
    public class ConverterData
    {
        public int id;
        public string enumText;
        public string comment;
    }

    const string TITLE = "SkillConverter";
    const string MENU_PATH = "Tools/SQLiteConverter/" + TITLE;
    const string WRITE_PATH = "Assets/Script/DB/SkillParam.cs";
    const string TABLE_NAME = "SkillTable";

    Object dbAsset = null;
    DataTable dataTable = null;
    ConverterData[] converterList = null;

    [MenuItem(MENU_PATH)]
    static void Open()
    {
        EditorWindow window = GetWindow<SkillConverter>();
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
            dataTable = GetDataTable(dbAsset.name, TABLE_NAME);

            if (dataTable != null)
            {
                converterList = GetConverterList(dataTable);

                using (var sw = new System.IO.StreamWriter(WRITE_PATH))
                {
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("// Skill データID");
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine("public enum SKILL_ID");
                    sw.WriteLine("{");

                    for (int i = 0; i < converterList.Length; i++)
                    {
                        ConverterData data = converterList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public class SkillParam");
                    sw.WriteLine("{");
                    sw.WriteLine("    public SKILL_ID id;");
                    sw.WriteLine("    public string name;");
                    sw.WriteLine("    public string explain;");
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
