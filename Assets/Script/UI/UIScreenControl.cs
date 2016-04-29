using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIScreenControl {

    static Dictionary<UI_SCREEN_TYPE,List<UIBase>> screenDict = new Dictionary<UI_SCREEN_TYPE, List<UIBase>>();
    static List<UI_SCREEN_TYPE> currentScreenList = new List<UI_SCREEN_TYPE>();

    public void Setup()
    {
        screenDict.Clear();
        currentScreenList.Clear();
    }

    public void Rigster(UI_SCREEN_TYPE screenType,UIBase uiBase)
    {
        if (!screenDict.ContainsKey(screenType))
        {
            screenDict.Add(screenType, new List<UIBase>());
        }

        screenDict[screenType].Add(uiBase);
    }

    // スクリーン情報を変更する。
    static public void ChangeScreen(UI_SCREEN_TYPE screenType)
    {
        for (int i = 0; i < currentScreenList.Count; i++)
        {
            CloseScreen(currentScreenList[i]);
        }

        currentScreenList.Clear();

        // 現在の情報を変更する。
        currentScreenList.Add(screenType);

        UIBase[] uiBaseList = screenDict[screenType].ToArray();

        for (int i = 0; i < uiBaseList.Length; i++)
        {
            uiBaseList[i].SetupUI();
            uiBaseList[i].Open();
        }
    }

    // スクリーン情報を加算
    static public void AdditiveScreen(UI_SCREEN_TYPE screenType)
    {
        if (currentScreenList.Contains(screenType)) return;

        currentScreenList.Add(screenType);

        UIBase[] uiBaseList = screenDict[screenType].ToArray();

        for (int i = 0; i < uiBaseList.Length; i++)
        {
            uiBaseList[i].SetupUI();
            uiBaseList[i].Open();
        }
    }

    // スクリーン情報を閉じる
    static public void CloseScreen(UI_SCREEN_TYPE screenType)
    {
        if (!currentScreenList.Contains(screenType)) return;

        UIBase[] uiBaseList = screenDict[screenType].ToArray();

        for (int i = 0; i < uiBaseList.Length; i++)
        {
            uiBaseList[i].Close();
        }

        currentScreenList.Remove(screenType);
    }

    // 最新のスクリーン情報を閉じる
    static public void BackScreen()
    {
        int count = currentScreenList.Count - 1;
        if (count <= -1) return;

        UI_SCREEN_TYPE screenType = currentScreenList[count];
        UIBase[] uiBaseList = screenDict[screenType].ToArray();

        for (int i = 0; i < uiBaseList.Length; i++)
        {
            uiBaseList[i].Close();
        }

        currentScreenList.Remove(screenType);
    }


    // すべて閉じる
    static public void AllCloseScreen()
    {
        int maxCount = currentScreenList.Count;
        for (int i = 0; i < maxCount; i++)
        {
            UI_SCREEN_TYPE screenType = currentScreenList[i];
            CloseScreen(screenType);
        }

        currentScreenList.Clear();
    }

}
