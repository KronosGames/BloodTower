using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tween : MonoBehaviour {

    static List<TweenBase> tweenList = new List<TweenBase>();

    /// <summary>
    /// 登録する。
    /// </summary>
    /// <param name="handle"></param>
    public static void Register(TweenBase handle)
    {
        tweenList.Add(handle);
    }

    public static bool IsPlaying(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                return tweenList[i].IsPlaying;
            }
        }

        return false;
    }

    ///// <summary>
    ///// Valueを取得する。
    ///// </summary>
    ///// <param name="name"></param>
    ///// <returns></returns>
    //public static float GetValue(string name)
    //{
    //    for (int i = 0; i < tweenList.Count; i++)
    //    {
    //        if (tweenList[i].tweenName == name)
    //        {
    //            TweenValue tweenValue = tweenList[i] as TweenValue;
    //            return tweenValue.GetValue();
    //        }
    //    }

    //    return 0;
    //}

    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static TweenBase Play(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                tweenList[i].Play();
                return tweenList[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static void Plays(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                tweenList[i].Play();
            }
        }
    }

    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static TweenBase[] GetPlayList(string name)
    {
        return tweenList.FindAll(i => i.tweenName == name).ToArray();
    }


    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static void Pause(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                tweenList[i].Pause();
                return;
            }
        }
    }


    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static void Resume(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                tweenList[i].Resume();
                return;
            }
        }
    }


    /// <summary>
    /// 再生する。
    /// </summary>
    /// <param name="name"></param>
    public static void Stop(string name)
    {
        for (int i = 0; i < tweenList.Count; i++)
        {
            if (tweenList[i].tweenName == name)
            {
                tweenList[i].Stop();
                return;
            }
        }
    }

}
