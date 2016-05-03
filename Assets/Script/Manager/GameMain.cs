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
public class ChildSequenceData
{
    public string sceneName = "";
    public bool isUnloadScene = false;
}

[System.Serializable]
public class GameSequenceData
{
    public GAME_SEQUENCE sequence = GAME_SEQUENCE.SETUP;
    public string sceneName = "";
    public LoadSceneMode loadMode = LoadSceneMode.Additive;
    public bool isUnloadScene = false;
    public List<ChildSequenceData> childSeqList = new List<ChildSequenceData>();
}

//  -----------------------------------------------------
//  ゲームの根っこになる部分です。
//  ここでは、ゲーム全体の制御をします。
//  -----------------------------------------------------
public class GameMain : ManagerBase
{
    const string PLAYER_STATUS_MENU_SCENE = "PlayerStatusMenuUI";

    enum TRANSACTION_STATE
    {
        NONE,
        In,
        Out,
    }

    [SerializeField]
    List<GameSequenceData> gameSequenceList = new List<GameSequenceData>();

    static GAME_SEQUENCE sequence = GAME_SEQUENCE.SETUP;
    static bool isChanging = false;
    static uTweenBase transactionTween = null;
    static TRANSACTION_STATE tranState = TRANSACTION_STATE.NONE;

    GameSequenceData currentGameSequence = null;

	void Start () 
    {
        InitManager(this, MANAGER_ID.GAME_MAIN);

        sequence = GAME_SEQUENCE.SETUP;
        isChanging = false;
	}

    // シーンを捨てる
    void UnloadScene()
    {
        if (currentGameSequence == null) return;
        if (currentGameSequence.isUnloadScene) return;

        SceneManager.UnloadScene(currentGameSequence.sceneName);

        for (int childIndex = 0; childIndex < currentGameSequence.childSeqList.Count; childIndex++)
        {
            ChildSequenceData childData = currentGameSequence.childSeqList[childIndex];
            if (childData.isUnloadScene) continue;

            SceneManager.UnloadScene(childData.sceneName);
        }

        OnUnloadSeq();
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

                for (int childIndex = 0; childIndex < data.childSeqList.Count; childIndex++)
                {
                    ChildSequenceData childData = data.childSeqList[childIndex];
                    SceneManager.LoadScene(childData.sceneName, LoadSceneMode.Additive);
                }

                OnLoadSeq();
                currentGameSequence = data;
            }
        }
    }

    // シーンを削除する際に呼ばれる関数
    void OnUnloadSeq()
    {
        // 現在のシーケンス
        switch (sequence)
        {
            case GAME_SEQUENCE.SETUP:

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
                SceneManager.UnloadScene(PLAYER_STATUS_MENU_SCENE);

                break;
        }
    }


    // シーンを読み込む際に呼ばれる関数
    void OnLoadSeq()
    {
        // 現在のシーケンス
        switch (sequence)
        {
            case GAME_SEQUENCE.SETUP:

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

    void UpdateTransaction()
    {
        switch (tranState)
        {
            case TRANSACTION_STATE.In:
                if (transactionTween.GetRate() >= 0.4)
                {
                    UnloadScene();
                    LoadScene();
                    tranState = TRANSACTION_STATE.Out;
                }
                break;
            case TRANSACTION_STATE.Out:
                if (!transactionTween.IsPlaying)
                {
                    isChanging = false;
                    transactionTween.Stop();
                    tranState = TRANSACTION_STATE.NONE;
                }
                break;
        }
    }

    void UpdateSeq()
    {
        // 現在のシーケンス
        switch (sequence)
        {
            case GAME_SEQUENCE.SETUP:
                ChangeSequence(GAME_SEQUENCE.ENTRY);
                break;
            case GAME_SEQUENCE.ENTRY:
                break;
            case GAME_SEQUENCE.TITLE:

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

    void Update ()
    {
        // 変更したらこの処理が来る。
        if (isChanging)
        {
            UpdateTransaction();
        }
        else
        {
            UpdateSeq();
        }
	}

    // ----------------------------------------------------
    // 公開用関数
    // ----------------------------------------------------

    static public bool IsEntryScene() { return sequence == GAME_SEQUENCE.ENTRY; }
    static public bool IsTitleScene() { return sequence == GAME_SEQUENCE.TITLE; }
    static public bool IsHomeScene() { return sequence == GAME_SEQUENCE.HOME; }
    static public bool IsBattleMapScene() { return sequence == GAME_SEQUENCE.BATTLE_MAP; }
    static public bool IsCampScene() { return sequence == GAME_SEQUENCE.CAMP; }

    static public void ChangeSequence(GAME_SEQUENCE changeSeq)
    {
        if (isChanging) return;

        sequence = changeSeq;

        if (sequence == GAME_SEQUENCE.SETUP || sequence == GAME_SEQUENCE.ENTRY)
        {
            GameMain instance = GetManager<GameMain>(MANAGER_ID.GAME_MAIN);
            instance.UnloadScene();
            instance.LoadScene();
            return;
        }

        isChanging = true;
        tranState = TRANSACTION_STATE.In;
        transactionTween = uTween.Play("TransactionTween");
    }

}
