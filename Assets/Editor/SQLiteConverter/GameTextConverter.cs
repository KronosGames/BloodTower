using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class GameTextConverter : EditorWindow
{
    public class GameTextConverterData
    {
        public int id;
        public string enumText;
        public string comment;
        public string text;
    }

    const string TITLE = "GameTextConverter";
    const string MENU_PATH = "Tools/" + TITLE;
    const string WRITE_PATH = "Assets/Script/DB/GameText.cs";
    const string TABLE_NAME = "GameTextTable";

    Object dbAsset = null;
    DataTable dataTable = null;
    GameTextConverterData[] converterList = null;

    [MenuItem(MENU_PATH)]
    static void Open()
    {
        EditorWindow window = GetWindow<GameTextConverter>();
        window.titleContent = new GUIContent(TITLE);
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("データベースファイル");
        dbAsset = EditorGUILayout.ObjectField(dbAsset, typeof(Object),false);

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
                    sw.WriteLine("// GameText データID");
                    sw.WriteLine("// -----------------------------------------");
                    sw.WriteLine("");

                    sw.WriteLine("public enum GAMETEXT_ID");
                    sw.WriteLine("{");

                    for (int i = 0; i < converterList.Length; i++)
                    {
                        GameTextConverterData data = converterList[i];
                        sw.Write("    ");
                        sw.WriteLine(data.enumText + " = " + data.id + ",     //< " + data.comment);
                    }

                    sw.WriteLine("}");

                    sw.WriteLine("");

                    sw.WriteLine("public class GameText");
                    sw.WriteLine("{");
                    sw.WriteLine("    public GAMETEXT_ID id;");
                    sw.WriteLine("    public string text;");
                    sw.WriteLine("}");
                }

                AssetDatabase.Refresh();

                Debug.Log("出力成功");
            }
        }
    }




    GameTextConverterData[] GetConverterList(DataTable dataTable)
    {
        List<GameTextConverterData> outputLust = new List<GameTextConverterData>();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            GameTextConverterData addData = new GameTextConverterData();

            addData.id = data.GetInt("id");
            addData.enumText = data.GetString("enumName");
            addData.comment = data.GetString("comment");
            addData.text = data.GetString("text");

            outputLust.Add(addData);
        }

        return outputLust.ToArray();
    }


    DataTable GetDataTable(string dbName,string tableName)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(dbName + ".db");
        string selectQuery = "SELECT * FROM " + tableName;
        return sqlDB.ExecuteQuery(selectQuery);
    }

}
