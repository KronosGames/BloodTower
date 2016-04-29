using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum UI_PARAM_ID
{
    NONE = -1,
    HP,
    STAMINA,
    ATTACK,
    DEFENSE,
    MOVE_SPEED,
    ATTACK_SPEED,
    MAX,
}

public class UIUserNameData
{
    public Transform trans = null;
    public Text nameText = null;
}

public class UIPlayerStatusParamData
{
    public Transform trans = null;
    public Text valueText = null;
    public Text addValueText = null;
    public Text[] arcText = new Text[2];
    public UI_PARAM_ID id = UI_PARAM_ID.NONE;
}

public class UIWeaponStatusData
{
    public Transform trans;
    public Text swordValueText = null;
    public Text spearValueText = null;
    public Text clubValueText = null;
    public Text daggerValueText = null;
}

public class UIPlayerStatusMenuData
{
    public Transform trans = null;
    public UIUserNameData userNameData = new UIUserNameData();
    public UIPlayerStatusParamData[] statusParamList = new UIPlayerStatusParamData[(int)UI_PARAM_ID.MAX];
}

// 武器説明
public class UIWeaponExlpainInfo
{
    public Transform trans = null;
    public Image iconImage = null;
    public Text nameText = null;
    public Text typeText = null;
    public Text skillText = null;
    public Text durabilityValueText = null;
    public Text durabilityMaxValueText = null;
    public Text exlpainText = null;
}

public class UIPlayerStatusMenuInfo : UIBase {

    const int WEAPON_MAX = 2;

    UIPlayerStatusMenuData statusData = new UIPlayerStatusMenuData();
    UIWeaponStatusData weaponStatusData = new UIWeaponStatusData();
    UIWeaponExlpainInfo[] weaponExlpainList = new UIWeaponExlpainInfo[WEAPON_MAX];

    int animationHandle = -1;

    void Start()
    {
        InitUI(this, UI_TYPE_ID.PLAYER_STATUS_MENU_INFO, UI_SCREEN_TYPE.PLAYER_STATUS_MENU_INFO);

        // Status
        statusData.trans = UIUtility.GetTrans(transform,"Info00");
        statusData.userNameData.trans = UIUtility.GetTrans(statusData.trans, "UserName");
        statusData.userNameData.nameText = UIUtility.GetText(statusData.userNameData.trans, "Text_Value");

        for (int i = 0; i < statusData.statusParamList.Length; i++)
        {
            UIPlayerStatusParamData data = new UIPlayerStatusParamData();

            data.id = (UI_PARAM_ID)i;
            data.trans = UIUtility.GetTrans(statusData.trans, "Info" + i.ToString("00"));
            data.valueText = UIUtility.GetText(data.trans, "Text_Value");
            data.addValueText = UIUtility.GetText(data.trans, "Text_AddValue");
            data.arcText[0] = UIUtility.GetText(data.trans, "Text_(");
            data.arcText[1] = UIUtility.GetText(data.trans, "Text_)");

            statusData.statusParamList[i] = data;
        }

        // 武器熟練度
        weaponStatusData.trans = UIUtility.GetTrans(transform, "Info01");

        Transform trans = UIUtility.GetTrans(weaponStatusData.trans, "Info00");
        weaponStatusData.swordValueText = UIUtility.GetText(trans, "Text_Value");

        trans = UIUtility.GetTrans(weaponStatusData.trans, "Info01");
        weaponStatusData.spearValueText = UIUtility.GetText(trans, "Text_Value");

        trans = UIUtility.GetTrans(weaponStatusData.trans, "Info01");
        weaponStatusData.clubValueText = UIUtility.GetText(trans, "Text_Value");

        trans = UIUtility.GetTrans(weaponStatusData.trans, "Info01");
        weaponStatusData.daggerValueText = UIUtility.GetText(trans, "Text_Value");

        // 武器説明
        trans = UIUtility.GetTrans(transform, "Info02");
        for (int i = 0; i < WEAPON_MAX; i++)
        {
            UIWeaponExlpainInfo data = new UIWeaponExlpainInfo();
            Transform dataTrans = null;

            data.trans = UIUtility.GetTrans(trans,"Info" + i.ToString("00"));

            dataTrans = UIUtility.GetTrans(data.trans, "Icon");
            data.iconImage = UIUtility.GetImage(dataTrans, "Image_Icon");

            dataTrans = UIUtility.GetTrans(data.trans, "Name");
            data.nameText = UIUtility.GetText(dataTrans, "Text_Name");

            dataTrans = UIUtility.GetTrans(data.trans, "Type");
            data.typeText = UIUtility.GetText(dataTrans, "Text_Value");

            dataTrans = UIUtility.GetTrans(data.trans, "Skill");
            data.skillText = UIUtility.GetText(dataTrans, "Text_Value");

            dataTrans = UIUtility.GetTrans(data.trans, "Durability");
            data.durabilityValueText = UIUtility.GetText(dataTrans, "Text_Value");
            data.durabilityMaxValueText = UIUtility.GetText(dataTrans, "Text_MaxValue");

            dataTrans = UIUtility.GetTrans(data.trans, "Exlpain");
            data.exlpainText = UIUtility.GetText(dataTrans, "Text_Value");

            weaponExlpainList[i] = data;
        }

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
    }


    protected override void OnButtonClickProcess(Button clickButton)
    {
        if (clickButton.name == "Button_Close")
        {
            UIScreenControl.BackScreen();
        }
    }

    public override void SetupUI()
    {

    }

    public override void Open()
    {
        animationHandle = UIAnimation.Play(this, "anim_player_status_menu_open");
    }

    public override void Close()
    {
        animationHandle = UIAnimation.Play(this, "anim_player_status_menu_close");
    }
}
