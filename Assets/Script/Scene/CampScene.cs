using UnityEngine;
using System.Collections;

public class CampScene : MonoBehaviour {

    void Update()
    {
        // Test Code
        if (InputManager.IsDown(INPUT_ID.SUBMIT))
        {
            GameMain.ChangeSequence(GAME_SEQUENCE.BATTLE_MAP);
        }
    }

}
