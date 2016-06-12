using UnityEngine;
using System.Collections;

public class ItemCatchController : MonoBehaviour
{
	enum SEQ_STATE
	{
		NONE,
		UPDATE,
		DIALOG,
	}
	
	[SerializeField]
	float catchDistance = 5.0f;

	SEQ_STATE seqState = SEQ_STATE.NONE;

	void Start()
	{
		seqState = SEQ_STATE.UPDATE;
	}

	void Update()
	{
		switch (seqState)
		{
			case SEQ_STATE.UPDATE:
				UpdateItemCatch();
				break;
			case SEQ_STATE.DIALOG:
				if (UIEvent.GetDialogPushType() == UIDialog.PUSH_TYPE.OK)
				{
					seqState = SEQ_STATE.UPDATE;
				}
				break;
		}

	}


	void UpdateItemCatch()
	{
		StoneItemInfo[] itemList = BattleMapUtility.GetStoneItemList();
		if (itemList == null) return;

		for (int i = 0; i < itemList.Length; i++)
		{
			StoneItemInfo item = itemList[i];
			if (!item.gameObject.activeInHierarchy) continue;

			float dist = Vector3.Distance(transform.position, item.transform.position);
			if (dist <= catchDistance)
			{
				// 取得処理
				string title = item.GetParam().name;
				string info = item.GetParam().explain;
				item.gameObject.SetActive(false);
				UIEvent.OpenDialog(UIDialog.BUTTON_TYPE.OK, ref title, ref info);
				seqState = SEQ_STATE.DIALOG;
				return;
			}
		}
	}
}
