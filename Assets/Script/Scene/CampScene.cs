﻿using UnityEngine;
using System.Collections;

public class CampScene : MonoBehaviour {


    enum SEQ_STATE
    {
        SETUP,
        OPEN,
        UPDATE,
        CLOSE,
        END,
    }

    SEQ_STATE seqState = SEQ_STATE.SETUP;

    void Start()
    {
        seqState = SEQ_STATE.SETUP;
    }

    void Update()
    {
        switch (seqState)
        {
            case SEQ_STATE.SETUP:
                seqState = SEQ_STATE.OPEN;
                break;
            case SEQ_STATE.OPEN:
                UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.PLAYER_INFO);
                seqState = SEQ_STATE.UPDATE;
                break;
            case SEQ_STATE.UPDATE:

                break;
            case SEQ_STATE.CLOSE:
                seqState = SEQ_STATE.END;
                UIScreenControl.AllCloseScreen();
                break;
            case SEQ_STATE.END:
                // 次のシーンに移行
                GameMain.ChangeSequence(GAME_SEQUENCE.BATTLE_MAP);
                break;
        }
    }


    void Close()
    {
        seqState = SEQ_STATE.CLOSE;
    }
}
