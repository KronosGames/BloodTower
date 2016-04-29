using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHolyWaterData
{
    public Transform trans = null;
    public Image barImage = null;
    public Text numText = null;
}

public class UIHolyWaterInfo : UIBase
{
    UIHolyWaterData infoData = new UIHolyWaterData();
    int animationHandle = -1;

    void Start()
    {
        InitUI(this, UI_TYPE_ID.HOLYWATER_INFO, UI_SCREEN_TYPE.PLAYER_INFO);

        infoData.trans = transform;
        infoData.barImage = UIUtility.GetImage(infoData.trans, "Image_Bar");
        infoData.numText = UIUtility.GetText(infoData.trans, "Text_Num");
    }

    void AnimationWaitStop()
    {
        if (animationHandle == -1) return;

        if (UIAnimation.IsStop(animationHandle))
        {
            UIAnimation.Stop(ref animationHandle);
        }
    }

    protected override void UpdateUI()
    {
        AnimationWaitStop();


        int num = GameCharacterParam.GetHolyWaterNum();
        int maxNum = GameCharacterParam.GetHolyWaterNum();
        infoData.numText.text = num.ToString();

        infoData.barImage.fillAmount = (float)num / (float)maxNum;
    }


    public override void Open()
    {
        animationHandle = UIAnimation.Play(this, "anim_holywater_open");
    }

    public override void Close()
    {
        animationHandle = UIAnimation.Play(this, "anim_holywater_close");
    }
}
