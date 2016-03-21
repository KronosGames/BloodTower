using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Reflection;


// UIBaseを継承したクラスの関数(データ)をまとめる。
public class UIEvent
{
    static public void SetScrollViewSize(int createNum)
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.SCROLL_CONTENT);
        UIScrollContentCreator info = cBase as UIScrollContentCreator;
        if (info != null)
        {
            info.SetViewSize(createNum);
        }
    }

}
