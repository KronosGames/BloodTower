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

    void Start()
    {
        InitUI(this, gameObject, UI_TYPE_ID.PLAYER_INFO);

        infoData.trans = transform;
        infoData.hpBarImage = UIUtility.GetImage(infoData.trans, "Image_HpBar");
        infoData.staminaBarImage = UIUtility.GetImage(infoData.trans, "Image_StaminaBar");
    }

    protected override void UpdateUI()
    {
        float hp = (float)GameCharacterParam.GetHp();
        float maxHp = (float)GameCharacterParam.GetMaxHp();
        infoData.hpBarImage.fillAmount = hp / maxHp;

        float stamina = (float)GameCharacterParam.GetStamin();
        float maxStamin = (float)GameCharacterParam.GetMaxStamin();
        infoData.staminaBarImage.fillAmount = stamina / maxStamin;
    }


}
