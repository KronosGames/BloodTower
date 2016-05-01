using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBlacksmithMenuInfo : UIBase
{
    int animHandle = -1;

	void Start ()
    {
        InitUI(this, UI_TYPE_ID.BLACKSMITH_MENU_INFO, UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);
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

    }

    public override void Open()
    {
        animHandle = UIAnimation.Play(this,"anim_blacksmith_menu_open");
    }

    public override void Close()
    {
        animHandle = UIAnimation.Play(this, "anim_blacksmith_menu_close");
    }

    public override void SetupUI()
    {

    }

}
