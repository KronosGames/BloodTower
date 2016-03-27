using UnityEngine;
using System.Collections;

public class EntryScene : MonoBehaviour
{
    void Update()
    {
        // Test Code
        if (InputManager.IsAnyDown())
        {
            GameMain.ChangeSequence(GAME_SEQUENCE.HOME);
        }
    }

}
