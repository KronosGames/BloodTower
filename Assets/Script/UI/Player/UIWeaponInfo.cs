using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWeaponData
{
    public Transform trans = null;
    public Image iconImage = null;
    public Text nameText = null;
}
    

public class UIWeaponInfo : UIBase
{
    public const int WEAPON_MAX = 2;

    UIWeaponData[] weaponInfoData = new UIWeaponData[WEAPON_MAX];
    int changeAnimHandle = -1;
    int screenAnimHandle = -1;
    int equipID = 0;

    void Start()
    {
        InitUI(this, UI_TYPE_ID.WEAPON_INFO, UI_SCREEN_TYPE.PLAYER_INFO);

        for (int i = 0; i < WEAPON_MAX; i++)
        {
            UIWeaponData data = new UIWeaponData();

            data.trans = UIUtility.GetTrans(transform,"Info" + i.ToString("00"));
            data.iconImage = UIUtility.GetImage(data.trans, "Image_Icon");
            data.nameText = UIUtility.GetText(data.trans, "Text_Name");

            weaponInfoData[i] = data;
        }
    }

    void AnimationWaitStop(ref int animHandle)
    {
        if (animHandle == -1) return;

        if (UIAnimation.IsStop(animHandle))
        {
            UIAnimation.Stop(ref animHandle);
        }
    }

    protected override void UpdateUI()
    {
        AnimationWaitStop(ref screenAnimHandle);
        AnimationWaitStop(ref changeAnimHandle);

        for (int i = 0; i < WEAPON_MAX; i++)
        {
            WeaponParam[] weaponList = GameCharacterParam.GetEquipWeaponParam();
            WeaponParam weapon = weaponList[i];

            weaponInfoData[i].iconImage.sprite = UIManager.GetWeaponIcon(weapon.id);
            weaponInfoData[i].nameText.text = weapon.name;
        }
    }

    //  ------------------------------------------
    //  公開用関数
    //  ------------------------------------------

    // 武器を変更する。
    public void ChangeWeapon()
    {
        changeAnimHandle = UIAnimation.Play(this,"anim_weapon_change" + equipID.ToString("00"));
        equipID = equipID == 0 ? 1 : 0;
    }

    public override void Open()
    {
        screenAnimHandle = UIAnimation.Play(this, "anim_weapon_info_open");
    }

    public override void Close()
    {
        screenAnimHandle = UIAnimation.Play(this, "anim_weapon_info_close");
    }
}
