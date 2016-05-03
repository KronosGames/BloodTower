using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBlacksmithItemSelectInfo : UIBase {

    int animHandle = -1;

    void Start()
    {
        InitUI(this, UI_TYPE_ID.BLACKSMITH_ITEM_SELECT_INFO, UI_SCREEN_TYPE.BLACKSMITH_ITEM_SELECT_INFO);
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

    protected override void OnButtonClickProcess(Button clickButton)
    {
        if (clickButton.name == "Button_Back")
        {
            UIScreenControl.BackScreen();
            return;
        }

        int count = UIUtility.GetStringToNumber(clickButton.name);
        if (clickButton.name == "Button_Item" + count.ToString("00"))
        {
            UIScreenControl.BackScreen();
        }
    }

    public override void Open()
    {
        animHandle = UIAnimation.Play(this, "anim_blacksmith_itemselect_open");
    }

    public override void Close()
    {
        animHandle = UIAnimation.Play(this, "anim_blacksmith_itemselect_close");
    }

    public override void SetupUI()
    {

    }

}
