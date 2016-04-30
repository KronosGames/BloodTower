using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIYouDiedInfo : UIBase
{
    Image bgImage = null;
    Text titleText = null;

    int animHandle = -1;

	void Start ()
    {
        InitUI(this, UI_TYPE_ID.YOU_DIED_INFO, UI_SCREEN_TYPE.YOU_DIED_INFO);

        bgImage = UIUtility.GetImage(transform, "Image_BG");
        titleText = UIUtility.GetText(transform, "Text_Title");

    }

    protected override void UpdateUI()
    {
        if (animHandle != -1)
        {
            titleText.SetAllDirty();
            bgImage.SetAllDirty();

            if (UIAnimation.IsStop(animHandle))
            {
                UIAnimation.Stop(ref animHandle);
            }
        }
    }
    
    public override void Open()
    {
        animHandle = UIAnimation.Play(this, "anim_youdied_open");
    }

    public override void Close()
    {
        animHandle = UIAnimation.Play(this, "anim_youdied_close");
    }



}



