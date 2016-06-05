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

        DataRow data = dataTable.Rows[0];
        GameUserTable tableData = new GameUserTable();

        tableData.name = data.GetString("name");

        for (int itemIndex = 0; itemIndex < GameUserTable.ITEM_NUM; itemIndex++)
        {
            tableData.itemList[itemIndex] = data.GetInt("item_" + itemIndex.ToString("00"));
        }

        gameUserTable = tableData;

        GameUserParam.Setup(ref gameUserTable);
    }


}
