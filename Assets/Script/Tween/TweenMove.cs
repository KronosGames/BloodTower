using UnityEngine;
using System.Collections;

public class TweenMove : TweenBase
{
    public Vector3 startPosition = Vector3.zero;
    public Vector3 targetPosition = Vector3.zero;
    Vector3 tweenStartPos = Vector3.zero;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        cashTransform.localPosition = startPosition;
        tweenStartPos = cashTransform.localPosition;
    }


    void Update()
    {
        if (!isTweening) return;

        cashTransform.localPosition = Vector3.Lerp(tweenStartPos, targetPosition, GetCurve());

        Finish();
    }
}
