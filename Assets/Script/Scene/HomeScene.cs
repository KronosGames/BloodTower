using UnityEngine;
using System.Collections;

public class HomeScene : MonoBehaviour {

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
                if (!GameCharacterParam.IsAlive())
                {
                    GameCharacterParam.InitHp();
                    GameCharacterParam.InitStamina();
                }

                seqState = SEQ_STATE.OPEN;
                break;
            case SEQ_STATE.OPEN:
                UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.PLAYER_INFO);
                seqState = SEQ_STATE.UPDATE;
                break;
            case SEQ_STATE.UPDATE:
                if (Input.GetKeyDown(KeyCode.K))
                {
                    UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.BLACKSMITH_MENU_INFO);
                }
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


    public void Close()
    {
        seqState = SEQ_STATE.CLOSE;
    }
}
