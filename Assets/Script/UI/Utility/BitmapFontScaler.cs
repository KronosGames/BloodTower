using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ビットマップフォントをuGUIで使用する場合、FontSizeの指定ができないため
/// 代替としてTransformのScaleによるスケーリングを行う
/// </summary>
public class BitmapFontScaler : MonoBehaviour
{
    [SerializeField, Header("ビットマップフォント生成時の元サイズ")]
    float bitmapFontScale = 32f;

    [SerializeField, Header("表示したいフォントサイズ")]
    float fonstScale = 32f;

    Transform myTransform;
    public new Transform transform
    {
        get
        {
            if (myTransform == null)
            {
                myTransform = GetComponent<Transform>();
            }
            return myTransform;
        }
    }

    public float FontScale
    {
        get
        {
            return fonstScale;
        }
        set
        {
            fonstScale = value;
            UpdateScale();
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        UpdateScale();
    }
#endif

    void UpdateScale()
    {
        float scale;

        if (FontScale <= 0 || bitmapFontScale <= 0)
        {
            scale = 0f;
        }
        else
        {
            scale = FontScale / bitmapFontScale;
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}