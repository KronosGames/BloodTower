/*
 * @file TalkListener.cs
 * @brief 会話システム、聞く側の機能実装部分
 * @date 2016-10-31
 * @auther kondo
 */

using UnityEngine;
using System.Collections;

/// <summary>
/// 会話システム
/// 聞く側の機能実装クラス
/// </summary>
public class TalkListener : MonoBehaviour {

    /// <summary>
    /// 登録済みかどうか
    /// </summary>
    public bool IsRegistered { get; set; }

    [SerializeField, Tooltip("聞こえる距離")]
    private float range = 5.0f;
    public float Range
    {
        get
        {
            return range;
        }
        set
        {
            range = value;
        }
    }

}
