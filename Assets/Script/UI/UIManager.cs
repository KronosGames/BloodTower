using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;

// UIBase群を管理するクラス
// データベースの管理
public class UIManager : ManagerBase
{
    static List<UIBase> baseList = new List<UIBase>();
    static GameText[] gameTextList = null;

    // 登録する。
    static public void Register(UIBase uiBase)
    {
        baseList.Add(uiBase);
    }

    // UIBaseを取得
    static public UIBase GetUIBase(UI_TYPE_ID id)
    {
        for(int i = 0;i < baseList.Count;i++)
        {
            if (baseList[i].GetID() == id)
            {
                return baseList[i];
            }
        }

        return null;
    }

    // 削除する。
    static public void Remove(UIBase uiBase)
    {
        baseList.Remove(uiBase);
    }

    void OnDestroy()
    {
        baseList.Clear();
    }

    void Awake()
    {
        UIAnimation.Init();
    }

    void Start() 
    {
        InitManager(this, MANAGER_ID.UI);
	}

    void Update()
    {
        for (int i = 0; i < baseList.Count; i++)
        {
            baseList[i].UpdateUIBase();
        }

        UIAnimation.UpdateAnim();
	}

    //  -----------------------------------------
    //  公開用関数
    //  -----------------------------------------

    static public void SetupGameText(GameText[] setGameTextList)
    {
        gameTextList = setGameTextList;   
    }

    // ID から Textを取得
    static public string GetText(GAMETEXT_ID id, bool isInspectorEdit = false)
    {
        if (isInspectorEdit)
        {
#if UNITY_EDITOR
            DataTable dataTable = DatabaseManager.DebugReqLoad(DB_ID.GAMETEXT, "GameText", "GameTextTable");

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

            for (int i = 0; i < gameTextList.Length; i++)
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
