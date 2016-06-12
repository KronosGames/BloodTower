using UnityEngine;
using UnityEditor;
using System.Collections;

// 石アイテムの情報
public class StoneItemInfo : MonoBehaviour {

	[SerializeField]
	ITEM_ID stoneItemID = ITEM_ID.NONE;

	ItemParam param = new ItemParam();
	int hierarchyCount = -1;

	public ItemParam GetParam() { return param; }
	public int GetHierarchyCount() { return hierarchyCount; }

	public void Setup(int hierarchyCount)
	{
		param = ItemDatabase.GetItemParam(stoneItemID);
		this.hierarchyCount = hierarchyCount;
	}

	// 石のアイテムIDを取得
	public ITEM_ID GetStoneItemID()
	{
		return stoneItemID;
	}

	void OnValidate()
	{
#if UNITY_EDITOR
		string materialPath = "";
		switch (stoneItemID)
		{
			case ITEM_ID.GOLD_STONE:
				materialPath = "Assets/Prefab/StoneItem/GoldStoneMaterial.mat";
				break;
			case ITEM_ID.SILVER_STONE:
				materialPath = "Assets/Prefab/StoneItem/SilverStoneMaterial.mat";
				break;
			case ITEM_ID.COPPER_STONE:
				materialPath = "Assets/Prefab/StoneItem/CopperStoneMaterial.mat";
				break;
		}
		Material itemMaterial = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
		MeshRenderer meshRender = GetComponent<MeshRenderer>();
		meshRender.material = itemMaterial;
#endif
	}
	
}
