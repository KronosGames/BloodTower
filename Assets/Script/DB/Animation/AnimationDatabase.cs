using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnimationData
{
    public string labelName = "";
    public AnimationClip clip = null;
    public bool isBlend = false;
    public float blendTime = 0.0f;
    public bool isFold = true;
}

public class AnimationDatabase : ScriptableObject
{
    public string assetName = "";
    public List<AnimationData> animationList = new List<AnimationData>();
}