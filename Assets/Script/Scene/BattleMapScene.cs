using UnityEngine;
using System.Collections;

public class BattleMapScene : MonoBehaviour
{
    // シーンを切り替える種類
    public enum CHANGE_SCENE_TYPE
    {
        NONE,   //< 未定
        HOME,   //< ホーム
        CAMP    //< キャンプ
    }

    enum SEQ_STATE
    {
        SETUP,
        OPEN,
        UPDATE,
        CLOSE,
        END,
    }

    SEQ_STATE seqState = SEQ_STATE.SETUP;
    CHANGE_SCENE_TYPE changeSceneType = CHANGE_SCENE_TYPE.NONE;

    void Start ()
    {
        seqState = SEQ_STATE.SETUP;
        changeSceneType = CHANGE_SCENE_TYPE.NONE;

    }
	
	void Update ()
    {
        switch (seqState)
        {
            case SEQ_STATE.SETUP:
                BattleManager.Setup();
                seqState = SEQ_STATE.OPEN;
                break;
            case SEQ_STATE.OPEN:
                seqState = SEQ_STATE.UPDATE;
                break;
            case SEQ_STATE.UPDATE:
                if(BattleManager.GetChangeSceneType() != CHANGE_SCENE_TYPE.NONE)
                {
                    Close(BattleManager.GetChangeSceneType());
                }
                break;
            case SEQ_STATE.CLOSE:
                seqState = SEQ_STATE.END;
                break;
            case SEQ_STATE.END:
                // 次のシーンに移行
                if (changeSceneType == CHANGE_SCENE_TYPE.CAMP)
                {
                    GameMain.ChangeSequence(GAME_SEQUENCE.CAMP);
                }
                else if(changeSceneType == CHANGE_SCENE_TYPE.HOME)
                {
                    GameMain.ChangeSequence(GAME_SEQUENCE.HOME);
                }
                break;
        }
	}


    void Close(CHANGE_SCENE_TYPE sceneType)
    {
        seqState = SEQ_STATE.CLOSE;
        changeSceneType = sceneType;
    }
}
