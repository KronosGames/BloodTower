using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Reflection;


// UIBaseを継承したクラスの関数(データ)をまとめる。
public class UIEvent
{
	static public void SetBossEnemyHpGauge(float fillAmount)
	{
		UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.BOSS_ENEMY_INFO);
		UIBossEnemyInfo info = cBase as UIBossEnemyInfo;
		if (info != null)
		{
			info.SetHpGauge(fillAmount);
		}
	}

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
    static public void ChangeEquipWeapon()
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.WEAPON_INFO);
        UIWeaponInfo info = cBase as UIWeaponInfo;
        if (info != null)
        {
            info.ChangeWeapon();
        }
    }
    
    // ダイアログを開く
    static public void OpenDialog(UIDialog.BUTTON_TYPE buttonType, ref string title, ref string explain)
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.DIALOG);
        UIDialog info = cBase as UIDialog;
        if (info != null)
        {
            info.OpenDialog(buttonType,ref title,ref explain);
        }
    }

    // ダイアログを閉じる
    static public void CloseDialog()
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.DIALOG);
        UIDialog info = cBase as UIDialog;
        if (info != null)
        {
            info.Close();
        }
    }

    // ダイアログ 押したボタンを取得
    static public UIDialog.PUSH_TYPE GetDialogPushType()
    {
        UIBase cBase = UIManager.GetUIBase(UI_TYPE_ID.DIALOG);
        UIDialog info = cBase as UIDialog;
        if (info != null)
        {
            return info.GetPushType();
        }

        return UIDialog.PUSH_TYPE.NONE;
    }

}
