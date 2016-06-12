using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	static List<ItemParam> itemList = new List<ItemParam>();

	void Start()
	{
		itemList.Clear();

		DataTable dataTable = DatabaseManager.RequestLoad(DB_ID.ITEM);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			DataRow data = dataTable.Rows[i];
			ItemParam addData = new ItemParam();

			addData.id = (ITEM_ID)data.GetInt("id");
			addData.name = data.GetString("name");
			addData.explain = data.GetString("explain");
			itemList.Add(addData);
		}
	}


	//  --------------------------------------------
	//  公開関数
	//  --------------------------------------------

	static public ItemParam GetItemParam(ITEM_ID id)
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].id == id)
			{
				return itemList[i];
			}
		}

		return null;
	}

}
