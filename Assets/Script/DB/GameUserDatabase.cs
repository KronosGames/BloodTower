using UnityEngine;
using System.Collections;

public class GameUserTable
{
    public string name;				//< ユーザー名
    public int gold_stone;			//< 金石
    public int silver_stone;		//< 銀石
    public int copper_stone;		//< 銅石
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
		tableData.gold_stone = data.GetInt("gold_stone");
		tableData.silver_stone = data.GetInt("silver_stone");
		tableData.copper_stone = data.GetInt("copper_stone");

		gameUserTable = tableData;

        GameUserParam.Setup(ref gameUserTable);
    }


}
