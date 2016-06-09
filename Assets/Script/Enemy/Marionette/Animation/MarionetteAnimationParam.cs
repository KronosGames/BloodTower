using UnityEngine;

// -----------------------------------------
// AnimationData
// -----------------------------------------

public class MarionetteAnimationParam {
    public enum ANIMATION_ID
    {
        Idling,
        Walking,
        Run,
        Jump,
        Attack00,
        Attack01,
        Dead,
        Hit,
    }
    public ANIMATION_ID id;
    public AnimationClip clip;
    public float blendTime;
    public bool isBlend;
}

