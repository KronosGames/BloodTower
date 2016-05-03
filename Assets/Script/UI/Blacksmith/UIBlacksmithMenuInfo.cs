using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBlacksmithWeaponContent
{
    public Transform m_cTrans = null;
    public Image activeImage = null;
    public Image deActiveImage = null;
    public Image iconImage = null;
    public Text weaponNameText = null;
    public Text skillNameText = null;
    public Text hardnessValueText = null;
}

public class UIBlacksmithMenuInfo : UIBase
{
    const int WEAPON_CONTENT_COUNT = 2;
    UIBlacksmithWeaponContent[] weaponContents = new UIBlacksmithWeaponContent[WEAPON_CONTENT_COUNT];

    int animHandle = -1;

	void Start ()
    {
        InitUI(this, UI_TYPE_ID.BLACKSMITH_MENU_INFO, UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);

        for (int i = 0; i < WEAPON_CONTENT_COUNT; i++)
        {
            UIBlacksmithWeaponContent data = new UIBlacksmithWeaponContent();
            data.m_cTrans = UIUtility.GetTrans(transform, "Weapon_Info" + i.ToString("00"));
            data.activeImage = UIUtility.GetImage(data.m_cTrans, "Image_ActiveBG");
            data.deActiveImage = UIUtility.GetImage(data.m_cTrans, "Image_DeActiveBG");
            data.iconImage = UIUtility.GetImage(data.m_cTrans, "Image_Icon");
            data.weaponNameText = UIUtility.GetText(data.m_cTrans, "Text_WeaponName");
            data.skillNameText = UIUtility.GetText(data.m_cTrans, "Text_SkillName");
            data.hardnessValueText = UIUtility.GetText(data.m_cTrans, "Text_EnduranceValue");

            weaponContents[i] = data;
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

    protected override void OnButtonClickProcess(Button clickButton)
    {
        if (clickButton.name == "Button_Back")
        {
            UIScreenControl.CloseScreen(UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);
            return;
        }

        int count = UIUtility.GetStringToNumber(clickButton.name);
        if (clickButton.name == "Weapon_Info" + count.ToString("00"))
        {
            UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.BLACKSMITH_ITEM_SELECT_INFO);
        }
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
        WeaponParam[] weaponParams = GameCharacterParam.GetEquipWeaponParam();
        for (int i = 0; i < WEAPON_CONTENT_COUNT; i++)
        {
            UIBlacksmithWeaponContent data = weaponContents[i];
            WeaponParam equipWeapon = weaponParams[i];
            if (equipWeapon == null) continue;

            bool isActive = equipWeapon.id != WEAPON_ID.NULL;
            data.m_cTrans.gameObject.SetActive(isActive);

            if (!isActive) continue;

            data.activeImage.enabled = false;
            data.deActiveImage.enabled = true;

            //data.iconImage.sprite = ;

            data.weaponNameText.text = equipWeapon.name;
            data.skillNameText.text = SkillDatabase.GetSkillById(equipWeapon.skillID).name;
            data.hardnessValueText.text = equipWeapon.hardness.ToString();
        }

        weaponContents[0].activeImage.enabled = true;
    }

}
