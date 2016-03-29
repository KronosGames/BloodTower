using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINotificationWindowData
{
    public Transform trans = null;
    public Image bgImage = null;
    public Image iconImage = null;
    public Image iconBGImage = null;
    public Text titleText = null;
    public Text nameText = null;
    public Text exlpainText = null;
}

// 通知を開く際に使用します。
public class NotificationWindowParam
{
    public int iconID = 0;
    public string title = "";
    public string name = "";
    public string exlpain = "";
}

public class UINotificationWindowInfo : UIBase
{
    enum STATE
    {
        NULL,
        OPEN,
        UPDATE,
        CLOSE,
    }

    UINotificationWindowData windowInfoData = new UINotificationWindowData();
    STATE state = STATE.NULL;
    int animHandel = -1;

    void Start()
    {
        InitUI(this, gameObject, UI_TYPE_ID.NOTIFICATION_WINDOW_INFO);

        windowInfoData.trans = transform;
        windowInfoData.bgImage = UIUtility.GetImage(windowInfoData.trans, "Image_BG");
        windowInfoData.iconBGImage = UIUtility.GetImage(windowInfoData.trans, "Image_IconBG");
        windowInfoData.iconImage = UIUtility.GetImage(windowInfoData.trans, "Image_Icon");
        windowInfoData.titleText = UIUtility.GetText(windowInfoData.trans, "Text_Title");
        windowInfoData.nameText = UIUtility.GetText(windowInfoData.trans, "Text_Name");
        windowInfoData.exlpainText = UIUtility.GetText(windowInfoData.trans, "Text_Exlpain");
    }

    // すべて表示系を反映
    void SetAllDirty()
    {
        windowInfoData.bgImage.SetAllDirty();
        windowInfoData.iconBGImage.SetAllDirty();
        windowInfoData.iconImage.SetAllDirty();
        windowInfoData.titleText.SetAllDirty();
        windowInfoData.nameText.SetAllDirty();
        windowInfoData.exlpainText.SetAllDirty();
    }

    protected override void UpdateUI()
    {
        switch (state)
        {
            case STATE.OPEN:
                SetAllDirty();

                if (UIAnimation.IsStop(animHandel))
                {
                    UIAnimation.Stop(ref animHandel);
                    state = STATE.UPDATE;
                }
                break;
            case STATE.UPDATE:
                animHandel = UIAnimation.Play(this, "anim_notification_out");
                state = STATE.CLOSE;
                break;
            case STATE.CLOSE:
                SetAllDirty();

                if (UIAnimation.IsStop(animHandel))
                {
                    UIAnimation.Stop(ref animHandel);
                    state = STATE.NULL;
                }
                break;
        }


    }

    //  -------------------------------------------
    //  公開用関数
    //  -------------------------------------------

    // Windowを開く
    public void OpenWindow(ref NotificationWindowParam param)
    {
        UIAnimation.Stop(ref animHandel);

        state = STATE.OPEN;
        animHandel = UIAnimation.Play(this, "anim_notification_in");

        windowInfoData.titleText.text = param.title;
        windowInfoData.nameText.text = param.name;
        windowInfoData.exlpainText.text = param.exlpain;

        //windowInfoData.iconImage.sprite = UIManager.GetWeaponIcon();
    }

}
