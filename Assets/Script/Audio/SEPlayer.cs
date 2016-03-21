// -------------------------------------------
//  SEを再生するスクリプト
//
//  code by m_yamada
// -------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SEPlayer : MonoBehaviour
{

    public class Data
    {
        public Data(SEAudioData data)
        {
            this.data = data;
            clip = Resources.Load("Audio/SE/" + data.label) as AudioClip;
        }
        public SEAudioData data;
        public AudioClip clip;
    }

    public class SEPlayData
    {
        public bool isActive = false;
        public int index = -1;
        public AudioSource audioSource = null;
    }

    [SerializeField]
    int sourceMaxCount = 10;

    static int sourceMaxList = 0;
    static int playIndex = -1;
    static int sourceIndex = -1;
    static List<SEPlayData> playAudioList = new List<SEPlayData>();
    static List<AudioSource> sources = new List<AudioSource>();
    static Dictionary<Audio.SEID, Data> audioMap = new Dictionary<Audio.SEID, Data>();

    public const float maxVolume = 1.0f;

    void Awake()
    {
        sourceMaxList = sourceMaxCount;
        for (int i = 0; i < sourceMaxList; i++)
        {
            sources.Add(gameObject.AddComponent<AudioSource>());
        }

        foreach (var data in AudioManager.SEData)
        {
            audioMap.Add(data.id, new Data(data));
        }

        sourceIndex = 0;
        playIndex = 0;
    }

    void Start()
    {
    }

    void Update()
    {
        for (int i = 0; i < playAudioList.Count; i++)
        {
            var playAudio = playAudioList[i];
            if (!playAudio.isActive) break;

            if (!playAudio.audioSource.isPlaying)
            {
                playAudioList.Remove(playAudio);
                break;
            }
        }

    }


    /// <summary>
    /// 再生
    /// </summary>
    /// <param name="resName">Resource名</param>
    static public int Play(Audio.SEID id, float pitch = 1.0f, bool loop = false)
    {
        SEPlayData data = new SEPlayData();
        data.isActive = true;
        data.index = playIndex++;

        sourceIndex = sourceIndex >= sourceMaxList ? 0 : sourceIndex;
        data.audioSource = sources[sourceIndex++];

        data.audioSource.clip = audioMap[id].clip;
        data.audioSource.pitch = pitch;
        data.audioSource.loop = loop;
        data.audioSource.volume = maxVolume;
        data.audioSource.Play();

        playAudioList.Add(data);

        return data.index;
    }

    /// <summary>
    /// 音量を変える
    /// </summary>
    /// <param name="resName"></param>
    /// <param name="volume"></param>
    static public void ChangeVolume(ref int handelIndex, float volume)
    {
        foreach (var source in playAudioList)
        {
            if (source.index == handelIndex)
            {
                source.audioSource.volume = volume;
                break;
            }
        }
    }


    /// <summary>
    /// 停止
    /// </summary>
    /// <param name="resName">Resource名</param>
    static public void Stop(ref int handelIndex, float time = 0.0f)
    {
        foreach (var source in playAudioList)
        {
            if (source.index == handelIndex)
            {
                source.audioSource.Stop();
                source.index = -1;
                source.isActive = false;
                handelIndex = -1;
                return;
            }
        }
    }


    /// <summary>
    /// すべて停止
    /// </summary>
    /// <param name="resName">Resource名</param>
    public void AllStop()
    {
        foreach (var source in playAudioList)
        {
            source.audioSource.Stop();
        }

        playAudioList.Clear();
    }

    /// <summary>
    /// 再生中かどうか
    /// </summary>
    /// <param name="resName">Resource名</param>
    /// <returns></returns>
    public bool IsPlaying(int handelIndex)
    {
        foreach (var source in playAudioList)
        {
            if (source.audioSource.isPlaying && source.index == handelIndex)
            {
                return true;
            }
        }

        return false;
    }

}


