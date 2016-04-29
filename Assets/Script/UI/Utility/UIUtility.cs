using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUtility
{
    static public RectTransform GetRectTrans(Transform parentTrans, string objName)
    {
        RectTransform rectTrans = parentTrans.FindChild(objName) as RectTransform;

        if (rectTrans == null)
        {
            Debug.LogError(objName + "(RectTransform) がありません。");
        }

        return rectTrans;
    }

    static public Transform GetTrans(Transform parentTrans, string objName)
    {
        Transform trans = parentTrans.FindChild(objName);

        if (trans == null)
        {
            Debug.LogError(objName + "(Transform) がありません。");
        }

        return trans;
    }

    static public Image GetImage(Transform parentTrans, string objName = null)
    {
        Image image = null;
        if (objName == null)
        {
            image = parentTrans.GetComponent<Image>();
        }
        else
        {
            Transform trans = parentTrans.FindChild(objName);
            image = trans.GetComponent<Image>();
        }

        if (image == null)
        {
            string strError = image == null ? parentTrans.name : objName;
            Debug.LogError(strError + "(Image) がありません。");
        }

        return image;
    }


    static public Button GetButton(Transform parentTrans, string objName = null)
    {
        Button button = null;
        if (objName == null)
        {
            button = parentTrans.GetComponent<Button>();
        }
        else
        {
            Transform trans = parentTrans.FindChild(objName);
            button = trans.GetComponent<Button>();
        }

        if (button == null)
        {
            string strError = button == null ? parentTrans.name : objName;
            Debug.LogError(strError + "(Button) がありません。");
        }

        return button;
    }


    static public Slider GetSlider(Transform parentTrans, string objName = null)
    {
        Slider slider = null;
        if (objName == null)
        {
            slider = parentTrans.GetComponent<Slider>();
        }
        else
        {
            Transform trans = parentTrans.FindChild(objName);
            slider = trans.GetComponent<Slider>();
        }

        if (slider == null)
        {
            string strError = slider == null ? parentTrans.name : objName;
            Debug.LogError(strError + "(Slider) がありません。");
        }

        return slider;
    }

    static public Text GetText(Transform parentTrans, string objName = null)
    {
        Text text = null;
        if (objName == null)
        {
            text = parentTrans.GetComponent<Text>();
        }
        else
        {
            Transform trans = parentTrans.FindChild(objName);
            text = trans.GetComponent<Text>();
        }

        if (text == null)
        {
            string strError = text == null ? parentTrans.name : objName;
            Debug.LogError(strError + "(Text) がありません。");
        }

        return text;
    }

    static public RawImage GetRawImage(Transform parentTrans, string objName = null)
    {
        RawImage rawImage = null;
        if (objName == null)
        {
            rawImage = parentTrans.GetComponent<RawImage>();
        }
        else
        {
            Transform trans = parentTrans.FindChild(objName);
            rawImage = trans.GetComponent<RawImage>();
        }

        if (rawImage == null)
        {
            string strError = rawImage == null ? parentTrans.name : objName;
            Debug.LogError(strError + "(RawImage) がありません。");
        }

        return rawImage;
    }


}
