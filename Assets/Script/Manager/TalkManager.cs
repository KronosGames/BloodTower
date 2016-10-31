/*
 * @file TalkManager.cs
 * @brief 会話システム、管理者機能実装部分
 * @date 2016-10-31
 * @auther kondo
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 会話システム
/// 管理者機能実装クラス
/// </summary>
public class TalkManager : MonoBehaviour {

    /// <summary>
    /// 話す側一覧
    /// </summary>
    private List<TalkSpeaker> speakerList = new List<TalkSpeaker>();

    /// <summary>
    /// 聞く側一覧
    /// </summary>
    private List<TalkListener> listenerList = new List<TalkListener>();

    /// <summary>
    /// 更新間隔[秒]
    /// </summary>
    /// <note>
    /// 毎フレームじゃなくても良いと思って…
    /// </note>
    [SerializeField,Tooltip("更新間隔[秒]")]
    private float updateInterval = 1.0f;

    /// <summary>
    /// 前回更新してからの経過時間
    /// </summary>
    /// <note>
    /// updateInterval秒経ったかの計測用
    /// </note>
    private float updateCoolingTime = 0.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        // 更新時間かのチェック&時間更新
        if( UpdateTimer() )
        {// 更新するまでの時間が経過している場合

            // 総当たりチェックを行う
        }
	    
	}

    /// <summary>
    /// 話す側を登録する
    /// </summary>
    /// <param name="talkSpeaker">話す側のコンポーネント登録</param>
    /// true...登録成功
    /// false...登録失敗
    /// <returns></returns>
    public bool RegistSpeaker( ref TalkSpeaker talkSpeaker )
    {
        if ( talkSpeaker.IsRegistered )
        {
            return false;
        }

        speakerList.Add( talkSpeaker );
        talkSpeaker.IsRegistered = true;
        return true;
    }

    /// <summary>
    /// 聞く側を登録する
    /// </summary>
    /// <param name="talkListener">聞く側のコンポーネント登録</param>
    /// <returns>
    /// true...登録成功
    /// false...登録失敗
    /// </returns>
    public bool RegistListener( ref TalkListener talkListener )
    {
        if ( talkListener.IsRegistered )
        {
            return false;
        }

        listenerList.Add( talkListener );
        talkListener.IsRegistered = true;
        return true;
    }

    /// <summary>
    /// 更新間隔計測タイマー更新
    /// </summary>
    /// <returns>
    /// true...更新タイミング
    /// false...更新タイミングでない
    /// </returns>
    private bool UpdateTimer()
    {
        if (updateCoolingTime < updateInterval)
        {// 更新するまでの時間に到達していない際の処理
            updateCoolingTime += Time.deltaTime;
            return false;
        }

        {// 更新するまでの時間が経った際の処理
            updateCoolingTime = 0.0f;
        }

        return true;
    }

    /// <summary>
    /// 声が聞こえる範囲に居るか
    /// </summary>
    /// <param name="speaker">話す側</param>
    /// <param name="listener">聞く側</param>
    /// <returns></returns>
    private bool IsHearing(TalkSpeaker speaker,TalkListener listener)
    {
        // 距離測定
        float distance = Vector3.Distance(speaker.transform.position, listener.transform.position);

        if (listener.Range < distance || speaker.Range < distance) 
        {// 声の届く/聞こえる距離内に居ない場合は聞こえない
            return false;
        }

        return true;
    }

}
