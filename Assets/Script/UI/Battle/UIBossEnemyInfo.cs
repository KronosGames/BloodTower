using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBossEnemyData
{
    public Transform trans = null;
    public Image hpBarImage = null;
    public Image hpBgImage = null;
    public Text nameText = null;
}

public class UIBossEnemyInfo : UIBase
{
    UIBossEnemyData infoData = new UIBossEnemyData();

    enum STATE
    {
        NULL,
        OPEN,
        UPDATE,
        CLOSE,
    }

    STATE state = STATE.NULL;
    int animHandel = -1;

    void Start()
    {
        InitUI(this, gameObject, UI_TYPE_ID.BOSS_ENEMY_INFO);

        infoData.trans = transform;
        infoData.hpBarImage = UIUtility.GetImage(infoData.trans, "Image_HpBar");
        infoData.hpBgImage = UIUtility.GetImage(infoData.trans, "Image_HpBG");
        infoData.nameText = UIUtility.GetText(infoData.trans, "Text_Name");

    }

    void SetAllDirty()
    {
        infoData.hpBarImage.SetAllDirty();
        infoData.hpBgImage.SetAllDirty();
        infoData.nameText.SetAllDirty();
    }

    protected override void UpdateUI()
    {
        switch (state)
        {
            case STATE.OPEN:
                SetAllDirty();

                if (UIAnimation.IsStop(animHandel))
                {
                    UIAnimation.Stop(ref animHandel);
                    state = STATE.UPDATE;
                }
                break;
            case STATE.UPDATE:

                float hp = BossEnemyManager.GetHp();
                float maxHp = BossEnemyManager.GetMaxHp();
                infoData.hpBarImage.fillAmount = hp / maxHp;

                break;
            case STATE.CLOSE:
                SetAllDirty();

                if (UIAnimation.IsStop(animHandel))
                {
                    UIAnimation.Stop(ref animHandel);
                    state = STATE.NULL;
                }
                break;
        }

    }

    //  --------------------------------------------------
    //  公開用関数
    //  --------------------------------------------------

    public void OpenBossEnemyUI()
    {
        infoData.nameText.text = BossEnemyManager.GetName();

        animHandel = UIAnimation.Play(this,"anim_bossenemy_in");
        state = STATE.OPEN;
    }

    public void CloseBossEnemyUI()
    {
        animHandel = UIAnimation.Play(this,"anim_bossenemy_out");
        state = STATE.CLOSE;
    }
}
