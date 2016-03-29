using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Animationを操作管理するクラス
public class UIAnimation
{
    const int ERROR_CODE = -1;

    struct UIAnimationData
    {
        public UIBase uiBase ;
        public Transform trans ;
        public Animation animation ;
        public AnimationState animationState ;
        public bool isPause ;
        public bool isActive ;
        public bool isPlaying ;
        public int animHandel ;
        public float animTime ;
        public float animSpeed ;
        public string animPlayingName;
    }

    static List<UIAnimationData> animationList = new List<UIAnimationData>();
    static List<UIAnimationData> playAnimationList = new List<UIAnimationData>();
    static int animationPlayHandel = 0;

    // 初期化
    static public void Init()
    {
        playAnimationList.Clear();
        animationList.Clear();
        animationPlayHandel = 0;
    }

    // アニメーションを登録する。
    static public void Register(UIBase cUIBase, GameObject cRoot)
    {
        Animation[] animations = cRoot.GetComponentsInChildren<Animation>();

        for (int i = 0; i < animations.Length; i++) 
        {
            UIAnimationData cAddData = new UIAnimationData();

            cAddData.trans = animations[i].transform;
            cAddData.animation = animations[i];
            cAddData.uiBase = cUIBase;
            cAddData.animHandel = ERROR_CODE;

            animationList.Add(cAddData);
        }

    }

    // 再生する。
    // 子オブジェクトについているアニメーションを使いたい場合はこれを使用してください。
    static public int Play(Transform cTrans, string strAnimName)
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            UIAnimationData cData = animationList[i];

            if (cData.trans == cTrans)
            {
                cData.animPlayingName = strAnimName;
                cData.animationState = cData.animation.PlayQueued(cData.animPlayingName);
                cData.isPause = false;
                cData.animHandel = animationPlayHandel++;

                playAnimationList.Add(cData);

                return cData.animHandel;
            }
        }

        return -1;
    }

    // 再生する。
    // 自分自身のアニメーションを再生したい場合は、こちらを使用してください
    static public int Play(UIBase cUIBase,string strAnimName)
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            UIAnimationData cData = animationList[i];

            if (cData.uiBase == cUIBase)
            {
                cData.animPlayingName = strAnimName;
                cData.animationState = cData.animation.PlayQueued(cData.animPlayingName);
                cData.isPause = false;
                cData.animHandel = animationPlayHandel++;
                playAnimationList.Add(cData);

                return cData.animHandel;
            }
        }

        return -1;
    }

    // アニメーションが停止させる。
    static public void Stop(ref int nHandel)
    {
        for (int i = 0; i < playAnimationList.Count; i++)
        {
            UIAnimationData cData = playAnimationList[i];

            if (cData.animHandel == ERROR_CODE) continue;

            if (cData.animHandel == nHandel)
            {
                cData.isActive = false;
                cData.isPause = false;
                cData.animation.Stop();
                cData.animationState = null;

                nHandel = ERROR_CODE;

                playAnimationList.RemoveAt(i);
                return;
            }
        }
    }

    // アニメーションが停止したかどうか
    static public bool IsStop(int nHandel)
    {
        for (int i = 0; i < playAnimationList.Count; i++)
        {
            UIAnimationData cData = playAnimationList[i];

            if (cData.animHandel == ERROR_CODE) continue;
            if (cData.animPlayingName == null) continue;

            if (cData.animHandel == nHandel)
            {
                if (!cData.isActive) continue;
                if (cData.isPause) continue;

                return !cData.isPlaying;
            }
        }

        return false;
    }

    // アニメーションを一時停止状態にする。
    static public void Pause(int nHandel)
    {
        for (int i = 0; i < playAnimationList.Count; i++)
        {
            UIAnimationData cData = playAnimationList[i];

            if (cData.animHandel == ERROR_CODE) continue;
            if (!cData.isActive) continue;
            if (!cData.isPlaying) continue;
            if (cData.isPause) continue;
            
            if (cData.animHandel == nHandel)
            {
                cData.isPause = true;
                cData.isPlaying = false;

                cData.animTime = cData.animationState.normalizedTime;
                cData.animSpeed = cData.animationState.normalizedSpeed;

                cData.animation.Stop();

                playAnimationList[i] = cData;
            }
        }
    }

    // 一時停止しているアニメーションを再再生させる。
    static public void Resume(int nHandel)
    {
        for (int i = 0; i < playAnimationList.Count; i++)
        {
            UIAnimationData cData = playAnimationList[i];

            if (cData.animHandel == ERROR_CODE) continue;
            if (!cData.isActive) continue;
            if (!cData.isPause) continue;

            if (cData.animHandel == nHandel)
            {
                cData.isPause = false;

                cData.animationState = cData.animation.PlayQueued(cData.animPlayingName);

                cData.animationState.normalizedTime = cData.animTime;
                cData.animationState.normalizedSpeed = cData.animSpeed;

                playAnimationList[i] = cData;

                return;
            }
        }
    }

    // 各UIBaseで呼ばれています。
    static public void Remove(UIBase cUIBase)
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            if (animationList[i].uiBase == cUIBase)
            {
                animationList.RemoveAt(i);
            }
        }
    }


    // UIManagerで呼ばれています。
    static public void UpdateAnim()
    {
        for (int i = 0; i < playAnimationList.Count; i++)
        {
            UIAnimationData cData = playAnimationList[i];

            if (cData.animHandel == ERROR_CODE) continue;

            cData.isPlaying = cData.animation.isPlaying;
            playAnimationList[i] = cData;

            if (cData.isPause) continue;
            if (cData.animationState == null) continue;

            cData.isActive = cData.isPlaying;

            cData.animSpeed = cData.animationState.normalizedSpeed;
            cData.animTime = cData.animationState.normalizedTime;

            playAnimationList[i] = cData;
        }
    }
}
