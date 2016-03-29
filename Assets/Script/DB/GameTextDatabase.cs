using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameTextDatabase : MonoBehaviour {

    List<GameText> gameTextList = new List<GameText>();

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

        UIManager.SetupGameText(gameTextList.ToArray());
    }

    

}
