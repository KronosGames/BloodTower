using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTextInfo : MonoBehaviour {

    [SerializeField]
    GAMETEXT_ID id = GAMETEXT_ID.NULL;

    public void Apply()
    {
        Text textComponent = GetComponent<Text>();
        textComponent.text = GameTextDatabase.GetText(id,true);
    }

}
