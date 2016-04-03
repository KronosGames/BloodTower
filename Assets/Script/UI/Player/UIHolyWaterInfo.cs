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

    void Start()
    {
        InitUI(this, gameObject, UI_TYPE_ID.HOLYWATER_INFO);

        infoData.trans = transform;
        infoData.barImage = UIUtility.GetImage(infoData.trans, "Image_Bar");
        infoData.numText = UIUtility.GetText(infoData.trans, "Text_Num");
    }

    protected override void UpdateUI()
    {
        int num = GameCharacterParam.GetHolyWaterNum();
        int maxNum = GameCharacterParam.GetHolyWaterNum();
        infoData.numText.text = num.ToString();

        infoData.barImage.fillAmount = (float)num / (float)maxNum;
    }
}
