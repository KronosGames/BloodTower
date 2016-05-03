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

    enum STATE
    {
        NONE,
        OPEN,
        UPDATE,
        DIALOG,
        CLOSE,
    }
    STATE state = STATE.NONE;

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
        switch (state)
        {
            case STATE.OPEN:
                if (UIAnimation.IsStop(animHandle))
                {
                    UIAnimation.Stop(ref animHandle);
                    state = STATE.UPDATE;
                }
                break;
            case STATE.UPDATE:
                break;
            case STATE.DIALOG:
                if (UIEvent.GetDialogPushType() == UIDialog.PUSH_TYPE.OK)
                {
                    UIEvent.CloseDialog();
                    UIScreenControl.CloseScreen(UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);
                    state = STATE.NONE;
                    return;
                }
                break;
            case STATE.CLOSE:
                if (UIAnimation.IsStop(animHandle))
                {
                    UIAnimation.Stop(ref animHandle);
                    state = STATE.NONE;
                }
                break;
        }
    }

    protected override void ButtonClickProccess(Button clickButton)
    {
        if (clickButton.name == "Button_Back")
        {
            UIScreenControl.CloseScreen(UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);
            return;
        }
    }

    protected override void ButtonClickSelect(Button clickButton, int count)
    {
        if (clickButton.name == "Weapon_Info" + count.ToString("00"))
        {
            UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.BLACKSMITH_ITEM_SELECT_INFO);
        }
    }

    public override void Open()
    {
        if (state != STATE.NONE) return;

        animHandle = UIAnimation.Play(this,"anim_blacksmith_menu_open");
        state = STATE.OPEN;
    }

    public override void Close()
    {
        if (state == STATE.NONE) return;

        animHandle = UIAnimation.Play(this, "anim_blacksmith_menu_close");
        state = STATE.CLOSE;
    }

    // 武器を装備しているかどうか
    bool IsEquipWeapon()
    {
        WeaponParam[] weaponParams = GameCharacterParam.GetEquipWeaponParam();
        for (int i = 0; i < WEAPON_CONTENT_COUNT; i++)
        {
            if (weaponParams[i].id != WEAPON_ID.NULL)
            {
                return true;
            }
        }

        return false;
    }

    public override void SetupUI()
    {
        if (!IsEquipWeapon())
        {
            string titleDialog = "Warning is Equip Weapon";
            string infoDialog = "なにも装備されていません。\n装備してからもう一度　来てください";

            UIEvent.OpenDialog(UIDialog.BUTTON_TYPE.OK, ref titleDialog, ref infoDialog);
            state = STATE.DIALOG;
            return;
        }

        WeaponParam[] weaponParams = GameCharacterParam.GetEquipWeaponParam();
        for (int i = 0; i < WEAPON_CONTENT_COUNT; i++)
        {
            UIBlacksmithWeaponContent data = weaponContents[i];
            WeaponParam equipWeapon = weaponParams[i];

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
