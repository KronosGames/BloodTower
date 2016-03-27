using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTextDatabase : MonoBehaviour {

    static List<GameText> gameTextList = new List<GameText>();

    void Start()
    {
        gameTextList.Clear();

        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.GAMETEXT);

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            GameText addData = new GameText();
            addData.id = (GAMETEXT_ID)data.GetInt("id");
            addData.text = data.GetString("text");

            gameTextList.Add(addData);
        }
    }


    //  ------------------------------------------------------
    //  公開用関数
    //  ------------------------------------------------------

    // ID から Textを取得
    static public string GetText(GAMETEXT_ID id,bool isInspectorEdit = false)
    {
        if (isInspectorEdit)
        {
#if UNITY_EDITOR
            DataTable dataTable = DatabaseManager.DebugReqLoad(DB_ID.GAMETEXT,"GameText","GameTextTable");

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow data = dataTable.Rows[i];
                GameText addData = new GameText();
                addData.id = (GAMETEXT_ID)data.GetInt("id");
                addData.text = data.GetString("text");

                if (addData.id == id)
                {
                    return addData.text;
                }
            }
#endif

        }
        else
        {

            for (int i = 0; i < gameTextList.Count; i++)
            {
                if (gameTextList[i].id == id)
                {
                    return gameTextList[i].text;
                }
            }
        }

        return "NULL";
    }


}
