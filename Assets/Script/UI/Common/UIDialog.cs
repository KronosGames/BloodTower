using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDialog : UIBase
{
    public enum BUTTON_TYPE
    {
        OK,
        YES_NO,
    }

    public enum PUSH_TYPE
    {
        NONE = -1,
        NO = 0,
        OK = 1,
        YES = 2,
    }

    const int BUTTON_COUNT = 3;

    Text titleText = null;
    Text infoText = null;

    Transform[] buttonTransList = new Transform[BUTTON_COUNT];

    PUSH_TYPE pushType = PUSH_TYPE.NONE;

    int animHandle = -1;

    void Start ()
    {
        InitUI(this, UI_TYPE_ID.DIALOG);

        titleText = UIUtility.GetText(transform, "Text_Title");
        infoText = UIUtility.GetText(transform, "Text_Info");

        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            buttonTransList[i] = UIUtility.GetTrans(transform,"Button_Info" + i.ToString("00"));
        }
	}

    protected override void UpdateUI()
    {
        if (animHandle != -1)
        {
            if (UIAnimation.IsStop(animHandle))
            {
                UIAnimation.Stop(ref animHandle);
            }
        }
    }

    protected override void ButtonClickSelect(Button clickButton, int count)
    {
        pushType = (PUSH_TYPE)count;
    }

    public override void Close()
    {
        SetButtonLock(false);
        animHandle = UIAnimation.Play(this, "anim_dialog_close");
    }

    public void OpenDialog(BUTTON_TYPE buttonType,ref string title, ref string info)
    {
        titleText.text = title;
        infoText.text = info;

        for (int i = 0; i < BUTTON_COUNT; i++)
        {
            buttonTransList[i].gameObject.SetActive(false);
        }
                 
        if (buttonType == BUTTON_TYPE.OK)
        {
            buttonTransList[(int)PUSH_TYPE.OK].gameObject.SetActive(true);
        }
        else if (buttonType == BUTTON_TYPE.YES_NO)
        {
            buttonTransList[(int)PUSH_TYPE.YES].gameObject.SetActive(true);
            buttonTransList[(int)PUSH_TYPE.NO].gameObject.SetActive(true);
        }

        animHandle = UIAnimation.Play(this, "anim_dialog_open");

        pushType = PUSH_TYPE.NONE;

        SetButtonLock(true, this);
    }


    public PUSH_TYPE GetPushType()
    {
        return pushType;
    }

}

