using UnityEngine;
using System.Collections;

public class GameUserTable
{
    public const int ITEM_NUM = 5;

    public string name;
    public int[] itemList = new int[ITEM_NUM];
}

public class GameUserDatabase : MonoBehaviour
{
    static GameUserTable gameUserTable = new GameUserTable();

    void Start()
    {
        DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.GAMEUSER);
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow data = dataTable.Rows[i];
            GameUserTable tableData = new GameUserTable();

            tableData.name = data.GetString("name");

            for (int itemIndex = 0; itemIndex < GameUserTable.ITEM_NUM; itemIndex++)
            {
                tableData.itemList[itemIndex] = data.GetInt("itemID_" + itemIndex.ToString("00"));
            }

            gameUserTable = tableData;
        }
    }


    //  -------------------------------------------------
    //  公開用関数
    //  -------------------------------------------------

    // ゲームユーザー情報を取得
    static public GameUserTable GetGameUserInfo()
    {
        return gameUserTable;
    }
}
