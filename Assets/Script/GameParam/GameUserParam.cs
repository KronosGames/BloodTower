using UnityEngine;
using System.Collections;

public static class GameUserParam
{
    static GameUserTable gameUserData = new GameUserTable();

    // セットアップ処理
    public static void Setup(ref GameUserTable table)
    {
        gameUserData = table;
    }

    static public void SetUserName(string newName)
    {
        gameUserData.name = newName;
    }

    static public string GetUserName()
    {
        return gameUserData.name;
    }

	// アイテムの個数を設定
    static public void SetItemCount(ITEM_ID itemeID,int count)
    {
		switch (itemeID)
		{
			case ITEM_ID.GOLD_STONE:
				gameUserData.gold_stone = count;
				break;
			case ITEM_ID.SILVER_STONE:
				gameUserData.silver_stone = count;
				break;
			case ITEM_ID.COPPER_STONE:
				gameUserData.copper_stone = count;
				break;
		}
    }

	// アイテムの個数を取得
    static public int GetItemCount(ITEM_ID itemID)
	{
		switch (itemID)
		{
			case ITEM_ID.GOLD_STONE:
				return gameUserData.gold_stone;
			case ITEM_ID.SILVER_STONE:
				return gameUserData.silver_stone;
			case ITEM_ID.COPPER_STONE:
				return gameUserData.copper_stone;
		}

		return -1;
	}
}
