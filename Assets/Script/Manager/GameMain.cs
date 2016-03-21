using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Sequence
public enum GAME_SEQUENCE
{
    SETUP,      //< セットアップ
    TITLE,      //< タイトル
    ENTRY,      //< 登録画面
    HOME,       //< ホーム画面
    BATTLE_MAP, //< バトル画面
    CAMP,       //< キャンプ場画面
    END,        //< 終了
}

[System.Serializable]
public class GameSequenceData
{
    public GAME_SEQUENCE sequence = GAME_SEQUENCE.SETUP;
    public string sceneName = "";
    public LoadSceneMode loadMode = LoadSceneMode.Additive;
    public bool isUnloadScene = false;
}

//  -----------------------------------------------------
//  ゲームの根っこになる部分です。
//  ここでは、ゲーム全体の制御をします。
//  -----------------------------------------------------
public class GameMain : ManagerBase 
{
    [SerializeField]
    List<GameSequenceData> gameSequenceList = new List<GameSequenceData>();

    static GAME_SEQUENCE sequence = GAME_SEQUENCE.SETUP;
    static bool isChanged = false;

    GameSequenceData currentGameSequence = null;

	void Start () 
    {
        InitManager(this, MANAGER_ID.GAME_MAIN);

        sequence = GAME_SEQUENCE.SETUP;
        isChanged = false;
	}

    bool IsChanged()
    {
        if (isChanged)
        {
            isChanged = false;
            return true;
        }

        return false;
    }

    // シーンを捨てる
    void UnloadScene()
    {
        if (currentGameSequence == null) return;
        if (currentGameSequence.isUnloadScene) return;

        SceneManager.UnloadScene(currentGameSequence.sceneName);
    }

    // シーンをロードする。
    void LoadScene()
    {
        for (int i = 0; i < gameSequenceList.Count; i++)
        {
            GameSequenceData data = gameSequenceList[i];
            if (data.sequence == sequence)
            {
                SceneManager.LoadScene(data.sceneName, data.loadMode);
                currentGameSequence = data;
            }
        }
    }

	void Update ()
    {
        // 変更したらこの処理が来る。
        if (IsChanged())
        {
            UnloadScene();
            LoadScene();
        }

        // 現在のシーケンス
        switch (sequence)
        { 
            case GAME_SEQUENCE.SETUP:
                ChangeSequence(GAME_SEQUENCE.ENTRY);
                break;
            case GAME_SEQUENCE.TITLE:

                break;
            case GAME_SEQUENCE.ENTRY:

                break;
            case GAME_SEQUENCE.HOME:

                break;
            case GAME_SEQUENCE.BATTLE_MAP:

                break;
            case GAME_SEQUENCE.CAMP:

                break;
            case GAME_SEQUENCE.END:

                break;
        }
	}

    // ----------------------------------------------------
    // 公開用関数
    // ----------------------------------------------------

    static public bool IsEntryScene() { return sequence == GAME_SEQUENCE.ENTRY; }
    static public bool IsHomeScene() { return sequence == GAME_SEQUENCE.HOME; }
    static public bool IsBattleMapScene() { return sequence == GAME_SEQUENCE.BATTLE_MAP; }
    static public bool IsCampScene() { return sequence == GAME_SEQUENCE.CAMP; }

    static public void ChangeSequence(GAME_SEQUENCE changeSeq)
    {
        sequence = changeSeq;
        isChanged = true;
    }

}
