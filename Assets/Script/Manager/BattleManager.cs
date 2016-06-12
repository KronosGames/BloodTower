using UnityEngine;
using System.Collections;

public class BattleManager : ManagerBase
{
    enum SEQ_STATE
    {
        NONE,
        CHANGE_MAP,
        UPDATE,
        YOU_DIED,
    }

    [SerializeField]
    float youDiedWaitTime = 3.0f;

    MapInfoData currentMapData = null;
    SEQ_STATE seqState = SEQ_STATE.NONE;
    BattleMapScene.CHANGE_SCENE_TYPE changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;
    float youDiedTime = 0.0f;
    int currentCount = 0;

    void Start ()
    { 
        InitManager(this, MANAGER_ID.BATTLE);
    }

    // マップ情報を変更
    void ChangeMapData()
    {
        if (currentMapData != null)
        {
            currentMapData.trans.gameObject.SetActive(false);
        }

        currentMapData = BattleMapUtility.GetMapInfo(currentCount);
        currentMapData.trans.gameObject.SetActive(true);
    }

    void Update()
    {
        switch (seqState)
        {
            case SEQ_STATE.CHANGE_MAP:
                ChangeMapData();
                seqState = SEQ_STATE.UPDATE;
                break;
            case SEQ_STATE.UPDATE:
                if (!GameCharacterParam.IsAlive())
                {
                    UIScreenControl.AdditiveScreen(UI_SCREEN_TYPE.YOU_DIED_INFO);
                    seqState = SEQ_STATE.YOU_DIED;
                    youDiedTime = youDiedWaitTime;
                }
                break;
            case SEQ_STATE.YOU_DIED:
                youDiedTime -= Time.deltaTime;
                if (youDiedTime <= 0.0f)
                {
                    changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.HOME;
                }
                break;
        }
    }

    //  ----------------------------------------------
    //  公開用 関数
    //  ----------------------------------------------

    // 初期化
    public void Setup()
    {
        currentCount = 0;
        currentMapData = null;
        seqState = SEQ_STATE.UPDATE;
        changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.NONE;

        ChangeMapData();
	}

    // 上の階層に行くときに呼んでください。
    public void ChangeUpFloor()
    {
        if (BattleMapUtility.GetMapCount() - 1 <= currentCount) return;

        seqState = SEQ_STATE.CHANGE_MAP;
        currentCount++;

        // キャンプに行く条件文を記述する。
        //if()
        //{
        //    changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.CAMP;
        //}
    }

    // 下の階層、行く時に呼んでください。
    public void ChangeDownFloor()
    {
        seqState = SEQ_STATE.CHANGE_MAP;
        currentCount--;

        if (0 >= currentCount)
        {
            currentCount = 0;
            changeSceneType = BattleMapScene.CHANGE_SCENE_TYPE.HOME;
        }
    }

    // 現在の階層マップデータ
    public MapInfoData GetCurrentMapData()
    {
        return currentMapData;
    }

    // 切り替えるシーンの種類を取得
    // 通知用に使います。
    public BattleMapScene.CHANGE_SCENE_TYPE GetChangeSceneType()
    {
        return changeSceneType;
    }


}
