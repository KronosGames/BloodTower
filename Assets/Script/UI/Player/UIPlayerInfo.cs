using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerInfoData
{
    public Transform trans = null;
    public Image hpBarImage = null;
    public Image staminaBarImage = null;
}

public class UIPlayerInfo : UIBase
{
    UIPlayerInfoData infoData = new UIPlayerInfoData();
    int animationHandle = -1;

    void Start()
    {
        InitUI(this, UI_TYPE_ID.PLAYER_INFO, UI_SCREEN_TYPE.PLAYER_INFO);

        infoData.trans = transform;
        infoData.hpBarImage = UIUtility.GetImage(infoData.trans, "Image_HpBar");
        infoData.staminaBarImage = UIUtility.GetImage(infoData.trans, "Image_StaminaBar");
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


        float hp = (float)GameCharacterParam.GetHp();
        float maxHp = (float)GameCharacterParam.GetMaxHp();
        infoData.hpBarImage.fillAmount = hp / maxHp;

        float stamina = (float)GameCharacterParam.GetStamin();
        float maxStamin = (float)GameCharacterParam.GetMaxStamina();
        infoData.staminaBarImage.fillAmount = stamina / maxStamin;
    }

    public override void Open()
    {
        animationHandle = UIAnimation.Play(this, "anim_player_info_open");
    }

    public override void Close()
    {
        animationHandle = UIAnimation.Play(this, "anim_player_info_close");
    }
}
