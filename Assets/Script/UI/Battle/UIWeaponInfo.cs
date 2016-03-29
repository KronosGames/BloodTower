﻿using UnityEngine;
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
    int animHandel = -1;
    int equipID = 0;

    void Start()
    {
        InitUI(this, gameObject, UI_TYPE_ID.WEAPON_INFO);

        for (int i = 0; i < WEAPON_MAX; i++)
        {
            UIWeaponData data = new UIWeaponData();

            data.trans = UIUtility.GetTrans(transform,"Info" + i.ToString("00"));
            data.iconImage = UIUtility.GetImage(data.trans, "Image_Icon");
            data.nameText = UIUtility.GetText(data.trans, "Text_Name");

            weaponInfoData[i] = data;
        }
    }

    protected override void UpdateUI()
    {
        for (int i = 0; i < WEAPON_MAX; i++)
        {
            WeaponParam[] weaponList = GameCharacterParam.GetEquipWeaponParam();
            WeaponParam weapon = weaponList[i];

            weaponInfoData[i].iconImage.sprite = UIManager.GetWeaponIcon(weapon.id);
            weaponInfoData[i].nameText.text= weapon.name;
        }

        if (UIAnimation.IsStop(animHandel))
        {
            UIAnimation.Stop(ref animHandel);
        }
    }

    //  ------------------------------------------
    //  公開用関数
    //  ------------------------------------------

    // 武器を変更する。
    public void ChangeWeapon()
    {
        animHandel = UIAnimation.Play(this,"anim_weapon_change" + equipID.ToString("00"));
        equipID = equipID == 0 ? 1 : 0;
    }
}