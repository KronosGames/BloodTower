using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScrollContentCreator : UIBase 
{

    [SerializeField]
    RectTransform itemBase = null;

    List<UIItemSelectNode> itemNodeList = new List<UIItemSelectNode>();

    RectTransform trans = null;

	void Start () 
    {
        InitUI(this, UI_TYPE_ID.SCROLL_CONTENT);

        trans = transform as RectTransform;

        itemBase.gameObject.SetActive(false);
	}

    void Clear()
    {
        for (int i = 0; i < itemNodeList.Count; i++)
        {
            Destroy(itemNodeList[i].gameObject);
        }
    }


    protected override void UpdateUI()
    {
        for (int i = 0; i < itemNodeList.Count; i++)
        {
            itemNodeList[i].OnUpdateItem(i);
        }

        trans.sizeDelta = new Vector2(trans.sizeDelta.x, itemNodeList.Count * itemBase.sizeDelta.y);
    }

    //  -------------------------------------------
    // 公開関数
    //  -------------------------------------------

    // Viewのサイズを設定する。
    public void SetViewSize(int createNum)
    {
        Clear();

        for (int i = 0; i < createNum; i++)
        {
            var obj = Instantiate(itemBase.gameObject) as GameObject;
            obj.SetActive(true);
            obj.name = itemBase.name + i.ToString("00");

            var rectTrans = obj.transform as RectTransform;
            rectTrans.SetParent(itemBase.parent);
            rectTrans.anchoredPosition3D = itemBase.anchoredPosition3D;
            rectTrans.anchoredPosition3D += new Vector3(0, -i * itemBase.sizeDelta.y, 0);
            rectTrans.localScale = Vector3.one;

            var node = obj.GetComponent<UIItemSelectNode>();
            node.OnSetupItem(i);
            itemNodeList.Add(node);
        }
    }



}
