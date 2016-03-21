using UnityEngine;
using System.Collections;

public class TweenScale : TweenBase {

    public Vector3 startScale = Vector3.zero;
    public Vector3 targetScale = Vector3.zero;
    Vector3 tweenStartScale = Vector3.zero;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        cashTransform.localScale = startScale;
        tweenStartScale = cashTransform.localScale;
    }

    void Update()
    {
        if (!isTweening) return;

        cashTransform.localScale = Vector3.Lerp(tweenStartScale, targetScale, GetCurve());

        Finish();

    }
}
