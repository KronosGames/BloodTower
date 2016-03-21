// -------------------------------------------
//  BGMを再生するスクリプト
//
//  code by m_yamada
// -------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct FadeTimeData
{
    public FadeTimeData(float inTime, float outTime)
        : this()
    {
        this.inTime = inTime;
        this.outTime = outTime;
    }

    public static FadeTimeData Zero { get { return new FadeTimeData(0, 0); } }

    public float inTime;
    public float outTime;
}


public class BGMPlayer : MonoBehaviour
{

    public class Data
    {
        public Data(BGMAudioData data)
        {
            this.data = data;
            clip = Resources.Load("Audio/BGM/" + data.label) as AudioClip;
        }

        public BGMAudioData data;
        public AudioClip clip;
    }

    const float minVolume = 0;
    const float maxVolume = 1.0f;
    const float startFadeInVolume = 0.005f;

    static AudioSource source = null;
    static Dictionary<Audio.BGMID, Data> audioMap = new Dictionary<Audio.BGMID, Data>();
    static FadeTimeData FadeTime;

    static public bool IsPlaying { get { return source.isPlaying; } }

    void Awake() 
    {
        source = GetComponent<AudioSource>();

        foreach (var data in AudioManager.BGMData)
        {
            audioMap.Add(data.id, new Data(data));
        }

    }

    void Update()
    { 
        
    }

    /// <summary>
    /// 名前で再生中かどうかを探す
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    static public bool IsPlayingToName(Audio.BGMID id)
    {
        if (source.clip == null) return false;

        if (source.clip.name == audioMap[id].clip.name && source.isPlaying)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="id">Resources/BGM/の中にあるオーディオ名</param>
    /// <param name="fadeInTime">フェードイン時間</param>
    static public void Play(Audio.BGMID id, FadeTimeData fadeTime)
    {
        FadeTime = fadeTime;
        source.clip = audioMap[id].clip;
        source.Play();
        source.volume = startFadeInVolume;
        StartFadeIn(FadeTime.inTime);
    }

    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="fadeOutTime">フェードアウト時間</param>
    static public void Stop()
    {
        StartFadeOut(FadeTime.outTime);
    }

    /// <summary>
    /// フェードアウトスタート
    /// </summary>
    /// <param name="time">時間</param>
    static void StartFadeOut(float time)
    {
        UpdateHandle(minVolume);
    }

    /// <summary>
    /// フェードインスタート
    /// </summary>
    /// <param name="time">時間</param>
    static void StartFadeIn(float time)
    {
        UpdateHandle(maxVolume);
    }

    static void UpdateHandle(float value)
    {
        source.volume = value;

        if (source.volume <= 0)
        {
            source.Stop();
        }
    }

}


