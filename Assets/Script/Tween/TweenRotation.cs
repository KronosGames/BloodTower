using UnityEngine;
using System.Collections;

public class TweenRotation : TweenBase
{

    public Vector3 startRotation = Vector3.zero;
    public Vector3 targetRotation = Vector3.zero;
    Vector3 tweenStartRotation = Vector3.zero;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        cashTransform.localRotation = Quaternion.Euler(startRotation);
        tweenStartRotation = cashTransform.localRotation.eulerAngles;
    }

    void Update()
    {
        if (!isTweening) return;

        cashTransform.localRotation = Quaternion.Euler(Vector3.Lerp(tweenStartRotation, targetRotation, GetCurve()));

        Finish();
    }
}
