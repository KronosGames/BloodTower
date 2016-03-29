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

    // 通知Windowを開く
    static public void OpenNotificationWindow(ref NotificationWindowParam param)
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.NOTIFICATION_WINDOW_INFO);
        UINotificationWindowInfo info = cBase as UINotificationWindowInfo;
        if (info != null)
        {
            info.OpenWindow(ref param);
        }
    }

    // 通知Windowを開く
    static public void ChangeWeapon()
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.WEAPON_INFO);
        UIWeaponInfo info = cBase as UIWeaponInfo;
        if (info != null)
        {
            info.ChangeWeapon();
        }
    }


}
